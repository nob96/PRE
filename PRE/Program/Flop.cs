using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRE.Program
{
    class Flop
    {
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
                return "Monotone";

            }
            else if (flopColors.Distinct().Count() == flopColors.Count())
            {
                return "Rainbow";
            }
            else
            {
                return "Twotone";
            }
        }

        public string IsPaired(string flop)
        {
            Hand hand = new Hand();

            if (hand.IsPair(flop))
            {
                return "YES";
            }

            return "NO";
        }

        public string IsStraightdraw(string flop)
        {
            Hand hand = new Hand();

            if (hand.IsStraightDraw(flop))
            {
                return "YES";
            }

            return "NO";
        }

    }
}
