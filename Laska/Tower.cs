using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public class Tower
    {
        private Stack<Counter> counters = new Stack<Counter>();
        public Tower(string s)
        {
            for (int i = s.Length - 1; i >= 0; i--)
                counters.Push(new Counter(s[i]));
        }
        public Tower()
        {
        }
        public Tower(Tower t)
        {
            Counter[] arr = t.counters.ToArray();
            for (int i = arr.Length-1; i >= 0; i--)
                Push(arr[i]);
        }
        public void Push(Counter c)
        {
            counters.Push(c);
        }
        public void Push(Tower t)
        {
            Counter[] arr = t.counters.ToArray();
            for (int i = arr.Length - 1; i >= 0; i--)
                Push(arr[i]);
        }
        public Counter Pop()
        {
            if (counters.Count == 0) throw new IndexOutOfRangeException("pop");
            return counters.Pop();
        }
        public Counter Peek()
        {
            if (counters.Count == 0) throw new IndexOutOfRangeException("peek");
            return counters.Peek();
        }
        public Counter Get(int pos)
        {
            if (pos < 0 || pos >= counters.Count) throw new IndexOutOfRangeException("get");
            Counter[] arr = counters.ToArray();
            return arr[pos];
        }
        public int Count { get { return counters.Count; } }
		public void Promote()
		{
			if (counters.Count == 0) throw new IndexOutOfRangeException("promote");
            Counter c = Pop();
            c.Promote();
            Push(c);
		}
        public override string ToString()
        {
            string s = "";
            Counter[] arr = counters.ToArray();
            for (int i = 0; i < arr.Length; i++)
                s += arr[i].ToString();
            return s;
        }
        public override bool Equals(object obj)
        {
            return this == (obj as Tower);
        }
        public static bool operator ==(Tower a, Tower b)
        {
            if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null)) return true;
            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null)) return false;
            Counter[] arr1 = a.counters.ToArray();
            Counter[] arr2 = b.counters.ToArray();
            if (arr1.Length != arr2.Length) return false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;

        }
        public static bool operator !=(Tower a, Tower b)
        {
            return !(a == b);
        }
    }
}
