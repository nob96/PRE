using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PRETest
{
    [TestClass]
    public class TestData
    {

        private PRE.Program.Data Data;
        private List<string> IPHeader;
        private Dictionary<int, Dictionary<string, string>> IPREcord;

        public TestData()
        {
            this.Data = PRE.Program.Data.Instance;

            this.IPHeader = new List<string>();
            this.IPHeader.Add("Flop");
            this.IPHeader.Add("Hand");
            this.IPHeader.Add("IP Equity");
            this.IPHeader.Add("IP EV");
            this.IPHeader.Add("Weight IP");
            this.IPHeader.Add("RAISE 113 Freq");
            this.IPHeader.Add("RAISE 113 EV");

            this.IPREcord = new Dictionary<int, Dictionary<string, string>>();
            Dictionary<string, string> kVRecord = new Dictionary<string, string>();
            kVRecord.Add("Flop", "Qs,2h,5c");

            this.IPREcord.Add(0, kVRecord);
        }

        [TestMethod]
        public void PrepareIPHeaders()
        {
            this.Data.ClearHeaders();
            this.Data.ClearRecords();

            this.Data.Headers = this.IPHeader;
            this.Data.PrepareIPHeaders();

            Assert.IsTrue(this.Data.Headers.Contains("HAND_CATEGORY"));
            Assert.IsTrue(this.Data.Headers.Contains("FLOP_CATEGORY"));
            Assert.IsTrue(this.Data.Headers.Contains("RAISE 113 Freq calculated"));
        }

        [TestMethod]
        public void PrepareIPRecords()
        {
            this.Data.ClearHeaders();
            this.Data.ClearRecords();

            this.Data.Headers = this.IPHeader;
            this.Data.Records = this.IPREcord;
            this.Data.PrepareIPHeaders();
            this.Data.PrepareIPRecords();

            Assert.IsTrue(this.Data.Records[0].ContainsKey("RAISE 113 Freq calculated"));
        }


    }
}
