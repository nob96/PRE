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

        public List<string> HeaderList
        {
            get;
            private set;
        }

        public void Read()
        {
            /*using(StreamReader reader = new StreamReader(this.filename))
            {
                int index = 0;

                while(reader.Peek() > -1)
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    string? line = reader.ReadLine();




                }
            }*/
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
                        this.HeaderList = new List<string>(line.Split(','));
                        break;
                    }

                    currentPosition++;
                }
            }
        }
    }
}
