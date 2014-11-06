using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public class Board
    {
        public delegate void OutLog(string line);

        public const string EMPTY = ",,,,/,,,/,,,,//,,,,/,,,/,,,,";
        public const string DEFAULT = "w,w,w,w,/w,w,w,/w,w,w,w,//b,b,b,b,/b,b,b,/b,b,b,b,/";

        public readonly Tower[,] Towers = new Tower[7, 7];

        public TokenColor Turn;

        public static OutLog Log = Console.WriteLine;

        public Board(TokenColor turn) : this(turn, EMPTY)
        {

        }

        public Board(TokenColor turn, string value)
        {
            Turn = turn;
            Apply(value);
        }

        public void Apply(string value)
        {
            string[] rows = value.Split('/');
            if (rows.Length < 7)
            {
                throw new ArgumentException(value + " is not a valid board");
            }
            for (int i = 0; i < 7; i++)
            {
                if (rows[i].Length == 0)
                {
                    continue;
                }
                string[] row = rows[i].Split(',');
                int count = 0;
                for (int j = i % 2; j < 7; j += 2, ++count)
                {
                    Towers[j, i] = new Tower(row[count]);
                    Towers[j, i].Position.BCol = (byte)j;
                    Towers[j, i].Position.BRow = (byte)i;
                }
            
            }
        }

        public void Init()
        {
            Apply(DEFAULT);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Towers[j, i] != null)
                    {
                        builder.Append(Towers[j, i]).Append(',');
                    }
                }
                builder.Append("/");
            }

            return builder.ToString();
        }

        public Tower this[Position index]
        {
            get
            {
                return Towers[index.BCol, index.BRow];
            }

            set
            {
                Towers[index.BCol, index.BRow] = value;
            }
        }

        public List<Position> GetValidActions(byte x, byte y)
        {
            return GetValidActions(new Position(x, y));
        }

        public List<Position> GetValidActions(Position position)
        {
            Log(position.Row + " " + position.Col);

            List<Position> actions = new List<Position>(2);

            Tower tower = Towers[position.BCol, position.BRow];
            if (tower != null && tower.Count() == 0)
            {
                return actions;
            }
            if (tower.Peek().Value == TokenValue.SOLDIER)
            {
                
            }

            return actions;
        }

        public static bool operator ==(Board b, Board b1)
        {
            if (Object.ReferenceEquals(b, null) && Object.ReferenceEquals(b1, null))
            {
                return true;
            }
            if (Object.ReferenceEquals(b, null) || Object.ReferenceEquals(b1, null))
            {
                return false;
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (b.Towers[i, j] != b1.Towers[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Board b, Board b1)
        {
            return !(b == b1);
        }
    }
}
