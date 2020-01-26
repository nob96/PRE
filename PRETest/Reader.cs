using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PRETest
{
    [TestClass]
    public class Reader
    {
        [TestMethod]
        public void ReadHeader()
        {
            PRE.Program.Reader reader = new PRE.Program.Reader();
            reader.Filename = @"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv";
            reader.ReadHeader();

            Assert.IsTrue(reader.HeaderList.Count > 1);
        }

        
    }
}
