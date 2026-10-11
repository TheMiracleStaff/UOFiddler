using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ultima;
using UoFiddler.Controls.Classes;

namespace UoFiddler.Controls.Forms
{
    /// <summary>
    /// Saves an area of a map as a .tbtdiff patch in the output directory.
    /// </summary>
    public partial class MapExportTbtDiffForm : Form
    {
        private readonly Map _map;

        public MapExportTbtDiffForm(Map map, Rectangle selection)
        {
            InitializeComponent();
            Icon = Options.GetFiddlerIcon();

            _map = map;

            foreach (NumericUpDown x in new[] { numericUpDownX1, numericUpDownX2 })
            {
                x.Maximum = map.Width - 1;
            }

            foreach (NumericUpDown y in new[] { numericUpDownY1, numericUpDownY2 })
            {
                y.Maximum = map.Height - 1;
            }

            numericUpDownX1.Value = Math.Min(selection.Left, map.Width - 1);
            numericUpDownY1.Value = Math.Min(selection.Top, map.Height - 1);
            numericUpDownX2.Value = Math.Min(selection.Right, map.Width - 1);
            numericUpDownY2.Value = Math.Min(selection.Bottom, map.Height - 1);

            UpdateBlocksLabel();
            ActiveControl = textBoxName;
        }

        private void OnAreaChanged(object sender, EventArgs e)
        {
            UpdateBlocksLabel();
        }

        private void UpdateBlocksLabel()
        {
            GetArea(out int x1, out int y1, out int x2, out int y2);

            // Patches hold whole 8x8 blocks
            int blockX1 = x1 >> 3, blockY1 = y1 >> 3, blockX2 = x2 >> 3, blockY2 = y2 >> 3;
            int blocks = (blockX2 - blockX1 + 1) * (blockY2 - blockY1 + 1);

            labelBlocks.Text = $"Saves {blocks} block(s): ({blockX1 * 8},{blockY1 * 8}) - ({(blockX2 * 8) + 7},{(blockY2 * 8) + 7})";
        }

        private void GetArea(out int x1, out int y1, out int x2, out int y2)
        {
            x1 = (int)Math.Min(numericUpDownX1.Value, numericUpDownX2.Value);
            x2 = (int)Math.Max(numericUpDownX1.Value, numericUpDownX2.Value);
            y1 = (int)Math.Min(numericUpDownY1.Value, numericUpDownY2.Value);
            y2 = (int)Math.Max(numericUpDownY1.Value, numericUpDownY2.Value);
        }

        private void OnClickExport(object sender, EventArgs e)
        {
            var contents = (checkBoxLand.Checked ? TbtDiff.Contents.Land : TbtDiff.Contents.None)
                         | (checkBoxStatics.Checked ? TbtDiff.Contents.Statics : TbtDiff.Contents.None);
            if (contents == TbtDiff.Contents.None)
            {
                MessageBox.Show("Check at least one of Land and Statics.", "Export TbtDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = textBoxName.Text.Trim();
            if (name.EndsWith(TbtDiff.Extension, StringComparison.OrdinalIgnoreCase))
            {
                name = name.Substring(0, name.Length - TbtDiff.Extension.Length);
            }

            if (name.Length == 0 || name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Enter a valid file name.", "Export TbtDiff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = Path.Combine(Options.OutputPath, name + TbtDiff.Extension);
            if (File.Exists(fileName) &&
                MessageBox.Show($"{Path.GetFileName(fileName)} already exists. Overwrite it?", "Export TbtDiff",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            GetArea(out int x1, out int y1, out int x2, out int y2);

            try
            {
                Cursor = Cursors.WaitCursor;
                TbtDiff.Create(_map, x1, y1, x2, y2, contents).Save(fileName);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show($"The patch could not be saved:\n{ex.Message}", "Export TbtDiff", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            FileSavedDialog.Show(Owner ?? this, fileName, "TbtDiff patch saved successfully.", "Export TbtDiff");
            Close();
        }
    }
}
