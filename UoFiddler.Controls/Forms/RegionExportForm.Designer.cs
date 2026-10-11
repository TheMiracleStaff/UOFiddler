namespace UoFiddler.Controls.Forms
{
    partial class RegionExportForm
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
            labelName = new System.Windows.Forms.Label();
            textBoxName = new System.Windows.Forms.TextBox();
            labelPriority = new System.Windows.Forms.Label();
            numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            labelMusic = new System.Windows.Forms.Label();
            textBoxMusic = new System.Windows.Forms.TextBox();
            labelRectangles = new System.Windows.Forms.Label();
            textBoxXml = new System.Windows.Forms.TextBox();
            buttonCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPriority).BeginInit();
            SuspendLayout();
            //
            // labelName
            //
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(12, 15);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(39, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Name";
            //
            // textBoxName
            //
            textBoxName.Location = new System.Drawing.Point(70, 12);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(220, 23);
            textBoxName.TabIndex = 1;
            textBoxName.TextChanged += OnFieldChanged;
            //
            // labelPriority
            //
            labelPriority.AutoSize = true;
            labelPriority.Location = new System.Drawing.Point(305, 15);
            labelPriority.Name = "labelPriority";
            labelPriority.Size = new System.Drawing.Size(45, 15);
            labelPriority.TabIndex = 2;
            labelPriority.Text = "Priority";
            //
            // numericUpDownPriority
            //
            numericUpDownPriority.Location = new System.Drawing.Point(360, 12);
            numericUpDownPriority.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownPriority.Name = "numericUpDownPriority";
            numericUpDownPriority.Size = new System.Drawing.Size(60, 23);
            numericUpDownPriority.TabIndex = 3;
            numericUpDownPriority.Value = new decimal(new int[] { 50, 0, 0, 0 });
            numericUpDownPriority.ValueChanged += OnFieldChanged;
            //
            // labelMusic
            //
            labelMusic.AutoSize = true;
            labelMusic.Location = new System.Drawing.Point(12, 44);
            labelMusic.Name = "labelMusic";
            labelMusic.Size = new System.Drawing.Size(39, 15);
            labelMusic.TabIndex = 4;
            labelMusic.Text = "Music";
            //
            // textBoxMusic
            //
            textBoxMusic.Location = new System.Drawing.Point(70, 41);
            textBoxMusic.Name = "textBoxMusic";
            textBoxMusic.Size = new System.Drawing.Size(220, 23);
            textBoxMusic.TabIndex = 5;
            textBoxMusic.TextChanged += OnFieldChanged;
            //
            // labelRectangles
            //
            labelRectangles.AutoSize = true;
            labelRectangles.Location = new System.Drawing.Point(305, 44);
            labelRectangles.Name = "labelRectangles";
            labelRectangles.Size = new System.Drawing.Size(75, 15);
            labelRectangles.TabIndex = 6;
            labelRectangles.Text = "Rectangles: 0";
            //
            // textBoxXml
            //
            textBoxXml.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxXml.Font = new System.Drawing.Font("Consolas", 9F);
            textBoxXml.Location = new System.Drawing.Point(12, 74);
            textBoxXml.Multiline = true;
            textBoxXml.Name = "textBoxXml";
            textBoxXml.ReadOnly = true;
            textBoxXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxXml.Size = new System.Drawing.Size(456, 230);
            textBoxXml.TabIndex = 7;
            textBoxXml.WordWrap = false;
            //
            // buttonCopy
            //
            buttonCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonCopy.Location = new System.Drawing.Point(338, 312);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new System.Drawing.Size(130, 27);
            buttonCopy.TabIndex = 8;
            buttonCopy.Text = "Copy to Clipboard";
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Click += OnClickCopy;
            //
            // RegionExportForm
            //
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(480, 351);
            Controls.Add(labelName);
            Controls.Add(textBoxName);
            Controls.Add(labelPriority);
            Controls.Add(numericUpDownPriority);
            Controls.Add(labelMusic);
            Controls.Add(textBoxMusic);
            Controls.Add(labelRectangles);
            Controls.Add(textBoxXml);
            Controls.Add(buttonCopy);
            MinimumSize = new System.Drawing.Size(496, 300);
            Name = "RegionExportForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Export Region XML";
            ((System.ComponentModel.ISupportInitialize)numericUpDownPriority).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.Label labelMusic;
        private System.Windows.Forms.TextBox textBoxMusic;
        private System.Windows.Forms.Label labelRectangles;
        private System.Windows.Forms.TextBox textBoxXml;
        private System.Windows.Forms.Button buttonCopy;
    }
}
