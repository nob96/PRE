using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PRE.Program
{
    public class Data
    {
        public static readonly Data Instance = new Data();

        public Dictionary<int, Dictionary<string, string>> Records;
        public List<string> Headers;

        public List<string> NonCalculatedHeaders
        {
            get;
            private set;
        }

        private Dictionary<int, int> EquityRange;

        private Data() 
        {
            this.Records = new Dictionary<int, Dictionary<string, string>>();
            this.NonCalculatedHeaders =  new List<string>(); 
            this.Headers = new List<string>();
            this.EquityRange = new Dictionary<int, int>();

            this.NonCalculatedHeaders.Add("Flop");
            this.NonCalculatedHeaders.Add("Hand");
            this.NonCalculatedHeaders.Add("IP EQR");
            this.NonCalculatedHeaders.Add("IP Equity");
            this.NonCalculatedHeaders.Add("IP EV");
            this.NonCalculatedHeaders.Add("Weight IP");
            this.NonCalculatedHeaders.Add("CATEGORY");
            this.NonCalculatedHeaders.Add("FLOP_CATEGORY");
            this.NonCalculatedHeaders.Add("HAND_CATEGORY");

            this.EquityRange.Add(100, 95);
            this.EquityRange.Add(95, 90);
            this.EquityRange.Add(90, 85);
            this.EquityRange.Add(85, 80);
            this.EquityRange.Add(80, 75);
            this.EquityRange.Add(75, 70);
            this.EquityRange.Add(70, 65);
            this.EquityRange.Add(65, 60);
            this.EquityRange.Add(60, 55);
            this.EquityRange.Add(55, 50);
            this.EquityRange.Add(50, 45);
            this.EquityRange.Add(45, 40);
            this.EquityRange.Add(40, 35);
            this.EquityRange.Add(35, 30);
            this.EquityRange.Add(30, 25);
            this.EquityRange.Add(25, 20);
            this.EquityRange.Add(20, 15);
            this.EquityRange.Add(15, 10);
            this.EquityRange.Add(10, 5);
            this.EquityRange.Add(5, 0);
        }

        

        public void PrepareIPRecords()
        {
            for (int i = 0; i < this.Records.Count; i++)
            {
                foreach(string header in this.Headers)
                {
                    if (this.Records[i].ContainsKey(header) == false)
                    {
                        this.Records[i].Add(header, "");
                    }
                }
            }
        }

        public void PrepareIPHeaders()
        {
            this.Headers.Add("FLOP_CATEGORY");
            this.Headers.Add("HAND_CATEGORY");
            this.PrepareCalculatedHeaders();
        }

        public void ClearRecords()
        {
            this.Records.Clear();
        }

        public void ClearHeaders()
        {
            this.Headers.Clear();
        }

        private void PrepareCalculatedHeaders()
        {
            List<string> tmp = new List<string>();

            foreach(string header in this.Headers)
            {
                //Expected values werden nicht gebraucht
                if(this.NonCalculatedHeaders.Contains(header) == false && header.Contains("EV") == false)
                {
                    tmp.Add(header + " calculated");
                }
            }

            this.Headers = this.Headers.Concat(tmp).ToList();
        }
    }
}
