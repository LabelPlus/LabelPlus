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
    class ZoomAdaptor
    {
        PicView picView;
        //Form form;
        ToolStripButton plusBtn;
        ToolStripButton minusBtn;
        ToolStripComboBox combox;

        public ZoomAdaptor(PicView picView, 
            ToolStripButton plusBtn,
            ToolStripButton minusBtn,
            ToolStripComboBox combox) 
        {

            this.picView = picView;
            //this.form = form;
            this.plusBtn = plusBtn;
            this.minusBtn = minusBtn;
            this.combox = combox;

            picView.ZoomChanged += new EventHandler(zoomChanged);

            plusBtn.Click += new EventHandler(btnClick);
            minusBtn.Click += new EventHandler(btnClick);

            combox.DropDownStyle = ComboBoxStyle.DropDown;
            combox.DropDownHeight = 200;
            for(int i=20; i<=100; i+=20){
                combox.Items.Add(i.ToString() + "%");
            }

            combox.SelectedIndexChanged += new EventHandler(comboxTextUpdate);            
            combox.Leave += new EventHandler(comboxTextUpdate);
            combox.KeyDown += new KeyEventHandler(comboxKeyDown);

            renew();
        }

        private void comboxKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                picView.Focus();
        } 

        internal void renew() {
            combox.Text = picView.Zoom.ToString("#%");
        }

        private void comboxTextUpdate(object sender, EventArgs e)
        {
            try
            {
                string str = combox.Text;
                if(str[str.Length - 1] == '%')
                    str = str.Substring(0, str.Length - 1);
                picView.Zoom = Convert.ToInt16(str) / 100f;
                picView.Focus();
            }
            catch {
                renew();
            }
        }

        private void comboxSelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void zoomChanged(object sender, EventArgs e)
        {
            renew();
        }

        private void btnClick(object sender, EventArgs e)
        {
            if (sender == this.plusBtn) {
                picView.Zoom += 0.1f;
            }
            else if (sender == this.minusBtn) {
                picView.Zoom -= 0.1f;
            }
        }


    }
}
