using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LabelPlus
{
    static class GlobalVar
    {
        public static Color[] CategoryColor;

        public static bool Reload() {
            try
            {
                CategoryColor = new Color[5];
                CategoryColor[1] = Color.Black;
                CategoryColor[1] = Color.Red;
                CategoryColor[2] = Color.Blue;
                CategoryColor[3] = Color.Green;
                CategoryColor[4] = Color.DarkOrange;

                return true;
            }
            catch {
                return false;
            }
        }

    }
}
