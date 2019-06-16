using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoPoker
{
    class Deck
    {
        public Globals cardValues = new Globals();
        public List<Card> cardsOnBoard = new List<Card>();
        public void GetRandomNumber() // generating cards
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Console.WriteLine("==========================================================================");
            for (int i = 1; i <= 5; i++)
            {
                Array suits = Enum.GetValues(typeof(Suits));
                int cardValue = rnd.Next(1, 14); // generate number from 1 to 13
                string cardSuit = suits.GetValue(rnd.Next(0, 4)).ToString(); // generate suit
                bool isCardUnique = !cardsOnBoard.Any(c => c.Value == cardValue && c.Suit == cardSuit);
                if (isCardUnique)
                {
                    cardsOnBoard.Add(new Card() { Value = cardValue, Suit = cardSuit });
                    cardValues.RemovedCards.Add(new Card() { Value = cardValue, Suit = cardSuit });
                    if (i<5)
                        Console.Write($"{ConvertToName(cardValue)} {cardSuit}  || ");
                    else
                        Console.Write($"{ConvertToName(cardValue)} {cardSuit}");
                }
                else
                      i--;
            }
            Console.WriteLine("\n"+"==========================================================================");
        }
        public void DrawCards()
        {
            string heldCards="";
            Console.WriteLine("You can held cards(1,2,3,4,5) or D to draw");
            Console.WriteLine("==========================================================================");
            heldCards = Console.ReadLine();
            while (heldCards != "D")
            {
                Globals.splitedCards = heldCards.Split(',');
                for (int i = 1; i <= 5; i++)
                {
                    if (Globals.splitedCards.Contains(i.ToString()))
                    {
                        Console.WriteLine(i + "-HELD");
                    }
                }
                heldCards=AfterHeld();
            }
        }
        public string ConvertToName(int cardNumber)
        {
            switch (cardNumber)
            {
                case 1:
                    return "Ace";  
                case 11:
                    return "Jack";
                case 12:
                    return "Queen";
                case 13:
                    return "King";
                default:
                    return cardNumber.ToString();
            }
        }
        public string AfterHeld()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 1; i <= 5; i++)
            {
                if (!Globals.splitedCards.Contains(i.ToString()))
                {
                    Array suits = Enum.GetValues(typeof(Suits));
                    int cardValue = rnd.Next(1, 14); // generate number from 1 to 13
                    string cardSuit = suits.GetValue(rnd.Next(0, 4)).ToString(); // generate suit
                    bool isCardUnique = !cardValues.RemovedCards.Any(c => c.Value == cardValue && c.Suit == cardSuit);
                    if (isCardUnique)
                    {
                        var tempCard = cardsOnBoard.ElementAt(i - 1);
                        tempCard.Suit = cardSuit;
                        tempCard.Value = cardValue;
                        cardsOnBoard[i-1] = tempCard;
                        cardValues.RemovedCards.Add(tempCard);
                    }
                }
            }
            return "D";
        }
    }
}
