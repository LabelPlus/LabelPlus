using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;
namespace LabelPlus
{
    static class GlobalVar
    {
        public static Color[] CategoryColor;
        public struct QuickTextItem
        {
            public string Text;
            public string Key;
        }
        public static QuickTextItem[] QuickTextItems;
        public static string AutoGroupActionGroupname;

        public static bool Reload() {
            try
            {
                CategoryColor = new Color[5];
                CategoryColor[1] = Color.Black;
                CategoryColor[1] = Color.Red;
                CategoryColor[2] = Color.Blue;
                CategoryColor[3] = Color.Green;
                CategoryColor[4] = Color.DarkOrange;

                /* 读配置文件 */

                FileInfo fi = new FileInfo(@"labelplus_config.xml");
                if (!fi.Exists)
                    return false;
                XmlReader reader = new XmlTextReader(fi.FullName);
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);

                /* QuickText */
                XmlNodeList QuickText = doc.SelectNodes("AppConfig/QuickText/Item");
                QuickTextItems = new QuickTextItem[QuickText.Count];
                for (int i=0; i < QuickText.Count; i++)
                {
                    QuickTextItem item;
                    item.Text = QuickText[i].SelectSingleNode("Text").InnerText;
                    item.Key = QuickText[i].SelectSingleNode("Key").InnerText;
                    QuickTextItems[i] = item;
                }

                /* AutoGroupActionGroupname */
                AutoGroupActionGroupname = doc.SelectSingleNode("AppConfig/AutoGroupActionGroupname").InnerText;

                    return true;
            }
            catch {
                return false;
            }
        }

    }
}
