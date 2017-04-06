using System;
using System.Collections.Generic;

namespace CoAP.Tester
{
    public class BlockingQueue<T>
    {
        private Queue<T> _queue = new Queue<T>();
        private Object _syncRoot = new Byte[0];

        public Boolean IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        public void Offer(T item)
        {
            System.Threading.Monitor.Enter(_syncRoot);

            _queue.Enqueue(item);
            System.Threading.Monitor.Pulse(_syncRoot);

            System.Threading.Monitor.Exit(_syncRoot);
        }

        public T Poll(Int32 millisecondsTimeout)
        {
            T item = default(T);

            System.Threading.Monitor.Enter(_syncRoot);

            if (IsEmpty)
                System.Threading.Monitor.Wait(_syncRoot, millisecondsTimeout);

            if (!IsEmpty)
                item = _queue.Dequeue();

            System.Threading.Monitor.Exit(_syncRoot);

            return item;
        }
    }
}
