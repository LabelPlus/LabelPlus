using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPlus
{
    class ToolStripButtonGroup
    {
        public EventHandler Click;

        ToolStrip myToolStrip;
        List<ToolStripButton> myToolStripButtonList = new List<ToolStripButton>();

        public ToolStripButton this[int index] {
            get { return myToolStripButtonList[index]; }
        }

        public List<ToolStripButton> ToolStripButtons {
            get { return myToolStripButtonList; }
        }

        public ToolStripButtonGroup(ToolStrip toolStrip) {
            myToolStrip = toolStrip;
        }

        public bool AddButton(ToolStripButton button) {
            try
            {
                myToolStripButtonList.Add(button);

                //button.CheckOnClick = true;

                if (!myToolStrip.Items.Contains(button))
                    myToolStrip.Items.Add(button);

                button.Click += new EventHandler(button_click);
                
                return true;
            }
            catch {
                return false;
            }
        }

        private void button_click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton button = (ToolStripButton)sender;
                foreach (ToolStripButton buttonN in myToolStripButtonList) { 
                    buttonN.Checked = false;
                }

                button.Checked = true;

                if (Click != null)
                    Click(sender, e);
            }
            catch {
                Console.WriteLine("ToolStripButtonGroup.cs button_click() Fault.");
            }
        }

        public int GetButtonIndex(object button) {
            try
            {
                return myToolStripButtonList.IndexOf((ToolStripButton)button);
            }
            catch {
                return -1;
            }
        }

        public bool ClickButton(int index) {
            try
            {
                button_click(myToolStripButtonList[index], new EventArgs());

                return true;
            }
            catch { return false; }
        }
    }
}
