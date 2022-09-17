using System;
using System.Collections.Generic;

namespace LabelPlus
{
    public class CommandPool
    {
        #region fields
        private Stack<Command> toRedoStack;
        private int maxCommandCount;
        private Deque<Command> toUndoDeque;
        #endregion

        #region properties
        public int TotalCommandCount
        {
            get
            {
                return toUndoDeque.Count;
            }
        }

        public int ToRedoCommandCount
        {
            get
            {
                return toRedoStack.Count;
            }
        }

        public int ToUndoCommandCount
        {
            get
            {
                return toUndoDeque.Count;
            }
        }
        #endregion

        #region constructors
        public CommandPool()
        {
            toUndoDeque = new Deque<Command>();
            toRedoStack = new Stack<Command>();
            this.maxCommandCount = 1;
        }

        public CommandPool(int maxCommandCount)
        {
            toUndoDeque = new Deque<Command>(maxCommandCount);
            toRedoStack = new Stack<Command>();
            this.maxCommandCount = maxCommandCount;
        }
        #endregion
        #region methods
        public void Register(Command command)
        {
            toRedoStack.Clear();

            if (toUndoDeque.Count == maxCommandCount)
            {
                toUndoDeque.RemoveHead();
            }

            toUndoDeque.AddTail(command);
        }

        public void Undo()
        {
            if (toUndoDeque.Count == 0)
            {
                return;
            }

            Command command = toUndoDeque.RemoveTail();
            command.Undo();
            toRedoStack.Push(command);
        }

        public void Redo()
        {
            if (toRedoStack.Count == 0)
            {
                return;
            }

            Command command = toRedoStack.Pop();
            command.Excute();
            toUndoDeque.AddTail(command);
        }

        public void Clear()
        {
            toRedoStack.Clear();
            toUndoDeque.Clear();
        }

        public string GetNextUndoCommandInfo()
        {
            Command command = toUndoDeque.GetTail();

            if (command == null)
            {
                return "no command";
            }

            return command.ToString();
        }

        public string GetNextRedoCommandInfo()
        {
            if (toRedoStack.Count == 0)
            {
                return "no command";
            }

            Command command = toRedoStack.Peek();
            return command.ToString();
        }

        public override string ToString()
        {
            return "this pool has " + toRedoStack.Count + " to redo command left and " + toUndoDeque.Count + " to undo command left";
        }
        #endregion
    }
}
