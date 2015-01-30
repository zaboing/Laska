using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Laska;

namespace LaskaUnitTests
{
    [TestClass]
    public class MoveTest
    {
        [TestMethod]
        public void TestMethodMove1()
        {
            Assert.AreEqual(new File('a'),new File(0));
            Assert.AreEqual(new File('a'), File.a);
            Assert.AreEqual(new Rank('1'), new Rank(0));
            Assert.AreEqual(new Rank('1'), Rank._1);
        }
    }
}
