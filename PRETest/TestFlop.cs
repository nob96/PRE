using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRETest
{
    [TestClass]
    public class TestFlop
    {
        public TestFlop()
        {

        }

        [TestMethod]
        public void GetCategory()
        {
            PRE.Program.Flop flop = new PRE.Program.Flop();
            string flopCards = "Qs 5s 5s";
            string expectedCategory = "Paired Monotone";
            string actualCategory = flop.GetCategory(flopCards);

            Assert.AreEqual(expectedCategory, actualCategory);
        }
    }
}
