using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace UoFiddler.Controls.Helpers
{
    /// <summary>
    /// Orders file paths by file name, comparing runs of digits by value so that
    /// "Hue 2.txt" sorts before "Hue 10.txt" and "0x00FF.bmp" before "0x0100.bmp".
    /// </summary>
    public sealed class NaturalFileNameComparer : IComparer<string>
    {
        public static readonly NaturalFileNameComparer Instance = new NaturalFileNameComparer();

        private static readonly Regex _chunks = new Regex(@"0x[0-9a-fA-F]+|\d+|\D+", RegexOptions.Compiled);

        public int Compare(string x, string y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            MatchCollection a = _chunks.Matches(Path.GetFileName(x));
            MatchCollection b = _chunks.Matches(Path.GetFileName(y));

            for (int i = 0; i < Math.Min(a.Count, b.Count); ++i)
            {
                int result = TryParseNumber(a[i].Value, out long na) && TryParseNumber(b[i].Value, out long nb)
                    ? na.CompareTo(nb)
                    : string.Compare(a[i].Value, b[i].Value, StringComparison.OrdinalIgnoreCase);

                if (result != 0)
                {
                    return result;
                }
            }

            int countResult = a.Count.CompareTo(b.Count);
            return countResult != 0 ? countResult : string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
        }

        private static bool TryParseNumber(string value, out long number)
        {
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                return long.TryParse(value.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out number);
            }

            return long.TryParse(value, out number);
        }
    }
}
