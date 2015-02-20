using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public enum Colour { White, Black };
    public enum Value { Private, Officer };
    public struct Counter
    {
        public Colour color;
        public Value value;
        public static Counter w = new Counter('w');
        public static Counter W = new Counter('W');
        public static Counter b = new Counter('b');
        public static Counter B = new Counter('B');
        public Counter(char c)
        {
            switch (c)
            {
                case 'w': color = Colour.White; value = Value.Private;
                    break;
                case 'W': color = Colour.White; value = Value.Officer;
                    break;
                case 'b': color = Colour.Black; value = Value.Private;
                    break;
                case 'B': color = Colour.Black; value = Value.Officer;
                    break;
                default:
                    throw new InvalidOperationException("char in Counter");
            }
        }
		public void Promote()
		{
			if (value == Value.Officer) throw new InvalidOperationException("promoting Officer");
			value = Value.Officer;
		}
        public override string ToString()
        {
            if (color == Colour.White)
                if (value == Value.Private)
                    return "w";
                else
                    return "W";
            else
                if (value == Value.Private)
                    return "b";
                else
                    return "B";
        }
        public static bool operator ==(Counter a, Counter b)
        {
            return a.color == b.color && a.value == b.value;
        }
        public static bool operator !=(Counter a, Counter b)
        {
            return !(a == b);
        }
    }
   }
