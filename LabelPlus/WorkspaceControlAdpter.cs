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

        ToolStripComboBox combo;
        TextBox textbox;
        ListViewAdpter listviewapt;
        PicView picview;
        Workspace wsp;
        GroupBox textboxgroupbox;

        int itemIndex = -1;
        string fileName = "";

        #endregion

        #region Properties
        public string FileName { get { return fileName; } }
        public int ItemIndex { get { return itemIndex; } }
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
            listviewapt.SelectedIndex = e.Index;
            textbox.Focus();
        }
        private void picView_UserActionEventAdd(object sender, PicView.LabelUserActionEventArgs e)
        {
            wsp.Store.AddLabelItem(FileName, new LabelItem(e.X_percent, e.Y_percent, ""));
        }
        private void picView_UserActionEventDel(object sender, PicView.LabelUserActionEventArgs e)
        {
            wsp.Store.DelLabelItem(FileName, e.Index);
        }
        private void picView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                page_left();
            else if (e.KeyCode == Keys.Right)
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

        #endregion

        #region Constructors
        public WorkspaceControlAdpter(ToolStripComboBox FileSelectComboBox, TextBox TranslateTextBox, GroupBox TextBoxGroupBox, ListViewAdpter LabelListViewAPT, PicView picView, Workspace workspace)
        {

            wsp = workspace;
            LabelItemsManager.FileListChanged += new EventHandler(fileListChanged);
            LabelItemsManager.LabelItemListChanged += new EventHandler(labelItemListChanged);
            LabelItemsManager.LabelItemTextChanged += new EventHandler(labelItemTextChanged);

            textboxgroupbox = TextBoxGroupBox;

            picview = picView;
            picview.Image = null;
            picview.Refresh();
            picview.LabelUserAddAction += new PicView.UserActionEventHandler(picView_UserActionEventAdd);
            picview.LabelUserDelAction += new PicView.UserActionEventHandler(picView_UserActionEventDel);
            picview.LabelUserClickAction += new PicView.UserActionEventHandler(picView_UserClickAction);
            picView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(picView_PreviewKeyDown);

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
        }
        #endregion

    }
}
