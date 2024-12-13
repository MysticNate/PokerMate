enum HandStrength
{
    HighCard,
    Pair,
    TwoPair,
    Trips,
    Straight,
    Flush,
    FullHouse,
    Quads,
    StraightFlush,
    RoyalFlush
}
/// <summary>
/// Logic for checking the hand strength,
/// -- Make the hand into a string so it will look like this "0KQT52" Means = 0 = High card (K high, Q kicker followed by T kicker......)
/// 1. Find the strength of the hand "0 - 9" according to Enum 'HandStrength'
/// 2. Check first index to see who has the stronger hand
///     2.2 If there are a few players with the same first index save them in the side to check the kickers / strongest card (in case of flush)
///     
///     !!! ALWAYS SORT THE MOST IMPORTANT CARDS NEXT TO THE HAND STRENGTH INDEX (0-9) 
///     !!! Two Pair = 2AAKKT, 2QQKKT, we can see that the index '1' (A > Q) so the first player wins.
///     
///     // Keep checking until the string is empty, if reached the hand there is a tie, Make sure you can test a few players and not only 2
///     
///     // Learn on how to handle issues when you're trying to split a pot of 100 to 3 people. (look at loses and give the player with the highest loss the extra)
///     
/// </summary>
class Hands
{
    



    public static string HandToString(Card[] playersCards)
    {
        /// 1. Sort players cards from biggest to smallest (Ace will be in the end always so lowest straight = 2,3,4,5,A | Highest = T,J,Q,K,A)
        return
    }

    // HandStrength Checker
    public static bool CheckRoyalFlush(Card[] playersCards)
    {
        // Check if all suits are the same

        // Check if the hand is a straight from 10 => Ace (T,J,Q,K,A)

        // If both are true must be a royal flush
        return true;
    }
}