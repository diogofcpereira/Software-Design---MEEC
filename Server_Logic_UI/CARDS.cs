namespace SERVER_TEST
{  
    public enum Naipe
    {
        Hearts,
        Spades,
        Clubs,
        Diamonds,
        NULL_TYPE
    }

    public enum Valor
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Queen,
        Jack,
        King,
        Seven,
        Ace,
        NULL_VALUE
    }

    public class Card
    {
        public Naipe Type { get; private set; }
        public Valor Value { get; private set; }

        public Card(Naipe symbol, Valor number)
        {
            Type = symbol;
            Value = number;
        }

        public Card()
        {
            
        }
    }
}