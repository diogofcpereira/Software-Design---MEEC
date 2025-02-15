namespace SERVER_TEST
{
    public class Game
    {
        public Player[] Players { get; set; 
        } = new Player[4];
        public Card GameTrump { get; set; }
        public int T1Wins { get; set; }
        public int T2Wins { get; set; }
        public int CurrentRound { get; set; }
        public int T1Pont { get; set; }
        public int T2Pont { get; set; }
        public int OverallTeamWinner { get; set; }
        public int RoundDealer { get; set; }

        public Game()
        {
            T1Wins = 0;
            T2Wins = 0;
            T1Pont = 0;
            T2Pont = 0;
            OverallTeamWinner = 0;
            CurrentRound = 1;
            RoundDealer = 0;
        }

        public void StartGame()
        {
            Pack deck = new Pack();
            int dealerIndex;

            T1Pont = 0;
            T2Pont = 0;
            CurrentRound = 1;
            RoundDealer++;
            if (RoundDealer == 4)
                RoundDealer = 0;

            deck.CreateDeck();
            deck.ShuffleDeck();
            GameTrump = deck.ChooseTrump();

            for (int i = 0; i < 4; i++)
            {
                dealerIndex = RoundDealer + i;
                if (dealerIndex >= 4)
                    dealerIndex -= 4;

                deck.AssignHand(Players[dealerIndex]);
            }
        }

        public void CalculateScore(int teamRoundWinner, Card[] cardsPlayed)
        {
            int teamPont;

            switch (teamRoundWinner)
            {
                case 1:
                    teamPont = T1Pont;
                    break;
                case 2:
                    teamPont = T2Pont;
                    break;
                default:
                    return;
            }

            for (int i = 0; i < 4; i++)
            {
                switch (cardsPlayed[i].Value)
                {
                    case Valor.Queen:
                        teamPont += 2;
                        break;
                    case Valor.Jack:
                        teamPont += 3;
                        break;
                    case Valor.King:
                        teamPont += 4;
                        break;
                    case Valor.Seven:
                        teamPont += 10;
                        break;
                    case Valor.Ace:
                        teamPont += 11;
                        break;
                }
            }

            switch (teamRoundWinner)
            {
                case 1:
                    T1Pont = teamPont;
                    break;
                case 2:
                    T2Pont = teamPont;
                    break;
            }

            if (CurrentRound == 10)
            {
                if (T1Pont > T2Pont)
                {
                    UpdateWins(1);
                }
                else if (T2Pont > T1Pont)
                {
                    UpdateWins(2);
                }
            }
            else
            {
                CurrentRound++;
            }
        }

        public void CheckGameOver()
        {
            if (T1Wins >= 5)
            {
                OverallTeamWinner = 1;
            }
            else if (T2Wins >= 5)
            {
                OverallTeamWinner = 2;
            }
        }

        private void UpdateWins(int team)
        {
            if (team == 1)
            {
                if (T2Pont == 0)
                    T1Wins += 4;

                if (T2Pont < 30)
                    T1Wins += 2;

                else
                    T1Wins++;
            }
            else if (team == 2)
            {
                if (T1Pont == 0)
                    T2Wins += 4;

                if (T1Pont < 30)
                    T2Wins += 2;

                else
                    T2Wins++;
            }
        }
    }
}