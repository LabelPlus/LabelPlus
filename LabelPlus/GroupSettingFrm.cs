using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPlus
{
    public partial class GroupSettingFrm : Form
    {
        Workspace wsp;

        public GroupSettingFrm(Workspace wsp)
        {
            InitializeComponent();

            this.wsp = wsp;

            string text = "";
            List<string> groupList = wsp.Store.GroupList;
            foreach (string str in groupList) {
                text += str + "\r\n";
            }

            textBox.Text = text.Trim();
            textBox.SelectionStart = 0;
            textBox.SelectionLength = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string inputText = textBox.Text;
            if (inputText.Trim() == "") {
                MessageBox.Show(StringResources.GetValue("tip_setting_group"));
                return;            
            }

            string[] strs = inputText.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> tmpList = new List<string>();
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = strs[i].Trim();
                if (strs[i] != "")
                    tmpList.Add(strs[i]);
            }

            if (strs.Length > 9)
            {
                MessageBox.Show(StringResources.GetValue("tip_setting_group"));
                return;
            }

            wsp.Store.GroupList = tmpList;

            this.Close();

        }
    }
}
