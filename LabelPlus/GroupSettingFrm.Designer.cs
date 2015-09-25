namespace LabelPlus
{
    partial class FileSettingFrm
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
            this.textBoxGroup = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelComment = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxGroup
            // 
            this.textBoxGroup.Location = new System.Drawing.Point(8, 29);
            this.textBoxGroup.Multiline = true;
            this.textBoxGroup.Name = "textBoxGroup";
            this.textBoxGroup.Size = new System.Drawing.Size(239, 177);
            this.textBoxGroup.TabIndex = 4;
            this.textBoxGroup.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(253, 212);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(106, 36);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(12, 11);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(59, 12);
            this.labelGroup.TabIndex = 6;
            this.labelGroup.Text = "Group设置";
            // 
            // labelComment
            // 
            this.labelComment.AutoSize = true;
            this.labelComment.Location = new System.Drawing.Point(251, 11);
            this.labelComment.Name = "labelComment";
            this.labelComment.Size = new System.Drawing.Size(29, 12);
            this.labelComment.TabIndex = 6;
            this.labelComment.Text = "备注";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(253, 29);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(353, 177);
            this.textBoxComment.TabIndex = 4;
            this.textBoxComment.Text = "DefaultComment";
            // 
            // FileSettingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 259);
            this.Controls.Add(this.labelComment);
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.textBoxGroup);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FileSettingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FileSettingFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGroup;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.Label labelComment;
        private System.Windows.Forms.TextBox textBoxComment;

    }
}