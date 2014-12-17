using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laska
{
    public struct Position
    {
        public Row Row;
        public Column Col;

        public byte BRow
        {
            get
            {
                return (byte)(Row - 1);
            }

            set
            {
                Row = (Row)(++value);
            }
        }

        public byte BCol
        {
            get
            {
                return (byte)Col;
            }

            set
            {
                Col = (Column)value;
            }
        }

        public bool IsValid
        {
            get
            {
                return Row >= Row.Min && Row <= Row.Max && Col >= Column.A && Col <= Column.G;
            }
        }

        public Position(byte x, byte y)
        {
            Col = (Column)(x);
            Row = (Row)(y + 1);
        }

        public Position(Position pos) : this(pos.BCol, pos.BRow)
        {

        }

        public override string ToString()
        {
            return Col + " " + Row;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is Position)
            {
                Position pos = (Position)obj;
                return pos.Col == Col && pos.Row == Row;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Position Convert(string pos)
        {
            Array values = Enum.GetValues(typeof(Column));
            Column[] columns = new Column[values.Length];
            values.CopyTo(columns, 0);
            Column? column = new Column?();
            foreach (Column col in columns)
            {
                if (Enum.GetName(typeof(Column), col).ToLower() == Char.ToLower(pos[0]).ToString())
                {
                    column = col;
                }
            }
            if (column.HasValue)
            {
                int row;
                if (!int.TryParse(pos[1].ToString(), out row))
                {
                    throw new ArgumentException("Invalid row: " + pos);
                }
                Position p = new Position();
                p.Col = column.Value;
                p.BRow = (byte)(row - 1);
                return p;
            }
            else
            {
                throw new ArgumentException("Invalid column: " + pos);
            }
        }
    }

    public enum Column
    {
        A = 0,
        B,
        C,
        D,
        E,
        F,
        G
    }

    public enum Row : byte
    {
        Min = 1,
        Max = 7
    }
}
