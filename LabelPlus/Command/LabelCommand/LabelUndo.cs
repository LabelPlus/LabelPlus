using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPlus
{
    /// <summary>
    /// 标记信息
    /// </summary>
    public class LabelUndo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// x轴坐标
        /// </summary>
        public float X_percent { get; set; }

        /// <summary>
        /// y轴坐标
        /// </summary>
        public float Y_percent { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public int Category { get; set; }
    }
}
