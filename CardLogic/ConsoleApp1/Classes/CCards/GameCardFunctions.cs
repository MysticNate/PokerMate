class GameCardFunctions
{
    


    public void AddToDiscardPileAndRemoveFromOriginalDeck(Card cardToMove, Deck drawPile, Deck discardPile)
    {
        // Move the card to the discard pile
        discardPile.AddCardToDeck(cardToMove);

        // Remove the card from the main deck
        drawPile.RemoveCardFromDeck(cardToMove);
    }

    public static bool CheckOdds(int from, int errorChance, int until)
    {
        if (GeneralFunctions.GetRandomNumber(from, until) <= errorChance) return true;
        return false;
    }


}