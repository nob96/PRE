using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRE.Program;

namespace PRETest
{
    [TestClass]
    public class HandTest
    {
        private Hand Hand;

        public HandTest()
        {
            this.Hand = new Hand();
        }

        [TestMethod]
        public void GetCategory()
        {
            string expectedCategory = "Straightdraw";
            string cards = "Ac Qs Kh Tc Js";

            Assert.AreEqual(expectedCategory, this.Hand.GetCategory(cards));
        }
    }
}
