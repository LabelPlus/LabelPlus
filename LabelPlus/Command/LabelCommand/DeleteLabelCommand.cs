using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LabelPlus
{
    /// <summary>
    /// 删除标记命令
    /// </summary>
    public class DeleteLabelCommand : LabelCommand
    {
        #region fields

        #endregion
        /// <summary>
        /// 删除标记命令
        /// </summary>
        /// <param name="excuteAction"></param>
        /// <param name="undoAction"></param>
        /// <param name="labelUndo"></param>
        public DeleteLabelCommand(Action<LabelUndo> excuteAction, Action<LabelUndo> undoAction, LabelUndo labelUndo)
        {
            this.excuteAction = excuteAction;
            this.undoAction = undoAction;
            this.labelUndo = labelUndo;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Excute()
        {
            excuteAction(labelUndo);
        }

        /// <summary>
        /// 撤销
        /// </summary>
        public override void Undo()
        {
            undoAction(labelUndo);
        }

        /// <summary>
        /// 显示序号
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DeleteLabelCommand" + labelUndo.Index;
        }
    }
}
