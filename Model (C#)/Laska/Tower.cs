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

        public int Count()
        {
            return tokens.Count;
        }

        public void Push(Token token)
        {
            tokens.Push(token);
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
            if (index < 0 || index >= Count())
            {
                throw new ArgumentOutOfRangeException("Index " + index + " not in range [0;" + (Count() - 1) + "]!");
            }
            return tokens.ToArray()[index];
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