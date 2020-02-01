using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace PRE.Program
{
    public class Exporter
    {
        public string Destination
        {
            get;
            set;
        }

        private Data Data;

        public Exporter()
        {
            this.Data = Data.Instance;
        }

        public void Export()
        {
            this.WriteHeaders();
            this.WriteRecords();
            
        }

        private void WriteHeaders()
        {
            using (StreamWriter ioWriter = new StreamWriter(this.Destination))
            using (CsvWriter csvWriter = new CsvWriter(ioWriter))
            {
                MessageBox.Show(JsonConvert.SerializeObject(this.Data.Headers));
                for (int i = 0; i < this.Data.Headers.Count; i++)
                {
                    csvWriter.WriteField(this.Data.Headers[i]);
                }

                csvWriter.NextRecord();
            }
        }

        private void WriteRecords()
        {

            using (StreamWriter ioWriter = new StreamWriter(this.Destination, append: true))
            using (CsvWriter csvWriter = new CsvWriter(ioWriter))
            {

                for (int i = 1; i < this.Data.Records.Count; i++)
                {
                    foreach(var field in this.Data.Records[i])
                    {
                        csvWriter.WriteField(field.Value);
                    }

                    csvWriter.NextRecord();
                }
            }
        }
    }
}
