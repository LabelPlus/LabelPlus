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
using System.Collections;
namespace LabelPlus
{
    class LangComboxAdaptor
    {
        ToolStripComboBox combox;
        Form frm;
        
        public LangComboxAdaptor(ToolStripComboBox combox,Form frm) {
            this.combox = combox;
            this.frm = frm;
            
            combox.Items.Clear();
            
            string lang = StringResources.GetValue("lang");
            
            if (lang == null)
            {
                lang = "EN";
            }
            ArrayList tmpList = (ArrayList)Language.GetLanguageList(lang);
            
            foreach (string tmpStr in tmpList)
            {
                int idex= combox.Items.Add(tmpStr);

                if (getLangFromString(tmpStr) == lang)
                {
                    combox.SelectedIndex = idex;
                }

            }
            /* Reg Event */
            combox.SelectedIndexChanged += combox_SelectedIndexChanged;            
        }


        private void combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (combox.SelectedIndex >= 0)
                {
                    string  lang = getLangFromString(combox.Text);

                    if (lang != null)
                    {
                        try {
                            Language.InitFormLanguage(frm,lang);
                            Language.InitStringResouce(lang);
                        }
                        catch {
                            // if error,do not alter setting
                            return;
                        }

                        StringResources.SetValue("lang", lang);
                        Language.WriteDefaultLanguage(lang);                        
                    }
                }
            }
            catch { }
        }

        private static string getLangFromString(string str)
        {
            string lang;
            lang = str.Substring(str.IndexOf('[') + 1);
            lang = lang.Substring(0, lang.Length - 1);
            return lang;
        }

    }
}
