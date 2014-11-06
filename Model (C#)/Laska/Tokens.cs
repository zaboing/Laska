using System;

namespace Laska
{
    public struct Token
    {
        public const char WHITE_SOLDIER = 'w';
        public const char WHITE_GENERAL = 'W';
        public const char BLACK_SOLDIER = 'b';
        public const char BLACK_GENERAL = 'B';

        public TokenColor Color;
        public TokenValue Value;

        public char Tag
        {
            get
            {
                if (Color == TokenColor.WHITE)
                {
                    if (Value == TokenValue.SOLDIER)
                    {
                        return WHITE_SOLDIER;
                    }
                    else
                    {
                        return WHITE_GENERAL;
                    }
                }
                else
                {
                    if (Value == TokenValue.SOLDIER)
                    {
                        return BLACK_SOLDIER;
                    }
                    else
                    {
                        return BLACK_GENERAL;
                    }
                }
            }

            set
            {
                Color = GetColor(value);
                Value = GetValue(value);
            }
        }

        public Token(char tag)
        {
            Color = GetColor(tag);
            Value = GetValue(tag);
        }

        public Token(TokenColor color, TokenValue value)
        {
            Color = color;
            Value = value;
        }

        public override string ToString()
        {
            return Tag.ToString();
        }

        public static bool operator ==(Token t, Token t1)
        {
            return t.Tag == t1.Tag;
        }

        public static bool operator !=(Token t, Token t1)
        {
            return !(t == t1);
        }

        public static TokenColor GetColor(char tag)
        {
            if (tag == WHITE_SOLDIER || tag == WHITE_GENERAL)
            {
                return TokenColor.WHITE;
            }
            else if (tag == BLACK_SOLDIER || tag == BLACK_GENERAL)
            {
                return TokenColor.BLACK;
            }
            else
            {
                throw new ArgumentException("Not a valid tag: " + tag);
            }
        }

        public static TokenValue GetValue(char tag)
        {
            if (tag == WHITE_SOLDIER || tag == BLACK_SOLDIER)
            {
                return TokenValue.SOLDIER;
            }
            else if (tag == WHITE_GENERAL || tag == BLACK_GENERAL)
            {
                return TokenValue.GENERAL;
            }
            else
            {
                throw new ArgumentException("Not a valid tag: " + tag);
            }
        }
    }

    public enum TokenColor
    {
        WHITE = 0, 
        BLACK
    }

    public enum TokenValue
    {
        SOLDIER = 0,
        GENERAL
    }
}
