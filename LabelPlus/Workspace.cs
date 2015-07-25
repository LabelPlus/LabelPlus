#region Using Directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Linq;
#endregion 

namespace LabelPlus
{ 
    public class Workspace
    {

        #region Fields
        LabelItemsManager store;

        bool alter = false;
        string path = "";
        #endregion

        #region Properties

        public Boolean Alter { get { return alter; } }
        public Boolean NeedSave { get { return Alter; } }

        public Boolean HavePath { get { return (path!=""); } }
        public string Path { set { path = value; } get { return path; } }
        public string DirPath { get { return (new FileInfo(path)).DirectoryName; } }

        public LabelItemsManager Store { get{ return store;} }

        #endregion

        #region Methods
        internal Boolean writeFileFromWorkspace(string path)
        {
            return store.ToFile(path);
        }

        public void readWorkspaceFromFile(string path) {
            this.path = path;
            store.FromFile(path);
            alter = false;
        } 
        public void NewFile()
        {
            path = "";
            store.DelAllFiles();
            alter = false;
        }
        public Boolean Save() {
            if (HavePath){
                bool tmp = writeFileFromWorkspace(path);
                if(tmp) alter = false;
                return tmp;
            }                
            else 
                return false;
        }
        public Boolean SaveBAK()
        {
            if (HavePath)
            {
                bool tmp = writeFileFromWorkspace(path+".bak");
                return tmp;
            }
            else
                return false;
        }

        private void storeChanged(object sender, EventArgs e)
        {
            alter = true;
        }
        #endregion

        #region Constructors
        public Workspace()
        {
            store = new LabelItemsManager();

            LabelItemsManager.FileListChanged += new EventHandler(storeChanged);
            LabelItemsManager.LabelItemListChanged += new EventHandler(storeChanged);
            LabelItemsManager.LabelItemTextChanged += new EventHandler(storeChanged);

            NewFile();
        }
        #endregion

    }
}
