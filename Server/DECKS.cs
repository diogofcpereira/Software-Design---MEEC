using System;
using System.Collections.Generic;

namespace SERVER_TEST
{
    public class Pack
    {
        public List<Card> Deck { get; } = new List<Card>();
        public int PTrump { get; set; }

        public void CreateDeck()
        {
            for (int i = (int)Naipe.Hearts; i <= (int)Naipe.Diamonds; i++)
            {
                for (int j = (int)Valor.Two; j <= (int)Valor.Ace; j++)
                {
                    Card c = new Card((Naipe)i, (Valor)j);
                    Deck.Add(c);
                }
            }
        }

        public void ShuffleDeck()
        {
            Random random = new Random();
            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card value = Deck[k];
                Deck[k] = Deck[n];
                Deck[n] = value;
            }
        }

        public Card ChooseTrump()
        {
            return Deck[Deck.Count - 1];
        }

        public void AssignHand(Player p)
        {
            for (int i = 0; i < 10; i++)
            {
                p.Hand.Add(Deck[Deck.Count - 1]);
                Deck.RemoveAt(Deck.Count - 1);
            }
        }
    }
}