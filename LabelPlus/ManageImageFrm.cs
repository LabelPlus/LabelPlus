/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace LabelPlus
{
    public partial class ManageImageFrm : Form
    {
        
        Workspace wsp;

        public ManageImageFrm(Workspace wsp)
        {
            this.wsp = wsp;
            InitializeComponent();
            Language.InitFormLanguage(this, StringResources.GetValue("lang"));
            
        }

        private void ManageImageFrm_Load(object sender, EventArgs e)
        {
            addFolderFilesToList();
            addIncludedFilesToList();
            
        }

        private void addIncludedFilesToList()
        {
            var includedFiles = wsp.Store.Filenames;
            foreach (var item in listBoxFolderFile.Items) {
                if (includedFiles.Contains(item.ToString()))
                {
                    listBoxIncludedFile.Items.Add(item);                    
                }
            }

            foreach (var item in listBoxIncludedFile.Items)
            {
                listBoxFolderFile.Items.Remove(item);
            }
            
        }

        private void addFolderFilesToList()
        {
            string[] extension_list = new string[] { ".jpg", ".jpeg", ".png", ".tif", ".tiff", ".bmp" };
            string[] filenames = Directory.GetFiles(wsp.DirPath);

            foreach (string filename in filenames)
            {
                var tmp = new FileInfo(filename);

                foreach (string extension in extension_list)
                {
                    if (tmp.Name.EndsWith(extension))
                    {
                        listBoxFolderFile.Items.Add(tmp.Name);
                    } 
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        { 
            var items = listBoxFolderFile.SelectedItems;
            if (items.Count == 0) return;
            foreach (var item in items)
            {
                listBoxIncludedFile.Items.Add(item);
            }

            foreach (var item in listBoxIncludedFile.Items)
            { 
                if (listBoxFolderFile.Items.Contains(item))
                    listBoxFolderFile.Items.Remove(item);
            }
        }

        private void buttonAddAll_Click(object sender, EventArgs e)
        {            
            foreach (var item in listBoxFolderFile.Items) {
                listBoxIncludedFile.Items.Add(item);
            }
            listBoxFolderFile.Items.Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var items = listBoxIncludedFile.SelectedItems;
            if (items.Count == 0) return;
            foreach (var item in items) {
                listBoxFolderFile.Items.Add(item);
            }

            foreach (var item in listBoxFolderFile.Items)
            {
                if(listBoxIncludedFile.Items.Contains(item))
                    listBoxIncludedFile.Items.Remove(item);
            }
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            foreach (var item in listBoxIncludedFile.Items)
            {
                listBoxFolderFile.Items.Add(item);
            }
            listBoxIncludedFile.Items.Clear();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var wspIncludedFiles = wsp.Store.Filenames;
            var store = wsp.Store;
            int newIncludedFilesNum = listBoxIncludedFile.Items.Count;
            int newNotIncludedFilesNum = listBoxFolderFile.Items.Count;
            string[] newIncludedFiles = new string[newIncludedFilesNum];
            string[] newNotIncludedFiles = new string[newNotIncludedFilesNum];

            for (int i = 0; i < newIncludedFilesNum; i++)
            {
                newIncludedFiles[i] = listBoxIncludedFile.Items[i].ToString();
            }

            for (int i = 0; i < newNotIncludedFilesNum; i++)
            {
                newNotIncludedFiles[i] = listBoxFolderFile.Items[i].ToString();
            }
            //添加原本不存在的文件
            foreach (string fileName in newIncludedFiles) {
                if (!wspIncludedFiles.Contains(fileName)) {
                    store.AddFile(fileName);                    
                }
            }

            //删除原本存在的文件
            foreach (string fileName in newNotIncludedFiles)
            {
                if (wspIncludedFiles.Contains(fileName))
                {
                    store.DelFile(fileName);                    
                }
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
