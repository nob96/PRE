using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PRE
{
    public static class Analyse
    {
        public static void Run(RunWindow runWindow)
        {
            //Read
            Program.Reader reader = new Program.Reader();
            reader.Filename = Program.Config.PathIP;
            reader.ReadHeader();
            reader.ReadRecords();
            runWindow.ReadingIPReport.Source = new BitmapImage(new Uri(@"/assets/done.png", UriKind.Relative));

            //Prepare Data-Object
            Program.Data data = Program.Data.Instance;
            data.PrepareIPHeaders();
            data.PrepareIPRecords();

            Program.Flop flop = new Program.Flop();
            flop.Categorize();

            Program.Hand hand = new Program.Hand();
            hand.Categorize();

            Program.Calc calc = new Program.Calc();
            calc.Calculate();

            Program.Exporter exporter = new Program.Exporter();
            exporter.Destination = Program.Config.DestinationFolderCalculated + @"\first.csv";
            exporter.Export();

            //MessageBox.Show(JsonConvert.SerializeObject(data.Records[2]));

        }
    }
}
