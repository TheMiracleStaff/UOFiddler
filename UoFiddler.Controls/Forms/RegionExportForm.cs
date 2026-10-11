using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using UoFiddler.Controls.Classes;

namespace UoFiddler.Controls.Forms
{
    /// <summary>
    /// Builds the &lt;region&gt; xml of a server region from rectangles picked on the map.
    /// </summary>
    public partial class RegionExportForm : Form
    {
        private readonly IReadOnlyList<Rectangle> _rectangles;

        /// <param name="rectangles">Areas in tiles, start and end inclusive.</param>
        public RegionExportForm(IReadOnlyList<Rectangle> rectangles)
        {
            InitializeComponent();
            Icon = Options.GetFiddlerIcon();

            _rectangles = rectangles;
            labelRectangles.Text = $"Rectangles: {rectangles.Count}";

            UpdateXml();
        }

        private void OnFieldChanged(object sender, EventArgs e)
        {
            UpdateXml();
        }

        private void UpdateXml()
        {
            var settings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Fragment };
            var text = new StringBuilder();

            using (var stringWriter = new StringWriter(text))
            using (XmlWriter writer = XmlWriter.Create(stringWriter, settings))
            {
                writer.WriteStartElement("region");
                writer.WriteAttributeString("priority", numericUpDownPriority.Value.ToString());
                writer.WriteAttributeString("name", textBoxName.Text);

                foreach (Rectangle rect in _rectangles)
                {
                    writer.WriteStartElement("rect");
                    writer.WriteAttributeString("x", rect.X.ToString());
                    writer.WriteAttributeString("y", rect.Y.ToString());
                    writer.WriteAttributeString("width", (rect.Width + 1).ToString());
                    writer.WriteAttributeString("height", (rect.Height + 1).ToString());
                    writer.WriteEndElement();
                }

                if (textBoxMusic.Text.Length > 0)
                {
                    writer.WriteStartElement("music");
                    writer.WriteAttributeString("name", textBoxMusic.Text);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            textBoxXml.Text = text.ToString();
        }

        private void OnClickCopy(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxXml.Text);
        }
    }
}
