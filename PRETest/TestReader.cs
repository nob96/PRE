using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PRETest
{
    [TestClass]
    public class TestReader
    {
        private PRE.Program.Data Data;
        private PRE.Program.Reader Reader;

        public TestReader()
        {
            this.Data = PRE.Program.Data.Instance;
            this.Reader = new PRE.Program.Reader();

        }

        [TestMethod]
        public void ReadHeader()
        {
            this.Data.ClearHeaders();
            this.Data.ClearRecords();

            this.Reader.Filename = @"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv";
            this.Reader.ReadHeader();

            Assert.IsTrue(this.Data.Headers.Count > 1);
            Assert.IsTrue(this.Data.Headers.Contains("Flop"));
        }

        [TestMethod]
        public void ReadRecords()
        {
            this.Data.ClearHeaders();
            this.Data.ClearRecords();

            this.Reader.Filename = @"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv";
            this.Reader.ReadHeader();
            this.Reader.ReadRecords();

            Assert.IsTrue(this.Data.Records.Count > 1);
            Assert.IsTrue(this.Data.Records[0].ContainsKey("Flop"));
        }

        
    }
}
