
#region Using Directives
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
#endregion

namespace LabelPlus
{
    public partial class MainFrm : Form
    {

        #region Constants

        static readonly string APPNAME = LabelPlus.Properties.Resources.AppName;
        static readonly string APPVER = LabelPlus.Properties.Resources.AppVer;
        static readonly string FROM_TITLE = APPNAME + " " + APPVER + " ";

        #endregion

        #region Fields

        Workspace wsp = new Workspace();
        WorkspaceControlAdpter wsp_control_apt;
        LangComboxAdaptor langComboxApt;

        #endregion

        #region Methods

        DialogResult alter_and_save()
        {
            DialogResult result = MessageBox.Show(
                StringResources.GetValue("save_question"),
                "save",
                MessageBoxButtons.YesNoCancel);

            if (result == System.Windows.Forms.DialogResult.Yes)
                return save_file(false);

            return result;
        }
        DialogResult save_file(bool newPath)
        {
            if (!newPath && wsp.HavePath)
            {
                if (wsp.Save())
                {
                    return System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    return System.Windows.Forms.DialogResult.Cancel;
                }

            }
            else
            {
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    wsp.Path = saveFileDialog.FileName;
                    if (wsp.Save())
                    {
                        return System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        return System.Windows.Forms.DialogResult.Cancel;
                    }
                }
                else return System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        { 
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint, true);

            wsp.NewFile();

            this.Text = FROM_TITLE;
        }
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wsp.NeedSave)
            {
                DialogResult tmp = MessageBox.Show(StringResources.GetValue("save_question"), "save", MessageBoxButtons.YesNoCancel);
                if (tmp == System.Windows.Forms.DialogResult.Yes)
                {
                    if (save_file(false) == System.Windows.Forms.DialogResult.OK)
                    {
                        MessageBox.Show(StringResources.GetValue("save_complete"));
                        e.Cancel = false;
                        this.Close();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else if (tmp == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = false;
                }
                else if (tmp == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

            Properties.Settings.Default.Save();
        }

        private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutFrm().ShowDialog(this);
        }
        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (wsp.NeedSave) 
                    alter_and_save();

                wsp.readWorkspaceFromFile(openFileDialog.FileName);

                this.Text = FROM_TITLE + new FileInfo(openFileDialog.FileName).Name;

                toolStripComboBox_File.DroppedDown = true;
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wsp.NeedSave)
            {
                if (alter_and_save() == System.Windows.Forms.DialogResult.Cancel)
                    return;
                else
                    MessageBox.Show(StringResources.GetValue("save_complete"));
            }

            wsp_control_apt.NewFile();
            wsp.NewFile();
            
            this.Text = FROM_TITLE;
        }
        private void saveAsDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_file(true) == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(StringResources.GetValue("save_complete"));
                this.Text = FROM_TITLE + new FileInfo(wsp.Path).Name;
            }
        }
        private void saveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_file(false) == System.Windows.Forms.DialogResult.OK) {
                MessageBox.Show(StringResources.GetValue("save_complete"));
                this.Text = FROM_TITLE + new FileInfo(wsp.Path).Name;
            }
        }
        private void outputAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!wsp.HavePath)
            {
                MessageBox.Show(StringResources.GetValue("input_images_need_save"));
                return;
            }

            new ImageOutputFrm(wsp, picView).ShowDialog();
        }
        private void outputPhotoshopScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!wsp.HavePath)
            {
                MessageBox.Show(StringResources.GetValue("input_images_need_save"));
                return;
            }

            new OutputScriptFrm(wsp).ShowDialog();
        }
        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wsp.HavePath)
            {
                new ManageImageFrm(wsp).ShowDialog();
            }
            else
            {
                MessageBox.Show(StringResources.GetValue("input_images_need_save"));
            }
        }

        private void toolStripButton_Left_Click(object sender, EventArgs e)
        {
            wsp_control_apt.page_left();
        }
        private void toolStripButton_Right_Click(object sender, EventArgs e)
        {
            wsp_control_apt.page_right();
        }        
        private void toolStripButton_Clear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                StringResources.GetValue("clear_all_label_question"),
                "warning！！！", 
                MessageBoxButtons.YesNo);

            if (result == System.Windows.Forms.DialogResult.Yes)
                wsp.Store.DelAllLabelInFile(wsp_control_apt.FileName);
                
        }
        private void toolStripButton_EditBig_Click(object sender, EventArgs e)
        {
            Font oldFont = TranslateTextBox.Font;
            Font newFont = new Font(oldFont.FontFamily, oldFont.Size + 1, oldFont.Style);

            TranslateTextBox.Font = newFont;
            listView.Font = newFont;

            oldFont.Dispose();
            newFont.Dispose();
        }
        private void toolStripButton_EditSmall_Click(object sender, EventArgs e)
        {
            Font oldFont = TranslateTextBox.Font;
            Font newFont = new Font(oldFont.FontFamily, oldFont.Size - 1, oldFont.Style);

            TranslateTextBox.Font = newFont;
            listView.Font = newFont;

            oldFont.Dispose();
            newFont.Dispose();
        }

        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            if (wsp.NeedSave && wsp.HavePath)
                wsp.SaveBAK();
        }

        #endregion

        #region Constructors
        public MainFrm()
        {
            InitializeComponent();

            Language.InitFormLanguage(this, StringResources.GetValue("lang"));

            wsp_control_apt = new WorkspaceControlAdpter(toolStripComboBox_File, TranslateTextBox, TextBox_GroupBox, new ListViewAdpter(listView), picView, wsp);
            langComboxApt = new LangComboxAdaptor(langToolStripComboBox, this);
        }
        #endregion

    }

}
