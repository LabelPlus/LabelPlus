/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

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
    public partial class FileSettingFrm : Form
    {
        Workspace wsp;

        public FileSettingFrm(Workspace wsp)
        {
            InitializeComponent();

            this.wsp = wsp;

            string text = "";
            List<string> groupList = wsp.Store.GroupList;
            foreach (string str in groupList) {
                text += str + "\r\n";
            }

            textBoxGroup.Text = text.Trim();
            textBoxGroup.SelectionStart = 0;
            textBoxGroup.SelectionLength = 0;

            textBoxComment.Text = wsp.Store.Comment;
            textBoxComment.SelectionStart = 0;
            textBoxComment.SelectionLength = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Group
            string inputText = textBoxGroup.Text;
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



            //Comment
            foreach (string line in textBoxComment.Lines) {
                if (line == "-") {
                    MessageBox.Show(StringResources.GetValue("tip_setting_comment"));
                    return;
                }
            }
            wsp.Store.Comment = textBoxComment.Text;


            this.Close();

        }
    }
}
