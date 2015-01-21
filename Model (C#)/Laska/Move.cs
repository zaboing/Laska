using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public class Move
    {
        public readonly List<Position> Hops;

        public Position Start
        {
            get
            {
                return Hops[0];
            }
        }

        public Position End
        {
            get
            {
                return Hops[Hops.Count - 1];
            }
        }

        public Move(params Position[] hops)
        {
            Hops = new List<Position>(hops);
        }

        public Move(List<Position> hops)
        {
            Hops = hops;
        }

        public Move(string hops)
        {
            Hops = new List<Position>();
            if (hops.Length % 2 != 0)
            {
                throw new ArgumentException("Invalid hops: " + hops);
            }
            for (int i = 0; i < hops.Length; i += 2)
            {
                Hops.Add(Position.Convert(hops.Substring(i, 2)));
            }
        }

        public void Perform(Board board)
        {
            if (!CanPerform(board))
            {
                return;
            }
            List<Token> bounty = new List<Token>();
            for (int i = 1; i < Hops.Count - 1; i++)
            {
                Tower tower = board[Hops[i]];
                if (tower != null && tower.Count > 0)
                {
                    bounty.Add(tower.Pop());
                }
            }
            Tower t = board[Start];
            board[Start] = new Tower("");
            board[End] = t;
            t.Position = End;
            foreach (Token token in bounty)
            {
                t.Append(token);
            }
            if (t.Peek().Color.Goal() == End.Row)
            {
                Token token = t.Pop();
                token.Value = TokenValue.GENERAL;
                t.Push(token);
            }
            board.ChangeTurns();
        }

        public bool CanPerform(Board board)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            Move act = obj as Move;
            if (act == null)
            {
                return false;
            }
            return Hops.SequenceEqual(act.Hops);
        }

        public static bool operator ==(Move m, Move m2)
        {
            if (Object.ReferenceEquals(m, m2))
            {
                return true;
            }
            if (Object.ReferenceEquals(m, null))
            {
                return false;
            }
            return m.Equals(m2);
        }

        public static bool operator !=(Move m, Move m2)
        {
            if (Object.ReferenceEquals(m, m2))
            {
                return true;
            }
            if (Object.ReferenceEquals(m, null))
            {
                return false;
            }
            return m.Equals(m2);
        }
        public override int GetHashCode()
        {
            return Start.ToString().GetHashCode() + End.ToString().GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (Position hop in Hops)
            {
                builder.Append(hop.ToString());
            }

            return builder.ToString();
        }
    }
}
