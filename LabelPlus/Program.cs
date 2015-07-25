using System;
using System.Windows.Forms;

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

            StringResources.SetValue("lang", Language.ReadDefaultLanguage());
            Language.InitStringResouce(StringResources.GetValue("lang"));

            Application.Run(new MainFrm());
        }
    }
}
