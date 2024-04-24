using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cviko3CSharp
{
    public class Stack
    {

        public class SimpleStack<T>
        {

            private List<T> data = new List<T>();
            private object lockObj = new object();

            public T Top
            {
                get
                {
                    lock(lockObj)
                    {
                        int idx = this.data.Count - 1;
                        if (idx == -1)
                        {
                            throw new StackEmptyException();
                        }
                        return data[idx];
                    }                  
                }
            }


            public bool IsEmpty
            {
                get
                {
                    return this.data.Count == 0;
                }
            }


            public void Push(T val)
            {
                lock(lockObj)
                {
                    this.data.Add(val);
                }
            }

            public T Pop()
            {
                lock (lockObj)
                {
                    int idx = this.data.Count - 1;
                    if (idx == -1)
                    {
                        throw new StackEmptyException();
                    }
                    T val = this.data[idx];
                    this.data.RemoveAt(idx);
                    return val;
                }
            }


            public class StackEmptyException : Exception
            {

            }
        }
    }
}
