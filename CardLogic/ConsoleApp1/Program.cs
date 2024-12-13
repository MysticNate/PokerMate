// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


Card[] standardDeck = new Card[54];
// Add Jokers
standardDeck[0] = new Card(5000, 0);
standardDeck[1] = new Card(5000, 0);

// Base values for each suit
int[] suitValues = { 1000, 2000, 3000, 4000 };

// Populate the deck with cards
int index = 2; // Start after the Jokers
for (int suit = 0; suit < 4; suit++) // Loop through suits
{
    if (suit < 2)
    {
        for (int rank = 1; rank <= 13; rank++) // Loop through ranks
        {
            standardDeck[index++] = new Card(suitValues[suit], rank);
        }
    }
    else
    {
        for (int rank = 13; rank >= 1; rank--) // Loop through ranks
        {
            standardDeck[index++] = new Card(suitValues[suit], rank);
        }
    }
}

Deck stanDeck = new Deck(standardDeck);

stanDeck.PrintAllCards();

// stanDeck.ShuffleRiffleHuman(40);

// stanDeck.RandomNumberShuffleDeck();

Console.WriteLine("-----------------");

for (int i = 0; i < 3; i++)
{
    stanDeck.ShuffleRiffleHuman(5);
}
stanDeck.PrintAllCards();
Console.WriteLine(stanDeck.GetAmountOfCards());
// A♠ => K♠,
// A♦ => K♦,
// K♣ => A♣,
// K♥ => A♥


