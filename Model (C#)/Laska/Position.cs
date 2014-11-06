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

        public Position(byte x, byte y)
        {
            Col = (Column)(x);
            Row = (Row)(y + 1);
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
