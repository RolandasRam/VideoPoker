using System.Collections.Generic;

namespace VideoPoker
{
    class Globals
    {
        public List<Card> cardList = new List<Card>();
        public static string[] splitedCards = new string[5];
        public List<Card> RemovedCards = new List<Card>();
        public static int PlayerMoney = 100;
        public static int Bet = 1;
    }
}
