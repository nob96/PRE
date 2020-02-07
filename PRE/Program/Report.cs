﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PRE.Program
{
    abstract class Report
    {
        private List<string> _headers;
        private Dictionary<int, Dictionary<string, string>> _records;
        public List<string> Headers { get => this._headers; set { this._headers = value; } }
        public Dictionary<int, Dictionary<string, string>> Records { get => this._records; set { this._records = value; } }

        public void ReadHeaders(string filename, int headerPosition = 0)
        {
            int currentPosition = 0;

            using (var reader = new StreamReader(filename))
            {
                while (reader.Peek() > -1)
                {
                    string line = reader.ReadLine();

                    if (currentPosition == headerPosition)
                    {
                        this._headers = new List<string>(line.Split(','));
                        break;
                    }

                    currentPosition++;
                }
            }
        }

        public void ReadRecords(string filename, int recordsPosition = 0)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                int index = 0;
                int currentPosition = 0;

                while (reader.Peek() > -1)
                {
                    string? line = reader.ReadLine();

                    if (currentPosition >= recordsPosition)
                    {
                        Dictionary<string, string> row = new Dictionary<string, string>();


                        for (int i = 0; i < this.Headers.Count; i++)
                        {
                            string[] rowValues = line.Split(',');
                            row.Add(this.Headers[i], rowValues[i]);
                        }

                        this.Records.Add(index, row);
                        index++;
                    }

                    currentPosition++;
                }

                reader.Close();
            }
        }

        public void CalculateCategories()
        {
            Flop flop = new Flop();
            Hand hand = new Hand();
            this.Headers.Add("FLOP_CATEGORY");
            this.Headers.Add("HAND_CATEGORY");

            //Flop
            for (int i = 0; i < this.Records.Count; i++)
            {
                Dictionary<string, string> singleRecord = this.Records[i];
                string flopCards = singleRecord["Flop"];
                string category = flop.GetCategory(flopCards);
                this.Records[i]["FLOP_CATEGORY"] = category;
            }

            //Hand
            for (int i = 0; i < this.Records.Count; i++)
            {
                string cards = this.Records[i]["Flop"] + " " + hand.FormatHand(this.Records[i]["Hand"]);

                this.Records[i]["HAND_CATEGORY"] = hand.GetCategory(cards);
            }
        }

        public void CalculateFreqActions()
        {
            //Append headers
            List<string> tmp = new List<string>();
            foreach (string header in this.Headers)
            {
                if (header.Contains("Freq"))
                {
                    tmp.Add(header + " calculated");
                }
            }

            this.Headers = this.Headers.Concat(tmp).ToList();
            string weightKey = this.Records[1].ContainsKey("Weight IP") ? "Weight IP" : "Weight OOP";

            //Calc Freq
            for (int i = 0; i < this.Records.Count; i++)
            {
                float weightIP = float.Parse(this.Records[i][weightKey]);

                foreach (string header in this.Headers)
                {
                    //Nur unberechnete Freq Actions berücksichtigen
                    if (header.Contains("calculated") == false && header.Contains("Freq"))
                    {
                        float freq = float.Parse(this.Records[i][header]);
                        float calculatedFreq = (freq / 100) * weightIP;
                        this.Records[i][header + " calculated"] = calculatedFreq.ToString();
                    }
                }
            }
        }

        public abstract void Export(string destination);
    }
}