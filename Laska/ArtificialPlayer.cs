using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laska
{
    public class ArtificialPlayer
    {
        public int Minimax(Board node, int depth, bool maximizing)
        {
            int result = 0;
            if (depth == 0 || IsTerminal(node))
            {
                result = Heuristic(node, maximizing);
            }
            else if (maximizing)
            {
                int bestValue = int.MinValue;
                var moves = node.possMoves();
                foreach (var move in moves)
                {
                    var val = Minimax(node.doMove(move), depth - 1, false);
                    bestValue = Math.Max(val, bestValue);
                }
                result = bestValue;
            }
            else
            {
                int bestValue = int.MaxValue;
                var moves = node.possMoves();
                foreach (var move in moves)
                {
                    var val = Minimax(node.doMove(move), depth - 1, true);
                    bestValue = Math.Min(val, bestValue);
                }
                result = bestValue;
            }
            return result;
        }

        public Result MinimaxAsync(Board node, int depth, bool maximizing)
        {
            Result result = new Result();

            Thread thread = new Thread(() =>
            {
                int ret = Minimax(node, depth, maximizing);
                result.Value = ret;
            });

            thread.Start();

            return result;
        }

        private bool IsTerminal(Board node)
        {
            return node.possMoves().Count == 0;
        }

        private int Heuristic(Board node, bool maximizing)
        {
            int sum = 0;

            foreach (var tower in node.Squares)
            {
                if (tower == null || tower.Count == 0)
                {
                    continue;
                }
                if (node.Turn == tower.Peek().color)
                {
                    sum += tower.GradeTower();
                }
                else
                {
                    sum -= tower.GradeTower();
                }
            }

            if (maximizing)
            {
                return sum;
            }
            else
            {
                return -sum;
            }
        }

        public class Result
        {
            public int? Value;

            public bool Finished { get { return Value.HasValue;  } }
        }
    }

    public static class Extensions
    {
        public static int GradeTower(this Tower tower)
        {
            if (tower.Count > 0 && tower.Peek().value == Value.Officer)
            {
                return tower.Count * 2;
            }
            return tower.Count;
        }

        public static ulong Hash (this Board board)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var valueBytes = Encoding.Default.GetBytes(board.ToString());
                var md5HashBytes = md5.ComputeHash(valueBytes);
                return BitConverter.ToUInt64(md5HashBytes, 0);
            }
        }
    }
}
