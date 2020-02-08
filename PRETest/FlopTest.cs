using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRE.Program;
using System.Linq;

namespace PRETest
{
    [TestClass]
    public class FlopTest
    {
        private Flop Flop;

        public FlopTest()
        {
            this.Flop = new Flop();
        }

        [TestMethod]
        public void GetCategory()
        {
            Hand hand = new Hand();
            string flop = "3s 3h 3d";
            string expectedCategory = "Rainbow";
            string expectedPaired = "NO";
            string expectedStraightdraw = "NO";
            string expectedHighCard = "3";

            int highCard = hand.ConvertCardValuesToInt(flop).Max();
            string highCardConverted = hand.ConvertIntToCard(highCard);

            Assert.AreEqual(expectedCategory, this.Flop.GetCategory(flop));
            Assert.AreEqual(expectedPaired, this.Flop.IsPaired(flop));
            Assert.AreEqual(expectedStraightdraw, this.Flop.IsStraightdraw(flop));
            Assert.AreEqual(expectedHighCard, highCardConverted);
        }
    }
}
