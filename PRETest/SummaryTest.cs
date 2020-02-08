using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRE.Program;
using System.Collections.Generic;

namespace PRETest
{
    [TestClass]
    public class SummaryTest
    {
        private Summary Summary;
        private ActiveReport ActiveReport;

        public SummaryTest()
        {
            this.ActiveReport = new ActiveReport();

            this.ActiveReport.ReadHeaders(@"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv");
            this.ActiveReport.ReadRecords(@"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv", 1);
            this.ActiveReport.CalculateActionFrequency();
            
            this.Summary = new Summary();
        }

        [TestMethod]
        public void FormatRecords()
        {
            string expectedFlopKey = "9s 4h 3s";
            string expectedComboKey = "Flushdraw CHECK Combos calculated";
            string expectedIPEquityKey = "IP 10-5%";
            string expectedOOPEquityKey = "OOP 100-95%";

            this.Summary.FormatRecords(this.ActiveReport.Records);

            Assert.IsTrue(this.Summary.Records.ContainsKey(expectedFlopKey));
            Assert.IsTrue(this.Summary.Records[expectedFlopKey].ContainsKey(expectedIPEquityKey));
            Assert.IsTrue(this.Summary.Records[expectedFlopKey].ContainsKey(expectedOOPEquityKey));
            Assert.IsTrue(this.Summary.Records[expectedFlopKey].ContainsKey(expectedComboKey));
        }

        [TestMethod]
        public void CalculateCombos()
        {
            string flop = "9s 4h 3s";
            string expectedComboKey = "Flushdraw CHECK Combos calculated";
            string expectedComboValue = "9.773055";

            this.Summary.FormatRecords(this.ActiveReport.Records);
            this.Summary.CalculateCombos(this.ActiveReport.Records);

            Assert.AreEqual(expectedComboValue, this.Summary.Records[flop][expectedComboKey]);
        }

        [TestMethod]
        public void CalculateEquityRanges()
        {
            string flop = "3s 3h 3d";
            string expectedEquityKey = "IP 50-45%";
            string expectedEquityValue = "10.297";

            this.Summary.FormatRecords(this.ActiveReport.Records);
            this.Summary.CalculateEquityRanges(this.ActiveReport.Records);

            Assert.AreEqual(expectedEquityValue, this.Summary.Records[flop][expectedEquityKey]);
        }

        [TestMethod]
        public void AddGlobalReport()
        {
            string expectedGlobalKey = "Global %";
            string flop = "9s 4h 3s";
            ActiveReport globalReport = new ActiveReport();

            this.Summary.FormatRecords(this.ActiveReport.Records);
            this.Summary.CalculateCombos(this.ActiveReport.Records);

            globalReport.ReadHeaders(@"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report.csv", 3);
            globalReport.ReadRecords(@"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report.csv", 4);
            this.Summary.AddGlobalReport(globalReport.Records);

            Assert.IsTrue(this.Summary.Records[flop].ContainsKey(expectedGlobalKey));
        }
    }
}
