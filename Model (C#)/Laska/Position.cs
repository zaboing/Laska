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
