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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try{
                StringResources.SetValue("lang", Language.ReadDefaultLanguage());
                Language.InitStringResouce(StringResources.GetValue("lang"));

                loadPsScript("ps_blank_layer");
                loadPsScript("ps_close_file");
                loadPsScript("ps_file_footer");
                loadPsScript("ps_file_header");
                loadPsScript("ps_header");
                loadPsScript("ps_labelnum");
                loadPsScript("ps_labeltext");
                loadPsScript("ps_add_group");

                if (GlobalVar.Reload() == false) {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("PS_Script Definition Error.");
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
