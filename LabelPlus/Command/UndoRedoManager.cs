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

        //TODO:文本撤回
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

        /// <summary>
        /// 更新标签池
        /// </summary>
        /// <param name="index"></param>
        /// <param name="x_percent"></param>
        /// <param name="y_percent"></param>
        /// <param name="text"></param>
        /// <param name="category"></param>
        public static void UpdateLabelPool(int index, float x_percent = -1, float y_percent = -1, string text = null, int category = -1)
        {
            var toUndoDequeList = LabelCommandPool.toUndoDeque.ToList();
            var toRedoStackList = LabelCommandPool.toRedoStack.ToList();

            if (toUndoDequeList.Count != 0)
            {
                if (x_percent != -1 && y_percent != -1)
                {
                    foreach (var label in toUndoDequeList)
                    {
                        if (label.labelUndo.Index == index)
                        {
                            label.labelUndo.Location.X_percent = x_percent;
                            label.labelUndo.Location.Y_percent = y_percent;
                            break;
                        }
                    }
                }

                if (text != null)
                {
                    foreach (var label in toUndoDequeList)
                    {
                        if (label.labelUndo.Index == index)
                        {
                            label.labelUndo.Text = text;
                            break;
                        }
                    }
                }

                if (category != -1)
                {
                    foreach (var label in toUndoDequeList)
                    {
                        if (label.labelUndo.Index == index)
                        {
                            label.labelUndo.Category = category;
                            break;
                        }
                    }
                }

            }
            if (toRedoStackList.Count != 0)
            {
                if (x_percent != -1 && y_percent != -1)
                {
                    foreach (var label in toRedoStackList)
                    {
                        if (label.labelUndo.Index == index)
                        {
                            label.labelUndo.Location.X_percent = x_percent;
                            label.labelUndo.Location.Y_percent = y_percent;
                            break;
                        }
                    }
                }

                if (text != null)
                {
                    foreach (var label in toRedoStackList)
                    {
                        if (label.labelUndo.Index == index)
                        {
                            label.labelUndo.Text = text;
                            break;
                        }
                    }
                }

                if (category != -1)
                {
                    foreach (var label in toRedoStackList)
                    {
                        if (label.labelUndo.Index == index)
                        {
                            label.labelUndo.Category = category;
                            break;
                        }
                    }
                }

            }
        }
    }
}
