using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using UoFiddler.Controls.Classes;

namespace UoFiddler.Controls.Forms
{
    /// <summary>
    /// Builds The Miracle's &lt;VeinInfo&gt; entry: the ore mix of an area, in parts per thousand.
    /// </summary>
    public partial class ResourceRegionExportForm : Form
    {
        private const int _totalParts = 1000;

        /// <summary>
        /// Ores in the order they are offered, as (name in the xml, label).
        /// </summary>
        private static readonly (string Name, string Label)[] _resources =
        {
            ("PiriteNew", "Pirite"),
            ("AzuriteNew", "Azurite"),
            ("ValoriumNew", "Valorium"),
            ("OssidianaNew", "Ossidiana"),
            ("MerkiteNew", "Merkite"),
            ("TitanioNew", "Titanio"),
            ("IthilmarNew", "Ithilmar"),
            ("OrialkonNew", "Orialkon"),
            ("TalavholkNew", "Talavholk"),
            ("AdamantioNew", "Adamantio"),
            ("Bronzo", "Bronzo"),
            ("Rame", "Rame"),
            ("Ferro", "Ferro"),
            ("Oro", "Oro"),
            ("Argento", "Argento"),
        };

        private readonly List<NumericUpDown> _amounts = new List<NumericUpDown>();

        /// <param name="area">Area in tiles, start and end inclusive.</param>
        /// <param name="mapId">Map number written in the entry.</param>
        public ResourceRegionExportForm(Rectangle area, int mapId)
        {
            InitializeComponent();
            Icon = Options.GetFiddlerIcon();

            numericUpDownX.Value = area.X;
            numericUpDownY.Value = area.Y;
            numericUpDownWidth.Value = area.Width + 1;
            numericUpDownHeight.Value = area.Height + 1;
            numericUpDownMap.Value = mapId;

            BuildResourceFields();
            UpdateXml();
        }

        private void BuildResourceFields()
        {
            tableLayoutPanelResources.SuspendLayout();
            for (int i = 0; i < _resources.Length; i++)
            {
                int column = i % 3 * 2;
                int row = i / 3;

                var label = new Label
                {
                    Text = _resources[i].Label,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Margin = new Padding(3, 6, 3, 3)
                };

                var amount = new NumericUpDown
                {
                    Maximum = _totalParts,
                    Width = 60,
                    Anchor = AnchorStyles.Left
                };
                amount.ValueChanged += OnFieldChanged;

                tableLayoutPanelResources.Controls.Add(label, column, row);
                tableLayoutPanelResources.Controls.Add(amount, column + 1, row);
                _amounts.Add(amount);
            }

            tableLayoutPanelResources.ResumeLayout();
        }

        private void OnFieldChanged(object sender, EventArgs e)
        {
            UpdateXml();
        }

        private void UpdateXml()
        {
            int total = _amounts.Sum(a => (int)a.Value);
            labelTotal.Text = $"Total: {total} / {_totalParts}";
            labelTotal.ForeColor = total == _totalParts ? SystemColors.ControlText : (Options.DarkMode ? Color.OrangeRed : Color.Red);

            if (total != _totalParts)
            {
                textBoxXml.Text = $"The amounts must add up to {_totalParts} (now {total}).";
                buttonCopy.Enabled = false;
                return;
            }

            var used = _resources.Zip(_amounts, (resource, amount) => (resource.Name, Amount: (int)amount.Value))
                                 .Where(r => r.Amount > 0)
                                 .ToList();

            var settings = new XmlWriterSettings { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment };
            var text = new StringBuilder();

            using (var stringWriter = new StringWriter(text))
            using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
            {
                writer.WriteStartElement("VeinInfo");
                writer.WriteAttributeString("rect", $"{numericUpDownX.Value},{numericUpDownY.Value},{numericUpDownWidth.Value},{numericUpDownHeight.Value}");
                writer.WriteAttributeString("commento", textBoxName.Text);
                writer.WriteAttributeString("map", numericUpDownMap.Value.ToString());
                writer.WriteAttributeString("tipi", used.Count.ToString());
                writer.WriteAttributeString("prio", numericUpDownPriority.Value.ToString());
                writer.WriteAttributeString("risorse", string.Join(",", used.Select(r => r.Name)));
                writer.WriteAttributeString("quantita", string.Join(",", used.Select(r => r.Amount)));
                writer.WriteEndElement();
            }

            textBoxXml.Text = text.ToString();
            buttonCopy.Enabled = true;
        }

        private void OnClickCopy(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxXml.Text);
        }
    }
}
