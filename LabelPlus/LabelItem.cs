/**
 * 
 * Copyright 2015, Noodlefighter
 * Released under GPL License.
 *
 * License: http://noodlefighter.com/label_plus/license
 */

using System;

namespace LabelPlus
{
    public class LabelItem
    {
        public float    X_percent;
        public float    Y_percent;        
        public string   Text;
        public int      Category;

        public LabelItem(float x_percent, float y_percent, string text, int category){
            if (!(category >= 1 && category <= 9))
                throw new Exception();

            X_percent = x_percent;
            Y_percent = y_percent;
            Text = text;
            Category = category;
        }
    }
}
