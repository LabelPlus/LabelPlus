using System;
using System.Collections.Generic;

namespace LabelPlus
{
    public class LabelCommandPool : CommandPool
    {
        #region fields
        public Stack<LabelCommand> toRedoStack;
        private int maxLabelCommandCount;
        public Deque<LabelCommand> toUndoDeque;
        #endregion

        #region constructors
        public LabelCommandPool()
        {
            toUndoDeque = new Deque<LabelCommand>();
            toRedoStack = new Stack<LabelCommand>();
            this.maxLabelCommandCount = 1;
        }

        public LabelCommandPool(int maxLabelCommandCount)
        {
            toUndoDeque = new Deque<LabelCommand>(maxLabelCommandCount);
            toRedoStack = new Stack<LabelCommand>();
            this.maxLabelCommandCount = maxLabelCommandCount;
        }
        #endregion
        #region methods
        public void Register(LabelCommand LabelCommand)
        {
            toRedoStack.Clear();

            if (toUndoDeque.Count == maxLabelCommandCount)
            {
                toUndoDeque.RemoveHead();
            }

            toUndoDeque.AddTail(LabelCommand);
        }

        public void Undo()
        {
            if (toUndoDeque.Count == 0)
            {
                return;
            }

            LabelCommand LabelCommand = toUndoDeque.RemoveTail();
            LabelCommand.Undo();
            toRedoStack.Push(LabelCommand);
        }

        public void Redo()
        {
            if (toRedoStack.Count == 0)
            {
                return;
            }

            LabelCommand LabelCommand = toRedoStack.Pop();
            LabelCommand.Excute();
            toUndoDeque.AddTail(LabelCommand);
        }
        public void Clear()
        {
            toRedoStack.Clear();
            toUndoDeque.Clear();
        }

        public string GetNextUndoLabelCommandInfo()
        {
            LabelCommand LabelCommand = toUndoDeque.GetTail();

            if (LabelCommand == null)
            {
                return "no LabelCommand";
            }

            return LabelCommand.ToString();
        }

        public string GetNextRedoLabelCommandInfo()
        {
            if (toRedoStack.Count == 0)
            {
                return "no LabelCommand";
            }

            LabelCommand LabelCommand = toRedoStack.Peek();
            return LabelCommand.ToString();
        }

        public override string ToString()
        {
            return "this pool has " + toRedoStack.Count + " to redo LabelCommand left and " + toUndoDeque.Count + " to undo LabelCommand left";
        }
        #endregion
    }
}
