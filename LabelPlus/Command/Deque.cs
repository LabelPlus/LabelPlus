using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public class Deque<T> : ICollection, IEnumerable<T>
    {
        #region constants
        private const int defaultCapacity = 4;
        #endregion

        #region fields
        private T[] items = null;
        private int count = 0;
        private int tail = 0;
        private int head = 0;
        private readonly static T[] emptyArray = new T[0];
        private int version = 0;
        private Object syncRoot;
        #endregion

        #region properties
        public int Capacity
        {
            get
            {
                return items.Length;
            }
        }

        public int Count
        {
            get
            {
                return count;
            }
        }
        #endregion

        #region constructors
        public Deque()
        {
            items = emptyArray;
        }

        public Deque(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", "The capacity must be greater than 0.");
            }

            if (capacity == 0)
            {
                items = emptyArray;
            }

            else
            {
                items = new T[capacity];
            }
        }

        public Deque(IEnumerable<T> collection, bool isAddTail)
        {
            if (collection == null)
            {
                throw new Exception("collection is null");
            }

            items = new T[defaultCapacity];

            using (IEnumerator<T> enumerator = collection.GetEnumerator())
            {
                if (isAddTail)
                {
                    while (enumerator.MoveNext())
                    {
                        AddTail(enumerator.Current);
                    }
                }

                else
                {
                    while (enumerator.MoveNext())
                    {
                        AddHead(enumerator.Current);
                    }
                }
            }
        }
        #endregion

        #region interface implementations
        int ICollection.Count
        {
            get
            {
                return count;
            }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                if (syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange<Object>(ref syncRoot, new Object(), null);
                }

                return syncRoot;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        void ICollection.CopyTo(Array targetArray, int targetIndex)
        {
            if (targetArray == null)
            {
                throw new ArgumentNullException("targetArray");
            }

            if (targetArray.Rank != 1)
            {
                throw new ArgumentException("targetArray is not an array with rank 1");
            }

            if (targetArray.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("targetArray must have lower bound zero");
            }

            if (targetIndex < 0 || targetIndex > targetArray.Length)
            {
                throw new ArgumentOutOfRangeException("targetIndex");
            }

            int targetArrayLength = targetArray.Length;

            if (targetArrayLength - targetIndex < count)
            {
                throw new ArgumentException("no enough space to store items");
            }

            if (count == 0)
            {
                return;
            }

            try
            {
                if (head < tail)
                {
                    Array.Copy(items, head, targetArray, targetIndex, count);
                }

                else
                {
                    Array.Copy(items, head, targetArray, targetIndex, items.Length - head);
                    Array.Copy(items, 0, targetArray, targetIndex + items.Length - head, tail);
                }
            }

            catch (ArrayTypeMismatchException)
            {
                throw new ArgumentException("targetArray is not the right type");
            }
        }
        #endregion

        #region methods
        public void Clear()
        {
            if (head < tail)
            {
                Array.Clear(items, head, count);
            }

            else
            {
                Array.Clear(items, head, items.Length - head);
                Array.Clear(items, 0, tail);
            }

            head = 0;
            tail = 0;
            count = 0;
            ++version;
        }

        public void CopyTo(T[] targetArray, int targetIndex)
        {
            if (targetArray == null)
            {
                throw new ArgumentNullException("targetArray");
            }

            if (targetIndex < 0 || targetIndex > targetArray.Length)
            {
                throw new ArgumentOutOfRangeException("targetIndex");
            }

            int targetArrayLength = targetArray.Length;

            if (targetArrayLength - targetIndex < count)
            {
                throw new ArgumentException("no enough space to store items");
            }

            if (count == 0)
            {
                return;
            }

            Copy(targetArray, targetIndex);
        }

        public void AddTail(T item)
        {
            if (count == items.Length)
            {
                EnsureCapacity(count + 1);
            }

            items[tail] = item;
            tail = (tail + 1) % items.Length;
            ++count;
            ++version;
        }

        public T RemoveTail()
        {
            if (count == 0)
            {
                throw new Exception("Empty Queue");
            }

            int indexToRemove = (tail - 1 + items.Length) % items.Length;
            T item = items[indexToRemove];
            items[indexToRemove] = default(T);
            tail = indexToRemove;
            --count;
            ++version;
            return item;
        }

        public T GetTail()
        {
            if (count == 0)
            {
                return default(T);
            }

            int index = (tail - 1 + items.Length) % items.Length;
            T item = items[index];
            return item;
        }

        public void AddHead(T item)
        {
            if (count == items.Length)
            {
                EnsureCapacity(count + 1);
            }

            head = (head - 1 + items.Length) % items.Length;
            items[head] = item;
            ++count;
            ++version;
        }

        public T RemoveHead()
        {
            if (count == 0)
            {
                throw new Exception("Empty Queue");
            }

            T item = items[head];
            items[head] = default(T);
            head = (head + 1) % items.Length;
            --count;
            ++version;
            return default(T);
        }

        public T GetHead()
        {
            if (count == 0)
            {
                return default(T);
            }

            T item = items[head];
            return item;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < items.Length; ++i)
            {
                T item = items[i];
                bool isHead = i == head;
                bool isTail = count == 0 ? i == 0 : i == tail - 1;
                string str = i.ToString() + " : " + item.ToString();

                if (isHead)
                {
                    str += " (Head)";
                }

                if (isTail)
                {
                    str += " (Tail)";
                }

                stringBuilder.AppendLine(str);
            }

            return stringBuilder.ToString();
        }

        public void PrintToConsole()
        {
            Console.Write(ToString());
        }

        private void EnsureCapacity(int minCapacity)
        {
            int newCapacity = items.Length == 0 ? defaultCapacity : minCapacity * 2;

            if (newCapacity <= items.Length)
            {
                return;
            }

            T[] newItems = new T[newCapacity];

            if (count <= 0)
            {
                return;
            }

            Copy(newItems, 0);
            items = newItems;
            head = 0;
            tail = count;
            ++version;
        }

        private void Copy(T[] targetArray, int targetIndex)
        {
            if (head < tail)
            {
                Array.Copy(items, head, targetArray, targetIndex, count);
            }

            else
            {
                Array.Copy(items, head, targetArray, targetIndex, items.Length - head);
                Array.Copy(items, 0, targetArray, targetIndex + items.Length - head, tail);
            }
        }

        private T GetItem(int index)
        {
            return items[(head + index) % items.Length];
        }
        #endregion

        #region structs
        public struct Enumerator : IEnumerator<T>
        {
            #region fields
            private Deque<T> deque;
            private int index;
            private T current;
            private int version;
            #endregion

            #region properties
            //public 
            #endregion

            #region constructors
            public Enumerator(Deque<T> deque)
            {
                this.deque = deque;
                this.index = -1;
                this.current = default(T);
                this.version = deque.version;
            }
            #endregion

            #region interface implementations
            public T Current
            {
                get
                {
                    if (index < 0)
                    {
                        if (index == -1)
                        {
                            throw new InvalidOperationException("enumerator not started");
                        }

                        else
                        {
                            throw new InvalidOperationException("enumerator has ended");
                        }
                    }

                    return current;
                }
            }

            public bool MoveNext()
            {
                if (version != deque.version)
                {
                    throw new InvalidOperationException("do not change deque when enumerating");
                }

                if (index == -2)
                {
                    return false;
                }

                ++index;

                if (index == deque.count)
                {
                    index = -2;
                    current = default(T);
                    return false;
                }

                current = deque.GetItem(index);
                return true;
            }

            public void Reset()
            {
                if (version != deque.version)
                {
                    throw new InvalidOperationException("do not change deque when enumerating");
                }

                index = -1;
                current = default(T);
            }

            public void Dispose()
            {
                index = -2;
                current = default(T);
            }

            Object IEnumerator.Current
            {
                get
                {
                    if (index < 0)
                    {
                        if (index == -1)
                        {
                            throw new InvalidOperationException("enumerator not started");
                        }

                        else
                        {
                            throw new InvalidOperationException("enumerator has ended");
                        }
                    }

                    return current;
                }
            }
            #endregion
        }
        #endregion
    }
}
