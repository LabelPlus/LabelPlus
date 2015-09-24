
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

        #endregion

        #region Events
        public EventHandler ListViewSelectedIndexChanged;

        public class UserSetCategoryEventArgs : EventArgs{
            int[] index;
            int category; 

            public UserSetCategoryEventArgs(int[] index, int category)
            {
                this.index = index;
                this.category = category;
            }

            public int[] Index { get{return index;} }
            public int Category { get { return category; } } 
        }
        public delegate void UserActionEventHandler(object sender, UserSetCategoryEventArgs e);
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
        #endregion

        #region Methods

        string getCategoryName(int index) {
            return group.GetFullViewName(index);
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

                return true;
            }
            catch
            {
                lvSelectedIndexChangedEnable = true;
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
            if (lvSelectedIndexChangedEnable && ListViewSelectedIndexChanged != null) ListViewSelectedIndexChanged(sender, e);
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
                List<int> tmp = new List<int>();
                foreach (int item in lv.SelectedIndices) {
                    tmp.Add(item);
                }

                UserSetCategory(sender,
                    new UserSetCategoryEventArgs(tmp.ToArray(), e.KeyCode - Keys.D1 + 1));
            }
            e.SuppressKeyPress = true;
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
            lvClientSizeChanged(this, new EventArgs());

            this.group = groupDefine;
        }

        #endregion
    }
}
