/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPlus
{
    public class ToolStripButtonGroup
    {
        public EventHandler IndexChanged;
        internal void OnIndexChanged() {
            if (IndexChanged != null)
                IndexChanged(this,new EventArgs());        
        }

        int selectedButtonIndex = -1;

        public int SelectedButtonIndex {
            get { return selectedButtonIndex; }
            set { setButtonChecked(myToolStripButtonList[value]); }
        }

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

        public void DelAllButtons() { 
            foreach(var btn in myToolStripButtonList){
                myToolStrip.Items.Remove(btn);
            }
            myToolStripButtonList.Clear();
        }

        public bool AddButton(ToolStripButton button) {
            try
            {
                myToolStripButtonList.Add(button);

                //button.CheckOnClick = true;

                if (!myToolStrip.Items.Contains(button))
                    myToolStrip.Items.Add(button);

                if (GetButtonIndex(button) == 0) {
                    button.Checked = true;
                    selectedButtonIndex = 0;
                }

                button.Click += new EventHandler(button_click);
                
                return true;
            }
            catch {
                return false;
            }
        }

        internal void setButtonChecked(ToolStripButton btn) {
            foreach (ToolStripButton buttonN in myToolStripButtonList)
            {
                buttonN.Checked = false;
            }

            btn.Checked = true;
            selectedButtonIndex = GetButtonIndex(btn);

            OnIndexChanged();
        }

        private void button_click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton button = (ToolStripButton)sender;
                setButtonChecked(button);

                OnIndexChanged();
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
