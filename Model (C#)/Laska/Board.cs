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

        public Position? LockedPosition;

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
                    for (int j = i % 2; j < 7; j += 2)
                    {
                        Towers[j, i] = new Tower("");
                        Towers[j, i].Position.BCol = (byte)j;
                        Towers[j, i].Position.BRow = (byte)i;
                    }
                    continue;
                }
                string[] row = rows[i].Split(',');
                int count = 0;
                for (int j = i % 2; j < 7 && count < row.Length; j += 2, ++count)
                {
                    Towers[j, i] = new Tower(row[count]);
                    Towers[j, i].Position.BCol = (byte)j;
                    Towers[j, i].Position.BRow = (byte)i;
                }
                for (int j = i % 2 + count * 2; j < 7; j += 2)
                {
                    Towers[j, i] = new Tower("");
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
                if (index.IsValid)
                {
                    return Towers[index.BCol, index.BRow];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (index.IsValid)
                {
                    if (value != null)
                    {
                        value.Position = index;
                    }
                    Towers[index.BCol, index.BRow] = value;
                }
            }
        }

        public void ChangeTurns()
        {
            if (Turn == TokenColor.BLACK)
            {
                Turn = TokenColor.WHITE;
            }
            else
            {
                Turn = TokenColor.BLACK;
            }
            LockedPosition = null;
        }

        public List<Action> GetValidActions(byte x, byte y)
        {
            return GetValidActions(new Position(x, y));
        }

        public List<Action> GetValidActions(Position position)
        {
            List<Action> actions = new List<Action>(2);

            if (LockedPosition.HasValue && !LockedPosition.Value.Equals(position))
            {
                return actions;
            }

            Tower tower = this[position];
            if (tower == null || tower.Count == 0)
            {
                return actions;
            }
            var token = tower.Peek();
            if (token.Color != Turn)
            {
                return actions;
            }
            if (token.Value == TokenValue.SOLDIER)
            {
                if (!LockedPosition.HasValue)
                {
                    actions.AddRange(soldierWalk(position));
                }
                actions.AddRange(soldierHop(position));
            }
            else if (token.Value == TokenValue.GENERAL)
            {
                if (!LockedPosition.HasValue)
                {
                    actions.AddRange(generalWalk(position));
                }
                actions.AddRange(generalHop(position));
            }

            return actions;
        }

        private List<Action> soldierWalk(Position position)
        {
            int direction = this[position].Peek().Color == TokenColor.WHITE ? 1 : -1;
            List<Action> actions = new List<Action>(2);

            Position left = new Position(position);
            left.BCol -= 1;
            left.BRow = (byte)(left.BRow + direction);
            if (left.IsValid)
            {
                Tower leftTower = this[left];
                if (leftTower == null || leftTower.Count == 0)
                {
                    actions.Add(new Action(position, left));
                }
            }

            Position right = new Position(position);
            right.BCol += 1;
            right.BRow = (byte)(right.BRow + direction);
            if (right.IsValid)
            {
                Tower rightTower = this[right];
                if (rightTower == null || rightTower.Count == 0)
                {
                    actions.Add(new Action(position, right));
                }
            }
            

            return actions;
        }

        private List<Action> soldierHop(Position position)
        {
            TokenColor color = this[position].Peek().Color;
            int direction = color == TokenColor.WHITE ? 1 : -1;
            List<Action> actions = new List<Action>(2);

            Position left = new Position(position);
            left.BCol -= 1;
            left.BRow = (byte)(left.BRow + direction);
            Tower leftTower = this[left];
            if (leftTower != null && leftTower.Count != 0 && leftTower.Peek().Color != color)
            {
                Position lefter = new Position(left);
                lefter.BCol -= 1;
                lefter.BRow = (byte)(lefter.BRow + direction);
                if (lefter.IsValid)
                {
                    Tower lefterTower = this[lefter];
                    if (lefterTower == null || lefterTower.Count == 0)
                    {
                        actions.Add(new Action(position, left, lefter));
                    }
                }
            }


            Position right = new Position(position);
            right.BCol += 1;
            right.BRow = (byte)(right.BRow + direction);
            Tower rightTower = this[right];
            if (rightTower != null && rightTower.Count != 0 && rightTower.Peek().Color != color)
            {
                Position righter = new Position(right);
                righter.BCol += 1;
                righter.BRow = (byte)(righter.BRow + direction);
                if (righter.IsValid)
                {
                    Tower righterTower = this[righter];
                    if (righterTower == null || righterTower.Count == 0)
                    {
                        actions.Add(new Action(position, right, righter));
                    }
                }
            }



            return actions;
        }

        private List<Action> generalWalk(Position position)
        {
            List<Action> actions = new List<Action>();

            for (int i = 1; i < 7; i++)
            {
                Position leftFront = new Position((byte)(position.BCol - i), (byte)(position.BRow + i));

                Tower tower = this[leftFront];
                if (tower == null || tower.Count > 0)
                {
                    break;
                }
                else
                {
                    actions.Add(new Action(position, leftFront));
                }
            }
            for (int i = 1; i < 7; i++)
            {
                Position leftBack = new Position((byte)(position.BCol - i), (byte)(position.BRow - i));

                Tower tower = this[leftBack];
                if (tower == null || tower.Count > 0)
                {
                    break;
                }
                else
                {
                    actions.Add(new Action(position, leftBack));
                }
            }
            for (int i = 1; i < 7; i++)
            {
                Position rightFront = new Position((byte)(position.BCol + i), (byte)(position.BRow + i));

                Tower tower = this[rightFront];
                if (tower == null || tower.Count > 0)
                {
                    break;
                }
                else
                {
                    actions.Add(new Action(position, rightFront));
                }
            }
            for (int i = 1; i < 7; i++)
            {
                Position rightBack = new Position((byte)(position.BCol + i), (byte)(position.BRow - i));

                Tower tower = this[rightBack];
                if (tower == null || tower.Count > 0)
                {
                    break;
                }
                else
                {
                    actions.Add(new Action(position, rightBack));
                }
            }

            return actions;
        }

        private List<Action> generalHop(Position position)
        {
            List<Action> actions = new List<Action>();

            for (int i = 1; i < 7; i++)
            {
                Position leftFront = new Position((byte)(position.BCol - i), (byte)(position.BRow + i));

                Tower tower = this[leftFront];

                if (tower == null)
                {
                    break;
                }

                if (tower.Count > 0 && tower.Peek().Color != this[position].Peek().Color)
                {
                    Position p = new Position(leftFront);
                    p.BCol--;
                    p.BRow++;
                    Tower t = this[p];
                    if (t != null && t.Count == 0)
                    {
                        actions.Add(new Action(position, leftFront, p));
                    }
                    break;
                }
            }
            for (int i = 1; i < 7; i++)
            {
                Position leftBack = new Position((byte)(position.BCol - i), (byte)(position.BRow - i));

                Tower tower = this[leftBack];
                if (tower == null)
                {
                    break;
                }

                if (tower.Count > 0 && tower.Peek().Color != this[position].Peek().Color)
                {
                    Position p = new Position(leftBack);
                    p.BCol--;
                    p.BRow--;
                    Tower t = this[p];
                    if (t != null && t.Count == 0)
                    {
                        actions.Add(new Action(position, leftBack, p));
                    }
                    break;
                }
            }
            for (int i = 1; i < 7; i++)
            {
                Position rightFront = new Position((byte)(position.BCol + i), (byte)(position.BRow + i));

                Tower tower = this[rightFront];
                if (tower == null)
                {
                    break;
                }

                if (tower.Count > 0 && tower.Peek().Color != this[position].Peek().Color)
                {
                    Position p = new Position(rightFront);
                    p.BCol++;
                    p.BRow++;
                    Tower t = this[p];
                    if (t != null && t.Count == 0)
                    {
                        actions.Add(new Action(position, rightFront, p));
                    }
                    break;
                }
            }
            for (int i = 1; i < 7; i++)
            {
                Position rightBack = new Position((byte)(position.BCol + i), (byte)(position.BRow - i));

                Tower tower = this[rightBack];
                if (tower == null)
                {
                    break;
                }

                if (tower.Count > 0 && tower.Peek().Color != this[position].Peek().Color)
                {
                    Position p = new Position(rightBack);
                    p.BCol++;
                    p.BRow--;
                    Tower t = this[p];
                    if (t != null && t.Count == 0)
                    {
                        actions.Add(new Action(position, rightBack, p));
                    }
                    break;
                }
            }

            return actions;
        }

        public override bool Equals(object obj)
        {
            return this == obj as Board;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
