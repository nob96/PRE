using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace PRE.Program
{
    class Summary
    {
        private Dictionary<string, Dictionary<string, string>> _records;
        private Dictionary<int, int> EquityRange;
        public Dictionary<string, Dictionary<string, string>> Records
        {
            get
            {
                return this._records;
            }
            set
            {
                this._records = value;
            }
        }

        public Summary(Dictionary<int, Dictionary<string, string>> activeRecords)
        {
            this._records = new Dictionary<string, Dictionary<string, string>>();
            this.EquityRange = new Dictionary<int, int>();


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

            this.FormatRecords(activeRecords);


        }

        public void CalculateEquityRanges(Dictionary<int, Dictionary<string, string>> activeRecords, Dictionary<int, Dictionary<string, string>> inactiveRecords)
        {
            
            //active Records
            foreach (var flop in this._records.Keys)
            {
                for (int i = 1; i < activeRecords.Count; i++)
                {
                    string equityKey = activeRecords[i].ContainsKey("IP Equity") ? "IP Equity" : "OOP Equity";
                    string type = activeRecords[i].ContainsKey("IP Equity") ? "IP" : "OOP";

                    if (activeRecords[i]["Flop"] == flop)
                    {
                        float equity = float.Parse(activeRecords[i][equityKey]);

                        foreach (KeyValuePair<int, int> range in this.EquityRange)
                        {
                            string ipKey = type + " " + range.Key.ToString() + "-" + range.Value.ToString() + "%";


                            if (equity >= range.Value && equity <= range.Key)
                            {
                                int newValue = int.Parse(this._records[flop][ipKey]) + 1;
                                this._records[flop][ipKey] = newValue.ToString();
                            }
                        }
                    }
                }
            }

            //inactive Records
            foreach (var flop in this._records.Keys)
            {
                for (int i = 1; i < inactiveRecords.Count; i++)
                {
                    string equityKey = inactiveRecords[i].ContainsKey("OOP Equity") ? "OOP Equity" : "IP Equity";
                    string type = inactiveRecords[i].ContainsKey("OOP Equity") ? "OOP" : "IP";

                    if (inactiveRecords[i]["Flop"] == flop)
                    {
                        float equity = float.Parse(inactiveRecords[i][equityKey]);

                        foreach (KeyValuePair<int, int> range in this.EquityRange)
                        {
                            string oopKey = type + " " + range.Key.ToString() + "-" + range.Value.ToString() + "%";


                            if (equity >= range.Value && equity <= range.Key)
                            {
                                int newValue = int.Parse(this._records[flop][oopKey]) + 1;
                                this._records[flop][oopKey] = newValue.ToString();
                            }
                        }
                    }
                }
            }
        }

        public void CalculateCombos(Dictionary<int, Dictionary<string, string>> activeRecord)
        {
            Hand hand = new Hand();
            for (int i = 1; i < activeRecord.Count; i++)
            {
                string flop = activeRecord[i]["Flop"];
                string gameCards = activeRecord[i]["Flop"] + " " + hand.FormatHand(activeRecord[i]["Hand"]);
                string handCategory = hand.GetCategory(gameCards);


                foreach (var field in activeRecord[i])
                {

                    if (field.Key.Contains("calculated") && field.Key.Contains("Freq"))
                    {
                        string key = handCategory + " " + field.Key.Replace("Freq", "Combos");
                        float newValue = float.Parse(this._records[flop][key]) + float.Parse(field.Value);
                        this._records[flop][key] = newValue.ToString();

                    }
                }
            }
        }

        public void AddGlobalReport(Dictionary<int, Dictionary<string, string>> globalReport)
        {
            for (int i = 0; i < globalReport.Count - 1; i++)
            {
                string flop = globalReport[i]["Flop"];

                foreach (var field in globalReport[i])
                {
                    if (this._records[flop].ContainsKey(field.Key) == false)
                    {
                        this._records[flop].Add(field.Key, field.Value);
                    }
                }
            }
        }

        public void Export(string destination)
        {
            //Headers
            using (StreamWriter ioWriter = new StreamWriter(destination))
            using (CsvWriter csvWriter = new CsvWriter(ioWriter))
            {
                csvWriter.WriteField("Flop");

                foreach (var flopKey in this._records.Keys)
                {

                    foreach (var key in this._records[flopKey].Keys)
                    {
                        csvWriter.WriteField(key);
                    }

                    break;
                }

                csvWriter.NextRecord();
            }

            //Records
            using (StreamWriter ioWriter = new StreamWriter(destination, append: true))
            using (CsvWriter csvWriter = new CsvWriter(ioWriter))
            {
                foreach (var singleRecord in this._records)
                {
                    string flop = singleRecord.Key;
                    //Flop
                    csvWriter.WriteField(flop);

                    foreach (var singleRecordData in this._records[flop])
                    {
                        csvWriter.WriteField(singleRecordData.Value);
                    }

                    csvWriter.NextRecord();
                }

                csvWriter.NextRecord();
            }
        }

        private void FormatRecords(Dictionary<int, Dictionary<string, string>> activeRecords)
        {
            List<string> flopList = new List<string>();

            //Flop list
            for (int i = 1; i < activeRecords.Count; i++)
            {
                flopList.Add(activeRecords[i]["Flop"]);
            }

            foreach (var flop in flopList.Distinct().ToList())
            {
                this._records.Add(flop, new Dictionary<string, string>());
            }

            //Flop Category
            Flop objFlop = new Flop();

            foreach (var flop1 in flopList.Distinct().ToList())
            {
                string flopCategory = objFlop.GetCategory(flop1);
                this._records[flop1].Add("FLOP_CATEGORY", flopCategory);
            }

            //Calced combos
            Hand hand = new Hand();
            List<string> tmp = new List<string>();

            for (int i = 1; i < activeRecords.Count; i++)
            {

                string gameCards = activeRecords[i]["Flop"] + " " + hand.FormatHand(activeRecords[i]["Hand"]);
                string handCategory = hand.GetCategory(gameCards);

                foreach (var field in activeRecords[i])
                {

                    if (field.Key.Contains("EV") == false && field.Key.Contains("calculated"))
                    {
                        string key = handCategory + " " + field.Key.Replace("Freq", "Combos");

                        if (tmp.Contains(key) == false)
                        {
                            tmp.Add(key);
                        }
                    }

                }
            }

            foreach (var flop in flopList.Distinct().ToList())
            {
                foreach (var value in tmp)
                {
                    this._records[flop].Add(value, "0");
                }

            }

            //Equity range
            foreach (var flop in flopList.Distinct().ToList())
            {

                foreach (KeyValuePair<int, int> range in this.EquityRange)
                {
                    string ipKey = "IP " + range.Key.ToString() + "-" + range.Value.ToString() + "%";
                    string oopKey = "OOP " + range.Key.ToString() + "-" + range.Value.ToString() + "%";

                    this._records[flop].Add(ipKey, "0");
                    this._records[flop].Add(oopKey, "0");
                }
            }

        }
    }
}
