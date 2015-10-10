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
        //public static string AutoGroupActionGroupname;

        public static float SetLabelVisualRatioX;
        public static float SetLabelVisualRatioY;

        public static string DefaultComment;

        public static void Reload() {
 
            /* 读配置文件 */

            FileInfo fi = new FileInfo(@"labelplus_config.xml");
            if (!fi.Exists)
                throw new Exception("Not found config file.");

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

            ///* AutoGroupActionGroupname */
            //AutoGroupActionGroupname = doc.SelectSingleNode("AppConfig/AutoGroupActionGroupname").InnerText;

            /* GroupDefine */
            XmlNodeList GroupDefine = doc.SelectNodes("AppConfig/GroupDefine/Group");
            List<GroupDefineItem> tmpItems = new List<GroupDefineItem>();

            int gourpItemNum = 0;
            bool noName = false;    
            foreach (XmlNode node in GroupDefine) {                    
                string name;
                string rgbText;
                 
                name = node.SelectSingleNode("Name").InnerText;

                //存在命名的项目必须从头开始并连续
                if (noName && name != "")                                                   
                    throw new Exception("GroupDefine Error: \r\n" + node.InnerXml);
                if (name == "")
                    noName = true;

                rgbText = node.SelectSingleNode("RGB").InnerText;
                tmpItems.Add(new GroupDefineItem(name, rgbText));

                gourpItemNum++;
                if (gourpItemNum == 10)
                    throw new Exception("gourpItemNum > 9");
            }

            DefaultGroupDefineItems = tmpItems.ToArray();


            //SetLabelVisualRatioX Y
            string[] setLabelVisualRatioStrs; 
            setLabelVisualRatioStrs = doc.SelectSingleNode("AppConfig/SetLabelVisualRatio").InnerText.Split(',');

            SetLabelVisualRatioX = Convert.ToSingle(setLabelVisualRatioStrs[0]);
            SetLabelVisualRatioY = Convert.ToSingle(setLabelVisualRatioStrs[1]);

            //DefaultComment
            DefaultComment = doc.SelectSingleNode("AppConfig/DefaultComment").InnerText;
            DefaultComment = DefaultComment.Replace(@"\n", "\r\n");
        }

    }
}
