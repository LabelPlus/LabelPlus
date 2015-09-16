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
            this.checkBoxUseOtherFileType = new System.Windows.Forms.CheckBox();
            this.textBoxFileType = new System.Windows.Forms.TextBox();
            this.checkBoxAutoGroupAction = new System.Windows.Forms.CheckBox();
            this.textBoxAutoGroupActionGroupname = new System.Windows.Forms.TextBox();
            this.labelAutoGroupActionTip = new System.Windows.Forms.Label();
            this.labelOutputItemTip = new System.Windows.Forms.Label();
            this.labelDefaultFormatTip = new System.Windows.Forms.Label();
            this.labelAutodoTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNumCheckBox
            // 
            this.labelNumCheckBox.AutoSize = true;
            this.labelNumCheckBox.Location = new System.Drawing.Point(12, 24);
            this.labelNumCheckBox.Name = "labelNumCheckBox";
            this.labelNumCheckBox.Size = new System.Drawing.Size(72, 16);
            this.labelNumCheckBox.TabIndex = 0;
            this.labelNumCheckBox.Text = "导出标号";
            this.labelNumCheckBox.UseVisualStyleBackColor = true;
            // 
            // notHeadFootSignCheckBox
            // 
            this.notHeadFootSignCheckBox.AutoSize = true;
            this.notHeadFootSignCheckBox.Location = new System.Drawing.Point(12, 181);
            this.notHeadFootSignCheckBox.Name = "notHeadFootSignCheckBox";
            this.notHeadFootSignCheckBox.Size = new System.Drawing.Size(96, 16);
            this.notHeadFootSignCheckBox.TabIndex = 0;
            this.notHeadFootSignCheckBox.Text = "添加头尾标志";
            this.notHeadFootSignCheckBox.UseVisualStyleBackColor = true;
            // 
            // notCloseFileCheckBox
            // 
            this.notCloseFileCheckBox.AutoSize = true;
            this.notCloseFileCheckBox.Location = new System.Drawing.Point(12, 225);
            this.notCloseFileCheckBox.Name = "notCloseFileCheckBox";
            this.notCloseFileCheckBox.Size = new System.Drawing.Size(180, 16);
            this.notCloseFileCheckBox.TabIndex = 0;
            this.notCloseFileCheckBox.Text = "修改文本后不关闭文档(慎用)";
            this.notCloseFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // outputButton
            // 
            this.outputButton.Location = new System.Drawing.Point(215, 255);
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
            this.checkBoxMakeUnLabeledFile.Location = new System.Drawing.Point(12, 46);
            this.checkBoxMakeUnLabeledFile.Name = "checkBoxMakeUnLabeledFile";
            this.checkBoxMakeUnLabeledFile.Size = new System.Drawing.Size(108, 16);
            this.checkBoxMakeUnLabeledFile.TabIndex = 0;
            this.checkBoxMakeUnLabeledFile.Text = "处理无标号文档";
            this.checkBoxMakeUnLabeledFile.UseVisualStyleBackColor = true;
            // 
            // checkBoxSetFont
            // 
            this.checkBoxSetFont.AutoSize = true;
            this.checkBoxSetFont.Location = new System.Drawing.Point(12, 111);
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
            this.comboBoxFont.Location = new System.Drawing.Point(302, 109);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(121, 20);
            this.comboBoxFont.TabIndex = 2;
            // 
            // checkBoxSetFontSize
            // 
            this.checkBoxSetFontSize.AutoSize = true;
            this.checkBoxSetFontSize.Location = new System.Drawing.Point(12, 133);
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
            this.numericUpDownFontSize.Location = new System.Drawing.Point(302, 130);
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
            // checkBoxUseOtherFileType
            // 
            this.checkBoxUseOtherFileType.AutoSize = true;
            this.checkBoxUseOtherFileType.Location = new System.Drawing.Point(12, 68);
            this.checkBoxUseOtherFileType.Name = "checkBoxUseOtherFileType";
            this.checkBoxUseOtherFileType.Size = new System.Drawing.Size(120, 16);
            this.checkBoxUseOtherFileType.TabIndex = 0;
            this.checkBoxUseOtherFileType.Text = "使用指定类型图源";
            this.checkBoxUseOtherFileType.UseVisualStyleBackColor = true;
            this.checkBoxUseOtherFileType.CheckedChanged += new System.EventHandler(this.checkBoxUseOtherFileType_CheckedChanged);
            // 
            // textBoxFileType
            // 
            this.textBoxFileType.Enabled = false;
            this.textBoxFileType.Location = new System.Drawing.Point(302, 63);
            this.textBoxFileType.Name = "textBoxFileType";
            this.textBoxFileType.Size = new System.Drawing.Size(58, 21);
            this.textBoxFileType.TabIndex = 5;
            this.textBoxFileType.Text = ".psd";
            // 
            // checkBoxAutoGroupAction
            // 
            this.checkBoxAutoGroupAction.AutoSize = true;
            this.checkBoxAutoGroupAction.Location = new System.Drawing.Point(12, 203);
            this.checkBoxAutoGroupAction.Name = "checkBoxAutoGroupAction";
            this.checkBoxAutoGroupAction.Size = new System.Drawing.Size(246, 16);
            this.checkBoxAutoGroupAction.TabIndex = 0;
            this.checkBoxAutoGroupAction.Text = "根据标号分组,执行动作组中的动作GroupN";
            this.checkBoxAutoGroupAction.UseVisualStyleBackColor = true;
            this.checkBoxAutoGroupAction.CheckedChanged += new System.EventHandler(this.checkBoxAutoGroupAction_CheckedChanged);
            // 
            // textBoxAutoGroupActionGroupname
            // 
            this.textBoxAutoGroupActionGroupname.Enabled = false;
            this.textBoxAutoGroupActionGroupname.Location = new System.Drawing.Point(302, 201);
            this.textBoxAutoGroupActionGroupname.Name = "textBoxAutoGroupActionGroupname";
            this.textBoxAutoGroupActionGroupname.Size = new System.Drawing.Size(121, 21);
            this.textBoxAutoGroupActionGroupname.TabIndex = 5;
            this.textBoxAutoGroupActionGroupname.Text = "LabelplusAction";
            // 
            // labelAutoGroupActionTip
            // 
            this.labelAutoGroupActionTip.AutoSize = true;
            this.labelAutoGroupActionTip.Location = new System.Drawing.Point(429, 204);
            this.labelAutoGroupActionTip.Name = "labelAutoGroupActionTip";
            this.labelAutoGroupActionTip.Size = new System.Drawing.Size(65, 12);
            this.labelAutoGroupActionTip.TabIndex = 6;
            this.labelAutoGroupActionTip.Text = "(动作组名)";
            // 
            // labelOutputItemTip
            // 
            this.labelOutputItemTip.AutoSize = true;
            this.labelOutputItemTip.Location = new System.Drawing.Point(12, 9);
            this.labelOutputItemTip.Name = "labelOutputItemTip";
            this.labelOutputItemTip.Size = new System.Drawing.Size(53, 12);
            this.labelOutputItemTip.TabIndex = 7;
            this.labelOutputItemTip.Text = "导出项目";
            // 
            // labelDefaultFormatTip
            // 
            this.labelDefaultFormatTip.AutoSize = true;
            this.labelDefaultFormatTip.Location = new System.Drawing.Point(12, 96);
            this.labelDefaultFormatTip.Name = "labelDefaultFormatTip";
            this.labelDefaultFormatTip.Size = new System.Drawing.Size(53, 12);
            this.labelDefaultFormatTip.TabIndex = 7;
            this.labelDefaultFormatTip.Text = "默认格式";
            // 
            // labelAutodoTip
            // 
            this.labelAutodoTip.AutoSize = true;
            this.labelAutodoTip.Location = new System.Drawing.Point(12, 166);
            this.labelAutodoTip.Name = "labelAutodoTip";
            this.labelAutodoTip.Size = new System.Drawing.Size(77, 12);
            this.labelAutodoTip.TabIndex = 7;
            this.labelAutodoTip.Text = "流程及自动化";
            // 
            // OutputScriptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(641, 292);
            this.Controls.Add(this.labelAutodoTip);
            this.Controls.Add(this.labelDefaultFormatTip);
            this.Controls.Add(this.labelOutputItemTip);
            this.Controls.Add(this.labelAutoGroupActionTip);
            this.Controls.Add(this.textBoxAutoGroupActionGroupname);
            this.Controls.Add(this.textBoxFileType);
            this.Controls.Add(this.numericUpDownFontSize);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.outputButton);
            this.Controls.Add(this.checkBoxUseOtherFileType);
            this.Controls.Add(this.checkBoxSetFontSize);
            this.Controls.Add(this.checkBoxSetFont);
            this.Controls.Add(this.checkBoxMakeUnLabeledFile);
            this.Controls.Add(this.notCloseFileCheckBox);
            this.Controls.Add(this.checkBoxAutoGroupAction);
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
        private System.Windows.Forms.CheckBox checkBoxUseOtherFileType;
        private System.Windows.Forms.TextBox textBoxFileType;
        private System.Windows.Forms.CheckBox checkBoxAutoGroupAction;
        private System.Windows.Forms.TextBox textBoxAutoGroupActionGroupname;
        private System.Windows.Forms.Label labelAutoGroupActionTip;
        private System.Windows.Forms.Label labelOutputItemTip;
        private System.Windows.Forms.Label labelDefaultFormatTip;
        private System.Windows.Forms.Label labelAutodoTip;
    }
}