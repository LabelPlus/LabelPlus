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
using System.Linq;
using System.Text;
using System.IO;
#endregion

namespace LabelPlus
{
    public class LabelFileManager
    {
        #region Const
        // 主次版本号
        // 主版本迭代一次 说明旧版本对新版本不兼容 可能无法正常读取 或导致丢失信息
        // 次版本迭代一次 说明文件结构有变 但旧版本可以读取不支持某些特性 不会导致信息丢失
        const int MY_FILE_VER_FIRST = 1;
        const int MY_FILE_VER_LAST = 0;

        // 文件头成员数
        const int FILEHEAD_LENGHT = 2;

        //
        static readonly string[] FILEHEAD_DEFAULT = { 
            MY_FILE_VER_FIRST.ToString(), 
            MY_FILE_VER_LAST.ToString() };

        #endregion

        internal enum stateEnum
        {
            start,   //文件头部区块
            file,   //文件区块
            label,  //标签区块
        }
        internal enum strlineType
        {
            normal,     //普通文本行
            fileHead,   //文件文本行
            labelHead,  //标签文本行            
        }
        internal struct getStrlineTypeResult
        {
            public strlineType type;
            public string[] value;
        }

        #region Fields

        string[] fileHead;   //文件头 0:主版本号 1:次版本号
        List<string> groupStringList; //分组名称定义
        string comment; //用户注释

        //标签信息
        Dictionary<string, List<LabelItem>> store;        
        
        #endregion

        #region Constructors
        public LabelFileManager(){
            fileHead = FILEHEAD_DEFAULT;
            groupStringList = new List<string>();
            comment = GlobalVar.DefaultComment;
            store = new Dictionary<string, List<LabelItem>>();

        }
        #endregion

        #region Events

        static public EventHandler FileListChanged;
        static public EventHandler LabelItemListChanged;
        static public EventHandler LabelItemTextChanged;
        static public EventHandler GroupListChanged;

        internal void OnFileListChanged()
        {
            if (FileListChanged != null) FileListChanged(this, new EventArgs());
        }
        internal void OnLabelItemListChanged()
        {
            if (LabelItemListChanged != null) LabelItemListChanged(this, new EventArgs());
        }
        internal void OnLabelItemTextChanged()
        {
            if (LabelItemTextChanged != null) LabelItemTextChanged(this, new EventArgs());
        }
        internal void OnGroupListChanged() 
        {
            if (GroupListChanged != null) GroupListChanged(this, new EventArgs());
        }
        #endregion

        #region Properties

        public string[] Filenames { get { return store.Keys.ToArray(); } }

        public List<LabelItem> this[string file]
        {
            get
            {
                return store[file];
            }
        }
        public LabelItem this[string file, int index]
        {
            get
            {
                try
                {
                    return store[file][index];
                }
                catch { return null; }
            }
        }

        public List<string> GroupList { 
            get { return groupStringList; }
            set {
                groupStringList = value;
                OnGroupListChanged();
            }
        }
        public string Comment { 
            get { return comment; }
            set {
                comment = value;
            }
        }

        #endregion

        #region Methods

        //
        // 从文本中 获取首部区块
        //
        private void readLabelFileStartBlocks(string nowText)
        {
            //老版本 无start blocks 特殊处理
            if (nowText.Trim() == "") { 

                //block1
                for (int i = 0; i < FILEHEAD_LENGHT; i++)
                {
                    fileHead[i] = FILEHEAD_DEFAULT[i];      //默认值
                }

                //block2
                groupStringList = new List<string>();
                for (int i = 0; i < GlobalVar.DefaultGroupDefineItems.Length; i++)
                {
                    if (GlobalVar.DefaultGroupDefineItems[i].Name != "")
                    {
                        groupStringList.Add(GlobalVar.DefaultGroupDefineItems[i].Name);
                    }
                    else
                    {
                        break;
                    }
                }
                
                //block end
                comment = GlobalVar.DefaultComment;

                return;
            }

            string[] textBlocks = nowText.Trim().Split('-');
            if (textBlocks.Length < 3)
                throw new Exception(StringResources.GetValue("error_file_startblocks_lost"));

            string[] tmp;
            //区块1 文件头
            tmp = textBlocks[0].Split(',');
            for (int i = 0; i < FILEHEAD_LENGHT; i++) {
                if(i<tmp.Length)
                    fileHead[i] = tmp[i].Trim();               //实际值
                else
                    fileHead[i] = FILEHEAD_DEFAULT[i];      //默认值
            }
            
            //检查版本信息
            if (Convert.ToInt16(fileHead[0]) > MY_FILE_VER_FIRST)
                throw new Exception(StringResources.GetValue("error_file_version_over"));

            //区块2 分组信息
            tmp = textBlocks[1].Trim().Split('\r');
            foreach (string str in tmp) {
                string t = str.Trim();
                if(t!="")
                    groupStringList.Add(t);
            }

            //最后区块 用户注释
            comment = textBlocks[textBlocks.Length - 1].Trim();
        }

        //
        // 从文本中 获取首部区块
        //
        private string getLabelFileStartBlocksString()
        {
            string result = "";

            //区块1 文件头
            foreach (string str in fileHead) {
                result += str + ",";
            }
            result = result.Substring(0, result.Length - 1);    //去掉最后一个逗号
            result += "\r\n-\r\n";

            //区块2 分组信息
            foreach (string str in groupStringList)
            {
                result += str + "\r\n";
            }
            result += "-\r\n";

            //最后区块 用户备注
            result += comment + "\r\n";

            return result;
        }

        internal bool addFile(string file) {
            try
            {
                store.Add(file, new List<LabelItem>());
                return true;
            }
            catch { return false; }
        }
        internal bool addLabelItem(string file, LabelItem item, int insertIndex = -1) 
        {
            try
            {
                if (insertIndex == -1)
                    store[file].Add(item);
                else
                    store[file].Insert(insertIndex, item);

                return true;
            }
            catch { return false; }
        }

        public bool AddFile(string file)
        {
            try
            {
                addFile(file);
                //以file升序排序
                store = store.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);

                OnFileListChanged();
                return true;
            }
            catch { return false; }
        }
        public bool AddLabelItem(string file, LabelItem item, int insertIndex = -1)
        {
            try
            {
                if (addLabelItem(file, item, insertIndex))
                {
                    OnLabelItemListChanged();
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
        public bool UpdateLabelItemText(string file, int index, string text)
        {
            try
            {
                store[file][index].Text = text;
                OnLabelItemTextChanged();
                return true;
            }
            catch { return false; }
        }

        public bool UpdateLabelCategory(string file, int index, int category)
        {
            try
            {
                store[file][index].Category = category;
                //OnLabelItemTextChanged();
                OnLabelItemListChanged();
                return true;
            }
            catch { return false; }
        }

        public bool DelFile(string file)
        {
            try
            {
                store.Remove(file);
                OnFileListChanged();
                return true;
            }
            catch { return false; }
        }
        //public bool DelAllFiles()
        //{
        //    try
        //    {
        //        store.Clear();
        //        OnFileListChanged();
        //        OnLabelItemListChanged();
        //        return true;
        //    }
        //    catch { return false; }
        //}

        public bool NewLabelFile(string[] groups) 
        {
            fileHead = FILEHEAD_DEFAULT;
            comment = GlobalVar.DefaultComment;
            groupStringList = groups.ToList();
            store.Clear();

            OnFileListChanged();
            OnLabelItemListChanged();
            OnGroupListChanged();
            return true;
        }

        public bool DelLabelItem(string file, int index)
        {
            try
            {
                store[file].RemoveAt(index);
                OnLabelItemListChanged();
                return true;
            }
            catch { return false; }
        }
        public bool DelAllLabelInFile(string file)
        {
            try
            {
                store[file] = new List<LabelItem>();
                OnLabelItemListChanged();
                return true;
            }
            catch { return false; }
        }

        public void FromFile(string path)
        {
            //错误信息
            int error_lineNum = 0;
            string error_state = "";

            try
            {
                store = new Dictionary<string, List<LabelItem>>();
                groupStringList = new List<string>();

                stateEnum state = stateEnum.start;
                string nowFilename = "";
                string nowText = "";
                string[] nowLabelResultValues = { };
                getStrlineTypeResult result = new getStrlineTypeResult();

                StreamReader sr = new StreamReader(path, Encoding.UTF8, true);
                while (!sr.EndOfStream)
                {
                    string str = sr.ReadLine();
                    error_lineNum++;
                    error_state = "imageFile=" + nowFilename + ", nowState=" + state.ToString();
                    result = getStrlineType(str);

                    switch (state)
                    {
                        case stateEnum.start:
                            if (result.type == strlineType.fileHead)
                            {
                                //处理Label文件的文件头
                                readLabelFileStartBlocks(nowText);

                                state = stateEnum.file;
                                nowFilename = result.value[0];

                                //创建新文件项
                                addFilenameToStore(nowFilename);
                            }
                            else if (result.type == strlineType.normal)
                            {
                                nowText += "\r\n" + result.value[0];
                            }
                            break;
                        case stateEnum.file:
                            if (result.type == strlineType.labelHead)
                            {
                                state = stateEnum.label;
                                nowText = "";
                                nowLabelResultValues = result.value;

                            }
                            else if (result.type == strlineType.fileHead)
                            {
                                state = stateEnum.file;
                                nowFilename = result.value[0];
                                //创建新文件项
                                if (!addFilenameToStore(nowFilename)) state = stateEnum.start;
                            }
                            break;
                        case stateEnum.label:
                            switch (result.type)
                            {
                                case strlineType.normal:
                                    if (nowText == "") nowText = result.value[0];
                                    else nowText += "\r\n" + result.value[0];
                                    break;
                                case strlineType.labelHead:
                                    //保存之前的内容
                                    addLabelToStore(nowText, nowLabelResultValues, nowFilename);
                                    nowText = "";
                                    nowLabelResultValues = result.value;
                                    break;
                                case strlineType.fileHead:
                                    //保存之前的内容
                                    addLabelToStore(nowText, nowLabelResultValues, nowFilename);

                                    state = stateEnum.file;
                                    nowFilename = result.value[0];
                                    if (!addFilenameToStore(nowFilename)) state = stateEnum.start;
                                    break;
                            }
                            break;

                    }   //switch (state)
                }   //while (!sr.EndOfStream)

                if (state == stateEnum.label)
                {
                    addLabelToStore(nowText, nowLabelResultValues, nowFilename);
                }

                if (state == stateEnum.start)
                {
                    //处理Label文件的文件头
                    readLabelFileStartBlocks(nowText);

                    state = stateEnum.file;
                    nowFilename = result.value[0];
                }

                OnFileListChanged();
                OnLabelItemListChanged();
                OnGroupListChanged();
            }
            catch(Exception e) 
            {
                throw new Exception("ReadFromFileError in line" + error_lineNum.ToString()
                    + "\r\n" + error_state
                    + "\r\n\r\n" + e.ToString());
            }
        }

        public bool ToFile(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sr = new StreamWriter(fs, Encoding.UTF8);

                var filenames = store.Keys;

                sr.WriteLine(getLabelFileStartBlocksString());

                foreach (var name in filenames)
                {
                    int count = 0;
                    List<LabelItem> items = store[name];

                    sr.WriteLine();
                    sr.WriteLine(">>>>>>>>[" + name + "]<<<<<<<<");
                    foreach (var n in items)
                    {
                        count++;
                        sr.WriteLine("----------------[" + count.ToString() +
                            "]----------------[" + 
                            n.X_percent.ToString("F3") + "," + 
                            n.Y_percent.ToString("F3") + "," +
                            n.Category.ToString() + 
                            "]");
                        sr.WriteLine(n.Text);
                        sr.WriteLine();
                    }
                }
                sr.Close();
                fs.Close();

                return true;
            }
            catch { return false; }

        }
        internal getStrlineTypeResult getStrlineType(string str)
        {
            str = str.Trim();

            getStrlineTypeResult tmp = new getStrlineTypeResult();
            if (str.StartsWith(">>>>>>>>[") && str.IndexOf("]<<<<<<<<") > 9)
            {
                tmp.type = strlineType.fileHead;
                tmp.value = new string[1];
                tmp.value[0] = str.Substring(9, str.IndexOf("]<<<<<<<<") - 9);
            }
            else if (str.StartsWith("----------------[") && str.IndexOf("]----------------") > 17)
            {
                tmp.type = strlineType.labelHead;
                List<string> tmpList = new List<string>();
                tmpList.Add(str.Substring(17, str.IndexOf("]----------------") - 17));
                string rightText = str.Substring(str.IndexOf("]----------------") + 17);
                if (rightText.StartsWith("[") && rightText.EndsWith("]"))
                {
                    rightText = rightText.Substring(1, rightText.Length - 2);
                    string[] splitText = rightText.Split(',');
                    foreach (string s in splitText) { tmpList.Add(s); }
                }
                else
                {
                    tmp.value = tmpList.ToArray();
                    goto TheEnd;
                }
                tmp.value = tmpList.ToArray();
            }
            else
            {
                tmp.type = strlineType.normal;
                tmp.value = new string[1];
                tmp.value[0] = str;
            }

        TheEnd:
            return tmp;
        }

        internal void addLabelToStore(string nowText, 
                                        string[] nowLabelResultValues, 
                                        string nowFilename)
        {
            int category;

            //nowLabelResultValues的元素个数 判断是否存在
            if (nowLabelResultValues.Length == 3) 
            {
                category = 1;
            }
            else if(nowLabelResultValues.Length == 4)
            {
                category = Convert.ToInt16(nowLabelResultValues[3]);
            }
            else 
            {
                return;     //解析失败
            }

            LabelItem labelItem = new LabelItem(
                            Convert.ToSingle(nowLabelResultValues[1]),
                            Convert.ToSingle(nowLabelResultValues[2]),
                            nowText.Trim(),
                            category);
            addLabelItem(nowFilename, labelItem);
        }

        internal bool addFilenameToStore(string nowFilename)
        {
            return addFile(nowFilename);
        }
        #endregion

    }
}
