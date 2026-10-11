/***************************************************************************
 *
 * $Author: Turley
 * 
 * "THE BEER-WARE LICENSE"
 * As long as you retain this notice you can do whatever you want with 
 * this stuff. If we meet some day, and you think this stuff is worth it,
 * you can buy me a beer in return.
 *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Ultima;
using UoFiddler.Controls.Classes;
using UoFiddler.Controls.Forms;
using UoFiddler.Controls.Helpers;

namespace UoFiddler.Controls.UserControls
{
    public partial class HuesControl : UserControl
    {
        public HuesControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            pictureBox.MouseWheel += OnMouseWheel;
        }

        private const int _itemHeight = 20;
        private int _selected;
        private readonly SortedSet<int> _selectedIndexes = new SortedSet<int>();
        private bool _loaded;
        private int _row;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HuesEditable { get; set; } = true;

        /// <summary>
        /// Sets Selected Hue
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                _selectedIndexes.Clear();
                _selectedIndexes.Add(value);
                if (IsAncestorSiteInDesignMode || FormsDesignerHelper.IsInDesignMode())
                {
                    return;
                }

                if (!_loaded)
                {
                    return;
                }

                if (Hues.List.Length > 0)
                {
                    pictureBox.Invalidate();
                }
            }
        }

        /// <summary>
        /// Reload when loaded (file changed)
        /// </summary>
        private void Reload()
        {
            if (!_loaded)
            {
                return;
            }

            _selected = 0;
            _selectedIndexes.Clear();
            OnLoad(this, EventArgs.Empty);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (IsAncestorSiteInDesignMode || FormsDesignerHelper.IsInDesignMode())
            {
                return;
            }

            Options.LoadedUltimaClass["Hues"] = true;

            if (!HuesEditable)
            {
                pictureBox.MouseDoubleClick -= OnMouseDoubleClick;
                pictureBox.ContextMenuStrip = new ContextMenuStrip();
            }

            vScrollBar.Maximum = Hues.List.Length;
            vScrollBar.Minimum = 0;
            vScrollBar.Value = 0;
            vScrollBar.SmallChange = 1;
            vScrollBar.LargeChange = 10;

            if (_selected > 0)
            {
                vScrollBar.Value = _selected;
            }

            pictureBox.Invalidate();
            if (!_loaded)
            {
                ControlEvents.FilePathChangeEvent += OnFilePathChangeEvent;
                ControlEvents.HueChangeEvent += OnHueChangeEvent;
            }
            _loaded = true;
        }

        private void OnFilePathChangeEvent()
        {
            Reload();
        }

        private void OnHueChangeEvent()
        {
            pictureBox.Invalidate();
        }

        private const int _noSelectionValue = -1;

        private int GetIndex(int y)
        {
            int value = vScrollBar.Value + y;
            return Hues.List.Length > value ? value : _noSelectionValue;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (IsAncestorSiteInDesignMode || FormsDesignerHelper.IsInDesignMode())
            {
                return;
            }

            e.Graphics.Clear(Color.White);

            for (int y = 0; y <= _row; ++y)
            {
                int index = GetIndex(y);
                if (index < 0)
                {
                    continue;
                }

                Rectangle rect = new Rectangle(0, y * _itemHeight, 200, _itemHeight);
                bool isSelected = index == _selected || _selectedIndexes.Contains(index);
                e.Graphics.FillRectangle(isSelected ? SystemBrushes.Highlight : SystemBrushes.Window, rect);

                float size = (float)(pictureBox.Width - 200) / 32;
                Hue hue = Hues.List[index];
                Rectangle stringRect = new Rectangle(3, y * _itemHeight, pictureBox.Width, _itemHeight);

                int hueDisplayIndex = hue.Index + (IndexOffsetButton.Checked ? 1 : 0);

                e.Graphics.DrawString(
                    $"{hueDisplayIndex,-5} {$"(0x{hueDisplayIndex:X})",-7} {hue.Name}", Font, SystemBrushes.ControlText, stringRect);

                for (int i = 0; i < hue.Colors.Length; ++i)
                {
                    Rectangle rectangle = new Rectangle(200 + (int)Math.Round(i * size), y * _itemHeight, (int)Math.Round(size + 1f), _itemHeight);
                    using (var brush = new SolidBrush(hue.GetColor(i)))
                    {
                        e.Graphics.FillRectangle(brush, rectangle);
                    }
                }
            }
        }

        private void OnScroll(object sender, ScrollEventArgs e)
        {
            pictureBox.Invalidate();
        }

        private const int _scrollStep = 4;

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            var newValue = e.Delta < 0 ? vScrollBar.Value + _scrollStep : vScrollBar.Value - _scrollStep;

            if (newValue < vScrollBar.Minimum || newValue >= vScrollBar.Maximum)
            {
                return;
            }

            vScrollBar.Value = newValue;

            pictureBox.Invalidate();
        }

        private void OnResize(object sender, EventArgs e)
        {
            _row = pictureBox.Height / _itemHeight;
            if (!_loaded)
            {
                return;
            }

            pictureBox.Invalidate();
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            pictureBox.Focus();

            if (!HuesEditable || (ModifierKeys & (Keys.Control | Keys.Shift)) == 0)
            {
                UpdateSelection();
                return;
            }

            int index = GetIndex(pictureBox.PointToClient(MousePosition).Y / _itemHeight);
            if (index < 0)
            {
                return;
            }

            _selectedIndexes.Add(_selected);

            if ((ModifierKeys & Keys.Shift) != 0)
            {
                // Range selection from the current anchor
                int from = Math.Min(_selected, index);
                int to = Math.Max(_selected, index);
                if ((ModifierKeys & Keys.Control) == 0)
                {
                    _selectedIndexes.Clear();
                }

                for (int i = from; i <= to; ++i)
                {
                    _selectedIndexes.Add(i);
                }
            }
            else if (!_selectedIndexes.Remove(index))
            {
                _selectedIndexes.Add(index);
                _selected = index;
            }
            else if (index == _selected && _selectedIndexes.Count > 0)
            {
                _selected = _selectedIndexes.Min;
            }
            else if (_selectedIndexes.Count == 0)
            {
                // Can't deselect the last hue
                _selectedIndexes.Add(index);
            }

            pictureBox.Invalidate();
        }

        private void UpdateSelection()
        {
            Point m = pictureBox.PointToClient(MousePosition);

            int index = GetIndex(m.Y / _itemHeight);

            if (index >= 0)
            {
                Selected = index;
            }
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            UpdateSelection();

            if (Selected >= 0)
            {
                new HueEditForm(Selected).Show();
            }
        }

        private void OnClickSave(object sender, EventArgs e)
        {
            string path = Options.OutputPath;
            Hues.Save(path);
            Options.ChangedUltimaClass["Hues"] = false;

            FileSavedDialog.Show(FindForm(), path, "Files saved successfully.");
        }

        private void OnTextChangedReplace(object sender, EventArgs e)
        {
            ReplaceText.ForeColor = Utils.ConvertStringToInt(ReplaceText.Text, out _, 1, 3000)
                ? SystemColors.ControlText
                : (Options.DarkMode ? Color.OrangeRed : Color.Red);
        }

        private void OnKeyDownReplace(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            if (!Utils.ConvertStringToInt(ReplaceText.Text, out int index, 0, Hues.List.Length))
            {
                return;
            }

            contextMenuStrip1.Close();
            Hues.List[_selected] = Hues.List[index];
            pictureBox.Invalidate();
        }

        private void OnExport(object sender, EventArgs e)
        {
            string path = Options.OutputPath;

            if (_selectedIndexes.Count <= 1)
            {
                string fileName = Path.Combine(path, $"Hue {_selected}.txt");
                Hues.List[_selected].Export(fileName);
                FileSavedDialog.Show(FindForm(), fileName, "Hue saved successfully.");
                return;
            }

            foreach (int index in _selectedIndexes)
            {
                Hues.List[index].Export(Path.Combine(path, $"Hue {index}.txt"));
            }

            FileSavedDialog.Show(FindForm(), path, $"{_selectedIndexes.Count} hues saved successfully.");
        }

        private void OnImport(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog { Multiselect = true, Title = "Choose txt file(s) to import", CheckFileExists = true, Filter = "txt files (*.txt)|*.txt" })
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // Multiple files are imported into consecutive hues starting from the selected one
                string[] files = dialog.FileNames.OrderBy(f => f, NaturalFileNameComparer.Instance).ToArray();
                if (_selected + files.Length > Hues.List.Length)
                {
                    MessageBox.Show($"Not enough hues: {files.Length} files starting from hue {_selected} exceed the last hue.",
                        "Import", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (files.Length > 1)
                {
                    DialogResult result = MessageBox.Show(
                        $"Import {files.Length} files into hues {_selected} to {_selected + files.Length - 1}? Existing data will be replaced.",
                        "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                for (int i = 0; i < files.Length; ++i)
                {
                    Hues.List[_selected + i].Import(files[i], resetNamesOnImportToolStripMenuItem.Checked);
                }
            }

            Options.ChangedUltimaClass["Hues"] = true;
            ControlEvents.FireHueChangeEvent();
        }

        /// <summary>
        /// Print a nice border
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            const int borderWidth = 1;

            Color borderColor = VisualStyleInformation.TextControlBorder;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, borderColor,
                borderWidth, ButtonBorderStyle.Solid, borderColor, borderWidth,
                ButtonBorderStyle.Solid, borderColor, borderWidth, ButtonBorderStyle.Solid,
                borderColor, borderWidth, ButtonBorderStyle.Solid);
        }

        private void HueIndexToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var maximumIndex = Hues.List.Length;

            if (!Utils.ConvertStringToInt(HueIndexToolStripTextBox.Text, out int indexValue))
            {
                return;
            }

            if (IndexOffsetButton.Checked)
            {
                indexValue--;
            }

            if (indexValue < 0)
            {
                indexValue = 0;
            }

            if (indexValue >= maximumIndex)
            {
                indexValue = maximumIndex - 1;
            }

            var scrollPosition = indexValue - 1;
            if (scrollPosition < 0)
            {
                scrollPosition = 0;
            }

            Selected = indexValue;
            vScrollBar.Value = scrollPosition;
        }

        private void SearchNameToolStripButton_Click(object sender, EventArgs e)
        {
            FindByName(Selected);
        }

        private void HueNameToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            FindByName(0);
        }

        private void FindByName(int startIndex)
        {
            if (HueNameToolStripTextBox.TextBox == null)
            {
                return;
            }

            var search = HueNameToolStripTextBox.TextBox.Text.Trim();

            if (string.IsNullOrEmpty(search))
            {
                return;
            }

            var searchResult = FindByNameInternal(search, startIndex);

            // try from the start
            if (searchResult == null)
            {
                searchResult = FindByNameInternal(search, 0);

                // searching from start failed we can return
                if (searchResult == null)
                {
                    return;
                }
            }

            Selected = searchResult.Index;
            vScrollBar.Value = searchResult.Index - 1 < 0 ? 0 : searchResult.Index;
        }

        private static Hue FindByNameInternal(string search, int currentIndex)
        {
            return Hues.List.FirstOrDefault(hue =>
                hue.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 && hue.Index > currentIndex);
        }

        private void ExportAllHueNamesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Options.OutputPath, "Hue names list.txt");

            Hues.ExportHueList(fileName);

            FileSavedDialog.Show(FindForm(), fileName, "Hue names list saved successfully.");
        }

        private void IndexOffsetButton_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }
    }
}
