using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public class Tower
    {
        private readonly Stack<Token> tokens = new Stack<Token>();

        public Position Position;

        public Tower(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                tokens.Push(new Token(value[value.Length - i - 1]));
            }
        }

        public int Count
        {
            get
            {
                return tokens.Count;
            }
        }

        public Tower Push(Token token)
        {
            tokens.Push(token);
            return this;
        }

        public Tower Append(Token token)
        {
            var array = tokens.ToArray();
            tokens.Clear();
            tokens.Push(token);
            for (int i = array.Length - 1; i >= 0; i--)
            {
                tokens.Push(array[i]);
            }
            return this;
        }

        public Token Pop()
        {
            return tokens.Pop();
        }

        public Token Peek()
        {
            return tokens.Peek();
        }

        public Token Get(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException("Index " + index + " not in range [0;" + (Count - 1) + "]!");
            }
            return tokens.ToArray()[index];
        }

        public Token this[int index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                Pop();
                Push(value);
            }
        }

        public override string ToString()
        {
            Token[] tokens = this.tokens.ToArray();
            StringBuilder builder = new StringBuilder();
            foreach (Token token in tokens)
            {
                builder.Append(token.ToString());
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return this == obj as Tower;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Tower t, Tower t1)
        {
            if (Object.ReferenceEquals(t, null) && Object.ReferenceEquals(t1, null))
            {
                return true;
            }
            if (Object.ReferenceEquals(t, null) || Object.ReferenceEquals(t1, null))
            {
                return false;
            }
            return t.tokens.SequenceEqual(t1.tokens);
        }

        public static bool operator !=(Tower t, Tower t1)
        {
            return !(t == t1);
        }
    }
}