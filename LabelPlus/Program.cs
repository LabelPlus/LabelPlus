/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

using System;
using System.Windows.Forms;
using System.IO;
namespace LabelPlus
{
    

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StringResources.SetValue("lang", Language.ReadDefaultLanguage());
            Language.InitStringResouce(StringResources.GetValue("lang"));
            
            //try{
            //    loadPsScript("ps_blank_layer");
            //    loadPsScript("ps_close_file");
            //    loadPsScript("ps_file_footer");
            //    loadPsScript("ps_file_header");
            //    loadPsScript("ps_header");
            //    loadPsScript("ps_labelnum");
            //    loadPsScript("ps_labeltext");
            //    loadPsScript("ps_add_group");
            //    loadPsScript("ps_run_action");
            //    loadPsScript("ps_del_group_sign");
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show("PS_Script Definition Error. \r\n"+ e.ToString());
            //    Environment.Exit(1);
            //}

            
            try{
                GlobalVar.Reload(); 
            }
            catch(Exception e){
                MessageBox.Show("Read \"labelplus_config.xml\" Error! \r\n" + e.ToString());
                Environment.Exit(1);
            }

            Application.Run(new MainFrm());
        }

        static void loadPsScript(string script_name) {
            StringResources.SetValue(script_name, loadStringFile(@"PS_Script/" + script_name + ".txt"));
        }

        static string loadStringFile(string filename) {
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string str = sr.ReadToEnd();            
            sr.Close();
            fs.Close();
            return str;
        }
    }
}
