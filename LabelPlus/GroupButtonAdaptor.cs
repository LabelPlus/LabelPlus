/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

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
        //int selectedButtonIndex;
        public int SelectIndex {
            get { return myButtonGroup.SelectedButtonIndex; }
            set { myButtonGroup.ClickButton(value); }
        }

        ToolStripButtonGroup myButtonGroup;
        public ToolStripButtonGroup Buttons{ get{return myButtonGroup; }}
        //GroupDefineItemCollection groups;
        ToolStrip toolStrip;

        Font buttonFont;

        public GroupButtonAdaptor(ToolStrip toolStrip, GroupDefineItemCollection groupDefines) {
            this.toolStrip = toolStrip;
            myButtonGroup = new ToolStripButtonGroup(toolStrip);
            //myButtonGroup.IndexChanged += new EventHandler(buttonGroupSelectChanged);

            Refresh(groupDefines);
        }

        public void Refresh(GroupDefineItemCollection groups)
        {
            myButtonGroup.DelAllButtons();  //清除当前按钮                
            
            buttonFont = new Font(toolStrip.Font.FontFamily, 12, toolStrip.Font.Style);            

            for (int i = 1; i <= groups.UserGroupCount; i++)
            {
                ToolStripButton button = new ToolStripButton();

                string title = groups.GetViewName(i);
                string fullTitle = groups.GetFullViewName(i);
                Color color = groups.GetColor(i);

                button.Font = buttonFont;
                button.ForeColor = color;
                button.Text = title;
                button.ToolTipText = fullTitle;
                myButtonGroup.AddButton(button);
            }

            if(myButtonGroup[0] != null)
                myButtonGroup[0].Checked = true;

                        
        }


        
    }
}
