using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VideoPoker
{
    class Game
    {
        public void StartGame()
        {
            Deck deck = new Deck();
            Instructions();
            Console.ReadKey();
            Console.WriteLine();
            deck.GetRandomNumber(); // get first 5 cards
            deck.DrawCards();  // cards after replacement
            Hand hand = new Hand(deck.cardsOnBoard);
            hand.WinCheck(); // Check Win/Lose
            Console.WriteLine("Press any key to try again");
            Console.WriteLine("Press ESC to quit");
            ConsoleKeyInfo cki = Console.ReadKey();
            if (cki.Key == ConsoleKey.Escape)
                Environment.Exit(1); // exits game
            else
                Console.Clear();
                StartGame();  // continue the game
        }
        public void Instructions()
        {
            Console.WriteLine("{0,8} {1,12}", "Hand","Prize");
            Console.WriteLine("==========================================================================");
            Console.WriteLine("{0,0} {1,8}", "Royal Flush", "800");
            Console.WriteLine("{0,0} {1,4}", "Straight Flush", "50");
            Console.WriteLine("{0,0} {1,4}", "Four Of A Kind", "25");
            Console.WriteLine("{0,0} {1,7}", "Full House", "9");
            Console.WriteLine("{0,0} {1,12}", "Flush", "6");
            Console.WriteLine("{0,0} {1,9}", "Straight", "4");
            Console.WriteLine("{0,0} {1,2}", "Three Of A Kind", "3");
            Console.WriteLine("{0,0} {1,9}", "Two Pair", "2");
            Console.WriteLine("{0,0} {1,2}", "Jacks or Better", "1");
            Console.WriteLine("{0,0} {1,8}", "All Other", "0");
            Console.WriteLine("==========================================================================");
            Console.WriteLine("Play begins by pressing any key. The player is then given 5 cards and has the opportunity to discard" +
                " one or more of them in exchange for new ones drawn from the same virtual deck." +
                " After the draw, the machine pays out if the hand or hands played match one of the winning combinations, " +
                "which are posted in the pay table.");
        }

    }
}