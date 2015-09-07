
#region Using Directives
using System;
using System.Collections.Generic;
using System.Windows.Forms;
#endregion

namespace LabelPlus
{
    public class ListViewAdpter
    {
        #region Fields

        ListView lv;
        bool lvSelectedIndexChangedEnable = true;   //SelectedIndexChanged Event Switch

        #endregion

        #region Events
        public EventHandler ListViewSelectedIndexChanged;
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
                setLvSelectItem(value);
            }
        }
        #endregion

        #region Methods

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
                        //edit the Text
                        lv.Items[number - 1].SubItems[1].Text = n.Text;
                    }
                    else
                    {
                        //Add item
                        lv.Items.Add(number.ToString());
                        lv.Items[number - 1].SubItems.Add(n.Text);
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
            lv.Columns[1].Width = lv.ClientSize.Width - lv.Columns[0].Width - 10;
        }

        #endregion

        #region Constructors

        public ListViewAdpter(ListView listview)
        {
            lv = listview;

            lv.Columns.Clear();
            lv.Columns.Add("No.", 30);
            lv.Columns.Add("文本", 20);
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.HideSelection = false;

            lv.MultiSelect = false;
            lv.Scrollable = true;
            lv.HeaderStyle = ColumnHeaderStyle.None;

            lv.ClientSizeChanged += new EventHandler(lvClientSizeChanged);
            lv.SelectedIndexChanged += new EventHandler(lvSelectedIndexChanged);
            lv.FontChanged += new EventHandler(lvFontChanged);
            lvClientSizeChanged(this, new EventArgs());
        }

        #endregion
    }
}
