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
namespace LabelPlus
{
    public class GroupDefineItem
    {
        string name;
        Color color;
        bool haveColor;
        public Color Color { 
            get {
                if (!haveColor)
                    throw new Exception();
                else
                    return color;
            }
        }

        public string Name { get { return name; } }

        public GroupDefineItem(string name)
        {
            this.name = name;
            haveColor = false;
        }

        public GroupDefineItem(string name, Color color) {
            this.name = name;
            this.color = color;
            haveColor = true;
        }

        public GroupDefineItem(string name, string rgbText)
        {
            this.name = name;

            string[] rgbTexts = rgbText.Split(',');
            this.color = Color.FromArgb(Convert.ToInt16(rgbTexts[0]),
                Convert.ToInt16(rgbTexts[1]),
                Convert.ToInt16(rgbTexts[2]));

            haveColor = true;
        }
    }
}
