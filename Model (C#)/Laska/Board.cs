﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public static class TokenColorExtension
    {
        public static int Direction(this TokenColor color)
        {
            return color == TokenColor.WHITE ? 1 : -1;
        }

        public static Row Goal(this TokenColor color)
        {
            return color.Direction() == 1 ? Row.Max : Row.Min;
        }
    }

    public class Board
    {
        public delegate void OutLog(string line);

        public const string EMPTY = ",,,,/,,,/,,,,//,,,,/,,,/,,,,";
        public const string DEFAULT = "w,w,w,w,/w,w,w,/w,w,w,w,//b,b,b,b,/b,b,b,/b,b,b,b,/";

        public readonly Tower[,] Towers = new Tower[7, 7];

        public TokenColor Turn;

        public Position? LockedPosition;

        public static OutLog Log = Console.WriteLine;

        public Board()
            : this(TokenColor.WHITE)
        {

        }

        public Board(TokenColor turn)
            : this(turn, EMPTY)
        {

        }

        public Board(string value, TokenColor turn)
            : this(turn, value)
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
                        Towers[j, (i)] = new Tower("");
                        Towers[j, (i)].Position.BCol = (byte)j;
                        Towers[j, (i)].Position.BRow = (byte)(i);
                    }
                    continue;
                }
                string[] row = rows[i].Split(',');
                int count = 0;
                for (int j = i % 2; j < 7 && count < row.Length; j += 2, ++count)
                {
                    Towers[j, (i)] = new Tower(row[count]);
                    Towers[j, (i)].Position.BCol = (byte)j;
                    Towers[j, (i)].Position.BRow = (byte)(i);
                }
                for (int j = i % 2 + count * 2; j < 7; j += 2)
                {
                    Towers[j, (i)] = new Tower("");
                    Towers[j, (i)].Position.BCol = (byte)j;
                    Towers[j, (i)].Position.BRow = (byte)(i);
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

            for (int i = 0; i < 7 * 7; i += 2)
            {
                byte col = (byte) (i % 7);
                byte row = (byte)(i / 7);
                if (col < 2 && row > 0)
                {
                    builder.Append("/");
                }
                Tower tower = this[new Position(col, row)];
                if (tower != null)
                {
                    builder.Append(tower.ToString());
                }
                builder.Append(",");
            }

            Log(builder.ToString());

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

        public Board doMove(Move move)
        {
            Log("Move: " + move.ToString());
            Board board = new Board(Turn, ToString());
            move.Perform(board);
            return board;
        }

        public HashSet<Move> possMoves()
        {
            HashSet<Move> moves = new HashSet<Move>();
            for (int i = 0; i < 7 * 7; i++)
            {
                int x = i % 7;
                int y = i / 7;
                List<Move> actions = GetValidActions((byte)x, (byte)y);
                foreach (Move action in actions)
                {
                    moves.Add(action);
                }
            }
            return moves;
        }

        public List<Move> GetValidActions(byte x, byte y)
        {
            return GetValidActions(new Position(x, y));
        }

        public List<Move> GetValidActions(Position position)
        {
            List<Move> actions = new List<Move>(2);

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

        private List<Move> soldierWalk(Position position)
        {
            int direction = this[position].Peek().Color.Direction();
            List<Move> actions = new List<Move>(2);

            Position left = new Position(position);
            left.BCol -= 1;
            left.BRow = (byte)(left.BRow + direction);
            if (left.IsValid)
            {
                Tower leftTower = this[left];
                if (leftTower == null || leftTower.Count == 0)
                {
                    actions.Add(new Move(position, left));
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
                    actions.Add(new Move(position, right));
                }
            }
            

            return actions;
        }

        private List<Move> soldierHop(Position position)
        {
            TokenColor color = this[position].Peek().Color;
            return soldierHop(position, color);
        }
        private List<Move> soldierHop(Position position, TokenColor color) 
        {
            int direction = color.Direction();
            List<Move> actions = new List<Move>(2);

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
                        actions.Add(new Move(position, left, lefter));
                        List<Move> furtherHops = soldierHop(lefter, color);
                        foreach (Move move in furtherHops)
                        {
                            move.Hops.Insert(0, left);
                            move.Hops.Insert(0, position);
                        }
                        actions.AddRange(furtherHops);
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
                        actions.Add(new Move(position, right, righter));
                        List<Move> furtherHops = soldierHop(righter, color);
                        foreach (Move move in furtherHops)
                        {
                            move.Hops.Insert(0, right);
                            move.Hops.Insert(0, position);
                        }
                        actions.AddRange(furtherHops);
                    }
                }
            }



            return actions;
        }

        private List<Move> generalWalk(Position position)
        {
            List<Move> actions = new List<Move>();

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
                    actions.Add(new Move(position, leftFront));
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
                    actions.Add(new Move(position, leftBack));
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
                    actions.Add(new Move(position, rightFront));
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
                    actions.Add(new Move(position, rightBack));
                }
            }

            return actions;
        }

        private List<Move> generalHop(Position position)
        {
            List<Move> actions = new List<Move>();

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
                        actions.Add(new Move(position, leftFront, p));
                        i++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
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
                        actions.Add(new Move(position, leftBack, p));
                        i++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
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
                        actions.Add(new Move(position, rightFront, p));
                        i++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
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
                        actions.Add(new Move(position, rightBack, p));
                        i++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
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
