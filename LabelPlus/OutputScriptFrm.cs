using System;
using System.Drawing;
using System.Text;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;

namespace LabelPlus
{
    public partial class OutputScriptFrm : Form
    {
        Workspace wsp;
        public OutputScriptFrm(Workspace wsp)
        {
            this.wsp = wsp;
            InitializeComponent();
            Language.InitFormLanguage(this, StringResources.GetValue("lang"));
        }

        private void outputButton_Click(object sender, EventArgs e)
        {
            bool not_label_num = !labelNumCheckBox.Checked;
            bool notSign = !notHeadFootSignCheckBox.Checked;
            bool notCloseFile = notCloseFileCheckBox.Checked;
            bool dealUnLabeledFile = checkBoxMakeUnLabeledFile.Checked;           

            try
            {
                string str_header = StringResources.GetValue("ps_header");   //只做一次
                string str_file_header = StringResources.GetValue("ps_file_header");   //每个图片做一次，{0}=文件名
                string str_labeltext = StringResources.GetValue("ps_labeltext");   //每个标签做一次,{0}=文本 {1}=x百分比 {2}=y百分比 {3}=字体大小
                string str_labelnum = StringResources.GetValue("ps_labelnum");   //每个标签做一次,{0}=文本 {1}=x百分比 {2}=y百分比
                string str_file_footer = StringResources.GetValue("ps_file_footer");   //每个图片做一次，{0}=文件名
                string str_blank_layer = StringResources.GetValue("ps_header");   //空白图层{0}=图层名
                string str_close_file = StringResources.GetValue("ps_close_file");

                string str_font_size = (checkBoxSetFontSize.Checked) ? (numericUpDownFontSize.Value.ToString()) : "bg.height/90.0";
                string str_font = (checkBoxSetFont.Checked && comboBoxFont.SelectedIndex != -1) ? comboBoxFont.Text : "SimSun";



                //让用户选择目录
                string ouputStr = str_header;

                var store = wsp.Store;
                var keys = store.Filenames;

                foreach (string filename in keys)
                {
                    bool fileWithoutLabel = false;
                    if (store[filename].Count == 0) {
                        if (dealUnLabeledFile)      //处理未标号文件
                        {
                            fileWithoutLabel = true;
                        }
                        else continue;   //跳过没有标号的文件
                    }

                    //打开文件
                    ouputStr += String.Format(str_file_header, filename);

                    //尾部标志
                    if (!notSign && !fileWithoutLabel)
                        ouputStr += String.Format(str_blank_layer, "end");

                    //插入文字
                    for(int labelIndex = store[filename].Count - 1; labelIndex >= 0;labelIndex--)
                    {
                        LabelItem label = store[filename][labelIndex];
                        string str = label.Text.Trim();
                        if (str == "") continue;
                        str = str.Replace("\r\n", @"\r");
                        str = str.Replace("\r", @"\r");
                        ouputStr += String.Format(str_labeltext, str, label.X_percent.ToString(), label.Y_percent.ToString(),str_font_size,str_font);
                    }
                    if (!not_label_num)
                    {
                        //插入标号
                        int labelNum = store[filename].Count;
                        for(int labelIndex = store[filename].Count - 1; labelIndex >= 0;labelIndex--)
                        {
                            LabelItem label = store[filename][labelIndex];                            
                            ouputStr += String.Format(str_labelnum, labelNum.ToString(), label.X_percent.ToString(), label.Y_percent.ToString());
                            labelNum--;
                        }
                    }
                    //头部标志
                    if (!notSign && !fileWithoutLabel)
                        ouputStr += String.Format(str_blank_layer, "start");
                    //保存 关闭文件
                    ouputStr += String.Format(str_file_footer, filename);
                    //关闭文件
                    if (!notCloseFile) ouputStr += str_close_file;
                }

                string path = wsp.DirPath + "\\auto_label_for_photoshop.jsx";
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sr = new StreamWriter(fs, Encoding.Unicode);
                sr.Write(ouputStr);
                sr.Close();
                fs.Close();
                MessageBox.Show(StringResources.GetValue("ps_output_complete"));
                this.Close();
            }
            catch
            {
                MessageBox.Show(StringResources.GetValue("output_fail"));
            }


        }

        private void OutputScriptFrm_Load(object sender, EventArgs e)
        {
            try
            {
                InstalledFontCollection fonts = new InstalledFontCollection();

                foreach (FontFamily FontFamily in fonts.Families)
                {
                    comboBoxFont.Items.Add(FontFamily.Name);
                }
            }
            catch { }
        }

        private void checkBoxSetFont_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxFont.Enabled = checkBoxSetFont.Checked;
        }

        private void checkBoxSetFontSize_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownFontSize.Enabled = checkBoxSetFontSize.Checked;
        }
    }
}
