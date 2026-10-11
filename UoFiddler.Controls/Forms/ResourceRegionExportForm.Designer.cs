namespace UoFiddler.Controls.Forms
{
    partial class ResourceRegionExportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxArea = new System.Windows.Forms.GroupBox();
            labelX = new System.Windows.Forms.Label();
            numericUpDownX = new System.Windows.Forms.NumericUpDown();
            labelY = new System.Windows.Forms.Label();
            numericUpDownY = new System.Windows.Forms.NumericUpDown();
            labelWidth = new System.Windows.Forms.Label();
            numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            labelHeight = new System.Windows.Forms.Label();
            numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            labelMap = new System.Windows.Forms.Label();
            numericUpDownMap = new System.Windows.Forms.NumericUpDown();
            labelName = new System.Windows.Forms.Label();
            textBoxName = new System.Windows.Forms.TextBox();
            labelPriority = new System.Windows.Forms.Label();
            numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            groupBoxResources = new System.Windows.Forms.GroupBox();
            tableLayoutPanelResources = new System.Windows.Forms.TableLayoutPanel();
            labelTotal = new System.Windows.Forms.Label();
            textBoxXml = new System.Windows.Forms.TextBox();
            buttonCopy = new System.Windows.Forms.Button();
            groupBoxArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMap).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPriority).BeginInit();
            groupBoxResources.SuspendLayout();
            SuspendLayout();
            //
            // groupBoxArea
            //
            groupBoxArea.Controls.Add(labelX);
            groupBoxArea.Controls.Add(numericUpDownX);
            groupBoxArea.Controls.Add(labelY);
            groupBoxArea.Controls.Add(numericUpDownY);
            groupBoxArea.Controls.Add(labelWidth);
            groupBoxArea.Controls.Add(numericUpDownWidth);
            groupBoxArea.Controls.Add(labelHeight);
            groupBoxArea.Controls.Add(numericUpDownHeight);
            groupBoxArea.Controls.Add(labelMap);
            groupBoxArea.Controls.Add(numericUpDownMap);
            groupBoxArea.Controls.Add(labelName);
            groupBoxArea.Controls.Add(textBoxName);
            groupBoxArea.Controls.Add(labelPriority);
            groupBoxArea.Controls.Add(numericUpDownPriority);
            groupBoxArea.Location = new System.Drawing.Point(12, 12);
            groupBoxArea.Name = "groupBoxArea";
            groupBoxArea.Size = new System.Drawing.Size(496, 90);
            groupBoxArea.TabIndex = 0;
            groupBoxArea.TabStop = false;
            groupBoxArea.Text = "Area";
            //
            // labelX
            //
            labelX.AutoSize = true;
            labelX.Location = new System.Drawing.Point(10, 25);
            labelX.Name = "labelX";
            labelX.Size = new System.Drawing.Size(14, 15);
            labelX.TabIndex = 0;
            labelX.Text = "X";
            //
            // numericUpDownX
            //
            numericUpDownX.Location = new System.Drawing.Point(30, 22);
            numericUpDownX.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericUpDownX.Name = "numericUpDownX";
            numericUpDownX.Size = new System.Drawing.Size(65, 23);
            numericUpDownX.TabIndex = 1;
            numericUpDownX.ValueChanged += OnFieldChanged;
            //
            // labelY
            //
            labelY.AutoSize = true;
            labelY.Location = new System.Drawing.Point(105, 25);
            labelY.Name = "labelY";
            labelY.Size = new System.Drawing.Size(14, 15);
            labelY.TabIndex = 2;
            labelY.Text = "Y";
            //
            // numericUpDownY
            //
            numericUpDownY.Location = new System.Drawing.Point(125, 22);
            numericUpDownY.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericUpDownY.Name = "numericUpDownY";
            numericUpDownY.Size = new System.Drawing.Size(65, 23);
            numericUpDownY.TabIndex = 3;
            numericUpDownY.ValueChanged += OnFieldChanged;
            //
            // labelWidth
            //
            labelWidth.AutoSize = true;
            labelWidth.Location = new System.Drawing.Point(200, 25);
            labelWidth.Name = "labelWidth";
            labelWidth.Size = new System.Drawing.Size(39, 15);
            labelWidth.TabIndex = 4;
            labelWidth.Text = "Width";
            //
            // numericUpDownWidth
            //
            numericUpDownWidth.Location = new System.Drawing.Point(245, 22);
            numericUpDownWidth.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.Size = new System.Drawing.Size(65, 23);
            numericUpDownWidth.TabIndex = 5;
            numericUpDownWidth.ValueChanged += OnFieldChanged;
            //
            // labelHeight
            //
            labelHeight.AutoSize = true;
            labelHeight.Location = new System.Drawing.Point(320, 25);
            labelHeight.Name = "labelHeight";
            labelHeight.Size = new System.Drawing.Size(43, 15);
            labelHeight.TabIndex = 6;
            labelHeight.Text = "Height";
            //
            // numericUpDownHeight
            //
            numericUpDownHeight.Location = new System.Drawing.Point(370, 22);
            numericUpDownHeight.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.Size = new System.Drawing.Size(65, 23);
            numericUpDownHeight.TabIndex = 7;
            numericUpDownHeight.ValueChanged += OnFieldChanged;
            //
            // labelMap
            //
            labelMap.AutoSize = true;
            labelMap.Location = new System.Drawing.Point(10, 57);
            labelMap.Name = "labelMap";
            labelMap.Size = new System.Drawing.Size(31, 15);
            labelMap.TabIndex = 8;
            labelMap.Text = "Map";
            //
            // numericUpDownMap
            //
            numericUpDownMap.Location = new System.Drawing.Point(45, 54);
            numericUpDownMap.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownMap.Name = "numericUpDownMap";
            numericUpDownMap.Size = new System.Drawing.Size(50, 23);
            numericUpDownMap.TabIndex = 9;
            numericUpDownMap.ValueChanged += OnFieldChanged;
            //
            // labelName
            //
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(105, 57);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(39, 15);
            labelName.TabIndex = 10;
            labelName.Text = "Name";
            //
            // textBoxName
            //
            textBoxName.Location = new System.Drawing.Point(150, 54);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(200, 23);
            textBoxName.TabIndex = 11;
            textBoxName.TextChanged += OnFieldChanged;
            //
            // labelPriority
            //
            labelPriority.AutoSize = true;
            labelPriority.Location = new System.Drawing.Point(360, 57);
            labelPriority.Name = "labelPriority";
            labelPriority.Size = new System.Drawing.Size(45, 15);
            labelPriority.TabIndex = 12;
            labelPriority.Text = "Priority";
            //
            // numericUpDownPriority
            //
            numericUpDownPriority.Location = new System.Drawing.Point(410, 54);
            numericUpDownPriority.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownPriority.Name = "numericUpDownPriority";
            numericUpDownPriority.Size = new System.Drawing.Size(55, 23);
            numericUpDownPriority.TabIndex = 13;
            numericUpDownPriority.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownPriority.ValueChanged += OnFieldChanged;
            //
            // groupBoxResources
            //
            groupBoxResources.Controls.Add(tableLayoutPanelResources);
            groupBoxResources.Controls.Add(labelTotal);
            groupBoxResources.Location = new System.Drawing.Point(12, 108);
            groupBoxResources.Name = "groupBoxResources";
            groupBoxResources.Size = new System.Drawing.Size(496, 200);
            groupBoxResources.TabIndex = 1;
            groupBoxResources.TabStop = false;
            groupBoxResources.Text = "Ores (parts per thousand)";
            //
            // tableLayoutPanelResources
            //
            tableLayoutPanelResources.ColumnCount = 6;
            tableLayoutPanelResources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanelResources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanelResources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanelResources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanelResources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanelResources.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            tableLayoutPanelResources.Location = new System.Drawing.Point(8, 20);
            tableLayoutPanelResources.Name = "tableLayoutPanelResources";
            tableLayoutPanelResources.RowCount = 5;
            tableLayoutPanelResources.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanelResources.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanelResources.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanelResources.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanelResources.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanelResources.Size = new System.Drawing.Size(480, 147);
            tableLayoutPanelResources.TabIndex = 0;
            //
            // labelTotal
            //
            labelTotal.AutoSize = true;
            labelTotal.Location = new System.Drawing.Point(10, 174);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new System.Drawing.Size(90, 15);
            labelTotal.TabIndex = 1;
            labelTotal.Text = "Total: 0 / 1000";
            //
            // textBoxXml
            //
            textBoxXml.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxXml.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxXml.Location = new System.Drawing.Point(12, 316);
            textBoxXml.Multiline = true;
            textBoxXml.Name = "textBoxXml";
            textBoxXml.ReadOnly = true;
            textBoxXml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBoxXml.Size = new System.Drawing.Size(496, 70);
            textBoxXml.TabIndex = 2;
            //
            // buttonCopy
            //
            buttonCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCopy.Location = new System.Drawing.Point(378, 394);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new System.Drawing.Size(130, 27);
            buttonCopy.TabIndex = 3;
            buttonCopy.Text = "Copy to Clipboard";
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Click += OnClickCopy;
            //
            // ResourceRegionExportForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(520, 433);
            Controls.Add(groupBoxArea);
            Controls.Add(groupBoxResources);
            Controls.Add(textBoxXml);
            Controls.Add(buttonCopy);
            MinimumSize = new System.Drawing.Size(536, 472);
            Name = "ResourceRegionExportForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Export Resource Region";
            groupBoxArea.ResumeLayout(false);
            groupBoxArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMap).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPriority).EndInit();
            groupBoxResources.ResumeLayout(false);
            groupBoxResources.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxArea;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.NumericUpDown numericUpDownMap;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.GroupBox groupBoxResources;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelResources;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.TextBox textBoxXml;
        private System.Windows.Forms.Button buttonCopy;
    }
}
