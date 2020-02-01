using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace PRE.Program
{
    public class Reader
    {
        public string Filename
        {
            get;
            set;
        }

        private Data data;

        public Reader()
        {
            this.data = Data.Instance;   
        }

        public void ReadRecords()
        {
            using(StreamReader reader = new StreamReader(this.Filename))
            {
                int index = 0;

                while(reader.Peek() > -1)
                {

                    Dictionary<string, string> row = new Dictionary<string, string>();
                    string? line = reader.ReadLine();

                    for(int i = 0; i < this.data.Headers.Count; i++)
                    {
                        string[] rowValues = line.Split(',');
                        row.Add(this.data.Headers[i], rowValues[i]);
                    }

                    this.data.Records.Add(index, row);
                    index++;
                }

                reader.Close();
            }
        }

        public void ReadHeader(int headerPosition = 1)
        {
            int currentPosition = 1;

            using (var reader = new StreamReader(this.Filename))
            {
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();

                    if (currentPosition == headerPosition)
                    {
                        this.data.Headers = new List<string>(line.Split(','));
                        break;
                    }

                    currentPosition++;
                }
            }
        }
    }
}
