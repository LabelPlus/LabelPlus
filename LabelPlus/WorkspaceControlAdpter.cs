#region Using Directives

using System;
using System.Linq;
using System.Windows.Forms;

#endregion

namespace LabelPlus
{
    public class WorkspaceControlAdpter
    {

        #region Fields

        ToolStripButton editlabelbutton; 
        ToolStripComboBox combo;
        TextBox textbox;
        ListViewAdpter listviewapt;
        PicView picview;
        Workspace wsp;
        GroupBox textboxgroupbox;
        ToolStripButton categorybutton1;
        ToolStripButton categorybutton2;
        ToolStripButton categorybutton3;
        ToolStripButton categorybutton4;
        ContextMenuStrip menuquicktext;

        bool editLabelMode = false;
        int itemIndex = -1;
        string fileName = "";
        int newLabelCcategory = 1;

        #endregion

        #region Properties
        public string FileName { get { return fileName; } }
        public int ItemIndex { get { return itemIndex; } }
        //public int NewLabelCcategory { 
        //    get { return newLabelCcategory; }
        //    set { newLabelCcategory = value; } 
        //}
        #endregion

        #region Methods
        public void page_left()
        {
            try
            {
                if (combo.SelectedIndex != 0)
                    combo.SelectedIndex--;
            }
            catch { }
        }
        public void page_right()
        {
            try
            {
                if (combo.SelectedIndex !=
                    combo.Items.Count - 1)
                    combo.SelectedIndex++;
            }
            catch { }
        }
        public void NewFile()
        {
            fileName = "";
            picview.Image = null;
            picview.LoadImage(Application.StartupPath + "\\default_image.png");
            textboxgroupbox.Text = "";
            setTextboxText("");
        }
        
        private void refreshListViewAdaptor()
        {
            listviewapt.ReloadItems(wsp.Store[fileName]);
        }
        private void setTextboxText(string text)
        {
            textbox.TextChanged -= textbox_TextChanged;

            textbox.Text = text;
            textbox.SelectionLength = 0;
            textbox.SelectionStart = textbox.Text.Length;

            textbox.TextChanged += new EventHandler(textbox_TextChanged);
        }

        private void picView_UserClickAction(object sender, PicView.LabelUserActionEventArgs e)
        {
            bool ctrlBePush = editLabelMode || Control.ModifierKeys == Keys.Control ;

            switch (e.Type) { 
                case PicView.LabelUserActionEventArgs.ClickType.left:
                    if (ctrlBePush)
                    {
                        //add
                        wsp.Store.AddLabelItem(FileName,
                            new LabelItem(e.X_percent, e.Y_percent, "", newLabelCcategory),
                            listviewapt.SelectedIndex);

                        listviewapt.SelectedIndex = -1;
                    }
                    else 
                    { 
                        //normal click
                        if (e.Index == -1)
                            return;

                        listviewapt.SelectedIndex = e.Index;
                        textbox.Focus();
                    }
                    break;
                case PicView.LabelUserActionEventArgs.ClickType.right:
                    if (ctrlBePush)
                    {
                        //del
                        wsp.Store.DelLabelItem(FileName, e.Index);

                        listviewapt.SelectedIndex = -1;
                    }
                    break;
            }
        } 
 
        private void picView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                page_left();
            else if (e.KeyCode == Keys.Right)
                page_right();
            else if(e.KeyCode == Keys.D1)
                SetCategoryButton_Click(categorybutton1,null);
            else if(e.KeyCode == Keys.D2)
                SetCategoryButton_Click(categorybutton2,null);
            else if(e.KeyCode == Keys.D3)
                SetCategoryButton_Click(categorybutton3, null);
            else if(e.KeyCode == Keys.D4)
                SetCategoryButton_Click(categorybutton4, null);

            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Tab)  
                //Ctrl+Tab
                page_right();               
            
        }

        private void listViewSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listviewapt.SelectedIndex == -1)
                {
                    //did not select item
                    setTextboxText("");
                    itemIndex = -1;
                    textboxgroupbox.Text = "";
                }
                else
                {
                    itemIndex = listviewapt.SelectedIndex;
                    setTextboxText(wsp.Store[fileName][itemIndex].Text);
                    textboxgroupbox.Text = (itemIndex + 1).ToString();
                }
            }
            catch { }
        }

        private void labelItemTextChanged(object sender, EventArgs e)
        {
            try
            {
                listviewapt.ReloadItems(wsp.Store[fileName]);
            }
            catch { }
        }
        private void labelItemListChanged(object sender, EventArgs e)
        {
            try
            {
                if (wsp.Store.Filenames.Contains(fileName))
                {
                    listviewapt.ReloadItems(wsp.Store[fileName]);
                    picview.SetLabels(wsp.Store[fileName]);
                    listviewapt.SelectedIndex = -1;
                }
                else {
                    listviewapt.ReloadItems(null);
                    picview.SetLabels(null);                    
                }
            }
            catch { }
        }        
        private void fileListChanged(object sender, EventArgs e)
        {
            try
            {
                //Set comboo items

                combo.SelectedIndexChanged -= comboSelectedIndexChanged;

                string beforeFile = combo.Text;

                combo.Items.Clear();

                var keys = wsp.Store.Filenames;
                if (keys != null)
                {
                    foreach (string name in keys)
                    {
                        combo.Items.Add(name);
                    }
                }

                int n = combo.FindStringExact(beforeFile);
                if (n != -1)
                {
                    combo.SelectedIndex = n;
                }

                combo.SelectedIndexChanged += comboSelectedIndexChanged;
            }
            catch { }
        }

        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if(e.KeyCode == Keys.Enter){
                    //Ctrl+enter
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }else if(e.KeyCode == Keys.Up){
                    //Ctrl+up
                    listviewapt.SelectedIndex--;
                }
                else if (e.KeyCode == Keys.Down) {
                    //Ctrl+down
                    listviewapt.SelectedIndex++;
                }
                else if (e.KeyCode == Keys.Left) {
                    //Ctrl+left
                    page_left();
                }
                else if (e.KeyCode == Keys.Right) {
                    //Ctrl+right
                    page_right();
                }
            }

            if (e.Alt) {
                if (e.KeyCode == Keys.A) { 
                    //Alt+A
                    menuquicktext.Show(textbox,textbox.Location);
                    e.SuppressKeyPress = true;
                }
            }
        }    
        private void textboxPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Ctrl+enter
                    try
                    {
                        listviewapt.SelectedIndex += 1;
                    }
                    catch
                    {
                        listviewapt.SelectedIndex = -1;
                    }
                    e.IsInputKey = true;
                }
            }
        }
        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if (itemIndex < 0) {
                textbox.Text = "";
                return; 
            }
            wsp.Store.UpdateLabelItemText(fileName, itemIndex, textbox.Text);
        } 

        private void comboSelectedIndexChanged(object sender, EventArgs e)
        {
            fileName = combo.Text;
            picview.LoadImage(wsp.DirPath + @"\" + combo.Text);
            labelItemListChanged(null, null);

        }

        private void editLabelButton_Click(object sender, EventArgs e)
        {
            editLabelMode = editlabelbutton.Checked;
        }

        private void picView_MouseMove(object sender, MouseEventArgs e)
        {
            //鼠标样式
            if (Control.ModifierKeys == Keys.Control || editLabelMode)
            {
                picview.Cursor = Cursors.Cross;
            }
            else
            {
                picview.Cursor = Cursors.Default;
            }
        }

        private void picView_MosueClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            { 
                //中键翻页
                page_right();
            }
        }

        private void listViewUserSetCategory(object sender, ListViewAdpter.UserSetCategoryEventArgs e)
        {
            wsp.Store.UpdateLabelCategory(fileName, e.Index, e.Category);
        }

        private void SetCategoryButton_Click(object sender, EventArgs e)
        {
            categorybutton1.Checked = false;
            categorybutton2.Checked = false;
            categorybutton3.Checked = false;
            categorybutton4.Checked = false;

            ((ToolStripButton)sender).Checked = true;

            if (sender == categorybutton1)
            {
                newLabelCcategory = 1;
            }
            else if (sender == categorybutton2)
            {
                newLabelCcategory = 2;
            }
            else if (sender == categorybutton3)
            {
                newLabelCcategory = 3;
            }
            else if (sender == categorybutton4)
            {
                newLabelCcategory = 4;
            }
        }

        private void quickTextItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            textbox.AppendText(e.ClickedItem.ToolTipText);
        }

        #endregion

        #region Constructors
        public WorkspaceControlAdpter(ToolStripButton toolStripButtonEditLabelMode, ToolStripComboBox FileSelectComboBox, TextBox TranslateTextBox, GroupBox TextBoxGroupBox, ListViewAdpter LabelListViewAPT, PicView picView, ToolStripButton catebtn1, ToolStripButton catebtn2, ToolStripButton catebtn3, ToolStripButton catebtn4,ContextMenuStrip contextMenuQuictText , Workspace workspace)
        {

            wsp = workspace;
            LabelItemsManager.FileListChanged += new EventHandler(fileListChanged);
            LabelItemsManager.LabelItemListChanged += new EventHandler(labelItemListChanged);
            LabelItemsManager.LabelItemTextChanged += new EventHandler(labelItemTextChanged);

            textboxgroupbox = TextBoxGroupBox;

            picview = picView;
            picview.Image = null;
            picview.Refresh();
            //picview.LabelUserAddAction += new PicView.UserActionEventHandler(picView_UserActionEventAdd);
            //picview.LabelUserDelAction += new PicView.UserActionEventHandler(picView_UserActionEventDel);            
            picview.LabelUserClickAction += new PicView.UserActionEventHandler(picView_UserClickAction);
            picView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(picView_PreviewKeyDown);
            picview.MouseMove += new MouseEventHandler(picView_MouseMove);
            picview.MouseClick += new MouseEventHandler(picView_MosueClick);
            
            combo = FileSelectComboBox;
            combo.Items.Clear();
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.SelectedIndexChanged += new EventHandler(comboSelectedIndexChanged);

            textbox = TranslateTextBox;
            textbox.PreviewKeyDown += new PreviewKeyDownEventHandler(textboxPreviewKeyDown);
            textbox.KeyDown += new KeyEventHandler(textbox_KeyDown);
            textbox.TextChanged += new EventHandler(textbox_TextChanged);

            listviewapt = LabelListViewAPT;
            listviewapt.ListViewSelectedIndexChanged += new EventHandler(listViewSelectedIndexChanged);
            listviewapt.UserSetCategory += new ListViewAdpter.UserActionEventHandler(listViewUserSetCategory);

            editlabelbutton = toolStripButtonEditLabelMode;
            editlabelbutton.Click += new EventHandler(editLabelButton_Click);

            categorybutton1 = catebtn1;
            categorybutton2 = catebtn2;
            categorybutton3 = catebtn3;
            categorybutton4 = catebtn4;
            categorybutton1.Click += new EventHandler(SetCategoryButton_Click);
            categorybutton2.Click += new EventHandler(SetCategoryButton_Click);
            categorybutton3.Click += new EventHandler(SetCategoryButton_Click);
            categorybutton4.Click += new EventHandler(SetCategoryButton_Click);
            categorybutton1.ForeColor = GlobalVar.CategoryColor[1];
            categorybutton2.ForeColor = GlobalVar.CategoryColor[2];
            categorybutton3.ForeColor = GlobalVar.CategoryColor[3];
            categorybutton4.ForeColor = GlobalVar.CategoryColor[4];
            categorybutton1.Checked = true;           

            menuquicktext = contextMenuQuictText;
            foreach(GlobalVar.QuickTextItem item in GlobalVar.QuickTextItems){
                string menuItemStr = item.Text + "(&" + item.Key + ")";
                menuquicktext.Items.Add(menuItemStr).ToolTipText = item.Text;

            }
            menuquicktext.ItemClicked += new ToolStripItemClickedEventHandler(quickTextItemClicked);

            NewFile();
        }
        #endregion

    }
}
