using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Laska
{
    class Program
    {
        static void Main(string[] args)
        {
            ArtificialPlayer ai = new ArtificialPlayer();
            Board b = new Board(Colour.White);
            b.Init();
            HashSet<Move> mov;
            //Move[] movarr;
            Regex regex = new Regex("^[0-9]+$");
            do
            {
                Console.WriteLine(b.Print());
                mov = b.possMoves();
                if (mov.Count == 0) Console.WriteLine("White Lost");
                else
                {
                    var ordered = mov.OrderBy(move => ai.Minimax(b.doMove(move), 5, true));
                    Move m = ordered.First();
                    /*
                    movarr = mov.ToArray();
                    for (int i = 0; i < movarr.Length; i++)
                        Console.WriteLine("{0}: {1}, {2}", i, movarr[i].ToString(), ai.Minimax(b.doMove(movarr[i]), 8, false));
                    do
                    {
                        s = Console.ReadLine();
                    }
                    while (!regex.Match(s).Success);
                    Move m = movarr[Convert.ToInt32(s)];*/
                    b = b.doMove(m);
                    Console.WriteLine(b.Print());
                    mov = b.possMoves();
                    if (mov.Count == 0) Console.WriteLine("Black Lost");
                    else
                    {
                        //movarr = mov.ToArray();
                        ordered = mov.OrderBy(move => ai.Minimax(b.doMove(move), 4, true));
                        m = ordered.First();
                        /*
                        for (int i = 0; i < movarr.Length; i++)
                            Console.WriteLine("{0}: {1}, {2}", i, movarr[i].ToString(), ai.Minimax(b.doMove(movarr[i]), 8, false));
                        do
                        {
                            s = Console.ReadLine();
                        }
                        while (!regex.Match(s).Success);
                        m = movarr[Convert.ToInt32(s)];*/
                        b = b.doMove(m);
                    }
                }
            }
            while (mov.Count != 0);
        }
    }
}
