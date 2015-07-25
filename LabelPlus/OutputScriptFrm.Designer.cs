namespace LabelPlus
{
    partial class OutputScriptFrm
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
            this.labelNumCheckBox = new System.Windows.Forms.CheckBox();
            this.notHeadFootSignCheckBox = new System.Windows.Forms.CheckBox();
            this.notCloseFileCheckBox = new System.Windows.Forms.CheckBox();
            this.outputButton = new System.Windows.Forms.Button();
            this.checkBoxMakeUnLabeledFile = new System.Windows.Forms.CheckBox();
            this.checkBoxSetFont = new System.Windows.Forms.CheckBox();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.checkBoxSetFontSize = new System.Windows.Forms.CheckBox();
            this.numericUpDownFontSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNumCheckBox
            // 
            this.labelNumCheckBox.AutoSize = true;
            this.labelNumCheckBox.Location = new System.Drawing.Point(14, 18);
            this.labelNumCheckBox.Name = "labelNumCheckBox";
            this.labelNumCheckBox.Size = new System.Drawing.Size(72, 16);
            this.labelNumCheckBox.TabIndex = 0;
            this.labelNumCheckBox.Text = "导出标号";
            this.labelNumCheckBox.UseVisualStyleBackColor = true;
            // 
            // notHeadFootSignCheckBox
            // 
            this.notHeadFootSignCheckBox.AutoSize = true;
            this.notHeadFootSignCheckBox.Location = new System.Drawing.Point(14, 40);
            this.notHeadFootSignCheckBox.Name = "notHeadFootSignCheckBox";
            this.notHeadFootSignCheckBox.Size = new System.Drawing.Size(96, 16);
            this.notHeadFootSignCheckBox.TabIndex = 0;
            this.notHeadFootSignCheckBox.Text = "添加头尾标志";
            this.notHeadFootSignCheckBox.UseVisualStyleBackColor = true;
            // 
            // notCloseFileCheckBox
            // 
            this.notCloseFileCheckBox.AutoSize = true;
            this.notCloseFileCheckBox.Location = new System.Drawing.Point(14, 62);
            this.notCloseFileCheckBox.Name = "notCloseFileCheckBox";
            this.notCloseFileCheckBox.Size = new System.Drawing.Size(180, 16);
            this.notCloseFileCheckBox.TabIndex = 0;
            this.notCloseFileCheckBox.Text = "修改文本后不关闭文档(慎用)";
            this.notCloseFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputButton
            // 
            this.outputButton.Location = new System.Drawing.Point(107, 154);
            this.outputButton.Name = "outputButton";
            this.outputButton.Size = new System.Drawing.Size(87, 26);
            this.outputButton.TabIndex = 1;
            this.outputButton.Text = "导出";
            this.outputButton.UseVisualStyleBackColor = true;
            this.outputButton.Click += new System.EventHandler(this.outputButton_Click);
            // 
            // checkBoxMakeUnLabeledFile
            // 
            this.checkBoxMakeUnLabeledFile.AutoSize = true;
            this.checkBoxMakeUnLabeledFile.Location = new System.Drawing.Point(14, 84);
            this.checkBoxMakeUnLabeledFile.Name = "checkBoxMakeUnLabeledFile";
            this.checkBoxMakeUnLabeledFile.Size = new System.Drawing.Size(108, 16);
            this.checkBoxMakeUnLabeledFile.TabIndex = 0;
            this.checkBoxMakeUnLabeledFile.Text = "处理无标号文档";
            this.checkBoxMakeUnLabeledFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxSetFont
            // 
            this.checkBoxSetFont.AutoSize = true;
            this.checkBoxSetFont.Location = new System.Drawing.Point(14, 106);
            this.checkBoxSetFont.Name = "checkBoxSetFont";
            this.checkBoxSetFont.Size = new System.Drawing.Size(84, 16);
            this.checkBoxSetFont.TabIndex = 0;
            this.checkBoxSetFont.Text = "自定义字体";
            this.checkBoxSetFont.UseVisualStyleBackColor = true;
            this.checkBoxSetFont.CheckedChanged += new System.EventHandler(this.checkBoxSetFont_CheckedChanged);
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFont.Enabled = false;
            this.comboBoxFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(188, 104);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(121, 20);
            this.comboBoxFont.TabIndex = 2;
            // 
            // checkBoxSetFontSize
            // 
            this.checkBoxSetFontSize.AutoSize = true;
            this.checkBoxSetFontSize.Location = new System.Drawing.Point(14, 128);
            this.checkBoxSetFontSize.Name = "checkBoxSetFontSize";
            this.checkBoxSetFontSize.Size = new System.Drawing.Size(84, 16);
            this.checkBoxSetFontSize.TabIndex = 0;
            this.checkBoxSetFontSize.Text = "自定义字号";
            this.checkBoxSetFontSize.UseVisualStyleBackColor = true;
            this.checkBoxSetFontSize.CheckedChanged += new System.EventHandler(this.checkBoxSetFontSize_CheckedChanged);
            // 
            // numericUpDownFontSize
            // 
            this.numericUpDownFontSize.Enabled = false;
            this.numericUpDownFontSize.Location = new System.Drawing.Point(188, 127);
            this.numericUpDownFontSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFontSize.Name = "numericUpDownFontSize";
            this.numericUpDownFontSize.Size = new System.Drawing.Size(58, 21);
            this.numericUpDownFontSize.TabIndex = 4;
            this.numericUpDownFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // OutputScriptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(330, 191);
            this.Controls.Add(this.numericUpDownFontSize);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.outputButton);
            this.Controls.Add(this.checkBoxSetFontSize);
            this.Controls.Add(this.checkBoxSetFont);
            this.Controls.Add(this.checkBoxMakeUnLabeledFile);
            this.Controls.Add(this.notCloseFileCheckBox);
            this.Controls.Add(this.notHeadFootSignCheckBox);
            this.Controls.Add(this.labelNumCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OutputScriptFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Output Photoshop Script";
            this.Load += new System.EventHandler(this.OutputScriptFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox labelNumCheckBox;
        private System.Windows.Forms.CheckBox notHeadFootSignCheckBox;
        private System.Windows.Forms.CheckBox notCloseFileCheckBox;
        private System.Windows.Forms.Button outputButton;
        private System.Windows.Forms.CheckBox checkBoxMakeUnLabeledFile;
        private System.Windows.Forms.CheckBox checkBoxSetFont;
        private System.Windows.Forms.ComboBox comboBoxFont;
        private System.Windows.Forms.CheckBox checkBoxSetFontSize;
        private System.Windows.Forms.NumericUpDown numericUpDownFontSize;
    }
}