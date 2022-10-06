using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPlus
{
    public class LabelCommand : Command
    {
        public LabelUndo labelUndo;
        public Action<LabelUndo> excuteAction;
        public Action<LabelUndo> undoAction;

        /// <summary>
        /// 执行
        /// </summary>
        public override void Excute()
        {
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public override void Undo()
        {
        }
    }
}
