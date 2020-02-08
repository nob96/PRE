using Microsoft.VisualStudio.TestTools.UnitTesting;
using PRE.Program;

namespace PRETest
{
    [TestClass]
    public class ReportTest
    {
        private ActiveReport ActiveReport;

        public ReportTest()
        {
            this.ActiveReport = new ActiveReport();

            this.ActiveReport.ReadHeaders(@"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv");
            this.ActiveReport.ReadRecords(@"C:\Users\noahb\OneDrive\Dokumente\Poker\PRE\TestData\report_IP_Full.csv", 1);
        }

        [TestMethod]
        public void CalculateActionFrequency()
        {
            string expectedActionFreqKey = "BET 18 Freq calculated";
            string expectedActionFreqValue = "0.4262159";

            this.ActiveReport.CalculateActionFrequency();

            //Berechnung des ersten Datensatzes prüfen, index = 0
            Assert.AreEqual(expectedActionFreqValue, this.ActiveReport.Records[0][expectedActionFreqKey]);
        }
    }
}
