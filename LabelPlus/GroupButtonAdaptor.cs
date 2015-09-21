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
        GroupDefineItemCollection groups; 

        Font buttonFont;

        public GroupButtonAdaptor(ToolStrip toolStrip, GroupDefineItemCollection groupDefines) {
            myButtonGroup = new ToolStripButtonGroup(toolStrip);
            buttonFont = new Font(toolStrip.Font.FontFamily, 12, toolStrip.Font.Style);
            groups = groupDefines;

            for(int i=1; i<=groups.UserGroupCount; i++){                
                ToolStripButton button = new ToolStripButton();

                string title = groups.GetViewName(i);
                Color color = groups.GetColor(i);

                button.Font = buttonFont;
                button.ForeColor = color;
                button.Text = title;
                button.ToolTipText = "G" + i.ToString();
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
