namespace SERVER_TEST
{
    public class Round
    {
        public Card[] PCards { get; } = new Card[4];
        public int PTurn { get; set; }
        public int TeamRoundWinner { get; set; }

        public Round()
        {
            for (int i = 0; i < 4; i++)
            {
                PCards[i] = new Card(Naipe.NULL_TYPE, Valor.NULL_VALUE);
            }

            TeamRoundWinner = 0;
            PTurn = 0;
        }

        public void ResetRound()
        {
            for (int i = 0; i < 4; i++)
            {
                PCards[i] = new Card(Naipe.NULL_TYPE, Valor.NULL_VALUE);
            }

            TeamRoundWinner = 0;
            PTurn = 0;
        }

        public void CurrentPlayer(Player[] players)
        {
            for (int i = 0; i < 4; i++)
            {
                if (PTurn < 0 || PTurn >= 4)
                    PTurn = 0;

                PCards[PTurn] = players[PTurn].PlayCard();
                PTurn++;

                if (PTurn == 4)
                {
                    PTurn = 0;
                }
            }
        }

        public void CheckRoundWinner(Card trump, Player[] players)
        {
            int winnerIndex;
            Card winnerCard;

            if (PCards[PTurn].Type == trump.Type) // If the first card played is a trump card
            {
                winnerIndex = PTurn;
                winnerCard = PCards[PTurn];
                PTurn++;

                if (PTurn >= 4)
                {
                    PTurn = 0;
                }

                for (int i = 0; i < 3; i++)
                {
                    if (PCards[PTurn].Type == trump.Type && PCards[PTurn].Value > winnerCard.Value)
                    {
                        winnerIndex = PTurn;
                        winnerCard = PCards[PTurn];
                    }

                    PTurn++;

                    if (PTurn >= 4)
                    {
                        PTurn = 0;
                    }
                }
            }
            else // If the first card played is not a trump card
            {
                winnerIndex = PTurn;
                winnerCard = PCards[PTurn];
                PTurn++;

                if (PTurn >= 4)
                {
                    PTurn = 0;
                }

                for (int i = 0; i < 3; i++)
                {
                    if (PCards[PTurn].Type == trump.Type)
                    {
                        if (PCards[PTurn].Type != winnerCard.Type)
                        {
                            winnerIndex = PTurn;
                            winnerCard = PCards[PTurn];
                        }

                        if (PCards[PTurn].Type == winnerCard.Type && PCards[PTurn].Value > winnerCard.Value)
                        {
                            winnerIndex = PTurn;
                            winnerCard = PCards[PTurn];
                        }
                    }
                    else
                    {
                        if (PCards[PTurn].Type == winnerCard.Type && PCards[PTurn].Value > winnerCard.Value)
                        {
                            winnerIndex = PTurn;
                            winnerCard = PCards[PTurn];
                        }
                    }

                    PTurn++;

                    if (PTurn >= 4)
                    {
                        PTurn = 0;
                    }
                }
            }

            PTurn = winnerIndex;
            TeamRoundWinner = players[PTurn].Team;
        }
    }
}