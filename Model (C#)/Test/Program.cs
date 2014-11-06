using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Laska;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(TokenColor.WHITE, Board.DEFAULT);
            Console.WriteLine(board);
        }
    }
}
