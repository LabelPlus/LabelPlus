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
        public struct GroupDefineItem
        {
            public string Name;
            public Color Color;
        }
        public static GroupDefineItem[] GroupDefineItems;

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

                /* GroupDefine */
                XmlNodeList GroupDefine = doc.SelectNodes("AppConfig/GroupDefine/Group");
                GroupDefineItems = new GroupDefineItem[GroupDefine.Count];

                int gourpItemNum = 0;
                foreach (XmlNode node in GroupDefine) {
                    GroupDefineItem item;
                    item.Name = node.SelectSingleNode("Name").InnerText;
                    string rgbText = node.SelectSingleNode("RGB").InnerText;
                    string[] rgbTexts = rgbText.Split(',');
                                        
                    item.Color = Color.FromArgb(Convert.ToInt16(rgbTexts[0]),
                        Convert.ToInt16(rgbTexts[1]),
                        Convert.ToInt16(rgbTexts[2]));

                    GroupDefineItems[gourpItemNum] = item;

                    gourpItemNum++;
                    if (gourpItemNum == 10)
                        return false;
                }

                return true;
            }
            catch {
                return false;
            }
        }

    }
}
