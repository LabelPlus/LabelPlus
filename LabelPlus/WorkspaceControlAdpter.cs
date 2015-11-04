/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

#region Using Directives

using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

#endregion

namespace LabelPlus
{
    public class WorkspaceControlAdpter
    {

        #region Fields

        ToolStripButtonGroup modebuttons;
        ToolStripComboBox combo;
        TextBox textbox;       
        PicView picview;        
        GroupBox textboxgroupbox;
        ContextMenuStrip menuquicktext;

        ListViewAdpter listviewapt;
        GroupButtonAdaptor groupbuttons;
        ToolStrip toolstrip;
        Workspace wsp;

        enum WorkMode { 
            Browse,
            Label,
            Input,
            Check,
        }

        WorkMode workMode;
        int itemIndex = -1;
        string fileName = "";

        Point picViewMousePosition;

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
            bool ctrlBePush = workMode==WorkMode.Label || Control.ModifierKeys == Keys.Control ;

            switch (e.Type) { 
                case PicView.LabelUserActionEventArgs.ActionType.leftClick:
                    if (ctrlBePush)
                    {
                        //add
                        wsp.Store.AddLabelItem(FileName,
                            new LabelItem(e.X_percent, e.Y_percent, "", groupbuttons.SelectIndex + 1),
                            listviewapt.Count);

                        listviewapt.SelectedIndex = listviewapt.Count -1;
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
                case PicView.LabelUserActionEventArgs.ActionType.rightClick:
                    if (ctrlBePush)
                    {
                        //del
                        wsp.Store.DelLabelItem(FileName, e.Index);

                        listviewapt.SelectedIndex = -1;
                    }
                    break;
                case PicView.LabelUserActionEventArgs.ActionType.mouseIndexChanged:

                    if (workMode == WorkMode.Check) 
                    {
                        if (e.Index == -1)
                            return;
                        listviewapt.SelectedIndex = e.Index;
                    }
                    break;
            }
        }

        private void picView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                int index = e.KeyCode - Keys.D1;
                if (index <= wsp.GroupDefine.UserGroupCount)
                {
                    groupbuttons.SelectIndex = index;
                }
            }else if (e.KeyCode == Keys.Left)
                page_left();
            else if (e.KeyCode == Keys.Right)
                page_right();
            else if (e.KeyCode == Keys.Tab)
                page_right();
        }

        private void picViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                modebuttons.SelectedButtonIndex = 0;
            }
            else if (e.KeyCode == Keys.W)
            {
                modebuttons.SelectedButtonIndex = 1;
            }
            else if (e.KeyCode == Keys.E)
            {
                modebuttons.SelectedButtonIndex = 2;
            }
            else if (e.KeyCode == Keys.R)
            {
                modebuttons.SelectedButtonIndex = 3;
            }
            else if (e.KeyCode == Keys.A)
            {
                menuquicktext.Show(Control.MousePosition);
                e.SuppressKeyPress = true;

            }   
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

                    if (workMode == WorkMode.Input) {
                        if (picview.Focused)
                            return;
                        if (listviewapt.SelectedIndexCount > 1)
                            return;
                        picview.SetLabelVisual(listviewapt.SelectedIndex);
                    }
                    else if (workMode == WorkMode.Check)
                    {
                        if (listviewapt.SelectedIndexCount > 1)
                            return;

                        if (listviewapt.ListView.Focused == true)
                            picview.SetLabelVisual(listviewapt.SelectedIndex);                        
                    }
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
                    picview.SetLabels(wsp.Store[fileName],wsp.GroupDefine.GetViewNames() , wsp.GroupDefine.GetColors());
                    //listviewapt.SelectedIndex = 0;
                }
                else {
                    listviewapt.ReloadItems(null);
                    picview.SetLabels(null,null,null);                    
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
            picview.EnableMakeImage = false;
            picview.LoadImage(wsp.DirPath + @"\" + combo.Text);
            picview.EnableMakeImage = true;
            labelItemListChanged(null, null);
            listviewapt.SelectedIndex = 0;
        }

        private void userGroupChanged(object sender, EventArgs e)
        {
            groupbuttons.Refresh(wsp.GroupDefine);
        }

        private void modeButtons_IndexChanged(object sender, EventArgs e)
        {
            workMode = (WorkMode)(modebuttons.SelectedButtonIndex);

            if (workMode == WorkMode.Check)
                picview.AlwaysShowGroup = true;
            else
                picview.AlwaysShowGroup = false;

            Console.WriteLine(workMode);
        }

        
        private void picView_MouseMove(object sender, MouseEventArgs e)
        {
            //记录位置
            picViewMousePosition = e.Location;            
            
            //鼠标样式
            if (Control.ModifierKeys == Keys.Control || workMode == WorkMode.Label)
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

        private void listViewUserAction(object sender, ListViewAdpter.UserActionEventArgs e)
        {
            if (e.Action == ListViewAdpter.UserActionEventArgs.ActionType.del){
                if(MessageBox.Show(
                    StringResources.GetValue("tip_sure_del_label"),
                    "warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }
            picview.EnableMakeImage = false;
            if (e.Value <= wsp.GroupDefine.UserGroupCount)
            {
                for(int i=e.Index.Length-1;i>=0;i--) {
                    int index = e.Index[i];
                    if (e.Action == ListViewAdpter.UserActionEventArgs.ActionType.setGroup)
                        wsp.Store.UpdateLabelCategory(fileName, index, e.Value);
                    else if (e.Action == ListViewAdpter.UserActionEventArgs.ActionType.del)
                        wsp.Store.DelLabelItem(fileName, index);

                }
                        
            }
            picview.EnableMakeImage = true;
            picview.MakeImageNow();
        }


        private void quickTextItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(textbox.Focused)
                textbox.AppendText(e.ClickedItem.ToolTipText);

            if (picview.Focused)
            {
                //百分比坐标转换
                PointF poi = picview.ClientToPercentPoint(picViewMousePosition);
                if (poi.X >= 1.0f || poi.X <= 0 || poi.Y >= 1.0f || poi.Y <= 0)
                    return;

                wsp.Store.AddLabelItem(FileName,
                    new LabelItem(
                        poi.X, 
                        poi.Y,
                        e.ClickedItem.ToolTipText,
                        groupbuttons.SelectIndex + 1),
                    listviewapt.Count);

                listviewapt.SelectedIndex = listviewapt.Count - 1;
            }
        }

        private void quickTextClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            textbox.ImeMode = ImeMode.NoControl;
        }

        private void quickTextOpened(object sender, EventArgs e)
        {
            textbox.ImeMode = ImeMode.Off;
        }
        #endregion

        #region Constructors
        public WorkspaceControlAdpter(
            ToolStripButtonGroup ModeButtons, 
            ToolStripComboBox FileSelectComboBox, 
            TextBox TranslateTextBox, 
            GroupBox TextBoxGroupBox,
            ListViewAdpter LabelListViewAPT, 
            PicView picView, 
            ContextMenuStrip contextMenuQuictText,
            ToolStrip toolStrip,
            Workspace workspace)
        {

            wsp = workspace;
            wsp.UserGroupDefineChanged += new EventHandler(userGroupChanged);

            LabelFileManager.FileListChanged += new EventHandler(fileListChanged);
            LabelFileManager.LabelItemListChanged += new EventHandler(labelItemListChanged);
            LabelFileManager.LabelItemTextChanged += new EventHandler(labelItemTextChanged);
            LabelFileManager.GroupListChanged += new EventHandler(labelItemTextChanged);
            textboxgroupbox = TextBoxGroupBox;

            picview = picView;
            picview.Image = null;
            picview.Refresh();
            picview.LabelUserAction += new PicView.UserActionEventHandler(picView_UserClickAction);
            picview.MouseMove += new MouseEventHandler(picView_MouseMove);
            picview.MouseClick += new MouseEventHandler(picView_MosueClick);
            picview.KeyDown += new KeyEventHandler(picViewKeyDown);
            picview.PreviewKeyDown += new PreviewKeyDownEventHandler(picView_PreviewKeyDown);
            
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
            listviewapt.UserSetCategory += new ListViewAdpter.UserActionEventHandler(listViewUserAction);

            this.modebuttons = ModeButtons;
            this.modebuttons.IndexChanged += new EventHandler(modeButtons_IndexChanged);

            menuquicktext = contextMenuQuictText;
            foreach(GlobalVar.QuickTextItem item in GlobalVar.QuickTextItems){
                string menuItemStr = item.Text + "(&" + item.Key + ")";
                menuquicktext.Items.Add(menuItemStr).ToolTipText = item.Text;

            }
            menuquicktext.ItemClicked += new ToolStripItemClickedEventHandler(quickTextItemClicked);
            menuquicktext.Opened += new EventHandler(quickTextOpened);
            menuquicktext.Closed += new  ToolStripDropDownClosedEventHandler(quickTextClosed);

            groupbuttons = new GroupButtonAdaptor(toolStrip, wsp.GroupDefine);

            toolstrip = toolStrip;
            NewFile();
        }


        #endregion

    }
}
