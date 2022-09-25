using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LabelPlus
{
    public static class UndoRedoManager
    {
        #region fields
        private static CommandPool commandPool = new CommandPool(100);
        /// <summary>
        /// 标签池
        /// </summary>
        public static LabelCommandPool labelCommandPool = new LabelCommandPool(100);
        /// <summary>
        /// 文本池
        /// </summary>
        public static CommandPool textCommandPool = new CommandPool(100);

        #endregion

        #region properties

        /// <summary>
        /// 命令池
        /// </summary>
        public static CommandPool CommandPool
        {
            get
            {
                return commandPool;
            }
        }
        /// <summary>
        /// 标签池
        /// </summary>
        public static LabelCommandPool LabelCommandPool
        {
            get
            {
                return labelCommandPool;
            }
        }

        /// <summary>
        /// 文本池
        /// </summary>
        public static CommandPool TextCommandPool
        {
            get
            {
                return textCommandPool;
            }
        }
        #endregion
        /// <summary>
        /// 撤销
        /// </summary>
        public static void Undo()
        {
            string info = commandPool.GetNextUndoCommandInfo();
            commandPool.Undo();
        }
        /// <summary>
        /// 反撤销
        /// </summary>
        public static void Redo()
        {
            string info = commandPool.GetNextRedoCommandInfo();
            commandPool.Redo();
        }

        /// <summary>
        /// 撤销标签
        /// </summary>
        public static void UndoLabel()
        {
            string info = labelCommandPool.GetNextUndoCommandInfo();
            labelCommandPool.Undo();
        }
        /// <summary>
        /// 反撤销标签
        /// </summary>
        public static void RedoLabel()
        {
            string info = labelCommandPool.GetNextRedoCommandInfo();
            labelCommandPool.Redo();
        }
    }
}
