using System;
using System.Collections.Generic;

namespace Laska
{
    public class PosEnumerator : IEnumerator<Pos>
    {
        int pos = -2;
        public Pos Current
        {
            get {
                if (pos >= 0) return new Pos((byte)(pos / 7), (byte)(pos % 7));
                else throw new InvalidOperationException("BoardEnumerator: use MoveNext()");
            }
        }
        public void Dispose()
        {
        }
        public bool MoveNext()
        {
            pos+=2;
            if (pos > 48) return false;
            else return true;
        }
        public void Reset()
        {
            pos = -2;
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }
    }
    public class Board : IEnumerable<Pos>
    {
        private static readonly Move[] forwardMoves ={Move.a1b2,Move.c1b2,Move.c1d2,Move.e1d2,Move.e1f2,Move.g1f2,
                                              Move.b2a3,Move.b2c3,Move.d2c3,Move.d2e3,Move.f2e3,Move.f2g3,
                                              Move.a3b4,Move.c3b4,Move.c3d4,Move.e3d4,Move.e3f4,Move.g3f4,
                                              Move.b4a5,Move.b4c5,Move.d4c5,Move.d4e5,Move.f4e5,Move.f4g5,
                                              Move.a5b6,Move.c5b6,Move.c5d6,Move.e5d6,Move.e5f6,Move.g5f6,
                                              Move.b6a7,Move.b6c7,Move.d6c7,Move.d6e7,Move.f6e7,Move.f6g7};
        private static readonly Move[] forwardCaptures = {Move.a1b2c3, Move.c1d2e3,Move.c1b2a3,Move.e1f2g3,Move.e1d2c3,Move.g1f2e3,
                                                Move.b2c3d4,Move.d2c3b4,Move.d2e3f4,Move.f2e3d4,
                                                Move.a3b4c5,Move.c3d4e5,Move.c3b4a5,Move.e3f4g5,Move.e3d4c5,Move.g3f4e5,
                                                Move.b4c5d6,Move.d4c5b6,Move.d4e5f6,Move.f4e5d6,
                                                Move.a5b6c7,Move.c5d6e7,Move.c5b6a7,Move.e5f6g7,Move.e5d6c7,Move.g5f6e7};
        public static readonly Move[] backwardMoves;
        public static readonly Move[] backwardCaptures;
        public static readonly Move[] allMoves;
        public static readonly Move[] allCaptures;
        public static readonly IDictionary<Pos, HashSet<Move>> forwardMovesMap;
        public static readonly IDictionary<Pos, HashSet<Move>> backwardMovesMap;
        public static readonly IDictionary<Pos, HashSet<Move>> allMovesMap;
        public static readonly IDictionary<Pos, HashSet<Move>> forwardCapturesMap;
        public static readonly IDictionary<Pos, HashSet<Move>> backwardCapturesMap;
        public static readonly IDictionary<Pos, HashSet<Move>> allCapturesMap;
        static Board()
        {
            backwardMoves = new Move[forwardMoves.Length];
            for (int i = 0; i < backwardMoves.Length; i++)
                backwardMoves[i] = forwardMoves[i].Reverse();
            backwardCaptures = new Move[forwardCaptures.Length];
            for (int i = 0; i < backwardCaptures.Length; i++)
                backwardCaptures[i] = forwardCaptures[i].Reverse();
            allMoves = new Move[2*forwardMoves.Length];
            forwardMoves.CopyTo(allMoves, 0);
            backwardMoves.CopyTo(allMoves, forwardMoves.Length);
            allCaptures = new Move[2 * forwardCaptures.Length];
            forwardCaptures.CopyTo(allCaptures, 0);
            backwardCaptures.CopyTo(allCaptures, forwardCaptures.Length);
            initMap(out forwardMovesMap, forwardMoves);
            initMap(out backwardMovesMap, backwardMoves);
            initMap(out allMovesMap, allMoves);
            initMap(out forwardCapturesMap, forwardCaptures);
            initMap(out backwardCapturesMap, backwardCaptures);
            initMap(out allCapturesMap, allCaptures);
        }
        static void initMap(out IDictionary<Pos, HashSet<Move>> dict, Move[] moves)
        {
            dict = new Dictionary<Pos, HashSet<Move>>();
            foreach (Move m in moves)
            {
                Pos p = m[0];
                HashSet<Move> mv;
                if (!dict.TryGetValue(p, out mv))
                {
                    mv = new HashSet<Move>();
                    dict.Add(p, mv);
                }
                mv.Add(m);
                dict[p] = mv;
            }
        }
        private Tower[,] squares = new Tower[7, 7];
        public Tower[,] Squares { get { return squares; } }
        private Colour turn;
        public Colour Turn { get { return turn; } }
        public Board()
        {
            turn = Colour.White;
        }
        public Board(Colour c)
        {
            turn = c;
        }
        public Board(Board b)
        {
            turn = b.turn;
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 7; row++)
                {
                    if ((col + row) % 2 == 0)
                        if (b.squares[row, col] == null) squares[row, col] = null;
                        else squares[row, col] = new Tower(b.squares[row, col]);
                }
            }

        }
        public Board(string s, Colour c)
        {
            turn = c;
            string[] rows = s.Split('/');
            if (rows.Length != 7) throw new IndexOutOfRangeException("wrong no of rows: " + rows.Length);
            for (int i = 0; i < 7; i++)
            {
                string[] cols = rows[6-i].Split(',');
                if (cols.Length < 3 + (i % 2 == 0 ? 1 : 0)) throw new IndexOutOfRangeException("wrong no of cols: " + cols.Length + " in row: " + i);
                for (int j = 0; j < 3 + (i % 2 == 0 ? 1 : 0); j++)
                {
                    squares[i, (i % 2 == 0 ? 0 : 1) + 2 * j] = (cols[j].Length == 0 ? null : new Tower(cols[j]));
                }
            }
        }
        public Tower this[Pos p]
        {
            get {
                return squares[p.Row.Row, p.Col.Col];
            }
            set {
                squares[p.Row.Row, p.Col.Col] = value;
            }
        }
        public IEnumerator<Pos> GetEnumerator()
        {
            return new PosEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            string s = "";
            for (int row = 6; row >= 0; row--) 
            {
                for (int col = 0; col < 7; col++)
                {
                    if ((col + row) % 2 == 0)
                    {
                        Tower t = squares[row, col];
                        if (t != null) s += t.ToString();
                        s += ",";
                    }
                }
                s += "/";
            }
            return s;
        }
        public string Print()
        {
            int maxlen = 0;
            for (int row = 6; row >= 0; row--)
            {
                for (int col = 0; col < 7; col++)
                {
                    Tower t = squares[row, col];
                    if (t != null)
                    {
                        int l = t.ToString().Length;
                        if (l > maxlen) maxlen = l;
                    }
                }
            }
            string s = (turn == Colour.White) ? "White" : "Black";
            s += " to move\n";
            for (int row = 6; row >= 0; row--)
            {
                for (int col = 0; col < 7; col++)
                {
                    Tower t = squares[row, col];
                    if (t != null) s += t.ToString();
                    int ts = (t == null) ? maxlen : maxlen - t.ToString().Length;
                    for (int i = 0; i < ts; i++) s += ' ';
                        s += ",";
                }
                s += "\n";
            }
            return s;
        }
        public void Init()
        {
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 3; row++)
                {
                    if ((col + row) % 2 == 0) squares[row, col] = new Tower("w");
                }
            }
            for (int col = 0; col < 7; col++)
            {
                for (int row = 4; row < 7; row++)
                {
                    if ((col + row) % 2 == 0) squares[row, col] = new Tower("b");
                }
            }

        }
        public void InvertTurn()
        {
            if (turn == Colour.White) turn = Colour.Black;
            else turn = Colour.White;
        }
        public override bool Equals(object obj)
        {
            return this == (obj as Board);
        }
        public override int GetHashCode()
        {
            return (int)turn;
        }
        public static bool operator ==(Board a, Board b)
        {
            if (Object.ReferenceEquals(a,null) && Object.ReferenceEquals(b,null)) return true;
            if (Object.ReferenceEquals(a,null) || Object.ReferenceEquals(b,null)) return false;
            if (a.turn != b.turn) return false;
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 7; row++)
                {
                    if (a.squares[row, col] != b.squares[row, col]) return false;
                }
            }
            return true;
       }
        public static bool operator !=(Board a, Board b)
        {
            return !(a == b);
        }
        public HashSet<Move> possCapturesExtend(Move prefix, IDictionary<Pos, HashSet<Move>> posmoves)
        {
            HashSet<Move> res = new HashSet<Move>();
            if (posmoves.ContainsKey(prefix.lastPos()))
            {
                HashSet<Move> moves = posmoves[prefix.lastPos()];
                foreach (Move m in moves)
                {
                    if (!prefix.PreviousCapture(m[1]) && this[m[1]] != null && this[m[1]].Peek().color != turn
                        && (this[m[2]] == null || m[2].Equals(prefix[0])))
                    {
                        res.UnionWith(possCapturesExtend(prefix.Add(m), posmoves));
                    }
                }
            }
            if (res.Count == 0) res.Add(prefix);
            return res;
        }
        public HashSet<Move> possCaptures(Pos p, IDictionary<Pos, HashSet<Move>> posmoves)
        {
            HashSet<Move> res = new HashSet<Move>();
            if (posmoves.ContainsKey(p))
            {
                HashSet<Move> moves = posmoves[p];
                foreach (Move m in moves)
                {
                    Tower t1 = this[m[1]];
                    Tower t2 = this[m[2]];
                    if (t1 != null && t1.Peek().color != turn
                        && t2 == null)
                    {
                        res.UnionWith(possCapturesExtend(m, posmoves));
                    }
                }
            }
            
            return res;
        }
        public HashSet<Move> possSimpleMoves(Pos p, IDictionary<Pos, HashSet<Move>> posmoves)
        {
            HashSet<Move> res = new HashSet<Move>();
            HashSet<Move> moves = posmoves[p];
            foreach (Move m in moves)
            {
                if (this[m[1]] == null)
                {
                    res.Add(m);
                }
            }
            return res;
        }
        public HashSet<Move> possMoves()
        {
            HashSet<Move> res = new HashSet<Move>();
            foreach(Pos p in this)
            {
                if (this[p] != null && this[p].Peek().color == turn)
                {
                    if (this[p].Peek().value == Value.Private)
                        if (this[p].Peek().color == Colour.White)
                            res.UnionWith(possCaptures(p, forwardCapturesMap));
                        else res.UnionWith(possCaptures(p, backwardCapturesMap));
                    else res.UnionWith(possCaptures(p, allCapturesMap));
                }
            }
            if (res.Count == 0)
            {
                foreach (Pos p in this)
                {
                    if (this[p] != null && this[p].Peek().color == turn)
                    {
                        if (this[p].Peek().value == Value.Private)
                            if (this[p].Peek().color == Colour.White)
                                res.UnionWith(possSimpleMoves(p, forwardMovesMap));
                            else res.UnionWith(possSimpleMoves(p, backwardMovesMap));
                        else res.UnionWith(possSimpleMoves(p, allMovesMap));
                    }
                }

            }
            return res;
        }
        public Board doCapture(Move m)
        {
            Board b = new Board(this);
            Tower t0 = null, t1 = null, t2 = null;
            for (int i = 0; i+1 < m.Count; i += 2)
            {
                t0 = b[m[i]];
                b[m[i]] = null;
                t1 = b[m[i+1]];
                Counter c = t1.Pop();
                if (t1.Count == 0) b[m[i+1]] = null;
                t2 = new Tower();
                t2.Push(c);
                t2.Push(t0);
                b[m[i+2]] = t2;
            }
            if (m.lastPos().Row.Row == 0 || m.lastPos().Row.Row == 6)
                if (t2.Peek().value == Value.Private)
                    t2.Promote();
            return b;
        }
        public Board doSingleMove(Move m)
        {
            Board b = new Board(this);
            b[m[1]] = b[m[0]];
            b[m[0]] = null;
			if (m[1].Row.Row == 0 || m[1].Row.Row == 6)
                if (b[m[1]].Peek().value == Value.Private)
                    b[m[1]].Promote();
            return b;
        }
        public Board doMove(Move m)
        {
            Board b;
            if (m.Count == 2)
                b = doSingleMove(m);
            else b = doCapture(m);
            b.InvertTurn();
            return b;
        }
    }
}
