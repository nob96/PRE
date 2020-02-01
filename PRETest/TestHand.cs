using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PRETest
{
    [TestClass]
    public class TestHand
    {

        public TestHand()
        {
            
        }

        [TestMethod]
        public void GetCategory()
        {
            PRE.Program.Hand hand = new PRE.Program.Hand();
            string flopCards = "Qs 2s 5c";
            string holeCards = "AsKs";
            string cards = flopCards + " " + hand.FormatHand(holeCards);
            string expectedCategory = "Flushdraw";
            string actualCategory = hand.GetCategory(cards);

            Assert.AreEqual(expectedCategory, actualCategory);

        }

        

        


    }
}
