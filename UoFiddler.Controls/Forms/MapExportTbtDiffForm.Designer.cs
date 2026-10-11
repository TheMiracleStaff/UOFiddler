namespace UoFiddler.Controls.Forms
{
    partial class MapExportTbtDiffForm
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
            labelFrom = new System.Windows.Forms.Label();
            numericUpDownX1 = new System.Windows.Forms.NumericUpDown();
            numericUpDownY1 = new System.Windows.Forms.NumericUpDown();
            labelTo = new System.Windows.Forms.Label();
            numericUpDownX2 = new System.Windows.Forms.NumericUpDown();
            numericUpDownY2 = new System.Windows.Forms.NumericUpDown();
            labelBlocks = new System.Windows.Forms.Label();
            checkBoxLand = new System.Windows.Forms.CheckBox();
            checkBoxStatics = new System.Windows.Forms.CheckBox();
            labelName = new System.Windows.Forms.Label();
            textBoxName = new System.Windows.Forms.TextBox();
            labelExtension = new System.Windows.Forms.Label();
            buttonExport = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            groupBoxArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY2).BeginInit();
            SuspendLayout();
            //
            // groupBoxArea
            //
            groupBoxArea.Controls.Add(labelFrom);
            groupBoxArea.Controls.Add(numericUpDownX1);
            groupBoxArea.Controls.Add(numericUpDownY1);
            groupBoxArea.Controls.Add(labelTo);
            groupBoxArea.Controls.Add(numericUpDownX2);
            groupBoxArea.Controls.Add(numericUpDownY2);
            groupBoxArea.Controls.Add(labelBlocks);
            groupBoxArea.Location = new System.Drawing.Point(12, 12);
            groupBoxArea.Name = "groupBoxArea";
            groupBoxArea.Size = new System.Drawing.Size(316, 110);
            groupBoxArea.TabIndex = 0;
            groupBoxArea.TabStop = false;
            groupBoxArea.Text = "Area (X, Y, inclusive)";
            //
            // labelFrom
            //
            labelFrom.AutoSize = true;
            labelFrom.Location = new System.Drawing.Point(10, 25);
            labelFrom.Name = "labelFrom";
            labelFrom.Size = new System.Drawing.Size(35, 15);
            labelFrom.TabIndex = 0;
            labelFrom.Text = "From";
            //
            // numericUpDownX1
            //
            numericUpDownX1.Location = new System.Drawing.Point(60, 22);
            numericUpDownX1.Name = "numericUpDownX1";
            numericUpDownX1.Size = new System.Drawing.Size(70, 23);
            numericUpDownX1.TabIndex = 1;
            numericUpDownX1.ValueChanged += OnAreaChanged;
            //
            // numericUpDownY1
            //
            numericUpDownY1.Location = new System.Drawing.Point(140, 22);
            numericUpDownY1.Name = "numericUpDownY1";
            numericUpDownY1.Size = new System.Drawing.Size(70, 23);
            numericUpDownY1.TabIndex = 2;
            numericUpDownY1.ValueChanged += OnAreaChanged;
            //
            // labelTo
            //
            labelTo.AutoSize = true;
            labelTo.Location = new System.Drawing.Point(10, 54);
            labelTo.Name = "labelTo";
            labelTo.Size = new System.Drawing.Size(20, 15);
            labelTo.TabIndex = 3;
            labelTo.Text = "To";
            //
            // numericUpDownX2
            //
            numericUpDownX2.Location = new System.Drawing.Point(60, 51);
            numericUpDownX2.Name = "numericUpDownX2";
            numericUpDownX2.Size = new System.Drawing.Size(70, 23);
            numericUpDownX2.TabIndex = 4;
            numericUpDownX2.ValueChanged += OnAreaChanged;
            //
            // numericUpDownY2
            //
            numericUpDownY2.Location = new System.Drawing.Point(140, 51);
            numericUpDownY2.Name = "numericUpDownY2";
            numericUpDownY2.Size = new System.Drawing.Size(70, 23);
            numericUpDownY2.TabIndex = 5;
            numericUpDownY2.ValueChanged += OnAreaChanged;
            //
            // labelBlocks
            //
            labelBlocks.AutoSize = true;
            labelBlocks.Location = new System.Drawing.Point(10, 83);
            labelBlocks.Name = "labelBlocks";
            labelBlocks.Size = new System.Drawing.Size(100, 15);
            labelBlocks.TabIndex = 6;
            labelBlocks.Text = "Saves 0 block(s)";
            //
            // checkBoxLand
            //
            checkBoxLand.AutoSize = true;
            checkBoxLand.Checked = true;
            checkBoxLand.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxLand.Location = new System.Drawing.Point(22, 132);
            checkBoxLand.Name = "checkBoxLand";
            checkBoxLand.Size = new System.Drawing.Size(97, 19);
            checkBoxLand.TabIndex = 1;
            checkBoxLand.Text = "Land (map)";
            checkBoxLand.UseVisualStyleBackColor = true;
            //
            // checkBoxStatics
            //
            checkBoxStatics.AutoSize = true;
            checkBoxStatics.Checked = true;
            checkBoxStatics.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxStatics.Location = new System.Drawing.Point(150, 132);
            checkBoxStatics.Name = "checkBoxStatics";
            checkBoxStatics.Size = new System.Drawing.Size(62, 19);
            checkBoxStatics.TabIndex = 2;
            checkBoxStatics.Text = "Statics";
            checkBoxStatics.UseVisualStyleBackColor = true;
            //
            // labelName
            //
            labelName.AutoSize = true;
            labelName.Location = new System.Drawing.Point(19, 167);
            labelName.Name = "labelName";
            labelName.Size = new System.Drawing.Size(60, 15);
            labelName.TabIndex = 3;
            labelName.Text = "File name";
            //
            // textBoxName
            //
            textBoxName.Location = new System.Drawing.Point(88, 164);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new System.Drawing.Size(180, 23);
            textBoxName.TabIndex = 4;
            //
            // labelExtension
            //
            labelExtension.AutoSize = true;
            labelExtension.Location = new System.Drawing.Point(270, 167);
            labelExtension.Name = "labelExtension";
            labelExtension.Size = new System.Drawing.Size(45, 15);
            labelExtension.TabIndex = 5;
            labelExtension.Text = ".tbtdiff";
            //
            // buttonExport
            //
            buttonExport.Location = new System.Drawing.Point(160, 202);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new System.Drawing.Size(80, 27);
            buttonExport.TabIndex = 6;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += OnClickExport;
            //
            // buttonCancel
            //
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(248, 202);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(80, 27);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            //
            // MapExportTbtDiffForm
            //
            AcceptButton = buttonExport;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(340, 241);
            Controls.Add(groupBoxArea);
            Controls.Add(checkBoxLand);
            Controls.Add(checkBoxStatics);
            Controls.Add(labelName);
            Controls.Add(textBoxName);
            Controls.Add(labelExtension);
            Controls.Add(buttonExport);
            Controls.Add(buttonCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MapExportTbtDiffForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Export TbtDiff Patch";
            groupBoxArea.ResumeLayout(false);
            groupBoxArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownX2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownY2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxArea;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.NumericUpDown numericUpDownX1;
        private System.Windows.Forms.NumericUpDown numericUpDownY1;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.NumericUpDown numericUpDownX2;
        private System.Windows.Forms.NumericUpDown numericUpDownY2;
        private System.Windows.Forms.Label labelBlocks;
        private System.Windows.Forms.CheckBox checkBoxLand;
        private System.Windows.Forms.CheckBox checkBoxStatics;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelExtension;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonCancel;
    }
}
