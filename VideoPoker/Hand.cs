using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoPoker
{
    class Hand
    {
        Deck deck = new Deck();
        private List<Card> cardsOnBoard;
        public Hand(List<Card> _cardsOnBoard)
        {
            cardsOnBoard = _cardsOnBoard;
        }
        private bool RoyalFlash()
        {
            int royalFlash = 0;
            string suitCheck = cardsOnBoard[0].Suit;
            foreach (Card value in cardsOnBoard)
            {
                if (value.Value == 10 || value.Value == 11 || value.Value == 12 || value.Value == 13 || value.Value == 1)
                {
                    if (value.Suit == suitCheck)
                        royalFlash++;
                }
            }
            if (royalFlash == 5)
            {
                return true;
            }
            return false;
        }

        private bool StraightFlash()
        {
            return Straight() && Flush();

        }
        private bool FourOfAKind()
        {
            return cardsOnBoard
                .Where(c => c.Value >= 1)
                .GroupBy(c => c.Value)
                .Where(c => c.Count() == 4)
                .Count() == 1;

        }

        private bool FullHouse()
        {
            return Pair() && ThreeOfAKind();

        }
        private bool Flush()
        {
            return cardsOnBoard
                .GroupBy(c => c.Suit)
                .Count() == 1;
        }

        private bool Straight()
        {
            int valueMin = cardsOnBoard[0].Value;
            foreach (Card value in cardsOnBoard)
            {
                if (valueMin > value.Value)
                {
                    valueMin = value.Value;
                }
            }
            var tempOrdered = cardsOnBoard.OrderBy(c => c.Value).ToList();
            if (tempOrdered[0].Value == tempOrdered[1].Value - 1 &&
                tempOrdered[0].Value == tempOrdered[2].Value - 2 &&
                tempOrdered[0].Value == tempOrdered[3].Value - 3 &&
                tempOrdered[0].Value == tempOrdered[4].Value - 4)
            {
                return true;
            }
            else if (tempOrdered[0].Value == 1)
            {
                if (tempOrdered[1].Value == tempOrdered[2].Value - 1 &&

                    tempOrdered[1].Value == tempOrdered[3].Value - 2 &&
                    tempOrdered[1].Value == tempOrdered[4].Value - 3 &&
                    tempOrdered[1].Value == tempOrdered[0].Value + 9)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ThreeOfAKind()
        {
            return cardsOnBoard
                .Where(c => c.Value >= 1)
                .GroupBy(c => c.Value)
                .Where(c => c.Count() == 3)
                .Count() == 1;

        }

        private bool TwoPair()
        {
            return cardsOnBoard
                .Where(c => c.Value >= 1)
                .GroupBy(c => c.Value)
                .Where(c => c.Count() == 2)
                .Count() == 2;
        }
        private bool JacksOrBetter()
        {
            return cardsOnBoard
                .Where(c => c.Value >= 11 || c.Value == 1)
                .GroupBy(c => c.Value)
                .Where(c => c.Count() == 2)
                .Count() == 1;
        }
        private bool Pair()
        {
            return cardsOnBoard
                .Where(c => c.Value >= 1)
                .GroupBy(c => c.Value)
                .Where(c => c.Count() == 2)
                .Count() == 1;
        }
        
        public void WinCheck()
        {
            if(RoyalFlash())
            {
                ShowMenu(800, "Win","Royal Flash!");
            }
            else if(StraightFlash())
            {
                ShowMenu(50, "Win","Straight Flash!");
            }
            else if(FourOfAKind())
            {
                ShowMenu(25, "Win","Four Of A Kind!");
            }
            else if(FullHouse())
            {
                ShowMenu(9, "Win","Full House!");
            }
            else if(Flush())
            {
                ShowMenu(6, "Win","Flush!");
            }
            else if (Straight())
            {
                ShowMenu(4, "Win","Straight!");
            }
            else if (ThreeOfAKind())
            {
                ShowMenu(3, "Win","Three Of A Kind!");
            }
            else if(TwoPair())
            {
                ShowMenu(2, "Win","Two Pair!");
            }
            else if(JacksOrBetter())
            {
                ShowMenu(1,"Win", "Jacks Or Better!");
            }
            else
            {
                ShowMenu(0, "Lose","");
            }
        }

        private void ShowMenu(int MoneyWon, string GameResultText, string Combination)
        {
            Console.Clear();
            if (MoneyWon == 0)
            {
                Console.WriteLine("                               " + GameResultText);
                Console.WriteLine("==========================================================================");
                Globals.PlayerMoney -= 1;
                if(Globals.PlayerMoney<=0)
                {
                    Console.WriteLine("                         You have no money!");
                    Console.WriteLine("==========================================================================");
                    Console.WriteLine("                         Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }
            else
            {
                Console.WriteLine("                             " + Combination);
                Console.WriteLine("==========================================================================");
                Globals.PlayerMoney += 1;
            }
            for (int i = 0; i <= 4; i++)
            {
                if (i < 4)
                    Console.Write($"{deck.ConvertToName(cardsOnBoard[i].Value)} {cardsOnBoard[i].Suit}  || ");
                else
                    Console.Write($"{deck.ConvertToName(cardsOnBoard[i].Value)} {cardsOnBoard[i].Suit}");
            }
            Console.WriteLine("\n"+"==========================================================================");
            if (MoneyWon == 0)
            {
                Console.WriteLine("{0,32} {1,32}", "Bet:" + Globals.Bet, "Credit:" + Globals.PlayerMoney);
                Console.WriteLine("==========================================================================");
            }
            else
            {
                Console.WriteLine("{0,-27} {1,-27} {2,-27}", GameResultText + ":" + MoneyWon, "Bet:" + Globals.Bet, "Credit:" + Globals.PlayerMoney);
                Console.WriteLine("==========================================================================");
            }
        }



    }
}
