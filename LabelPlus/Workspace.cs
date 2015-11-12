/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

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
        LabelFileManager store;
        GroupDefineItemCollection groupDefine = new GroupDefineItemCollection(GlobalVar.DefaultGroupDefineItems);

        bool alter = false;
        bool alterNeedBak = false; 
        string path = "";
        

        #endregion

        public EventHandler UserGroupDefineChanged;
        internal void OnUserGroupDefineChanged()
        {
            if (UserGroupDefineChanged != null) UserGroupDefineChanged(this, new EventArgs());
        }

        #region Properties

        public Boolean Alter { get { return alter; } }
        public Boolean NeedSave { get { return Alter; } }
        public Boolean HavePath { get { return (path!=""); } }
        public string Path { set { path = value; } get { return path; } }
        public string DirPath { get { return (new FileInfo(path)).DirectoryName; } }
        public string Filename { get { return (new FileInfo(path)).Name; } }

        public LabelFileManager Store { get{ return store;} }
        public GroupDefineItemCollection GroupDefine { get { return groupDefine; } }
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
            store.NewLabelFile(groupDefine.GetDefaultGroupNameArray());
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
            try
            {
                if (HavePath && alterNeedBak)
                {
                    DateTime time = DateTime.Now;
                    string timeStr = time.ToString("yyMMdd_HHmmss_");
                    DirectoryInfo dirInfo = new DirectoryInfo(DirPath + "\\bak\\");
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }
                    bool tmp = writeFileFromWorkspace(dirInfo.FullName + "\\" + timeStr + Filename);
                    if (tmp)
                        alterNeedBak = false;
                    return tmp;
                }
                else
                    return false;
            }
            catch { return false; }
        }

        private void storeChanged(object sender, EventArgs e)
        {
            alter = true;
            alterNeedBak = true;
        }

        private void groupListChanged(object sender, EventArgs e)
        {
            groupDefine.ClearUserGroup();
            groupDefine.LoadUserGroup(store.GroupList);

            OnUserGroupDefineChanged();
        }
        #endregion

        #region Constructors
        public Workspace()
        {
            store = new LabelFileManager();

            LabelFileManager.FileListChanged += new EventHandler(storeChanged);
            LabelFileManager.LabelItemListChanged += new EventHandler(storeChanged);
            LabelFileManager.LabelItemTextChanged += new EventHandler(storeChanged);
            LabelFileManager.GroupListChanged += new EventHandler(storeChanged);
            LabelFileManager.GroupListChanged += new EventHandler(groupListChanged);
            NewFile();
        }

        #endregion

    }
}
