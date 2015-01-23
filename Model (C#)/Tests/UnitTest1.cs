﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using Laska;

namespace LaskaUnitTests
{
    [TestClass]
    public class BoardTest
    {
        private Board initW, b;
        private ISet<Move> moves;
        [TestInitialize]
        public void Init()
        {
            initW = new Board(TokenColor.WHITE, Board.DEFAULT);
            initW.Init();
            moves = new HashSet<Move>();
            b = new Board(TokenColor.WHITE);
            b.Init();
        }
        [TestMethod]
        public void TestMethodBoardMovesInit()
        {
            moves.Add(new Move("a3b4"));
            moves.Add(new Move("c3b4"));
            moves.Add(new Move("c3d4"));
            moves.Add(new Move("e3d4"));
            moves.Add(new Move("e3f4"));
            moves.Add(new Move("g3f4"));
            Assert.IsTrue(moves.SetEquals(initW.possMoves()));
        }
        [TestMethod]
        public void TestMethodBoardPosMovesData1()
        {
            Board b = new Board(TokenColor.WHITE, "Bww,w,w,/,w,w/bw,,w,w/,,bw/b,bw,,b/bb,,b/,,b,b");
            moves.Add(new Move("a3b4"));
            moves.Add(new Move("c3b4"));
            moves.Add(new Move("c3d4"));
            moves.Add(new Move("e3d4"));
            moves.Add(new Move("e3f4"));
            moves.Add(new Move("g3f4e5"));

            var m = initW.possMoves();

            Assert.IsTrue(moves.SetEquals(b.possMoves()));
        }
        [TestMethod]
        public void TestMethodBoardPosMovesData2()
        {
            initW = initW.doMove(new Move("a3b4"));
            moves.Add(new Move("c5d4"));
            moves.Add(new Move("e5d4"));
            moves.Add(new Move("e5f4"));
            moves.Add(new Move("g5f4"));

            moves.Add(new Move("c5b4a3"));

            Assert.IsTrue(moves.SetEquals(initW.possMoves()));
        }

        [TestMethod]
        public void TestGame1()  // http://www.lasca.org/show?1VB
        {
            Move m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b2c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a5b4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6e5f4e3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c1d2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4e3d2c1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a3b4c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c1d2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c7b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3b2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a1b2c3d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b6c5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4c3b2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2e3d4c5b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d4c5b6a7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g5f4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e7f6g5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1f2g3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b2c1");
            var s = b.ToString() + " " + b.Turn;
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4");  // Zug 23 e3-d4 unzulässig???
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g5f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g1f2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f2e1d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
        }
        [TestMethod]
        public void TestGame2()  // http://www.lasca.org/show?5YD
        {
            Move m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b2c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2e3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4c5b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a1b2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a5b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3b4a5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d4c5b6a7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3b2a1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6c5b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b4c3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e7d6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));

            b = b.doMove(m);
            m = new Move("d4c3b2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g5f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6e5f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b4c3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c1b2a3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a1b2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g1f2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2e3f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2e3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b4c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c7d6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d6e7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e7f6g5f4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a5b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b6c7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b4c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6e7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e7f6g5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c7d6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3b2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6g5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            Assert.IsTrue(b.possMoves().Count == 0);
        }
        [TestMethod]
        public void TestGame3()  // http://www.lasca.org/show?67Y
        {
            Move m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b2c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a3b4c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6c5b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e5d6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c7d6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6e5f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2g3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f4e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e3f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g1f2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2g3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
        }
        [TestMethod]
        public void TestProblem1()  // http://www.lasca.org/show?6Y2
        {
            string s = "Bw,,,b/,BB,,/w,,,www/b,bw,,/,B,,w/,,wb,/,WWb,,Wbb";
            b = new Board(s, TokenColor.WHITE);
            Move m = new Move("g5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f4g3f2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g1f2e3d4c5d6e7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4e5f6g7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e7d6c5b4a3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a5b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c1d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4c5d6e5f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            Assert.IsTrue(b.possMoves().Count == 0);
        }
        [TestMethod]
        public void TestProblem2()  // http://www.lasca.org/show?3AK
        {
            string s = ",,,,/,b,,/bBB,,,,/,,w,/bBw,w,Ww,,/BBw,,,/Bwww,Wb,Wb,,";
            b = new Board(s, TokenColor.WHITE);
            Move m = new Move("c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a5b4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d6e5f4e3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            Assert.IsTrue(b.possMoves().Count == 0);
        }
        [TestMethod]
        public void TestProblem3()  // http://www.lasca.org/show?6YB
        {
            string s = "Wwwww,,,,/wb,w,Wbb,/,,,b,/,BBw,bw,/b,,,,/B,w,bb,/,,,,";
            b = new Board(s, TokenColor.BLACK);
            Move m = new Move("b2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3b4a5b6c7d6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a7b6c5d4e3f2g1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            // Assert.IsTrue(b.possMoves().Count == 0); 
        }
        [TestMethod]
        public void TestProblem4()  // http://www.lasca.org/show?6YF
        {
            string s = "WW,W,Wb,,/,wWb,,/WWb,,,B,/,bB,,/,,,W,/,,wWbb,/,BB,B,,";
            b = new Board(s, TokenColor.BLACK);
            Move m = new Move("d4e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f2e3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e3d4c5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4c5b6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c1d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e7f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g5f6e7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            Assert.IsTrue(b.possMoves().Count == 0);
        }
        [TestMethod]
        public void TestProblem5()  // http://www.lasca.org/show?6YF
        {
            string s = ",,,b,/,,bB,/,,,,/W,wwBb,bB,/W,ww,,,/w,Wwb,,/,,Bbw,Bw,";
            b = new Board(s, TokenColor.WHITE);
            Move m = new Move("d2e3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f4e3d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5d4e3f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3b2a1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("b4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d4c3b2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a3b2c1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            Assert.IsTrue(b.possMoves().Count == 0);
        }
        [TestMethod]
        public void TestLargestChange()  // http://www.lasca.org/show?6JS
        {
            string s = ",,,BBww,/WB,WB,WB,/,,,,/WB,WB,WB,/,,,,/WB,WB,WB,/,,,,";
            b = new Board(s, TokenColor.BLACK);
            Move m = new Move("g1f2e3d2c1b2a3b4c5d6e7f6g5f4e3d4c5b6a7");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            string s2 = ",,,,/B,B,B,/,,,,/B,B,B,/,,,,/B,B,B,/BBwwWWWWWWWWW,,,,";
            Board b2 = new Board(s2, TokenColor.WHITE);
            Assert.AreEqual(b, b2);
        }

        [TestMethod]
        public void TestSimple()
        {

            Move m = new Move("a3b4");
            b = b.doMove(m);
            Assert.IsFalse(b.possMoves().Contains(m));
        }

        [TestMethod]
        public void TestFoolsMate()  // http://www.lasca.org/show?6H6
        {
            Move m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e5d4c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("d2c3b4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("f6e5");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g7f6");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c3d4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("a5b4c3d2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f4");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1f2g3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g1f2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("g3f2e1");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("c1d2");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            m = new Move("e1d2c3");
            Assert.IsTrue(b.possMoves().Contains(m));
            b = b.doMove(m);
            Assert.IsTrue(b.possMoves().Count == 0);
        }
    }
}
