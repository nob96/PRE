using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace PRE
{
    public class Analyse
    {
        public void Run(RunWindow runWindow)
        {
            BitmapImage imageDone = new BitmapImage(new Uri("/assets/done.png", UriKind.Relative));
            Program.ActiveReport activeReport = new Program.ActiveReport();

            activeReport.ReadHeaders(Program.Config.ActiveReport);
            activeReport.ReadRecords(Program.Config.ActiveReport, 1);
            runWindow.ReadingIPReport.Source = imageDone;

            activeReport.CalculateCategories();
            activeReport.CalculateActionFrequency();
            runWindow.CategorizingFlop.Source = imageDone;
            runWindow.CategorizingHand.Source = imageDone;
            runWindow.CalculatingFreq.Source = imageDone;

            activeReport.Export(Program.Config.DestinationFolderCalculated + @"\calculated.csv");
            runWindow.ExportingCalculated.Source = imageDone;

            Program.ActiveReport inactiveReport = new Program.ActiveReport();
            inactiveReport.ReadHeaders(Program.Config.PathOOP);
            inactiveReport.ReadRecords(Program.Config.PathOOP, 1);

            Program.ActiveReport globalReport = new Program.ActiveReport();
            globalReport.ReadHeaders(Program.Config.PathPIO, 3);
            globalReport.ReadRecords(Program.Config.PathPIO, 4);

            Program.Summary summary = new Program.Summary();
            summary.FormatRecords(activeReport.Records);

            summary.CalculateEquityRanges(activeReport.Records);
            summary.CalculateEquityRanges(inactiveReport.Records);

            summary.CalculateNutsAdvantage(activeReport.Records);
            summary.CalculateNutsAdvantage(inactiveReport.Records);

            summary.CalculateCombos(activeReport.Records);
            summary.AddGlobalReport(globalReport.Records);
            runWindow.SummingUp.Source = imageDone;

            summary.Export(Program.Config.DestinationFolderSummary + @"\summary.csv");
            runWindow.ExportingSummary.Source = imageDone;

            runWindow.ResultCalculated.Content = "Calculated available in: " + Program.Config.DestinationFolderCalculated + @"\calculated.csv";
            runWindow.ResultSummary.Content = "Summary available in: " + Program.Config.DestinationFolderSummary + @"\summary.csv";
            

        }
    }
}