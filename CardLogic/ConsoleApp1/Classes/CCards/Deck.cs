using System.ComponentModel.DataAnnotations;

class Deck
{
    #region // Attributes \\
    public Card[] cardDeck;

    #endregion


    #region // Constructors \\
    public Deck(Card[] cards)
    {
        SetCardDeck(cards);
    }
    #endregion

    #region // Setters \\
    public void SetCardDeck(Card[] cardDeck)
    {
        this.cardDeck = cardDeck;
    }

    #endregion


    #region // Getters \\
    public Card[] GetCardsOfDeck()
    {
        return cardDeck;
    }



    public int GetAmountOfCards()
    {
        int amount = 0;
        foreach (Card item in cardDeck)
        {
            amount++;
        }
        return amount;
    }

    #endregion

    #region // Other \\

    public void PrintAllCards()
    {
        foreach (Card card in cardDeck)
        {
            Console.WriteLine(card);
        }
    }



    // Remove card from deck
    public void RemoveCardFromDeck(Card card)
    {
        bool skipOnlyOne = false;

        int counterForOriginalDeck = 0;
        // Make a new temp deck - 1
        Card[] temp = new Card[cardDeck.Length-1];

        for (int i = 0; i < temp.Length; i++)
        {
            // Skip the card (only once)
            if (cardDeck[counterForOriginalDeck] == card && skipOnlyOne == false)
            {
                skipOnlyOne = true;
                i--;
            }
            else 
            {
                temp[i] = cardDeck[counterForOriginalDeck];
            }
            counterForOriginalDeck++;
        }
        cardDeck = temp;
    }

    public void AddCardToDeck(Card card)
    {
        cardDeck = GeneralFunctions.AddOneToArray(cardDeck);
        cardDeck[cardDeck.Length-1] = card;

    }

    // Add to discard pile (add to other deck)

    
    // [ ] 1. Split the deck into 2 parts,
    // 2. Chose starting side (left / right)
    // 3. Check falseDrop percent (Random num e.g: 1-5 out of 100 [5%] if true: don't drop card switch hand)
    // 4. Each drop card is collected in the new deck
    // 5. When both sides empty, return new deck

    // WAYPOINT Meth Shuffle
    public void ShuffleRiffleHuman(double errorMargin)
    {
        errorMargin = GeneralFunctions.CheckErrorMargin(errorMargin); // Check that the Error Margin is valid (0 - 99.9)

        int intErrorMargin = Convert.ToInt32(errorMargin * 100);

        int amount = GetAmountOfCards();
        // Base case if card amount is 1 or less, return (no shuffle possible)
        if (GetAmountOfCards() <= 1) return;

        bool rightHandDroppingCard = false;
        Card[] leftHand;
        Card[] rightHand;

        int counterLeftHand = 0;
        int counterRightHand = 0;

        Card[] newShuffledDeck = new Card[cardDeck.Length];

        // Chose which hand to add the extra card (0 == leftHand, 1 == rightHand)
        if (GetAmountOfCards() % 2 != 0)
        {
            if (GeneralFunctions.GetRandomNumber(0,1) == 1)
            {
                leftHand = new Card[GetAmountOfCards() / 2];
                rightHand = new Card[GetAmountOfCards() / 2 + 1];
            }
            else
            {
                leftHand = new Card[GetAmountOfCards() / 2 + 1];
                rightHand = new Card[GetAmountOfCards() / 2];
            }
        }
        else 
        {
            leftHand = new Card[GetAmountOfCards() / 2];
            rightHand = new Card[GetAmountOfCards() / 2];
        }

        // Split the deck into 2 parts
        for (int i = 0; i < leftHand.Length; i++)
        {
            leftHand[i] = cardDeck[i];
        }
        for (int i = leftHand.Length; i < amount; i++)
        {
            rightHand[counterRightHand++] = cardDeck[i];
        }
        // Reset counter
        counterRightHand = 0;

        // Check what hand will start dropping cards into the new shuffled deck.
        if (GeneralFunctions.GetRandomNumber(0,1) == 1) rightHandDroppingCard = true;

        // Make the new shuffled deck
        for (int i = 0; i < amount; i++)
        {
            // Boolean variable to see if card is skipped 
            bool cardSkipped = GameCardFunctions.CheckOdds(0,intErrorMargin,10000);

            if (rightHandDroppingCard == true && counterRightHand < rightHand.Length)  // Right hand
            {
                if (counterLeftHand < leftHand.Length) rightHandDroppingCard = false; // To avoid infinite loop
                if (cardSkipped == true)
                {
                    i--;
                    continue;
                }
                newShuffledDeck[i] = rightHand[counterRightHand++];
            }   
            else if (rightHandDroppingCard == false && counterLeftHand < leftHand.Length) // Left hand
            {
                if (counterRightHand < rightHand.Length) rightHandDroppingCard = true; // To avoid infinite loop
                if (cardSkipped == true)
                {
                    i--;
                    continue;
                }
                newShuffledDeck[i] = leftHand[counterLeftHand++];
            }
            // else if (counterLeftHand < leftHand.Length || counterRightHand < rightHand.Length) i--;
        }

        cardDeck = newShuffledDeck;

    }
    
    public void CutShuffle()
    {
        int maxCutCard  = GetAmountOfCards();
        if (maxCutCard < 2) return; // Base case for a deck of 1 card only

        Console.WriteLine("Press 'C' to cut");
        bool stopCut = false;
        int cut = 1;

        Thread keyListenerThread = new Thread(() =>
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // Wait for any key press
            if (keyInfo.KeyChar == 'c' || keyInfo.KeyChar == 'C')
            {
                stopCut = true;  // Set flag to stop the cut
            }
        });
        keyListenerThread.IsBackground = true;
        keyListenerThread.Start();  // Start the thread

        while (stopCut == false)
        {
            // -1 to avoid cutting nothing. so there will be always at least 1 card cut.
            if (cut > maxCutCard-1) cut = 1;
            Console.WriteLine(cut++);
            Thread.Sleep(1);
            GeneralFunctions.ClearConsoleLines(0,1);
        }
        cut--;

        Card[] newShuffledDeck = new Card[maxCutCard];
        int newDeckCounter = 0;
        // Add the end of the deck before the cut
        for (int i = cut; i < maxCutCard; i++)
        {
            newShuffledDeck[newDeckCounter++] = cardDeck[i];
        }
        // Add the start of the deck before the cut
        for (int i = 0; i < cut; i++)
        {
            newShuffledDeck[newDeckCounter++] = cardDeck[i];
        }

        cardDeck = newShuffledDeck;

    }

    public void RandomNumberShuffleDeck()
    {
        Card[] temp = new Card[cardDeck.Length];
        int shuffleCounter = 0;

        int [] usedNumbers = new int[cardDeck.Length];
        int usedNumCounter = 0;

        bool goOn = false;

        // Fill usedNumbers array with -1s
        for (int i = 0; i < usedNumbers.Length; i++)
        {
            usedNumbers[i] = -1;
        }

        while (true)
        {
            goOn = false;
            int randomNum = GeneralFunctions.GetRandomNumber(0,cardDeck.Length-1);
            foreach (int usedNum in usedNumbers)
            {
                if (randomNum == usedNum)
                {
                    goOn = true;
                    break;
                }
                else if (usedNum == -1) break;
            }
            if (goOn == false)
            {
                temp[shuffleCounter++] = cardDeck[randomNum];
                usedNumbers[usedNumCounter++] = randomNum;
            }

            if (usedNumbers[usedNumbers.Length - 1] != -1) break;
        }
        cardDeck = temp;
    }
    


    
    #endregion


}