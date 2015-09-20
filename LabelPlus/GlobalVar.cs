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
        public static GroupDefineItem[] DefaultGroupDefineItems; 

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
                List<GroupDefineItem> tmpItems = new List<GroupDefineItem>();

                int gourpItemNum = 0;
                foreach (XmlNode node in GroupDefine) {                    
                    string name;
                    string rgbText;
                    try
                    {
                        name = node.SelectSingleNode("Name").InnerText;
                        if (name == "")
                            throw new XmlException();
                    }
                    catch (XmlException) {
                        name = "G" + (gourpItemNum + 1).ToString();
                    }

                    rgbText = node.SelectSingleNode("RGB").InnerText;

                    tmpItems.Add(new GroupDefineItem(name, rgbText));

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
