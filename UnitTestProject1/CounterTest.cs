using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laska;

namespace LaskaUnitTests
{
    [TestClass]
    public class CounterTest
    {
        private Counter w, W, b, B;
        [TestInitialize]
        public void TestInit()
        {
            w.color = Colour.White; w.value = Value.Private;
            W.color = Colour.White; W.value = Value.Officer;
            b.color = Colour.Black; b.value = Value.Private;
            B.color = Colour.Black; B.value = Value.Officer;
        }
        [TestMethod]
        public void TestMethodCounter1()
        {
            Assert.AreEqual(w, new Counter('w'));
            Assert.AreEqual(W, new Counter('W'));
            Assert.AreEqual(b, new Counter('b'));
            Assert.AreEqual(B, new Counter('B'));
            Assert.IsTrue(w == new Counter('w'));
            Assert.IsFalse(w != new Counter('w'));
            Assert.AreEqual(w.ToString(), "w");
            Assert.AreEqual(W.ToString(), "W");
            Assert.AreEqual(b.ToString(), "b");
            Assert.AreEqual(B.ToString(), "B");
        }
    }
}
