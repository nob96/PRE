using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PRE.Program
{
    public class Hand
    {

        public string GetCategory(string cards)
        {
            if (IsFlushDraw(cards))
            {
                return "Flushdraw";
            }
            else if (IsSet(cards))
            {
                return "Set";
            }
            else if (IsQuads(cards))
            {
                return "Quads";
            }
            else if (IsStraightDraw(cards))
            {
                return "Straightdraw";
            }
            else if (IsTwoPair(cards))
            {
                return "Two_Pair";
            }
            else if (IsPair(cards))
            {
                return GetPairCategory(cards);
            }
            else
            {
                return "Nothing";
            }
        }

        public string FormatHand(string hand)
        {
            return hand.Insert(2, " ");
        }

        public bool IsStraightDraw(string cards, bool straigthDrawOnFlop = false)
        {
            return this.GetConnectnessLevel(cards) > 0;

        }

        public List<int> ConvertCardValuesToInt(string cards)
        {
            string[] gameCardsSplitted = cards.Split();
            List<int> cardValues = new List<int>();

            foreach (string gameCardValue in gameCardsSplitted)
            {
                switch (gameCardValue[0])
                {
                    case 'A':
                        {
                            cardValues.Add(14);
                            break;
                        }
                    case 'K':
                        {
                            cardValues.Add(13);
                            break;
                        }
                    case 'Q':
                        {
                            cardValues.Add(12);
                            break;
                        }
                    case 'J':
                        {
                            cardValues.Add(11);
                            break;
                        }
                    case 'T':
                        {
                            cardValues.Add(10);
                            break;
                        }
                    default:
                        {
                            cardValues.Add(int.Parse(gameCardValue[0].ToString()));
                            break;
                        }
                }

            }

            return cardValues;
        }

        public string ConvertIntToCard(int cardValue)
        {
            switch (cardValue)
            {
                case 14:
                    {
                        return "A";
                    }
                case 13:
                    {
                        return "K";
                    }
                case 12:
                    {
                        return "Q";
                    }
                case 11:
                    {
                        return "J";
                    }
                case 10:
                    {
                        return "T";
                    }
                default:
                    {
                        return cardValue.ToString();
                    }
            }
        }

        public int GetConnectnessLevel(string flop)
        {
            List<int> values = this.ConvertCardValuesToInt(flop);
            //Spezialfall A = 1
            
            if (values.Contains(14))
            {
                if(values.OrderByDescending(r => r).Skip(1).FirstOrDefault() <= 5)
                {
                    values[values.FindIndex(ind => ind.Equals(14))] = 1;
                }
            }
            

            values = values.Distinct().ToList();

            if(values.Count >= 3)
            {
                int diffrence = values.Max() - values.Min();

                switch (diffrence)
                {
                    case 2:
                        return 3;
                    case 3:
                        return 2;
                    case 4:
                        return 1;
                }
            }

            return 0;
        }

        public bool IsQuads(string gameCards)
        {
            List<char> values = new List<char>();

            foreach (string color in gameCards.Split(" "))
            {
                values.Add(color[0]);
            }

            var countPerValue = from bereichsvariable in values
                                group bereichsvariable by bereichsvariable into grouping
                                let count = grouping.Count()
                                orderby count descending
                                select new { Value = grouping.Key, Count = count };

            foreach (var x in countPerValue)
            {
                if (x.Count == 4)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSet(string gameCards)
        {
            List<char> values = new List<char>();

            foreach (string color in gameCards.Split(" "))
            {
                values.Add(color[0]);
            }

            var countPerValue = from bereichsvariable in values
                                group bereichsvariable by bereichsvariable into grouping
                                let count = grouping.Count()
                                orderby count descending
                                select new { Value = grouping.Key, Count = count };

            foreach (var x in countPerValue)
            {
                if (x.Count == 3)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFlushDraw(string cards)
        {
            List<char> colors = new List<char>();

            foreach (string color in cards.Split(" "))
            {
                colors.Add(color[1]);
            }

            var countPerColor = from bereichsvariable in colors
                                group bereichsvariable by bereichsvariable into grouping
                                let count = grouping.Count()
                                orderby count descending
                                select new { Value = grouping.Key, Count = count };

            foreach (var x in countPerColor)
            {
                if (x.Count >= 4)
                {
                    return true;
                }
            }

            return false;
        }

        private string GetPairCategory(string cards)
        {
            List<int> cardValues = ConvertCardValuesToInt(cards);
            List<int> pairList = new List<int>();

            var countPerValue = from bereichsvariable in cardValues
                                group bereichsvariable by bereichsvariable into grouping
                                let count = grouping.Count()
                                orderby count descending
                                select new { Value = grouping.Key, Count = count };

            int secondPairValue = (from number in cardValues
                                   orderby number descending
                                   select number).Distinct().Skip(1).First();



            int thirdPairValue = (from number in cardValues
                                  orderby number descending
                                  select number).Distinct().Skip(2).First();

            foreach (var value in countPerValue)
            {
                if (value.Count == 2)
                {
                    if (value.Value == cardValues.Max())
                    {
                        return "top-pair";
                    }
                    else if (value.Value == secondPairValue)
                    {
                        return "2nd-Pair";
                    }
                    else if (value.Value == thirdPairValue)
                    {
                        return "3rd-pair";
                    }
                    else
                    {
                        return "low-pair";
                    }
                }
            }
            return "Nothing";
        }

        public bool IsPair(string cards)
        {
            List<int> cardValues = ConvertCardValuesToInt(cards);

            var countPerValue = from bereichsvariable in cardValues
                                group bereichsvariable by bereichsvariable into grouping
                                let count = grouping.Count()
                                orderby count descending
                                select new { Value = grouping.Key, Count = count };

            foreach (var value in countPerValue)
            {

                if (value.Count == 2)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTwoPair(string cards)
        {
            List<int> cardValues = ConvertCardValuesToInt(cards);
            List<int> pairList = new List<int>();

            var countPerValue = from bereichsvariable in cardValues
                                group bereichsvariable by bereichsvariable into grouping
                                let count = grouping.Count()
                                orderby count descending
                                select new { Value = grouping.Key, Count = count };

            foreach (var value in countPerValue)
            {
                if (value.Count == 2)
                {
                    pairList.Add(value.Value);
                }
            }

            if (pairList.Count == 2)
            {
                return true;
            }

            return false;
        }
    }
}
