using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace LabelPlus
{
    class GroupButtonAdaptor
    {
        int selectedButtonIndex;
        public int SelectIndex {
            get { return selectedButtonIndex; }
            set { myButtonGroup.ClickButton(value); }
        }

        ToolStripButtonGroup myButtonGroup;
        public ToolStripButtonGroup Buttons{ get{return myButtonGroup; }}

        Font buttonFont;

        public GroupButtonAdaptor(ToolStrip toolStrip) {
            myButtonGroup = new ToolStripButtonGroup(toolStrip);
            buttonFont = new Font(toolStrip.Font.FontFamily, 12, toolStrip.Font.Style);

            int i = 1;
            foreach(GlobalVar.GroupDefineItem item in GlobalVar.GroupDefineItems){
                ToolStripButton button = new ToolStripButton();
                button.Font = buttonFont;
                button.ForeColor = item.Color;
                button.Text = "G" + i++.ToString();
                button.ToolTipText = item.Name;
                myButtonGroup.AddButton(button);
            }

            myButtonGroup[0].Checked = true;

            myButtonGroup.Click += new EventHandler(buttonGroupClick);
        }

        private void buttonGroupClick(object sender, EventArgs e)
        {
            int index = myButtonGroup.GetButtonIndex(sender);
            if (index != -1) {
                selectedButtonIndex = index;
            }
        }

        
    }
}
