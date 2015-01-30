using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Laska
{
    public struct File
    {
        private byte col;
        public byte Col { get { return col;} }
        public File(char r)
        {
            if (r < 'a' || r > 'g') throw new InvalidOperationException("char in File");
            col = (byte) (r - 'a');
        }
        public File(byte r)
        {
            if (r > 6) throw new InvalidOperationException("byte in File");
            col = r;
        }
        public File(int r)
            : this((byte)r)
        {
        }
        public override string ToString()
        {
            return "" + (char)(col + 'a');
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return col == ((File)obj).col;
        }
        public override int GetHashCode()
        {
            return col;
        }        
        public readonly static File a = new File(0);
        public readonly static File b = new File(1);
        public readonly static File c = new File(2);
        public readonly static File d = new File(3);
        public readonly static File e = new File(4);
        public readonly static File f = new File(5);
        public readonly static File g = new File(6);
    };
    public struct Rank
    {
        private byte row;
        public byte Row{ get { return row; } }
        public Rank(char r)
        {
            if (r < '1' || r > '7') throw new InvalidOperationException("char in Rank");
            row = (byte)(r - '1');
        }
        public Rank(byte r)
        {
            if (r > 6) throw new InvalidOperationException("byte in Row");
            row = r;
        }
        public Rank(int r)
            : this((byte)r)
        {
        }
        public override string ToString()
        {
            return "" + (char)(row + '1');
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return row == ((Rank)obj).row;
        }
        public override int GetHashCode()
        {
            return row;
        }
        public readonly static Rank _1 = new Rank(0);
        public readonly static Rank _2 = new Rank(1);
        public readonly static Rank _3 = new Rank(2);
        public readonly static Rank _4 = new Rank(3);
        public readonly static Rank _5 = new Rank(4);
        public readonly static Rank _6 = new Rank(5);
        public readonly static Rank _7 = new Rank(6);
    };
    public struct Pos
    {
        private Rank row;
        public Rank Row { get { return row; } }
        private File col;
        public File Col { get { return col; } }
        public Pos(File c, Rank r)
        {
            col = c;
            row = r;
        }
        public Pos(byte r, byte c)
        {
            row = new Rank(r);
            col = new File(c);
        }
        public Pos NorthEast()
        {
            if (col.Col < 6 && row.Row < 6)
                return new Pos(new File((byte)(col.Col+1)),new Rank((byte)(row.Row+1)));
            else throw new IndexOutOfRangeException("NorthEast Pos");
        }
        public Pos NorthWest()
        {
            if (col.Col > 0 && row.Row < 6)
                return new Pos(new File((byte)(col.Col-1)),new Rank((byte)(row.Row+1)));
            else throw new IndexOutOfRangeException("NorthWest Pos");
        }
        public Pos SouthEast()
        {
            if (col.Col < 6 && row.Row > 0)
                return new Pos(new File((byte)(col.Col+1)),new Rank((byte)(row.Row-1)));
            else throw new IndexOutOfRangeException("SouthEast Pos");
        }
        public Pos SouthWest()
        {
            if (col.Col > 0 && row.Row > 0)
                return new Pos(new File((byte)(col.Col-1)),new Rank((byte)(row.Row-1)));
            else throw new IndexOutOfRangeException("SouthWest Pos");
        }
        public override string ToString()
        {
            return col.ToString() + row.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return row.Equals(((Pos)obj).row) && col.Equals(((Pos)obj).col);
        }
        public override int GetHashCode()
        {
            return row.Row + col.Col;
        }

        public readonly static Pos a1 = new Pos(File.a, Rank._1);
        public readonly static Pos a3 = new Pos(File.a, Rank._3);
        public readonly static Pos a5 = new Pos(File.a, Rank._5);
        public readonly static Pos a7 = new Pos(File.a, Rank._7);
        public readonly static Pos b2 = new Pos(File.b, Rank._2);
        public readonly static Pos b4 = new Pos(File.b, Rank._4);
        public readonly static Pos b6 = new Pos(File.b, Rank._6);
        public readonly static Pos c1 = new Pos(File.c, Rank._1);
        public readonly static Pos c3 = new Pos(File.c, Rank._3);
        public readonly static Pos c5 = new Pos(File.c, Rank._5);
        public readonly static Pos c7 = new Pos(File.c, Rank._7);
        public readonly static Pos d2 = new Pos(File.d, Rank._2);
        public readonly static Pos d4 = new Pos(File.d, Rank._4);
        public readonly static Pos d6 = new Pos(File.d, Rank._6);
        public readonly static Pos e1 = new Pos(File.e, Rank._1);
        public readonly static Pos e3 = new Pos(File.e, Rank._3);
        public readonly static Pos e5 = new Pos(File.e, Rank._5);
        public readonly static Pos e7 = new Pos(File.e, Rank._7);
        public readonly static Pos f2 = new Pos(File.f, Rank._2);
        public readonly static Pos f4 = new Pos(File.f, Rank._4);
        public readonly static Pos f6 = new Pos(File.f, Rank._6);
        public readonly static Pos g1 = new Pos(File.g, Rank._1);
        public readonly static Pos g3 = new Pos(File.g, Rank._3);
        public readonly static Pos g5 = new Pos(File.g, Rank._5);
        public readonly static Pos g7 = new Pos(File.g, Rank._7);
    }
    public class Move
    {
        IList<Pos> positions = new List<Pos>();
        public Move(string m)
        {
            Regex regex = new Regex("^([a-g][1-7])+$");
            Match match = regex.Match(m);
            if (!match.Success)
                throw new InvalidOperationException("Pattern in Move");
            for (int i = 0; i < m.Length; i += 2)
                positions.Add(new Pos(new File(m[i]), new Rank(m[i+1])));
        }
        public Move()
        {
        }
        public Move(Pos a, Pos b)
        {
            positions.Add(a);
            positions.Add(b);
        }
        public Move(Pos a, Pos b, Pos c)
        {
            positions.Add(a);
            positions.Add(b);
            positions.Add(c);
        }
        public Pos this[int p]
        {
            get { return positions[p]; }
        }
        public int Count { get { return positions.Count; } }
		public Pos lastPos()
		{
			return positions[positions.Count-1];
		}
		public void Add (Pos p)
		{
			positions.Add(p);
		}
        public Move Add (Move m)
        {
            //if (positions.Count > 0 && lastPos().Equals(m.positions[0])) throw new InvalidOperationException("add moves!!!");
            Move mov = new Move();
            foreach (Pos p in positions)
                mov.positions.Add(p);
            if (positions.Count == 0) mov.positions.Add(m[0]);
            for (int i = 1; i < m.Count; i++ )
                mov.positions.Add(m[i]);
            return mov;
        }
        public bool PreviousCapture(Pos p)
        {
            if (positions.Count > 1)
            for (int i = 1; i < positions.Count; i += 2)
                if (p.Equals(positions[i])) return true;
            return false;
        }
        public Move Reverse()
        {
            Move m = new Move();
            for (int i = positions.Count - 1; i >= 0; i--)
                m.Add(positions[i]);
            return m;
        }
        public override string ToString()
        {
            string s = "";
            foreach (Pos p in positions)
                s += p.ToString();
            return s;
        }
        public override bool Equals(object obj)
        {
            Move m = obj as Move;
            if (m == null) return false;
            if (positions.Count != m.positions.Count) return false;
            for (int i = 0; i < positions.Count; i++)
                if (!positions[i].Equals(m.positions[i])) return false;
            return true;
        }
        public override int GetHashCode()
        {
            int i = 0;
            foreach (Pos p in positions) i += p.GetHashCode();
            return i;
        }
        // single Moves for White
        public readonly static Move a1b2 = new Move(Pos.a1, Pos.b2);
        public readonly static Move c1b2 = new Move(Pos.c1, Pos.b2);
        public readonly static Move c1d2 = new Move(Pos.c1, Pos.d2);
        public readonly static Move e1d2 = new Move(Pos.e1, Pos.d2);
        public readonly static Move e1f2 = new Move(Pos.e1, Pos.f2);
        public readonly static Move g1f2 = new Move(Pos.g1, Pos.f2);

        public readonly static Move b2a3 = new Move(Pos.b2, Pos.a3);
        public readonly static Move b2c3 = new Move(Pos.b2, Pos.c3);
        public readonly static Move d2c3 = new Move(Pos.d2, Pos.c3);
        public readonly static Move d2e3 = new Move(Pos.d2, Pos.e3);
        public readonly static Move f2e3 = new Move(Pos.f2, Pos.e3);
        public readonly static Move f2g3 = new Move(Pos.f2, Pos.g3);

        public readonly static Move a3b4 = new Move(Pos.a3, Pos.b4);
        public readonly static Move c3b4 = new Move(Pos.c3, Pos.b4);
        public readonly static Move c3d4 = new Move(Pos.c3, Pos.d4);
        public readonly static Move e3d4 = new Move(Pos.e3, Pos.d4);
        public readonly static Move e3f4 = new Move(Pos.e3, Pos.f4);
        public readonly static Move g3f4 = new Move(Pos.g3, Pos.f4);

        public readonly static Move b4a5 = new Move(Pos.b4, Pos.a5);
        public readonly static Move b4c5 = new Move(Pos.b4, Pos.c5);
        public readonly static Move d4c5 = new Move(Pos.d4, Pos.c5);
        public readonly static Move d4e5 = new Move(Pos.d4, Pos.e5);
        public readonly static Move f4e5 = new Move(Pos.f4, Pos.e5);
        public readonly static Move f4g5 = new Move(Pos.f4, Pos.g5);

        public readonly static Move a5b6 = new Move(Pos.a5, Pos.b6);
        public readonly static Move c5b6 = new Move(Pos.c5, Pos.b6);
        public readonly static Move c5d6 = new Move(Pos.c5, Pos.d6);
        public readonly static Move e5d6 = new Move(Pos.e5, Pos.d6);
        public readonly static Move e5f6 = new Move(Pos.e5, Pos.f6);
        public readonly static Move g5f6 = new Move(Pos.g5, Pos.f6);

        public readonly static Move b6a7 = new Move(Pos.b6, Pos.a7);
        public readonly static Move b6c7 = new Move(Pos.b6, Pos.c7);
        public readonly static Move d6c7 = new Move(Pos.d6, Pos.c7);
        public readonly static Move d6e7 = new Move(Pos.d6, Pos.e7);
        public readonly static Move f6e7 = new Move(Pos.f6, Pos.e7);
        public readonly static Move f6g7 = new Move(Pos.f6, Pos.g7);
    
        // single captures for White

        public readonly static Move a1b2c3 = new Move(Pos.a1, Pos.b2, Pos.c3);
        public readonly static Move c1d2e3 = new Move(Pos.c1, Pos.d2, Pos.e3);
        public readonly static Move c1b2a3 = new Move(Pos.c1,Pos.b2,Pos.a3);
        public readonly static Move e1f2g3 = new Move(Pos.e1,Pos.f2,Pos.g3);
        public readonly static Move e1d2c3 = new Move(Pos.e1,Pos.d2,Pos.c3);
        public readonly static Move g1f2e3 = new Move(Pos.g1,Pos.f2,Pos.e3);

        public readonly static Move b2c3d4 = new Move(Pos.b2,Pos.c3,Pos.d4);
        public readonly static Move d2c3b4 = new Move(Pos.d2,Pos.c3,Pos.b4);
        public readonly static Move d2e3f4 = new Move(Pos.d2,Pos.e3,Pos.f4);
        public readonly static Move f2e3d4 = new Move(Pos.f2,Pos.e3,Pos.d4);

        public readonly static Move a3b4c5 = new Move(Pos.a3, Pos.b4, Pos.c5);
        public readonly static Move c3d4e5 = new Move(Pos.c3, Pos.d4, Pos.e5);
        public readonly static Move c3b4a5 = new Move(Pos.c3, Pos.b4, Pos.a5);
        public readonly static Move e3f4g5 = new Move(Pos.e3, Pos.f4, Pos.g5);
        public readonly static Move e3d4c5 = new Move(Pos.e3, Pos.d4, Pos.c5);
        public readonly static Move g3f4e5 = new Move(Pos.g3, Pos.f4, Pos.e5);

        public readonly static Move b4c5d6 = new Move(Pos.b4, Pos.c5, Pos.d6);
        public readonly static Move d4c5b6 = new Move(Pos.d4, Pos.c5, Pos.b6);
        public readonly static Move d4e5f6 = new Move(Pos.d4, Pos.e5, Pos.f6);
        public readonly static Move f4e5d6 = new Move(Pos.f4, Pos.e5, Pos.d6);

        public readonly static Move a5b6c7 = new Move(Pos.a5, Pos.b6, Pos.c7);
        public readonly static Move c5d6e7 = new Move(Pos.c5, Pos.d6, Pos.e7);
        public readonly static Move c5b6a7 = new Move(Pos.c5, Pos.b6, Pos.a7);
        public readonly static Move e5f6g7 = new Move(Pos.e5, Pos.f6, Pos.g7);
        public readonly static Move e5d6c7 = new Move(Pos.e5, Pos.d6, Pos.c7);
        public readonly static Move g5f6e7 = new Move(Pos.g5, Pos.f6, Pos.e7);

    }
}
