using System;
using System.Collections.Generic;

namespace SERVER_TEST
{
    public class Player
    {
        public List<Card> Hand { get; } = new List<Card>();
        public int ID { get; set; }
        public int Team { get; set; }
        public string nick;
        public Card PlayCard()
        {
            Card card;
            card = Hand[Hand.Count - 1];
            Hand.RemoveAt(Hand.Count - 1);

            return card;
        }

        public void CreatePlayer(int playerId)
        {
            ID = playerId;
            if (playerId == 1 || playerId == 3)
                Team = 1;

            if (playerId == 2 || playerId == 4)
                Team = 2;
        }
    }
}