using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Laska
{
    class Program
    {
        static void Main(string[] args)
        {
            ArtificialPlayer ai = new ArtificialPlayer();
            Board b = new Board(Colour.White);
            b.Init();
            ISet<Move> mov;
            Move[] movarr;
            Regex regex = new Regex("^[0-9]+$");
            string s;
            do
            {
                Console.WriteLine(b.Print());
                mov = b.possMoves();
                if (mov.Count == 0) Console.WriteLine("White Lost");
                else
                {
                    movarr = mov.ToArray();
                    for (int i = 0; i < movarr.Length; i++)
                        Console.WriteLine("{0}: {1}, {2}", i, movarr[i].ToString(), ai.Minimax(b.doMove(movarr[i]), 10, true));
                    do
                    {
                        s = Console.ReadLine();
                    }
                    while (!regex.Match(s).Success);
                    Move m = movarr[Convert.ToInt32(s)];
                    b = b.doMove(m);
                    Console.WriteLine(b.Print());
                    mov = b.possMoves();
                    if (mov.Count == 0) Console.WriteLine("Black Lost");
                    else
                    {
                        movarr = mov.ToArray();
                        for (int i = 0; i < movarr.Length; i++)
                            Console.WriteLine("{0}: {1}, {2}", i, movarr[i].ToString(), ai.Minimax(b.doMove(movarr[i]), 10, false));
                        do
                        {
                            s = Console.ReadLine();
                        }
                        while (!regex.Match(s).Success);
                        m = movarr[Convert.ToInt32(s)];
                        b = b.doMove(m);
                    }
                }
            }
            while (mov.Count != 0);
        }
    }
}
