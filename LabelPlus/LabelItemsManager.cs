#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
#endregion

namespace LabelPlus
{
    public class LabelItemsManager
    {
        internal enum stateEnum
        {
            none,   //无区块状态
            file,   //文件区块
            label,  //标签区块
        }
        internal enum strlineType
        {
            normal,     //普通文本行
            fileHead,   //文件文本行
            labelHead,  //标签文本行
            tip,        //注释文本行
        }
        internal struct getStrlineTypeResult
        {
            public strlineType type;
            public string[] value;
        }

        #region Fields

        Dictionary<string, List<LabelItem>> store = new Dictionary<string, List<LabelItem>>();

        #endregion

        #region Events

        static public EventHandler FileListChanged;
        static public EventHandler LabelItemListChanged;
        static public EventHandler LabelItemTextChanged;

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

        #endregion

        #region Methods

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
        public bool DelAllFiles()
        {
            try
            {
                store.Clear();
                OnFileListChanged();
                OnLabelItemListChanged();
                return true;
            }
            catch { return false; }
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

        public bool FromFile(string path)
        {
            store = new Dictionary<string, List<LabelItem>>();
            stateEnum state = stateEnum.none;
            string nowFilename = "";
            string nowText = "";
            string[] nowLabelResultValues = { };
            getStrlineTypeResult result = new getStrlineTypeResult();

            StreamReader sr = new StreamReader(path, Encoding.Unicode);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                result = getStrlineType(str);
                try
                {
                    switch (state)
                    {
                        case stateEnum.none:
                            if (result.type == strlineType.fileHead)
                            {
                                state = stateEnum.file;
                                nowFilename = result.value[0];
                                //创建新文件项
                                addFilenameToStore(nowFilename);
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
                                if (!addFilenameToStore(nowFilename)) state = stateEnum.none;
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
                                    if (!addFilenameToStore(nowFilename)) state = stateEnum.none;
                                    break;
                                case strlineType.tip:
                                    nowText += "\r\n" + @"//" + result.value[0];

                                    break;
                            }
                            break;

                    }
                }
                catch { return false; }
            }

            if (state == stateEnum.label)
            {
                addLabelToStore(nowText, nowLabelResultValues, nowFilename);
            }

            OnFileListChanged();
            OnLabelItemListChanged();
            return true;
        }
        public bool ToFile(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sr = new StreamWriter(fs, Encoding.Unicode);

                var filenames = store.Keys;

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
            if (str.StartsWith(@"//"))
            {
                tmp.type = strlineType.tip;
                tmp.value = new string[1];
                tmp.value[0] = str.Substring(2);   //返回余下文本
            }
            else if (str.StartsWith(">>>>>>>>[") && str.IndexOf("]<<<<<<<<") > 9)
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
