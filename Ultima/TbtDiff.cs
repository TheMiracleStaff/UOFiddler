using System;
using System.Collections.Generic;
using System.IO;

namespace Ultima
{
    /// <summary>
    /// Reads, writes and applies .tbtdiff map patches: whole 8x8 blocks of land and/or statics cut out of a map,
    /// which can be pasted back into the same or another map, at their place or moved elsewhere.
    /// </summary>
    /// <remarks>
    /// File layout (little endian), compatible with the files written by the Maybacco fork:
    /// <code>
    /// int32 blockWidth, int32 blockHeight      size in blocks of the map the patch was cut from
    /// byte  content                            1 = land, 2 = statics, 3 = both
    /// land section (content &amp; 1):
    ///     repeated: int32 block, 64 x (uint16 id, sbyte z)      cells in row order (y, then x)
    ///     int32 -1
    /// statics section (content &amp; 2):
    ///     repeated: int32 block, then for x 0..7, for y 0..7:
    ///         int32 count, count x (uint16 id, sbyte z, uint16 hue)
    ///     int32 -1
    /// </code>
    /// A block number is blockX * blockHeight + blockY of the source map. Blocks are written in increasing order.
    /// </remarks>
    public sealed class TbtDiff
    {
        [Flags]
        public enum Contents : byte
        {
            None = 0,
            Land = 1,
            Statics = 2,
            Both = Land | Statics
        }

        public const string Extension = ".tbtdiff";

        private const int _landBlockLength = 64 * 3;
        private const int _mapBlockLength = 4 + _landBlockLength;
        private const int _staticRecordLength = 7;
        private const int _indexRecordLength = 12;
        private const int _endOfSection = -1;

        public struct StaticItem
        {
            public ushort Id;
            public sbyte Z;
            public ushort Hue;
        }

        public sealed class LandBlock
        {
            public int X { get; set; }
            public int Y { get; set; }

            /// <summary>
            /// 64 cells of (uint16 id, sbyte z), exactly as stored in the file and in mapN.mul.
            /// </summary>
            public byte[] Data { get; set; }
        }

        public sealed class StaticsBlock
        {
            public int X { get; set; }
            public int Y { get; set; }

            /// <summary>
            /// Items of each cell, indexed [x][y] within the block.
            /// </summary>
            public StaticItem[][][] Cells { get; set; }
        }

        /// <summary>
        /// Size in blocks of the map the patch was cut from.
        /// </summary>
        public int BlockWidth { get; set; }

        public int BlockHeight { get; set; }

        public List<LandBlock> LandBlocks { get; } = new List<LandBlock>();

        public List<StaticsBlock> StaticsBlocks { get; } = new List<StaticsBlock>();

        /// <summary>
        /// Sections the file holds; a section can be present and still list no block.
        /// </summary>
        public Contents Content { get; set; }

        /// <summary>
        /// Top left block of the patch, the one moved onto the target when the patch is applied elsewhere.
        /// </summary>
        /// <remarks>
        /// The lowest block number of the file, as the Maybacco fork used it. For the rectangles written by
        /// <see cref="Create"/> that is the top left block.
        /// </remarks>
        public (int X, int Y) Origin
        {
            get
            {
                int best = int.MaxValue;
                (int X, int Y) origin = (0, 0);

                foreach (var (x, y) in AllBlocks())
                {
                    int number = (x * BlockHeight) + y;
                    if (number < best)
                    {
                        best = number;
                        origin = (x, y);
                    }
                }

                return origin;
            }
        }

        private IEnumerable<(int X, int Y)> AllBlocks()
        {
            foreach (LandBlock block in LandBlocks)
            {
                yield return (block.X, block.Y);
            }

            foreach (StaticsBlock block in StaticsBlocks)
            {
                yield return (block.X, block.Y);
            }
        }

        /// <summary>
        /// Cuts the blocks covering the given tile rectangle (inclusive) out of a map.
        /// </summary>
        /// <remarks>Land and statics are taken as the map shows them, including map diff patches when enabled.</remarks>
        public static TbtDiff Create(Map map, int xStart, int yStart, int xEnd, int yEnd, Contents contents)
        {
            TileMatrix tiles = map.Tiles;
            var diff = new TbtDiff { BlockWidth = tiles.BlockWidth, BlockHeight = tiles.BlockHeight, Content = contents };

            int blockXStart = Math.Max(0, Math.Min(xStart, xEnd) >> 3);
            int blockXEnd = Math.Min(tiles.BlockWidth - 1, Math.Max(xStart, xEnd) >> 3);
            int blockYStart = Math.Max(0, Math.Min(yStart, yEnd) >> 3);
            int blockYEnd = Math.Min(tiles.BlockHeight - 1, Math.Max(yStart, yEnd) >> 3);

            for (int blockX = blockXStart; blockX <= blockXEnd; blockX++)
            {
                for (int blockY = blockYStart; blockY <= blockYEnd; blockY++)
                {
                    if ((contents & Contents.Land) != 0)
                    {
                        Tile[] land = tiles.GetLandBlock(blockX, blockY);
                        var data = new byte[_landBlockLength];
                        for (int i = 0; i < 64; i++)
                        {
                            data[i * 3] = (byte)land[i].Id;
                            data[(i * 3) + 1] = (byte)(land[i].Id >> 8);
                            data[(i * 3) + 2] = (byte)land[i].Z;
                        }

                        diff.LandBlocks.Add(new LandBlock { X = blockX, Y = blockY, Data = data });
                    }

                    if ((contents & Contents.Statics) != 0)
                    {
                        HuedTile[][][] statics = tiles.GetStaticBlock(blockX, blockY);
                        var cells = NewCells();
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                HuedTile[] source = statics[x][y];
                                var items = new StaticItem[source.Length];
                                for (int i = 0; i < source.Length; i++)
                                {
                                    items[i] = new StaticItem { Id = source[i].Id, Z = source[i].Z, Hue = (ushort)source[i].Hue };
                                }

                                cells[x][y] = items;
                            }
                        }

                        diff.StaticsBlocks.Add(new StaticsBlock { X = blockX, Y = blockY, Cells = cells });
                    }
                }
            }

            return diff;
        }

        private static StaticItem[][][] NewCells()
        {
            var cells = new StaticItem[8][][];
            for (int x = 0; x < 8; x++)
            {
                cells[x] = new StaticItem[8][];
            }

            return cells;
        }

        public static TbtDiff Load(string fileName)
        {
            using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(stream);

            var diff = new TbtDiff { BlockWidth = reader.ReadInt32(), BlockHeight = reader.ReadInt32() };
            if (diff.BlockWidth <= 0 || diff.BlockHeight <= 0)
            {
                throw new InvalidDataException($"{Path.GetFileName(fileName)} is not a tbtdiff file (invalid map size).");
            }

            var contents = (Contents)reader.ReadByte();
            diff.Content = contents;

            if ((contents & Contents.Land) != 0)
            {
                for (int block = reader.ReadInt32(); block != _endOfSection; block = reader.ReadInt32())
                {
                    diff.LandBlocks.Add(new LandBlock
                    {
                        X = block / diff.BlockHeight,
                        Y = block % diff.BlockHeight,
                        Data = reader.ReadBytes(_landBlockLength)
                    });
                }
            }

            if ((contents & Contents.Statics) != 0)
            {
                for (int block = reader.ReadInt32(); block != _endOfSection; block = reader.ReadInt32())
                {
                    var cells = NewCells();
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            int count = reader.ReadInt32();
                            var items = new StaticItem[count];
                            for (int i = 0; i < count; i++)
                            {
                                items[i] = new StaticItem { Id = reader.ReadUInt16(), Z = reader.ReadSByte(), Hue = reader.ReadUInt16() };
                            }

                            cells[x][y] = items;
                        }
                    }

                    diff.StaticsBlocks.Add(new StaticsBlock { X = block / diff.BlockHeight, Y = block % diff.BlockHeight, Cells = cells });
                }
            }

            return diff;
        }

        public void Save(string fileName)
        {
            using var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new BinaryWriter(stream);

            writer.Write(BlockWidth);
            writer.Write(BlockHeight);
            writer.Write((byte)Content);

            if ((Content & Contents.Land) != 0)
            {
                foreach (LandBlock block in SortedByNumber(LandBlocks, b => (b.X, b.Y)))
                {
                    writer.Write((block.X * BlockHeight) + block.Y);
                    writer.Write(block.Data);
                }

                writer.Write(_endOfSection);
            }

            if ((Content & Contents.Statics) != 0)
            {
                foreach (StaticsBlock block in SortedByNumber(StaticsBlocks, b => (b.X, b.Y)))
                {
                    writer.Write((block.X * BlockHeight) + block.Y);
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            StaticItem[] items = block.Cells[x][y] ?? Array.Empty<StaticItem>();
                            writer.Write(items.Length);
                            foreach (StaticItem item in items)
                            {
                                writer.Write(item.Id);
                                writer.Write(item.Z);
                                writer.Write(item.Hue);
                            }
                        }
                    }
                }

                writer.Write(_endOfSection);
            }
        }

        private List<T> SortedByNumber<T>(List<T> blocks, Func<T, (int X, int Y)> position)
        {
            var sorted = new List<T>(blocks);
            sorted.Sort((a, b) =>
            {
                var (ax, ay) = position(a);
                var (bx, by) = position(b);
                return ((ax * BlockHeight) + ay).CompareTo((bx * BlockHeight) + by);
            });
            return sorted;
        }

        /// <summary>
        /// Outcome of <see cref="Apply"/>.
        /// </summary>
        public sealed class ApplyResult
        {
            public int LandBlocks { get; set; }
            public int StaticsBlocks { get; set; }

            /// <summary>
            /// Blocks that fell outside the target map once moved, and were left out.
            /// </summary>
            public int SkippedBlocks { get; set; }
        }

        /// <summary>
        /// Pastes patches into a map and writes the result as mapN.mul, staidxN.mul and staticsN.mul in the output directory.
        /// </summary>
        /// <param name="map">Target map; its client files are the base the patches are pasted on.</param>
        /// <param name="patches">Applied in order, so a later patch wins where two cover the same block.</param>
        /// <param name="target">
        /// When set, each patch is moved so that its <see cref="Origin"/> lands on this tile (rounded down to its block);
        /// otherwise blocks keep their position.
        /// </param>
        /// <param name="outputPath">Directory receiving the three files. It must not be the client directory.</param>
        public static ApplyResult Apply(Map map, IEnumerable<TbtDiff> patches, (int X, int Y)? target, string outputPath)
        {
            int blockWidth = map.Tiles.BlockWidth;
            int blockHeight = map.Tiles.BlockHeight;
            int blockCount = blockWidth * blockHeight;

            var land = new byte[blockCount][];
            var statics = new StaticItem[blockCount][][][];
            var result = new ApplyResult();

            foreach (TbtDiff patch in patches)
            {
                int dx = 0;
                int dy = 0;
                if (target.HasValue)
                {
                    var (originX, originY) = patch.Origin;
                    dx = (target.Value.X >> 3) - originX;
                    dy = (target.Value.Y >> 3) - originY;
                }

                foreach (LandBlock block in patch.LandBlocks)
                {
                    int x = block.X + dx;
                    int y = block.Y + dy;
                    if (x < 0 || y < 0 || x >= blockWidth || y >= blockHeight)
                    {
                        result.SkippedBlocks++;
                        continue;
                    }

                    land[(x * blockHeight) + y] = block.Data;
                }

                foreach (StaticsBlock block in patch.StaticsBlocks)
                {
                    int x = block.X + dx;
                    int y = block.Y + dy;
                    if (x < 0 || y < 0 || x >= blockWidth || y >= blockHeight)
                    {
                        result.SkippedBlocks++;
                        continue;
                    }

                    statics[(x * blockHeight) + y] = block.Cells;
                }
            }

            string mapPath = RequireClientFile($"map{map.FileIndex}.mul");
            string indexPath = RequireClientFile($"staidx{map.FileIndex}.mul");
            string staticsPath = RequireClientFile($"statics{map.FileIndex}.mul");

            Directory.CreateDirectory(outputPath);
            string outMap = Path.Combine(outputPath, $"map{map.FileIndex}.mul");
            string outIndex = Path.Combine(outputPath, $"staidx{map.FileIndex}.mul");
            string outStatics = Path.Combine(outputPath, $"statics{map.FileIndex}.mul");

            foreach (var (source, output) in new[] { (mapPath, outMap), (indexPath, outIndex), (staticsPath, outStatics) })
            {
                if (string.Equals(Path.GetFullPath(source), Path.GetFullPath(output), StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException($"The output directory holds the client's {Path.GetFileName(source)}; choose another one.");
                }
            }

            // Land: copy every block header, and the cells of the blocks without a patch
            using (var mapIn = new FileStream(mapPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var mapOut = new FileStream(outMap, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var buffer = new byte[_mapBlockLength];
                for (int i = 0; i < blockCount; i++)
                {
                    mapIn.Seek((long)i * _mapBlockLength, SeekOrigin.Begin);
                    int read = mapIn.Read(buffer, 0, _mapBlockLength);
                    if (read < _mapBlockLength)
                    {
                        Array.Clear(buffer, read, _mapBlockLength - read);
                    }

                    if (land[i] != null)
                    {
                        Buffer.BlockCopy(land[i], 0, buffer, 4, _landBlockLength);
                        result.LandBlocks++;
                    }

                    mapOut.Write(buffer, 0, _mapBlockLength);
                }
            }

            // Statics: rewrite both files, taking each block from its patch or from the client
            using (var indexIn = new BinaryReader(new FileStream(indexPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            using (var staticsIn = new FileStream(staticsPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var indexOut = new BinaryWriter(new FileStream(outIndex, FileMode.Create, FileAccess.Write, FileShare.None)))
            using (var staticsOutStream = new FileStream(outStatics, FileMode.Create, FileAccess.Write, FileShare.None))
            using (var staticsOut = new BinaryWriter(staticsOutStream))
            {
                long indexLength = indexIn.BaseStream.Length;
                var buffer = Array.Empty<byte>();

                for (int i = 0; i < blockCount; i++)
                {
                    int lookup = (int)staticsOutStream.Position;
                    int extra = 0;

                    if (statics[i] != null)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                foreach (StaticItem item in statics[i][x][y] ?? Array.Empty<StaticItem>())
                                {
                                    staticsOut.Write(item.Id);
                                    staticsOut.Write((byte)x);
                                    staticsOut.Write((byte)y);
                                    staticsOut.Write(item.Z);
                                    staticsOut.Write(item.Hue);
                                }
                            }
                        }

                        result.StaticsBlocks++;
                    }
                    else if (((long)i + 1) * _indexRecordLength <= indexLength)
                    {
                        indexIn.BaseStream.Seek((long)i * _indexRecordLength, SeekOrigin.Begin);
                        int sourceLookup = indexIn.ReadInt32();
                        int sourceLength = indexIn.ReadInt32();
                        extra = indexIn.ReadInt32();

                        if (sourceLookup >= 0 && sourceLength > 0)
                        {
                            // Whole records only, as the client reads them
                            int length = sourceLength / _staticRecordLength * _staticRecordLength;
                            if (buffer.Length < length)
                            {
                                buffer = new byte[length];
                            }

                            staticsIn.Seek(sourceLookup, SeekOrigin.Begin);
                            staticsIn.ReadExactly(buffer, 0, length);
                            staticsOut.Write(buffer, 0, length);
                        }
                    }

                    int written = (int)staticsOutStream.Position - lookup;
                    if (written > 0)
                    {
                        indexOut.Write(lookup);
                        indexOut.Write(written);
                        indexOut.Write(extra);
                    }
                    else
                    {
                        indexOut.Write(-1);
                        indexOut.Write(-1);
                        indexOut.Write(-1);
                    }
                }
            }

            return result;
        }

        private static string RequireClientFile(string name)
        {
            string path = Files.GetFilePath(name);
            if (path == null || !File.Exists(path))
            {
                throw new FileNotFoundException($"{name} was not found in the client files.", name);
            }

            return path;
        }
    }
}
