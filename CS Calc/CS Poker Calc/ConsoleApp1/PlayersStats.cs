public class PlayerStats
{
    public double BuyIn { get; set; }
    public double BuyOut { get; set; }

    public PlayerStats(double buyIn, double buyOut = 0)
    {
        BuyIn = buyIn;
        BuyOut = buyOut;
    }
}