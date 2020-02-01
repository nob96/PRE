using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PRE.Program
{
    public class Calc
    {
        private Data Data;

        public Calc()
        {
            this.Data = Data.Instance;
            
        }

        public void Calculate()
        {
            for (int i = 1; i < this.Data.Records.Count; i++)
            {
                float weightIP = float.Parse(this.Data.Records[i]["Weight IP"]);

                foreach (string header in this.Data.Headers)
                {
                    //Calculated nicht berücksichtigen, da diese erst gefüllt werden. EV Felder werden nicht berechnet.
                    if (this.Data.NonCalculatedHeaders.Contains(header) == false && header.Contains("calculated") == false && header.Contains("EV") == false)
                    {
                        float freq = float.Parse(this.Data.Records[i][header]);
                        float calculatedFreq = (freq / 100) * weightIP;
                        this.Data.Records[i][header + " calculated"] = calculatedFreq.ToString();
                    }
                }
            }
        }
    }
}
