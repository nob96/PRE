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
    public class Summary
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

        public Summary()
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
        }

        public void CalculateEquityRanges(Dictionary<int, Dictionary<string, string>> records)
        {
            foreach (var flop in this._records.Keys)
            {
                for (int i = 1; i < records.Count; i++)
                {
                    string equityKey = records[i].ContainsKey("IP Equity") ? "IP Equity" : "OOP Equity";
                    string weightKey = records[i].ContainsKey("Weight IP") ? "Weight IP" : "Weight OOP";
                    string type = records[i].ContainsKey("IP Equity") ? "IP" : "OOP";

                    if (records[i]["Flop"] == flop)
                    {
                        float equity = float.Parse(records[i][equityKey]);

                        foreach (KeyValuePair<int, int> range in this.EquityRange)
                        {
                            string ipKey = type + " " + range.Key.ToString() + "-" + range.Value.ToString() + "%";


                            if (equity >= range.Value && equity <= range.Key)
                            {
                                float newValue = float.Parse(this._records[flop][ipKey]) + float.Parse(records[i][weightKey]);
                                this._records[flop][ipKey] = newValue.ToString();
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

        public void CalculateNutsAdvantage(Dictionary<int, Dictionary<string, string>> records)
        {
            float ipEquityNuts = 0;
            float oopEquityNuts = 0;
            List<string> ipEquities = new List<string>();
            List<string> oopEquities = new List<string>();

            ipEquities.Add("IP 100-95%");
            ipEquities.Add("IP 95-90%");
            ipEquities.Add("IP 90-85%");
            ipEquities.Add("IP 85-80%");

            oopEquities.Add("OOP 100-95%");
            oopEquities.Add("OOP 95-90%");
            oopEquities.Add("OOP 90-85%");
            oopEquities.Add("OOP 85-80%");

            foreach (string flop in this._records.Keys)
            {
                foreach (string ipEquityKey in ipEquities)
                {
                    ipEquityNuts += float.Parse(this._records[flop][ipEquityKey]);
                }

                foreach (string oopEquityKey in oopEquities)
                {
                    oopEquityNuts += float.Parse(this._records[flop][oopEquityKey]);
                }

                if (this._records[flop].ContainsKey("IP 100-80% Equity") == false && this._records[flop].ContainsKey("OOP 100-80% Equity") == false)
                {
                    float ipGesamt = this.GetAnzahlCombos(flop, "IP");
                    float oopGesamt = this.GetAnzahlCombos(flop, "OOP");
                    float ipNutsAdvantage = ipEquityNuts / ipGesamt;
                    float oopNutsAdvantage = oopEquityNuts / oopGesamt;
                    float nutsRatio = ipNutsAdvantage / oopNutsAdvantage;

                    this._records[flop].Add("IP 100-80% Equity", ipEquityNuts.ToString());
                    this._records[flop].Add("Gesamte Anzahl Combos IP", ipGesamt.ToString());
                    this._records[flop].Add("IP Nuts Percentage", ipNutsAdvantage.ToString());

                    this._records[flop].Add("OOP 100-80% Equity", oopEquityNuts.ToString());
                    this._records[flop].Add("Gesamte Anzahl Combos OOP", oopGesamt.ToString());
                    this._records[flop].Add("OOP Nuts Percentage", oopNutsAdvantage.ToString());

                    this._records[flop].Add("Nuts Advantage Ratio", nutsRatio.ToString());


                }

                ipEquityNuts = 0;
                oopEquityNuts = 0;
            }
        }

        private float GetAnzahlCombos(string flop, string position = "IP")
        {
            float gesamteComboAnzahl = 0;

            foreach (KeyValuePair<int, int> range in this.EquityRange)
            {
                string equityKey = position + " " + range.Key.ToString() + "-" + range.Value.ToString() + "%";
                gesamteComboAnzahl += float.Parse(this._records[flop][equityKey]);
            }

            return gesamteComboAnzahl;
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

        public void FormatRecords(Dictionary<int, Dictionary<string, string>> activeRecords)
        {
            List<string> flopList = this.GenerateUniqueFlopList(activeRecords);
            List<string> actionList = this.GenerateActionList(activeRecords);
            Hand hand = new Hand();
            Flop flop = new Flop();

            foreach (var flop1 in flopList)
            {
                this._records.Add(flop1, new Dictionary<string, string>());
                string flopCategory = flop.GetCategory(flop1);
                int highCard = hand.ConvertCardValuesToInt(flop1).Max();
                string highCardConverted = hand.ConvertIntToCard(highCard);

                this._records[flop1].Add("FLOP_CATEGORY", flopCategory);
                this._records[flop1].Add("PAIRED", flop.IsPaired(flop1));
                this._records[flop1].Add("STRAIGHTDRAW", flop.IsStraightdraw(flop1));
                this._records[flop1].Add("FLOP_HIGHCARD", highCardConverted);
                this._records[flop1].Add("CONNECTNESS_LEVEL", hand.GetConnectnessLevel(flop1).ToString());

                foreach (var value in actionList)
                {
                    this._records[flop1].Add(value, "0");
                }

                foreach (KeyValuePair<int, int> range in this.EquityRange)
                {
                    string ipKey = "IP " + range.Key.ToString() + "-" + range.Value.ToString() + "%";
                    string oopKey = "OOP " + range.Key.ToString() + "-" + range.Value.ToString() + "%";

                    this._records[flop1].Add(ipKey, "0");
                    this._records[flop1].Add(oopKey, "0");
                }
            }
        }

        private List<string> GenerateUniqueFlopList(Dictionary<int, Dictionary<string, string>> activeRecords)
        {
            List<string> flopList = new List<string>();

            for (int i = 1; i < activeRecords.Count; i++)
            {
                flopList.Add(activeRecords[i]["Flop"]);
            }

            return flopList.Distinct().ToList();
        }

        private List<string> GenerateActionList(Dictionary<int, Dictionary<string, string>> activeRecords)
        {
            Hand hand = new Hand();
            List<string> actionList = new List<string>();

            for (int i = 1; i < activeRecords.Count; i++)
            {
                string gameCards = activeRecords[i]["Flop"] + " " + hand.FormatHand(activeRecords[i]["Hand"]);
                string handCategory = hand.GetCategory(gameCards);

                foreach (var field in activeRecords[i])
                {
                    
                    if (field.Key.Contains("EV") == false && field.Key.Contains("calculated"))
                    {
                        string key = handCategory + " " + field.Key.Replace("Freq", "Combos");

                        if (actionList.Contains(key) == false)
                        {
                            actionList.Add(key);
                        }
                    }

                }
            }
            
            return actionList;
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
    }
}
