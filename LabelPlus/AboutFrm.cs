using System;
using System.Windows.Forms;
using LabelPlus.Properties;
namespace LabelPlus 
{
    public partial class AboutFrm : Form
    {
        public AboutFrm()
        {
            InitializeComponent();
        }

        private void okbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            textBox.Text += Resources.AppName+" \r\n";
            textBox.Text += "Version " + Resources.AppVer + "\r\n";
            textBox.Text += ".NET Framework Version:"+Environment.Version.ToString ()+"\r\n";
            
        }
    }
}
