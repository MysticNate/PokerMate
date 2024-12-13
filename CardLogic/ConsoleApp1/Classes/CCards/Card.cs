enum BackColor
{
    Red,
    Blue,
    Black,
    Gold
}
enum Suit
{
    Spade = 1000,
    Diamond = 2000,
    Club = 3000,
    Heart = 4000,
    Joker = 5000
}

enum CardValue
{
    Joker,
    Ace,
    Deuce,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}
class Card
{
    #region // Attributes \\
    public BackColor backColor;
    public Suit suit;
    public CardValue value;
    #endregion


    #region // Constructors \\
    public Card(int suit, int value, int backColor = 0)
    {
        SetBackColor(backColor);
        SetSuit(suit);
        SetValue(value);
    }
    #endregion

    #region // Setters \\
    public void SetBackColor(int backColor)
    {
        this.backColor = (BackColor)backColor;
    }
    public void SetSuit(int suit)
    {
        this.suit = (Suit)suit;
    }
    public void SetValue(int value)
    {
        this.value = (CardValue)value;
    }
    #endregion


    #region // Getters \\
    public int GetBackColor()
    {
        return Convert.ToInt32(backColor);
    }
    public int GetSuit()
    {
        return Convert.ToInt32(suit);
    }
    public int GetValue()
    {
        return Convert.ToInt32(value);
    }


    public override string ToString()
    {
        return $"{(int)suit + (int)value}";
    }

    #endregion

}