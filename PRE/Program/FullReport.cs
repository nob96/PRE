using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PRE.Program
{
    class FullReport : Report
    {

        public FullReport()
        {
            this.Headers = new List<string>();
            this.Records = new Dictionary<int, Dictionary<string, string>>();

        }

        public override void Export(string destination)
        {
            //Headers
            using (StreamWriter ioWriter = new StreamWriter(destination))
            using (CsvWriter csvWriter = new CsvWriter(ioWriter))
            {

                foreach (string header in this.Headers)
                {
                    csvWriter.WriteField(header);
                }

                csvWriter.NextRecord();
            }

            //Records
            using (StreamWriter ioWriter = new StreamWriter(destination, append: true))
            using (CsvWriter csvWriter = new CsvWriter(ioWriter))
            {

                foreach (var record in this.Records)
                {
                    csvWriter.WriteField(record.Value);
                    csvWriter.NextRecord();
                }
            }
        }
    }
}
