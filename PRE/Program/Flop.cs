using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRE.Program
{
    public class Flop
    {
        private Data Data;

        public Flop()
        {
            this.Data = Data.Instance;
        }

        public void Categorize()
        {
            for (int i = 1; i < this.Data.Records.Count; i++)
            {
                Dictionary<string, string> singleRecord = this.Data.Records[i];
                string flop = singleRecord["Flop"];
                string category = this.GetCategory(flop);
                this.Data.Records[i]["FLOP_CATEGORY"] = category;
            }
        }

        public string GetCategory(string flop)
        {
            Hand hand = new Hand();
            string[] flopCards = flop.Split(" ");
            List<char> flopColors = new List<char>();

            foreach (string flopCard in flopCards)
            {
                flopColors.Add(flopCard[1]);
            }


            if (flopColors.All(color => color == flopColors.First()))
            {
                if (hand.IsPair(flop))
                {
                    return "Paired Monotone";
                }
                else if (hand.IsStraightDraw(flop, true))
                {
                    return "Straightdraw Monotone";
                }

                return "Monotone";

            }
            else if (flopColors.Distinct().Count() == flopColors.Count())
            {
                if (hand.IsPair(flop))
                {
                    return "Paired Rainbow";
                }
                else if (hand.IsStraightDraw(flop, true))
                {
                    return "Straightdraw Rainbow";
                }

                return "Rainbow";
            }
            else
            {
                if (hand.IsPair(flop))
                {
                    return "Paired Twotone";
                }
                else if (hand.IsStraightDraw(flop, true))
                {
                    return "Straightdraw Twotone";
                }

                return "Twotone";
            }
        }
    }
}
