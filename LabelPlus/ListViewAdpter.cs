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
using System.Windows.Forms;
using System.Drawing;
#endregion

namespace LabelPlus
{
    public class ListViewAdpter
    {
        #region Fields

        ListView lv;
        bool lvSelectedIndexChangedEnable = true;   //SelectedIndexChanged Event Switch
        GroupDefineItemCollection group;
        ContextMenuStrip myMenuStrip;

        #endregion

        #region Events
        public EventHandler ListViewSelectedIndexChanged;
        internal void OnListViewSelectedIndexChanged() {
            if (lvSelectedIndexChangedEnable && ListViewSelectedIndexChanged != null) 
                ListViewSelectedIndexChanged(this, new EventArgs());
        }

        public class UserActionEventArgs : EventArgs{
            int[] index;
            int value; 
            ActionType actionType;

            public enum ActionType { 
                setGroup,
                del,
            }

            public UserActionEventArgs(int[] index, ActionType type, int value = -1) 
            {
                this.index = index;
                this.value = value;
                this.actionType = type;
            }

            public int[] Index { get{return index;} }
            public int Value { get { return value; } }
            public ActionType Action { get { return actionType; } }
        }
        public delegate void UserActionEventHandler(object sender, UserActionEventArgs e);
        public UserActionEventHandler UserSetCategory;

        #endregion
 
        #region Properties

        public int Count { get { return lv.Items.Count; } }

        public int SelectedIndex
        {
            get
            {
                if (lv.SelectedIndices.Count != 0)
                    return lv.SelectedIndices[0];
                else
                    return -1;
            }
            set
            {
                setLvSelectItem(-1);
                setLvSelectItem(value);
            }
        }

        public int SelectedIndexCount { get { return lv.SelectedItems.Count; } }

        public ListView ListView { get { return lv; } }
        #endregion

        #region Methods

        string getCategoryName(int index) {
            return group.GetFullViewName(index);
        }

        internal void onDelItems() {
            List<int> tmp = new List<int>();
            foreach (int item in lv.SelectedIndices)
            {
                tmp.Add(item);
            }
            if (tmp.Count != 0)
                UserSetCategory(this,
                    new UserActionEventArgs(tmp.ToArray(), UserActionEventArgs.ActionType.del));
        }

        internal void onSetCategory(int category)
        {
            List<int> tmp = new List<int>();
            foreach (int item in lv.SelectedIndices)
            {
                tmp.Add(item);
            }
            if (tmp.Count != 0)
                UserSetCategory(this,
                    new UserActionEventArgs(tmp.ToArray(), UserActionEventArgs.ActionType.setGroup, category));
        }
        public bool ReloadItems(List<LabelItem> items)
        {
            try
            {
                //record selected item
                int index;
                if (lv.SelectedIndices.Count != 0)
                {
                    index = lv.SelectedIndices[0];
                }
                else 
                { 
                    index = -1; 
                }

                if (items == null)
                {
                    lv.Items.Clear();
                    return true;
                }

                int number = 1;
                foreach (LabelItem n in items)
                {
                    if (lv.Items.Count >= number)
                    {
                        //edit the category                        
                        lv.Items[number - 1].SubItems[2].Text = getCategoryName(n.Category);

                        lv.Items[number - 1].SubItems[2].ForeColor =
                            group.GetColor(n.Category); 
                    
                        //edit the Text                        
                        lv.Items[number - 1].SubItems[1].Text = n.Text;
                        //lv.Items[number - 1].SubItems[1].ForeColor = GlobalVar.CategoryColor[n.Category];                        
                    }
                    else
                    {
                        //Add item
                        lv.Items.Add(number.ToString());
                        lv.Items[number - 1].UseItemStyleForSubItems = false;
                        lv.Items[number - 1].SubItems.Add(n.Text);
                        lv.Items[number - 1].SubItems.Add(getCategoryName(n.Category),
                            group.GetColor(n.Category), 
                            lv.BackColor, 
                            lv.Font);                        
                    }
                    number++;
                }

                //delete
                number -= 1;
                if (lv.Items.Count > number)
                {
                    for (int i = lv.Items.Count - 1; i > number - 1; i--)
                    {
                        lv.Items.RemoveAt(i);
                    }
                }

                //select
                if (index != -1 && lv.Items.Count >= index + 1)
                {
                    lvSelectedIndexChangedEnable = false;
                    lv.Items[index].Selected = true;
                    lvSelectedIndexChangedEnable = true;
                }

                //OnListViewSelectedIndexChanged();
                return true;
            }
            catch
            {
                lvSelectedIndexChangedEnable = true;
                //OnListViewSelectedIndexChanged();
                return false;
            }
        }

        private bool setLvSelectItem(int index)
        {
            try
            {
                if (index == -1)
                    lv.SelectedIndices.Clear();
                else
                    lv.Items[index].Selected = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void lvFontChanged(object sender, EventArgs e)
        {
            lvClientSizeChanged(null, null);
        }
        private void lvSelectedIndexChanged(object sender, EventArgs e)
        {
            OnListViewSelectedIndexChanged();
        }
        private void lvClientSizeChanged(object sender, EventArgs e)
        {
            lv.Columns[0].Width =(int)lv.Font.SizeInPoints * 3;
            lv.Columns[2].Width =(int)lv.Font.SizeInPoints * 10;
            lv.Columns[1].Width = lv.ClientSize.Width - lv.Columns[0].Width - lv.Columns[2].Width - 10;
        }

        private void lvKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                onSetCategory(e.KeyCode - Keys.D1 + 1);
            }
            else if (e.KeyCode == Keys.Delete) {
                onDelItems();
            }
            e.SuppressKeyPress = true;
        }

        private void lvMosuseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                rebuildMenuStrip();
                myMenuStrip.Show(Control.MousePosition);
            }
        }

        private void rebuildMenuStrip() {
            myMenuStrip.Items.Clear();            
            foreach (string i in group.GetUserGroupNameArray())
            {
                myMenuStrip.Items.Add(i);
            }

            myMenuStrip.Items.Add(StringResources.GetValue("listview_menustrip_del"));

        }

        private void myMenuStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            myMenuStrip.Close();

            int index = myMenuStrip.Items.IndexOf(e.ClickedItem);
            if (index == myMenuStrip.Items.Count - 1) 
            {
                //del
                onDelItems();
            }
            else 
            { 
                //set group
                onSetCategory(index+1);
            }
        }

        #endregion

        #region Constructors

        public ListViewAdpter(ListView listview, GroupDefineItemCollection groupDefine)
        {
            lv = listview;

            lv.Columns.Clear();
            lv.Columns.Add("No.");            
            lv.Columns.Add("Text");
            lv.Columns.Add("Category");
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.HideSelection = false;

            lv.MultiSelect = true;
            lv.Scrollable = true;
            lv.HeaderStyle = ColumnHeaderStyle.None;

            lv.ClientSizeChanged += new EventHandler(lvClientSizeChanged);
            lv.SelectedIndexChanged += new EventHandler(lvSelectedIndexChanged);
            lv.FontChanged += new EventHandler(lvFontChanged);
            lv.KeyDown += new KeyEventHandler(lvKeyDown);
            lv.MouseClick += new MouseEventHandler(lvMosuseClick);
            lvClientSizeChanged(this, new EventArgs());

            myMenuStrip = new ContextMenuStrip();
            myMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(myMenuStripItemClicked);

            this.group = groupDefine;
        }


        #endregion
    }
}
