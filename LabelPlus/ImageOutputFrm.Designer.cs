namespace LabelPlus
{
    partial class ImageOutputFrm
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
            this.button = new System.Windows.Forms.Button();
            this.radioButtonPNG = new System.Windows.Forms.RadioButton();
            this.radioButtonJPG = new System.Windows.Forms.RadioButton();
            this.labelOutputFormat = new System.Windows.Forms.Label();
            this.labelZoom = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.outPgBar = new System.Windows.Forms.ProgressBar();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.checkBoxJumpNoNum = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(12, 94);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(74, 25);
            this.button.TabIndex = 0;
            this.button.Text = " ";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // radioButtonPNG
            // 
            this.radioButtonPNG.AutoSize = true;
            this.radioButtonPNG.Checked = true;
            this.radioButtonPNG.Location = new System.Drawing.Point(93, 25);
            this.radioButtonPNG.Name = "radioButtonPNG";
            this.radioButtonPNG.Size = new System.Drawing.Size(41, 16);
            this.radioButtonPNG.TabIndex = 1;
            this.radioButtonPNG.TabStop = true;
            this.radioButtonPNG.Text = "PNG";
            this.radioButtonPNG.UseVisualStyleBackColor = true;
            // 
            // radioButtonJPG
            // 
            this.radioButtonJPG.AutoSize = true;
            this.radioButtonJPG.Location = new System.Drawing.Point(147, 25);
            this.radioButtonJPG.Name = "radioButtonJPG";
            this.radioButtonJPG.Size = new System.Drawing.Size(41, 16);
            this.radioButtonJPG.TabIndex = 1;
            this.radioButtonJPG.Text = "JPG";
            this.radioButtonJPG.UseVisualStyleBackColor = true;
            // 
            // labelOutputFormat
            // 
            this.labelOutputFormat.AutoSize = true;
            this.labelOutputFormat.Location = new System.Drawing.Point(12, 27);
            this.labelOutputFormat.Name = "labelOutputFormat";
            this.labelOutputFormat.Size = new System.Drawing.Size(53, 12);
            this.labelOutputFormat.TabIndex = 2;
            this.labelOutputFormat.Text = "输出格式";
            // 
            // labelZoom
            // 
            this.labelZoom.AutoSize = true;
            this.labelZoom.Location = new System.Drawing.Point(12, 67);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(53, 12);
            this.labelZoom.TabIndex = 2;
            this.labelZoom.Text = "缩放比例";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(90, 63);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(95, 21);
            this.textBox.TabIndex = 3;
            this.textBox.Text = "1.0";
            // 
            // outPgBar
            // 
            this.outPgBar.Location = new System.Drawing.Point(12, 134);
            this.outPgBar.Name = "outPgBar";
            this.outPgBar.Size = new System.Drawing.Size(184, 21);
            this.outPgBar.TabIndex = 4;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Enabled = false;
            this.buttonAbort.Location = new System.Drawing.Point(122, 94);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(74, 25);
            this.buttonAbort.TabIndex = 5;
            this.buttonAbort.Text = " ";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxJumpNoNum
            // 
            this.checkBoxJumpNoNum.AutoSize = true;
            this.checkBoxJumpNoNum.Location = new System.Drawing.Point(93, 47);
            this.checkBoxJumpNoNum.Name = "checkBoxJumpNoNum";
            this.checkBoxJumpNoNum.Size = new System.Drawing.Size(96, 16);
            this.checkBoxJumpNoNum.TabIndex = 6;
            this.checkBoxJumpNoNum.Text = "跳过无标号图";
            this.checkBoxJumpNoNum.UseVisualStyleBackColor = true;
            // 
            // ImageOutputFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 166);
            this.Controls.Add(this.checkBoxJumpNoNum);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.outPgBar);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelZoom);
            this.Controls.Add(this.labelOutputFormat);
            this.Controls.Add(this.radioButtonJPG);
            this.Controls.Add(this.radioButtonPNG);
            this.Controls.Add(this.button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ImageOutputFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Image Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageOutputFrm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        private System.Windows.Forms.RadioButton radioButtonPNG;
        private System.Windows.Forms.RadioButton radioButtonJPG;
        private System.Windows.Forms.Label labelOutputFormat;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ProgressBar outPgBar;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.CheckBox checkBoxJumpNoNum;
    }
}