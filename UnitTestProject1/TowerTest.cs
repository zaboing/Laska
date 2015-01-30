using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laska;

namespace LaskaUnitTests
{
    [TestClass]
    public class TowerTest
    {
        private Counter w, W, b, B; 
        private Tower t1, t2;
        [TestInitialize]
        public void Init()
        {
            t1 = new Tower("wWbB");
            t2 = new Tower("wWbB");
            w.color = Colour.White; w.value = Value.Private;
            W.color = Colour.White; W.value = Value.Officer;
            b.color = Colour.Black; b.value = Value.Private;
            B.color = Colour.Black; B.value = Value.Officer;
        }
        [TestMethod]
        public void TestMethodTower1()
        {
            t1.Push(t1.Pop());
            Assert.AreEqual(t1,t2);
            Assert.AreEqual(t1.Peek(), w);
            Assert.AreEqual(t1.Get(3), B);
            Assert.AreEqual(t1.ToString(), "wWbB");
            t1.Pop();
            Assert.IsTrue(t1 != t2);
            Assert.IsFalse(t1 == t2);
            Assert.IsFalse(t1 == null);
            Assert.IsFalse(null == t1);
            Tower t3 = new Tower(t2);
            Assert.AreEqual(t2, t3);
            Assert.AreNotEqual(t1,"abc");
        }
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void TestMethodTower2()
        {
            t1.Get(4);
        }
    }
}
