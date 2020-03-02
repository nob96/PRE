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

        [TestMethod]
        public void GetConnectnessLevel()
        {
            Hand hand = new Hand();
            string flop = "Ac 4d 3s";
            int expectedConnectnessLevel = 1;

            Assert.AreEqual(expectedConnectnessLevel, hand.GetConnectnessLevel(flop));
        }

        [TestMethod]
        public void IsQuads()
        {
            Hand hand = new Hand();
            string gameCards = "3s 3h 3d 3c Ad";
            bool expected = true;

            Assert.AreEqual(expected, hand.IsQuads(gameCards));
        }
    }
}
