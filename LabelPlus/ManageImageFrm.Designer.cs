namespace LabelPlus
{
    partial class ManageImageFrm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.listBoxFolderFile = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonAddAll = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonDeleteAll = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.listBoxIncludedFile = new System.Windows.Forms.ListBox();
            this.labelFolderFile = new System.Windows.Forms.Label();
            this.labelIncludedFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(161, 245);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(73, 31);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // listBoxFolderFile
            // 
            this.listBoxFolderFile.FormattingEnabled = true;
            this.listBoxFolderFile.ItemHeight = 12;
            this.listBoxFolderFile.Location = new System.Drawing.Point(12, 25);
            this.listBoxFolderFile.Name = "listBoxFolderFile";
            this.listBoxFolderFile.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFolderFile.Size = new System.Drawing.Size(207, 208);
            this.listBoxFolderFile.Sorted = true;
            this.listBoxFolderFile.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(225, 43);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(73, 31);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = ">";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonAddAll
            // 
            this.buttonAddAll.Location = new System.Drawing.Point(225, 90);
            this.buttonAddAll.Name = "buttonAddAll";
            this.buttonAddAll.Size = new System.Drawing.Size(73, 31);
            this.buttonAddAll.TabIndex = 0;
            this.buttonAddAll.Text = ">>";
            this.buttonAddAll.UseVisualStyleBackColor = true;
            this.buttonAddAll.Click += new System.EventHandler(this.buttonAddAll_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(225, 139);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(73, 31);
            this.buttonDelete.TabIndex = 0;
            this.buttonDelete.Text = "<";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonDeleteAll
            // 
            this.buttonDeleteAll.Location = new System.Drawing.Point(225, 186);
            this.buttonDeleteAll.Name = "buttonDeleteAll";
            this.buttonDeleteAll.Size = new System.Drawing.Size(73, 31);
            this.buttonDeleteAll.TabIndex = 0;
            this.buttonDeleteAll.Text = "<<";
            this.buttonDeleteAll.UseVisualStyleBackColor = true;
            this.buttonDeleteAll.Click += new System.EventHandler(this.buttonDeleteAll_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(288, 245);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(73, 31);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // listBoxIncludedFile
            // 
            this.listBoxIncludedFile.FormattingEnabled = true;
            this.listBoxIncludedFile.ItemHeight = 12;
            this.listBoxIncludedFile.Location = new System.Drawing.Point(304, 25);
            this.listBoxIncludedFile.Name = "listBoxIncludedFile";
            this.listBoxIncludedFile.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxIncludedFile.Size = new System.Drawing.Size(207, 208);
            this.listBoxIncludedFile.Sorted = true;
            this.listBoxIncludedFile.TabIndex = 2;
            // 
            // labelFolderFile
            // 
            this.labelFolderFile.AutoSize = true;
            this.labelFolderFile.Location = new System.Drawing.Point(15, 9);
            this.labelFolderFile.Name = "labelFolderFile";
            this.labelFolderFile.Size = new System.Drawing.Size(143, 12);
            this.labelFolderFile.TabIndex = 3;
            this.labelFolderFile.Text = "File in the same folder";
            // 
            // labelIncludedFile
            // 
            this.labelIncludedFile.AutoSize = true;
            this.labelIncludedFile.Location = new System.Drawing.Point(302, 9);
            this.labelIncludedFile.Name = "labelIncludedFile";
            this.labelIncludedFile.Size = new System.Drawing.Size(83, 12);
            this.labelIncludedFile.TabIndex = 4;
            this.labelIncludedFile.Text = "Included File";
            // 
            // ManageImageFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 285);
            this.Controls.Add(this.labelIncludedFile);
            this.Controls.Add(this.labelFolderFile);
            this.Controls.Add(this.listBoxIncludedFile);
            this.Controls.Add(this.listBoxFolderFile);
            this.Controls.Add(this.buttonDeleteAll);
            this.Controls.Add(this.buttonAddAll);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ManageImageFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Images";
            this.Load += new System.EventHandler(this.ManageImageFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ListBox listBoxFolderFile;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonAddAll;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonDeleteAll;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBoxIncludedFile;
        private System.Windows.Forms.Label labelFolderFile;
        private System.Windows.Forms.Label labelIncludedFile;
    }
}