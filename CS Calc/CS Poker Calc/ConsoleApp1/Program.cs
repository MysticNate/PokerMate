using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Runtime.InteropServices;

Console.OutputEncoding = System.Text.Encoding.UTF8;
#region Colors Methods

static void Black()
{
    Console.ForegroundColor = ConsoleColor.Black;
}
static void Blue()
{
    Console.ForegroundColor = ConsoleColor.Blue;
}
static void DarkBlue()
{
    Console.ForegroundColor = ConsoleColor.DarkBlue;
}
static void Yellow()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
}
static void DarkYellow()
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
}
static void Green()
{
    Console.ForegroundColor = ConsoleColor.Green;
}
static void DarkGreen()
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
}
static void Cyan()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
}
static void DarkCyan()
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
}
static void Red()
{
    Console.ForegroundColor = ConsoleColor.Red;
}
static void DarkRed()
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
}
static void Magenta()
{
    Console.ForegroundColor = ConsoleColor.Magenta;
}
static void DarkMagenta()
{
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
}
static void Gray()
{
    Console.ForegroundColor = ConsoleColor.Gray;
}
static void DarkGray()
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
}
static void ResetColor()
{
    Console.ForegroundColor = ConsoleColor.White;
}

#region color check
// Console.ForegroundColor = ConsoleColor.Black;
// Console.Write("\nBlack\n");
// Console.ForegroundColor = ConsoleColor.Blue;
// Console.Write("Blue");
// Console.ForegroundColor = ConsoleColor.DarkBlue;
// Console.Write("DarkBlue\n");
// Console.ForegroundColor = ConsoleColor.Yellow;
// Console.Write("Yellow");
// Console.ForegroundColor = ConsoleColor.DarkYellow;
// Console.Write("DarkYellow\n");
// Console.ForegroundColor = ConsoleColor.Green;
// Console.Write("Green");
// Console.ForegroundColor = ConsoleColor.DarkGreen;
// Console.Write("DarkGreen\n");
// Console.ForegroundColor = ConsoleColor.Cyan;
// Console.Write("Cyan");
// Console.ForegroundColor = ConsoleColor.DarkCyan;
// Console.Write("DarkCyan\n");
// Console.ForegroundColor = ConsoleColor.Red;
// Console.Write("Red");
// Console.ForegroundColor = ConsoleColor.DarkRed;
// Console.Write("DarkRed\n");
// Console.ForegroundColor = ConsoleColor.Magenta;
// Console.Write("Magenta");
// Console.ForegroundColor = ConsoleColor.DarkMagenta;
// Console.Write("DarkMagenta\n");
// Console.ForegroundColor = ConsoleColor.Gray;
// Console.Write("Gray");
// Console.ForegroundColor = ConsoleColor.DarkGray;
// Console.Write("DarkGray\n");
#endregion

#endregion

// Check operating system to determine time and file paths
PlatformID os = Environment.OSVersion.Platform;

string filePathDBGameHistory = "";
string filePathDBMilestones = "";
string filePathDBProfitLossTracker = "";

// Will determine if Replit is being used or not
bool isUsingReplit = false;

// Set correct file path
if (os == PlatformID.Win32NT)     // Windows file path
{
    filePathDBGameHistory = "C:\\Users\\Giora\\OneDrive\\MyLife\\Giora\\Programming\\My Projects\\CSharp\\Poker and Cards\\CS Poker Calc\\Databases\\Game History.txt";
    filePathDBMilestones = "C:\\Users\\Giora\\OneDrive\\MyLife\\Giora\\Programming\\My Projects\\CSharp\\Poker and Cards\\CS Poker Calc\\Databases\\Milestones.txt";
    filePathDBProfitLossTracker = "C:\\Users\\Giora\\OneDrive\\MyLife\\Giora\\Programming\\My Projects\\CSharp\\Poker and Cards\\CS Poker Calc\\Databases\\ProfitLoss Tracker.txt";
}
else if (os == PlatformID.Unix) // macOS or other Unix-like systems
{
    // Check if it's macOS specifically
    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        filePathDBGameHistory = "/Users/giorasmacbook/Library/CloudStorage/OneDrive-Personal/MyLife/Giora/Programming/My Projects/CSharp/Poker and Cards/CS Poker Calc/Databases/Game History.txt";
        filePathDBMilestones = "/Users/giorasmacbook/Library/CloudStorage/OneDrive-Personal/MyLife/Giora/Programming/My Projects/CSharp/Poker and Cards/CS Poker Calc/Databases/Milestones.txt";
        filePathDBProfitLossTracker = "/Users/giorasmacbook/Library/CloudStorage/OneDrive-Personal/MyLife/Giora/Programming/My Projects/CSharp/Poker and Cards/CS Poker Calc/Databases/ProfitLoss Tracker.txt";
    }
    else // Default for other Unix-based systems
    {
        filePathDBGameHistory = "Databases/Game History.txt";
        filePathDBMilestones = "Databases/Milestones.txt";
        filePathDBProfitLossTracker = "Databases/ProfitLoss Tracker.txt";

        isUsingReplit = true;
    }
}
else
{
    throw new PlatformNotSupportedException("Unknown OS platform");
}

// Will determine the minimum transfer amount (BIT limitation)
// Set - '0' to ignore
double minTransfer = 1;

// Set the char count for printing the sentences 
int replitCharCount = 44;

// Set the minimum chip of the game for calculating pots
double minGameChip = 0.25;

// Set the amount of seconds for the timer
int timerSeconds = 30;

// Favorite players :)
string[] favPlayers = { "" };

// START
while (true)
{
    Console.Clear();
    PrintMainMenu(timerSeconds);
    char key = Console.ReadKey(true).KeyChar; // Read key without displaying it in console 
    Console.Clear();
    if (key == '1')
    {
        RunPCS(isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers);
    }
    else if (key == '2')
    {
        RunCC(minGameChip, replitCharCount);
    }
    // This does not work well in Replit hence I've disabled it, Works ok in PC / Mac
    // else if (key == '3')
    // {
    //     SetTimer(timerSeconds, replitCharCount);
    // }
    else if (key == '0')
    {
        Console.Clear();
        PrintGoodByeMessage();
        return;
    }
}


// Method to find all combinations of losers that sum to the winner's amount
static List<KeyValuePair<string, double>> DFSettleWinner(List<KeyValuePair<string, double>> losers, double winnerSum)
{
    List<KeyValuePair<string, double>> currentCombination = new List<KeyValuePair<string, double>>();
    List<KeyValuePair<string, double>> combinationFound = new List<KeyValuePair<string, double>>();
    FindCombinations(losers, winnerSum, 0, currentCombination, combinationFound);
    return combinationFound;
}

// Recursive helper method to find combinations
static void FindCombinations(List<KeyValuePair<string, double>> losers, double targetSum, int startIndex, List<KeyValuePair<string, double>> currentCombination, List<KeyValuePair<string, double>> combinationFound)
{
    double currentSum = 0;
    foreach (var item in currentCombination)
    {
        currentSum += item.Value;
    }

    // Check if the current combination sums to the target
    if (Math.Abs(currentSum - targetSum) < 0.001) // Adding a small tolerance for floating point accuracy
    {
        // If a combination has been found
        foreach (var kvp in currentCombination)
        {
            // Add them to the new list that will be used later to remove them and settle debts
            combinationFound.Add(new(kvp.Key, kvp.Value));
        }
        return;
    }

    // If the current sum exceeds the target or we have exhausted the list, backtrack
    if (currentSum >= targetSum || startIndex >= losers.Count)
    {
        return;
    }

    // Explore further by adding each subsequent loser one by one to the combination
    for (int i = startIndex; i < losers.Count; i++)
    {
        currentCombination.Add(losers[i]);
        FindCombinations(losers, targetSum, i + 1, currentCombination, combinationFound);
        currentCombination.RemoveAt(currentCombination.Count - 1); // Backtrack
    }
}

static void CalcRun(string gameStatsString, Dictionary<string, PlayerStats> playerStatsDic, bool isUsingReplit, string filePathDBGameHistory, string filePathDBMilestones, string filePathDBProfitLossTracker, double minTransfer, int replitCharCount, string[] favPlayers, bool solutionUsed = false, string gameStatsStringResolved = "")
{
    // 'gameStatsString' is saved in order to keep the data sent for altering purposes!

    Console.Clear();

    // Used to switch places between players with equal profit/loss (50% chance)
    playerStatsDic = RandomizeEqualPlayers(playerStatsDic);

    #region Get longest vars
    // Find longest stuff for nice printing output
    string longestPlayerName = "";
    string longestPlayerBuyIn = "";
    string longestPlayerBuyOut = "";
    string longestPlayerProfitOrLoss = "";

    int lineLength = 0;
    double longestLineBuyIn = 0; // Impotent for getting correct spaces in printing
    int longestLineLength = 0;

    bool firstLongestName = false;


    foreach (var player in playerStatsDic)
    {
        string playerName = player.Key;
        double playerBuyIn = player.Value.BuyIn;
        double playerBuyOut = player.Value.BuyOut;
        double playerProfitOrLoss = Math.Abs(Math.Round(playerBuyOut - playerBuyIn, 2));

        lineLength = 20 + Convert.ToString(playerBuyIn).Length + Convert.ToString(playerBuyOut).Length + playerName.Length;

        if (firstLongestName == false && (player.Value.BuyIn != 0 || player.Value.BuyOut != 0))
        {
            longestPlayerName = playerName;
            longestPlayerBuyIn = Convert.ToString(playerBuyIn);
            longestPlayerBuyOut = Convert.ToString(playerBuyOut);
            longestPlayerProfitOrLoss = Convert.ToString(Math.Abs(Math.Round(playerBuyOut - playerBuyIn, 2)));
            longestLineLength = lineLength;
            firstLongestName = true;
        }
        else if (player.Key.Length > longestPlayerName.Length && (player.Value.BuyIn != 0 || player.Value.BuyOut != 0)) longestPlayerName = player.Key;

        if (Convert.ToString(playerBuyIn).Length > longestPlayerBuyIn.Length) longestPlayerBuyIn = Convert.ToString(playerBuyIn);
        if (Convert.ToString(playerBuyOut).Length > longestPlayerBuyOut.Length) longestPlayerBuyOut = Convert.ToString(playerBuyOut);
        if (Convert.ToString(playerProfitOrLoss).Length > longestPlayerProfitOrLoss.Length) longestPlayerProfitOrLoss = Convert.ToString(playerProfitOrLoss);

        if (lineLength > longestLineLength)
        {
            longestLineLength = lineLength;
            longestLineBuyIn = player.Value.BuyIn;
        }
    }
    // Set the longest line length for profit / loss printing (20 = existing chars in printing)
    longestLineLength += Convert.ToString(longestPlayerBuyIn).Length - Convert.ToString(longestLineBuyIn).Length;


    longestLineLength = longestPlayerName.Length + longestPlayerBuyIn.Length + longestPlayerBuyOut.Length + 21; // Reset longest line to longest possible line.. for stats headline (21 other chars in line)



    #endregion


    // Create winners and losers lists
    List<KeyValuePair<string, double>> winnersList = new List<KeyValuePair<string, double>>();
    List<KeyValuePair<string, double>> losersList = new List<KeyValuePair<string, double>>();


    #region Make lists
    foreach (var player in playerStatsDic)
    {
        string playerName = player.Key;                                     // The player's name (key in the dictionary)
        double playerBuyIn = player.Value.BuyIn;                            // The BuyIn value
        string playerBuyInToString = Convert.ToString(player.Value.BuyIn);  // The BuyIn ToString so I can make spaces nicer
        double playerBuyOut = player.Value.BuyOut;                          // The BuyOut value

        double difference = Math.Abs(playerBuyIn - playerBuyOut); // Lose / Win

        // Add to winners or losers list based on the comparison of BuyIn and BuyOut
        if (playerBuyIn < playerBuyOut)
        {
            winnersList.Add(new KeyValuePair<string, double>(playerName, difference));
        }
        else if (playerBuyIn > playerBuyOut) // If buy in and buy out are the same, skip the player
        {
            losersList.Add(new KeyValuePair<string, double>(playerName, difference));
        }
        #endregion

    }




    #region Check L&&W > 0
    if (winnersList.Count == 0 && losersList.Count == 0)
    {
        Console.Clear();
        PrintErrorNoWinnersNoLosers();
        return;
    }
    else if (winnersList.Count == 0)
    {
        Console.Clear();
        PrintErrorNoWinners();
        return;
    }
    else if (losersList.Count == 0)
    {
        Console.Clear();
        PrintErrorNoLosers();
        return;
    }

    #endregion




    #region Print Stats
    Console.Clear();
    int middleIndexOfStatsPrinting = PrintGameDataHeading(longestLineLength);

    // Get lengths for print 'Gain/Loss/Even'

    string spacesGainOrLoss = "";
    string spacesEven = "";

    for (int i = 6; i < middleIndexOfStatsPrinting; i++) // 6 = represent 'Gain =' / 'Loss =' 
    {
        spacesGainOrLoss += " ";
    }

    for (int i = 3; i < middleIndexOfStatsPrinting; i++) // 3 = represent '∞Ev' 
    {
        spacesEven += " ";
    }


    Console.WriteLine();
    // Sort array to be in the correct order
    // WARNING Players with a buyIn and buyOut of '0' will be removed!
    playerStatsDic = OrderDicForPrinting(playerStatsDic);

    foreach (var player in playerStatsDic)
    {
        string playerName = player.Key;                                     // The player's name (key in the dictionary)
        double playerBuyIn = player.Value.BuyIn;                            // The BuyIn value
        string playerBuyInToString = Convert.ToString(player.Value.BuyIn);  // The BuyIn ToString so I can make spaces nicer
        double playerBuyOut = player.Value.BuyOut;                          // The BuyOut value

        double difference = Math.Abs(playerBuyIn - playerBuyOut); // Lose / Win

        if (playerBuyIn != 0 || playerBuyOut != 0) // If player bought in / cashed out
        {
            string spacesP = "";
            for (int i = 0; i < longestPlayerName.Length - playerName.Length; i++)
            {
                spacesP += " ";
            }
            string spacesBuyIn = "";
            for (int i = 0; i < longestPlayerBuyIn.Length - playerBuyInToString.Length; i++)
            {
                spacesBuyIn += " ";
            }
            PrintPlayerStats(playerName, spacesP, spacesBuyIn, playerBuyIn, playerBuyOut, spacesGainOrLoss, spacesEven);
        }

    }
    #endregion

    // After printing add all of the OG players again in case there are errors (for name recognition)
    AddOGPlayerToDictionary(playerStatsDic);



    // WAYPOINT CHECK ERRORS
    #region CHECK ERRORS


    // Check difference in chips
    double differenceChips = Math.Round(losersList.Sum(x => x.Value) - winnersList.Sum(x => x.Value), 2);

    #region Get MaxMin W/L

    // Values of the biggest loser and winner

    // WARNING: Do not round else error (Must be exact value, if need rounding, round printed value)
    double biggestWinnerValue = winnersList.Max(x => x.Value); // Get biggest winner win
    double biggestLoserValue = losersList.Max(x => x.Value); // Get biggest loser loss

    double smallestWinnerValue = winnersList.Min(x => x.Value); // Get smallest winner win
    double smallestLoserValue = losersList.Min(x => x.Value); // Get smallest loser loss

    // Names of the biggest loser and winner
    string biggestWinnerName = Convert.ToString(winnersList.Find(x => x.Value.Equals(biggestWinnerValue))); // Get biggest winner name
    biggestWinnerName = biggestWinnerName.Substring(1, biggestWinnerName.IndexOf(',') - 1);

    string biggestLoserName = Convert.ToString(losersList.Find(x => x.Value.Equals(biggestLoserValue))); // Get biggest loser name
    biggestLoserName = biggestLoserName.Substring(1, biggestLoserName.IndexOf(',') - 1);


    string smallestWinnerName = Convert.ToString(winnersList.Find(x => x.Value.Equals(smallestWinnerValue))); // Get smallest winner name
    smallestWinnerName = smallestWinnerName.Substring(1, smallestWinnerName.IndexOf(',') - 1);

    string smallestLoserName = Convert.ToString(losersList.Find(x => x.Value.Equals(smallestLoserValue))); // Get smallest loser name
    smallestLoserName = smallestLoserName.Substring(1, smallestLoserName.IndexOf(',') - 1);


    double biggestLoserCashout = playerStatsDic[biggestLoserName].BuyOut; // Get biggest loser cashout
    double biggestWinnerCashout = playerStatsDic[biggestWinnerName].BuyOut; // Get biggest winner cashout

    #endregion


    // Count losers and winners
    int countLosers = losersList.Count;
    int countWinners = winnersList.Count;
    int countRemainingLosers = countLosers - 1;
    int countRemainingWinners = countWinners - 1;


    // Losers for solution \\
    // (%In percentage%) Biggest LOSER discount for solution 2.
    double biggestLoserDiscountPercent = Math.Ceiling(biggestLoserValue / losersList.Sum(x => x.Value) * 100);
    // ($In value$) Biggest LOSER discount for solution 2.
    double biggestLoserDiscountValue = Math.Round(differenceChips * (biggestLoserDiscountPercent / 100), 2);
    // ($In value$) left over chips for solution 2.
    double leftOverPerLoser = Math.Round((differenceChips - (differenceChips * (biggestLoserDiscountPercent / 100))) / countRemainingLosers, 2);

    // Winners for solution \\
    // (%In percentage%) Biggest WINNER forfeits for solution 2.
    double biggestWinnerForfeitPercent = Math.Ceiling(biggestWinnerValue / winnersList.Sum(x => x.Value) * 100);
    // ($In value$) Biggest WINNER forfeits for solution 2.
    double biggestWinnerForfeitValue = Math.Abs(Math.Round(differenceChips * (biggestWinnerForfeitPercent / 100), 2));
    // ($In value$) left over chips for solution 2.
    double leftOverPerWinner = Math.Abs(Math.Round((differenceChips - (differenceChips * (biggestWinnerForfeitPercent / 100))) / countRemainingWinners, 2));


    string longestWinnerName = "";
    string longestLoserName = "";

    firstLongestName = false;
    foreach (var winner in winnersList)
    {
        if (firstLongestName == false)
        {
            longestWinnerName = winner.Key;
            firstLongestName = true;
        }
        else if (winner.Key.Length > longestWinnerName.Length) longestWinnerName = winner.Key;
    }

    firstLongestName = false;
    foreach (var loser in losersList)
    {
        if (firstLongestName == false)
        {
            longestLoserName = loser.Key;
            firstLongestName = true;
        }
        else if (loser.Key.Length > longestLoserName.Length) longestLoserName = loser.Key;
    }

    double sumBuyIn = Math.Round(playerStatsDic.Sum(x => x.Value.BuyIn), 2); // Sums the buy-in of the game
    double sumCashOut = Math.Round(playerStatsDic.Sum(x => x.Value.BuyOut), 2); // Sums the buy-out of the game


    string solutionAnswer = "";


    // Set values for remainders in case of a... remainder! :D

    // For DB print of remainder
    bool remainderPositive = false;

    string remainderPlayer2 = "";
    double remainder2 = 0;

    string remainderPlayer3 = "";
    double remainder3 = 0;

    // Will be used like to add remainder only once! 
    bool addRemainderOnce = false;
    // Will be used like to add value to biggest winner/loser only once! 
    bool addBiggestOnce = false;

    #region Losers GAIN








    // When losers lost less then the buyins ### MISSING CHIPS ### AKA PrintLosersGain()
    #region chips > 0, 1 L
    if (differenceChips > 0 && countLosers == 1) // If there is only one loser
    {
        differenceChips = Math.Abs(differenceChips);

        PrintLosersGain();
        PrintStartLoserError(differenceChips, sumBuyIn, sumCashOut);

        Magenta();
        Console.Write("\n\nSuggested solution: \n");

        PrintDiscountOnlyLoserSolution(biggestLoserName, biggestLoserValue, biggestLoserCashout, differenceChips, true);

        PrintSolutionImplement1();

        solutionAnswer = GetSolutionResponse1(Console.ReadLine(), replitCharCount);

        if (solutionAnswer == "1")
        {
            foreach (var player in playerStatsDic)
            {
                if (player.Key == biggestLoserName)
                {
                    player.Value.BuyOut = Math.Round(biggestLoserCashout + differenceChips, 2);
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: true, typeSolution: "1", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestLoserName);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("new"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForGameStatsString();
            string validStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);
            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("alt"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForUpdatedGameStatsString();
            string newValidStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            string validStats = AlterPlayersDataString(gameStatsString, newValidStats);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);

            // AskIfGameSolvedDueToAlter(44) = Will set the bool value of if the game was resolved due to altering the data string
            bool altUsedForResolve = AskIfGameSolvedDueToAlter(44);

            if (altUsedForResolve == true)
            {
                gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, validStats);
                gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: true, typeSolution: "alt", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips));
            }

            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, altUsedForResolve, gameStatsStringResolved);
            return;
        }
        else return;
    }
    #endregion

    #region chips > 0
    else if (differenceChips > 0)
    {
        differenceChips = Math.Abs(differenceChips);

        PrintLosersGain();
        PrintStartLoserError(differenceChips, sumBuyIn, sumCashOut);

        // No remainder possible
        Magenta();
        Console.Write("\n\nSuggested solution 1: \n");
        PrintDiscountOnlyLoserSolution(biggestLoserName, biggestLoserValue, biggestLoserCashout, differenceChips, false);

        Magenta();
        Console.Write("\n\nSuggested solution 2: \n");
        Blue();
        Console.Write($"Extra chips ");
        Yellow();
        Console.Write($"(");
        Cyan();
        Console.Write($"{Math.Round(differenceChips, 2)}");
        Yellow();
        Console.Write(") / ");
        Blue();
        Console.Write("total ");
        Red();
        Console.Write("losers ");
        Yellow();
        Console.Write($"(");
        Red();
        Console.Write($"{countLosers}");
        Yellow();
        Console.Write($")");
        Red();
        Console.Write("\nAll L's");
        Green();
        Console.Write(" += ");
        Yellow();
        Console.Write($"{Math.Round(differenceChips / countLosers, 2)}");
        Green();
        Console.Write(" added");
        Blue();
        Console.Write(" to each ");
        Red();
        Console.Write("Cashout");
        Magenta();
        Console.Write(".");


        #region Remainder sol 2
        // Remainder should be extremely small if at all, I've implemented the logic below to handle it.
        if (Math.Round(Math.Round(differenceChips / countLosers, 2) * countLosers, 2) != differenceChips)
        {
            double remainder = Math.Round(differenceChips - Math.Round(differenceChips / countLosers, 2) * countLosers, 2);
            int sentenceLength = 34 + Convert.ToString(Math.Round(differenceChips / countLosers, 2)).Length; // 34 = Undefended words

            // The remainder should be give to the biggest loser (increases profit => less to pay)
            if (remainder > 0)
            {
                remainderPlayer2 = biggestLoserName;
                remainder2 = remainder;
                PrintRemainderSolutionLosersGain(sentenceLength, remainder2, remainderPlayer2);
                // For DB print of remainder
                remainderPositive = true;
            }
            else
            {
                remainderPlayer2 = smallestLoserName;
                remainder2 = remainder;
                PrintRemainderSolutionLosersGain(sentenceLength, remainder2, remainderPlayer2);
            }
        }
        #endregion

        Magenta();
        Console.Write("\n\nSuggested solution 3: \n");
        Yellow();
        Console.Write($"{biggestLoserName.ToUpper()} (");
        Red();
        Console.Write("Biggest loser");
        Yellow();
        Console.Write(")");
        Blue();
        Console.Write(" who ");
        Red();
        Console.Write("lost ");
        Yellow();
        Console.Write($"(");
        Red();
        Console.Write($"{Math.Round(biggestLoserValue, 2)}");
        Yellow();
        Console.Write($")");
        Green();
        Console.Write("\nGets ");
        Magenta();
        Console.Write($"{biggestLoserDiscountPercent}%");
        Green();
        Console.Write(" added");
        Blue();
        Console.Write(" to their ");
        Red();
        Console.Write("Cashout");
        Blue();
        Console.Write("\nAnd the other ");
        Red();
        Console.Write("losers ");
        Yellow();
        Console.Write($"(");
        Red();
        Console.Write($"{countRemainingLosers}");
        Yellow();
        Console.Write($")");
        Green();
        Console.Write(" get");
        Blue();
        Console.Write(" the rest\nOf the extra chips ");
        Yellow();
        Console.Write($"(");
        Cyan();
        Console.Write($"{Math.Round(differenceChips - differenceChips * (biggestLoserDiscountPercent / 100), 2)}");
        Yellow();
        Console.Write($")");
        Blue();
        Console.Write(" divided equally\n");
        Yellow();
        Console.Write($"{biggestLoserName.ToUpper()}");
        Green();
        Console.Write(" += ");
        Cyan();
        Console.Write($"{Math.Round(biggestLoserDiscountValue, 2)}");
        Green();
        Console.Write(" added");
        Blue();
        Console.Write(" to their ");
        Red();
        Console.Write("Cashout\nOther L's");
        Green();
        Console.Write(" += ");
        Cyan();
        Console.Write($"{Math.Round(leftOverPerLoser, 2)}");
        Green();
        Console.Write(" added ");
        Blue();
        Console.Write("to each ");
        Red();
        Console.Write("Cashout");
        Magenta();
        Console.Write(".");


        #region Remainder sol 3
        // Remainder should be extremely small if at all, I've implemented the logic below to handle it.
        if (Math.Round(biggestLoserDiscountValue + Math.Round(leftOverPerLoser * countRemainingLosers, 2), 2) != differenceChips)
        {
            double remainder = Math.Round(differenceChips - (biggestLoserDiscountValue + leftOverPerLoser * countRemainingLosers), 2);
            int sentenceLength = 36 + Convert.ToString(leftOverPerLoser).Length; // 36 = Independent words

            // The remainder should be give to the biggest loser (increases profit => less to pay)
            if (remainder > 0)
            {
                remainderPlayer3 = biggestLoserName;
                remainder3 = remainder;
                PrintRemainderSolutionLosersGain(sentenceLength, remainder3, remainderPlayer3);
                // For DB print of remainder
                remainderPositive = true;
            }
            else
            {
                remainderPlayer3 = smallestLoserName;
                remainder3 = remainder;
                PrintRemainderSolutionLosersGain(sentenceLength, remainder3, remainderPlayer3);
            }
        }
        #endregion
        ResetColor();

        PrintSolutionImplement2();

        solutionAnswer = GetSolutionResponse2(Console.ReadLine(), replitCharCount);


        if (solutionAnswer == "1")
        {
            foreach (var player in playerStatsDic)
            {
                if (player.Key == biggestLoserName)
                {
                    player.Value.BuyOut = Math.Round(biggestLoserCashout + differenceChips, 2);
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: true, typeSolution: "1", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestLoserName);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer == "2")
        {
            addRemainderOnce = false;
            foreach (var player in playerStatsDic)
            {
                foreach (var loser in losersList)
                {
                    if (player.Key == loser.Key)
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut + Math.Round(differenceChips / countLosers, 2), 2);
                    if (player.Key == remainderPlayer2 && addRemainderOnce == false)
                    {
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut + remainder2, 2); // if there is a remainder, add it to the designated player
                        addRemainderOnce = true;
                    }
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: true, typeSolution: "2", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestLoserName, equalSplit: Convert.ToString(Math.Round(differenceChips / countLosers, 2)), remainder: remainder2, remainderPlayer: remainderPlayer2);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer == "3")
        {
            addRemainderOnce = false;
            addBiggestOnce = false;
            foreach (var player in playerStatsDic)
            {
                foreach (var loser in losersList)
                {
                    if (player.Key == biggestLoserName && addBiggestOnce == false)
                    {
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut + Math.Round(biggestLoserDiscountValue, 2), 2);
                        addBiggestOnce = true;
                    }
                    else if (player.Key == loser.Key && player.Key != biggestLoserName)
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut + Math.Round(leftOverPerLoser, 2), 2);
                    if (player.Key == remainderPlayer3 && addRemainderOnce == false)
                    {
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut + remainder3, 2); // if there is a remainder, add it to the designated player
                        addRemainderOnce = true;
                    }
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: true, typeSolution: "3", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestLoserName, equalSplit: Convert.ToString(Math.Round(leftOverPerLoser, 2)), percentageSplit: Convert.ToString(Math.Round(biggestLoserDiscountValue, 2)), percentOfSolution3: Convert.ToString(biggestLoserDiscountPercent), remainder: remainder3, remainderPlayer: remainderPlayer3);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("new"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForGameStatsString();
            string validStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);
            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("alt"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForUpdatedGameStatsString();
            string newValidStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            string validStats = AlterPlayersDataString(gameStatsString, newValidStats);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);

            // AskIfGameSolvedDueToAlter(44) = Will set the bool value of if the game was resolved due to altering the data string
            bool altUsedForResolve = AskIfGameSolvedDueToAlter(44);

            if (altUsedForResolve == true)
            {
                gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, validStats);
                gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: true, typeSolution: "alt", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips));
            }

            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, altUsedForResolve, gameStatsStringResolved);
            return;
        }
        else return;
    }
    #endregion
    #endregion







    #region Winners LOSE









    #region chips < 0, 1 W
    // When winners won more then the buyins ### OVER CHIPS ###
    if (differenceChips < 0 && countWinners == 1) // If there is only one winner
    {
        differenceChips = Math.Abs(differenceChips);

        PrintWinnersLose();
        PrintStartWinnerError(differenceChips, sumBuyIn, sumCashOut);

        Magenta();
        Console.Write("\n\nSuggested solution: \n");

        PrintForfeitOnlyWinnerSolution(biggestWinnerName, biggestWinnerValue, biggestWinnerCashout, differenceChips, true);

        PrintSolutionImplement1();

        solutionAnswer = GetSolutionResponse1(Console.ReadLine(), replitCharCount);

        if (solutionAnswer == "1")
        {
            foreach (var player in playerStatsDic)
            {
                if (player.Key == biggestWinnerName)
                {
                    player.Value.BuyOut = Math.Round(biggestWinnerCashout - differenceChips, 2);
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: false, typeSolution: "1", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestWinnerName);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("new"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForGameStatsString();
            string validStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);
            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("alt"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForUpdatedGameStatsString();
            string newValidStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            string validStats = AlterPlayersDataString(gameStatsString, newValidStats);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);

            // AskIfGameSolvedDueToAlter(44) = Will set the bool value of if the game was resolved due to altering the data string
            bool altUsedForResolve = AskIfGameSolvedDueToAlter(44);

            if (altUsedForResolve == true)
            {
                gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, validStats);
                gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: false, typeSolution: "alt", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips));
            }

            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, altUsedForResolve, gameStatsStringResolved);
            return;
        }
        else return;

    }

    #endregion
    #region chips < 0
    else if (differenceChips < 0)
    {
        differenceChips = Math.Abs(differenceChips);

        PrintWinnersLose();
        PrintStartWinnerError(differenceChips, sumBuyIn, sumCashOut);

        // No remainder possible
        Magenta();
        Console.Write("\n\nSuggested solution 1: \n");
        PrintForfeitOnlyWinnerSolution(biggestWinnerName, biggestWinnerValue, biggestWinnerCashout, differenceChips, false);

        Magenta();
        Console.Write("\n\nSuggested solution 2: \n");
        Blue();
        Console.Write($"Missing chips ");
        Yellow();
        Console.Write($"(");
        Cyan();
        Console.Write($"{Math.Round(differenceChips, 2)}");
        Yellow();
        Console.Write($")");
        Yellow();
        Console.Write(" / ");
        Blue();
        Console.Write("total ");
        Green();
        Console.Write("winners ");
        Yellow();
        Console.Write($"(");
        Green();
        Console.Write($"{countWinners}");
        Yellow();
        Console.Write($")");
        Green();
        Console.Write("\nAll W's");
        Red();
        Console.Write(" -= ");
        Yellow();
        Console.Write($"{Math.Round(differenceChips / countWinners, 2)}");
        Red();
        Console.Write(" reduced");
        Blue();
        Console.Write(" from each ");
        Red();
        Console.Write("Cashout");
        Magenta();
        Console.Write(".");


        #region Remainder sol 2
        // Remainder should be extremely small if at all, I've implemented the logic below to handle it.
        if (Math.Round(Math.Round(differenceChips / countWinners, 2) * countWinners, 2) != differenceChips)
        {
            double remainder = Math.Round(differenceChips - Math.Round(differenceChips / countWinners, 2) * countWinners, 2);
            int sentenceLength = 38 + Convert.ToString(Math.Round(differenceChips / countWinners, 2)).Length; // 38 = Independent words

            // The remainder should be give to the biggest loser (increases profit => less to pay)
            if (remainder < 0)
            {
                remainderPlayer2 = smallestWinnerName;
                remainder2 = remainder;
                PrintRemainderSolutionWinnersLose(sentenceLength, remainder2, remainderPlayer2);
                // For DB print of remainder
                remainderPositive = true;
            }
            else
            {
                remainderPlayer2 = biggestWinnerName;
                remainder2 = remainder;
                PrintRemainderSolutionWinnersLose(sentenceLength, remainder2, remainderPlayer2);
            }
        }

        Magenta();
        Console.Write("\n\nSuggested solution 3: \n");
        Yellow();
        Console.Write($"{biggestWinnerName.ToUpper()} (");
        Green();
        Console.Write("Biggest winner");
        Yellow();
        Console.Write(")");
        Blue();
        Console.Write(" who ");
        Green();
        Console.Write("won ");
        Yellow();
        Console.Write($"(");
        Green();
        Console.Write($"{Math.Round(biggestWinnerValue, 2)}");
        Yellow();
        Console.Write($")");
        Red();
        Console.Write("\nGives ");
        Magenta();
        Console.Write($"{biggestWinnerForfeitPercent}%");
        Blue();
        Console.Write(" of the missing chips from ");
        Red();
        Console.Write("Cashout");
        Blue();
        Console.Write("\nAnd the other ");
        Green();
        Console.Write("winners ");
        Yellow();
        Console.Write($"(");
        Green();
        Console.Write($"{countRemainingWinners}");
        Yellow();
        Console.Write($")");
        Red();
        Console.Write(" forfeit");
        Blue();
        Console.Write(" the rest\nOf the missing chips ");
        Yellow();
        Console.Write($"(");
        Cyan();
        Console.Write($"{Math.Round(differenceChips - differenceChips * (biggestWinnerForfeitPercent / 100), 2)}");
        Yellow();
        Console.Write($")");
        Blue();
        Console.Write(" div equally\n");
        Yellow();
        Console.Write($"{biggestWinnerName.ToUpper()}");
        Red();
        Console.Write(" -= ");
        Cyan();
        Console.Write($"{Math.Round(biggestWinnerForfeitValue, 2)}");
        Red();
        Console.Write(" reduced");
        Blue();
        Console.Write(" from ");
        Red();
        Console.Write("Cashout\n");
        Green();
        Console.Write("Other W's");
        Red();
        Console.Write(" -= ");
        Cyan();
        Console.Write($"{Math.Round(leftOverPerWinner, 2)}");
        Red();
        Console.Write(" reduced ");
        Blue();
        Console.Write("to each ");
        Red();
        Console.Write("Cashout");
        Magenta();
        Console.Write(".");


        #region Remainder sol 3
        // Remainder should be extremely small if at all, I've implemented the logic below to handle it.
        if (Math.Round(biggestWinnerForfeitValue + Math.Round(leftOverPerWinner * countRemainingWinners, 2), 2) != differenceChips)
        {
            double remainder = Math.Round(differenceChips - Math.Round(biggestWinnerForfeitValue + leftOverPerWinner * countRemainingWinners, 2), 2);
            int sentenceLength = 38 + Convert.ToString(leftOverPerWinner).Length; // 38 = Independent words

            // The remainder should be give to the biggest loser (increases profit => less to pay)
            if (remainder < 0)
            {
                remainderPlayer3 = smallestWinnerName;
                remainder3 = remainder;
                PrintRemainderSolutionWinnersLose(sentenceLength, remainder3, remainderPlayer3);
                // For DB print of remainder
                remainderPositive = true;
            }
            else
            {
                remainderPlayer3 = biggestWinnerName;
                remainder3 = remainder;
                PrintRemainderSolutionWinnersLose(sentenceLength, remainder3, remainderPlayer3);
            }
        }
        #endregion
        ResetColor();

        PrintSolutionImplement2();

        solutionAnswer = GetSolutionResponse2(Console.ReadLine(), replitCharCount);

        if (solutionAnswer == "1")
        {
            foreach (var player in playerStatsDic)
            {
                if (player.Key == biggestWinnerName)
                {
                    player.Value.BuyOut = Math.Round(biggestWinnerCashout - differenceChips, 2);
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: false, typeSolution: "1", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestWinnerName);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer == "2")
        {
            addRemainderOnce = false;
            foreach (var player in playerStatsDic)
            {
                foreach (var winner in winnersList)
                {
                    if (player.Key == winner.Key)
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut - Math.Round(differenceChips / countWinners, 2), 2);
                    if (player.Key == remainderPlayer2 && addRemainderOnce == false)
                    {
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut - remainder2, 2); // if there is a remainder, deduct it from the designated player
                        addRemainderOnce = true;
                    }
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: false, typeSolution: "2", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestWinnerName, equalSplit: Convert.ToString(Math.Round(differenceChips / countWinners, 2)), remainder: remainder2, remainderPlayer: remainderPlayer2);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer == "3")
        {
            addRemainderOnce = false;
            addBiggestOnce = false;
            foreach (var player in playerStatsDic)
            {
                foreach (var winner in winnersList)
                {
                    if (player.Key == biggestWinnerName && addBiggestOnce == false)
                    {
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut - Math.Round(biggestWinnerForfeitValue, 2), 2);
                        addBiggestOnce = true;
                    }
                    else if (player.Key == winner.Key && player.Key != biggestWinnerName)
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut - Math.Round(leftOverPerWinner, 2), 2);
                    if (player.Key == remainderPlayer3 && addRemainderOnce == false)
                    {
                        player.Value.BuyOut = Math.Round(player.Value.BuyOut - remainder3, 2); // if there is a remainder, deduct it from the designated player
                        addRemainderOnce = true;
                    }
                }
            }
            Console.Clear();
            gameStatsStringResolved = ExtractDataStringFromDictionary(playerStatsDic);
            gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, gameStatsStringResolved);
            gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: false, typeSolution: "3", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips), biggestPlayer: biggestWinnerName, Convert.ToString(Math.Round(leftOverPerWinner, 2)), Convert.ToString(Math.Round(biggestWinnerForfeitValue, 2)), Convert.ToString(biggestWinnerForfeitPercent), remainder: remainder3, remainderPlayer: remainderPlayer3);

            CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, true, gameStatsStringResolved);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("new"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForGameStatsString();
            string validStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);
            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers);
            return;
        }
        else if (solutionAnswer.ToLower().Contains("alt"))
        {
            ResetDic(playerStatsDic);
            Console.WriteLine();
            AskForUpdatedGameStatsString();
            string newValidStats = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
            string validStats = AlterPlayersDataString(gameStatsString, newValidStats);
            RecordStatsStringAndAddToDictionary(validStats, playerStatsDic);

            // AskIfGameSolvedDueToAlter(44) = Will set the bool value of if the game was resolved due to altering the data string
            bool altUsedForResolve = AskIfGameSolvedDueToAlter(44);

            if (altUsedForResolve == true)
            {
                gameStatsStringResolved = GetDifferenceInTwoGameStrings(gameStatsString, validStats);
                gameStatsStringResolved = GetResolvedSolutionForDB(losersGainSolutionType: false, typeSolution: "alt", changesToGameString: gameStatsStringResolved, differenceChips: Convert.ToString(differenceChips));
            }

            Console.Clear();
            CalcRun(validStats, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers, altUsedForResolve, gameStatsStringResolved);
            return;
        }
        else return;

        #endregion
    }

    #endregion

    #endregion


    #endregion


























    #region Who Owes Who


    int gameID = GetMaxGameIDFromDB(filePathDBGameHistory);


    PrintH2WithTime(middleIndexOfStatsPrinting, gameID, isUsingReplit);
    int whoOwesWhoIndex = PrintWhoOwesWhoHeading(middleIndexOfStatsPrinting);

    Console.WriteLine();


    int printCashIndex = PrintWhoOwesWho(winnersList, losersList, longestWinnerName, longestLoserName, whoOwesWhoIndex, minTransfer, favPlayers);

    #endregion

    Console.WriteLine();

    PrintCashOnTable(playerStatsDic, printCashIndex, longestLoserName);

    Console.WriteLine();
    Console.WriteLine();

    // Print wining sentence
    PrintSentence(replitCharCount, biggestWinnerName, biggestWinnerValue);

    Console.WriteLine();

    #region Record to DB


    bool saveStats = PrintIfWantToRecordGameStats(replitCharCount);
    if (saveStats == true)
    {
        UpdateProfitLossDB(filePathDBProfitLossTracker, playerStatsDic);
        UpdateGameHistoryDB(filePathDBGameHistory, filePathDBMilestones, playerStatsDic, winnersList, losersList, longestPlayerName, longestWinnerName, longestLoserName, longestPlayerBuyIn, longestPlayerBuyOut, longestPlayerProfitOrLoss, solutionUsed, biggestWinnerName, biggestWinnerValue, biggestLoserName, biggestLoserValue, isUsingReplit, favPlayers, gameStatsString, gameStatsStringResolved);
    }



    #endregion
}

// If 2 players have the same profit/loss randomize them (or not) 50% 50%
static Dictionary<string, PlayerStats> RandomizeEqualPlayers(Dictionary<string, PlayerStats> playerStatsDic)
{
    Random random = new Random();

    List<string> keys = new List<string>(playerStatsDic.Keys);

    for (int i = 0; i < keys.Count - 1; i++)
    {
        string player1Key = keys[i];
        PlayerStats player1 = playerStatsDic[player1Key];
        double player1ProfitOrLoss = Math.Round(player1.BuyOut - player1.BuyIn, 2);
        double player1TotalSum = player1.BuyOut + player1.BuyIn;

        // Skip if player is "Giora"
        if (player1Key == "Giora") continue;

        for (int j = i + 1; j < keys.Count; j++)
        {
            string player2Key = keys[j];
            PlayerStats player2 = playerStatsDic[player2Key];
            double player2ProfitOrLoss = Math.Round(player2.BuyOut - player2.BuyIn, 2);
            double player2TotalSum = player2.BuyOut + player2.BuyIn;

            // Skip if player is "Giora" or the profit/loss values do not match
            if (player2Key == "Giora" || player1ProfitOrLoss != player2ProfitOrLoss || player1TotalSum == 0 || player2TotalSum == 0) continue;

            int rndNum = random.Next(0, 2);
            // Randomize swapping players
            if (rndNum == 1)
            {
                // Save player stats in temporary variables
                var player1Stats = playerStatsDic[player1Key];
                var player2Stats = playerStatsDic[player2Key];

                // Remove both players from the dictionary
                playerStatsDic.Remove(player1Key);
                playerStatsDic.Remove(player2Key);

                // Re-add the players in opposite order
                playerStatsDic.Add(player1Key, player1Stats);
                playerStatsDic.Add(player2Key, player2Stats);
            }
        }
    }
    return playerStatsDic;
}

static int GetMaxGameIDFromDB(string filePathDBGameHistory)
{
    // Read all lines from the file and find the max game ID in order to set the new game ID to be a sequel
    string[] lines = File.ReadAllLines(filePathDBGameHistory);

    int maxGameID = 0;
    int gameIDcurrent = 0;
    string gameIDcurrentStr = "";

    foreach (var line in lines)
    {

        if (line.Contains("Game ID"))
        {
            string[] parts = line.Split('['); // Game ID : [000] = "Game ID :","000]"
            gameIDcurrentStr = ReplaceCharAtIndex(parts[1], parts[1].Length - 1, ' ');
            gameIDcurrentStr = gameIDcurrentStr.Trim();
            gameIDcurrent = Convert.ToInt32(gameIDcurrentStr.Substring(0, gameIDcurrentStr.Length - 1)); // Get the number from the string

            // Check if this number is bigger than the max ID, if it is, set it as the new max ID
            if (gameIDcurrent > maxGameID) maxGameID = gameIDcurrent;
        }
    }
    maxGameID++; // Add 1 to maxGameID to use the new ID in the new to be added game.
    return maxGameID;
}


static bool PrintIfWantToRecordGameStats(int replitCharCount)
{
    Console.WriteLine("════════════════════════════════════════════");
    Blue();
    Console.Write("Would you like to ");
    Green();
    Console.Write("save ");
    Magenta();
    Console.Write("game stats ");
    Blue();
    Console.Write("to DBs: ");
    ResetColor();
    return GetYesOrNo(Console.ReadLine(), replitCharCount);
}


static void UpdateProfitLossDB(string filePath, Dictionary<string, PlayerStats> playerStatsDic)
{
    // Update profit/loss for every player
    if (File.Exists(filePath))
    {
        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Create a list to hold the updated lines
        List<string> updatedLines = new List<string>();

        foreach (var line in lines)
        {
            if (line.Contains(":")) // Go and update the players values with the new game (if player did not play the value won't change)
            {
                bool playerUpdated = false;

                foreach (var player in playerStatsDic)
                {
                    double currentPlayerValue = 0;
                    string newStringLine = "";
                    string[] parts = line.Split(':');

                    if (parts[0].Trim() == player.Key && (player.Value.BuyIn != player.Value.BuyOut))
                    {
                        // 2 is the correct format for the recording player stats
                        if (parts.Length == 2)
                        {
                            newStringLine += parts[0];
                            newStringLine += ": ";
                            // Trim and take the value part of the player
                            parts[1] = parts[1].Trim(); // Trim excess ' ' (spaces)
                            currentPlayerValue = Convert.ToDouble(parts[1]);
                            double newValue = Math.Round(currentPlayerValue + (player.Value.BuyOut - player.Value.BuyIn), 2);
                            if (newValue >= 0) newStringLine += " "; // Add another space to balance negative numbers in the eye

                            // Add new value to the new line string
                            newStringLine += Convert.ToString(newValue);

                            updatedLines.Add(newStringLine);

                            playerUpdated = true;
                            break;
                        }
                    }
                }
                if (playerUpdated == false)
                {
                    updatedLines.Add(line); // Add same line if player didn't play / went out neutral
                }
            }
            else updatedLines.Add(line); // Add line if it's not a player (heading)
        }

        // Write all lines back to the file
        File.WriteAllLines(filePath, updatedLines);
    }
    else Console.WriteLine("File no exists :(");
}

static void UpdateMileStonesDB(string filePath, string recordCashOnTable = "", string recordMaxPlayerProfit = "", string recordMaxPlayerLoss = "")
{
    // Update profit/loss for every player
    if (File.Exists(filePath))
    {
        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Create a list to hold the updated lines
        List<string> updatedLines = new List<string>();


        // Update values if a string was sent to the function from 'UpdateGameHistoryDB() function
        foreach (var line in lines)
        {
            if (line.Contains("Most chips on table") && recordCashOnTable != "")
            {
                updatedLines.Add(recordCashOnTable);
            }
            else if (line.Contains("Highest profit") && recordMaxPlayerProfit != "")
            {
                updatedLines.Add(recordMaxPlayerProfit);
            }
            else if (line.Contains("Highest loss") && recordMaxPlayerLoss != "")
            {
                updatedLines.Add(recordMaxPlayerLoss);
            }
            else updatedLines.Add(line); // If value was not sent / line is different add it instead.
        }

        // Write all lines back to the file
        File.WriteAllLines(filePath, updatedLines);
    }
    else Console.WriteLine("File no exists :(");
}

static void PrintAskLocation()
{
    Blue();
    Console.Write("Location Shortcuts");
    Magenta();
    Console.Write(":");

    Blue();
    Console.Write("\nE.g. ");
    Yellow();
    Console.Write("(");
    Magenta();
    Console.Write("Name");
    Yellow();
    Console.Write(" || ");
    DarkRed();
    Console.Write("First ");
    Blue();
    Console.Write("name ");
    Magenta();
    Console.Write("char");
    DarkGray();
    Console.Write(" + ");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write("p");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(" || ");
    Magenta();
    Console.Write("cof");
    Yellow();
    Console.Write(")");

    Blue();
    Console.Write("\nPlease input ");
    Magenta();
    Console.Write("game location");
    Blue();
    Console.Write(": ");
    ResetColor();
}

static string GetResolvedSolutionForDB(bool losersGainSolutionType, string typeSolution, string changesToGameString, string differenceChips, string biggestPlayer = "", string equalSplit = "", string percentageSplit = "", string percentOfSolution3 = "", double remainder = 0, string remainderPlayer = "")
{
    switch (typeSolution)
    {
        case "1":
            typeSolution = losersGainSolutionType ? $"1 - The biggest loser '{biggestPlayer}' gets all of the extra chips to his cashout ({differenceChips}₪)" : $"1 - The biggest winner '{biggestPlayer}' covers all of the missing chips from his cashout ({differenceChips}₪)";
            break;
        case "2":
            typeSolution = losersGainSolutionType ? $"2 - All extra chips are added equally to each loser's cashout ({equalSplit}₪)" : $"2 - All missing chips are deducted equally from each winner's cashout ({equalSplit}₪)";
            break;
        case "3":
            typeSolution = losersGainSolutionType ? $"3 - The biggest loser '{biggestPlayer}' gets {percentOfSolution3}% of the extra chips added to his cashout ({percentageSplit}₪), while the rest is added equally to each loser's cashout ({equalSplit}₪)" : $"3 - The biggest winner '{biggestPlayer}' covers {percentOfSolution3}% of the missing chips from to his cashout ({percentageSplit}₪), while the remainder is deducted equally from each winner's cashout ({equalSplit}₪)";
            break;
        case "alt":
            typeSolution = "Alter - Direct modifications were applied to the game string";
            break;
        default:
            typeSolution = "Error: Error in typeSolution";
            break;
    }
    string temp = "Problem:    ";
    temp += losersGainSolutionType ? $"Missing chips ({differenceChips}₪) - Losers Gain Error - Total cash-out is less than total buy-in - Losers gain unclaimed chips" : $"Extra chips ({differenceChips}₪) - Winners Lose Error - Total cash-out exceeds total buy-in - Winners forfeit excess winnings";
    temp += "\nSolution:   " + typeSolution;
    // Add remainder if there was one
    if (remainder != 0)
    {
        string remainderSentence = "";
        if (losersGainSolutionType == true)
        {
            if (remainder >= 0)
            {
                remainderSentence = $"{remainderPlayer} (biggest loser) gets an extra ({Convert.ToString(remainder)}₪)";
            }
            else
            {
                remainderSentence = $"{remainderPlayer} (smallest loser) misses out on ({Convert.ToString(remainder)}₪)";
            }
        }
        else
        {
            if (remainder >= 0)
            {
                remainderSentence = $"{remainderPlayer} (biggest winner) forfeits an extra ({Convert.ToString(remainder * -1)}₪)";
            }
            else
            {
                remainderSentence = $"{remainderPlayer} (smallest winner) keeps an extra ({Convert.ToString(Math.Abs(remainder))}₪)";
            }
        }
        temp += "\nRemainder:  " + remainderSentence;
    }
    temp += "\nGS Changes: " + changesToGameString;
    return temp;
}

// WAYPOINT: UpdateGameHistoryDB()
static void UpdateGameHistoryDB(string filePathDBGameHistory, string filePathDBMilestones, Dictionary<string, PlayerStats> playerStatsDic, List<KeyValuePair<string, double>> winnersList, List<KeyValuePair<string, double>> losersList, string longestPlayerName, string longestWinnerName, string longestLoserName, string longestPlayerBuyIn, string longestPlayerBuyOut, string longestPlayerProfitOrLoss, bool solutionUsed, string biggestWinnerName, double biggestWinnerProfit, string biggestLoserName, double biggestLoserLoss, bool isUsingReplit, string[] favPlayers, string gameStatsString, string gameStatsStringResolved = "")
{
    // Order Dic for printing (Remove all of the players with a buyIn and buyOut of '0')
    playerStatsDic = OrderDicForPrinting(playerStatsDic);

    // WORKS
    #region Get Line
    int separationLineLength = GetLongestLineLengthForGameHistoryDB(longestPlayerName, longestPlayerBuyIn, longestPlayerBuyOut, longestPlayerProfitOrLoss);
    string separationLine = "";
    for (int i = 0; i < separationLineLength; i++)
    {
        separationLine += "═";
    }
    #endregion

    if (File.Exists(filePathDBGameHistory))
    {
        #region Game Information




        Console.WriteLine("════════════════════════════════════════════");




        #region Get Max Game ID
        // Get the new GameID
        int newGameID = GetMaxGameIDFromDB(filePathDBGameHistory);
        #endregion



        #region Get Location
        // Ask for the location of the game
        PrintAskLocation();
        string gameLocation = TrimAndCapitalizeSentence(Console.ReadLine());
        gameLocation = CheckAndSetSavedLocation(gameLocation);
        #endregion


        Console.WriteLine("════════════════════════════════════════════");


        #region Get Game Date
        string dateTimeInfo = GetTimeStringForDB(isUsingReplit);   // 06/09/2024 - 05:27|Thursday
        string dateTime = dateTimeInfo.Split('|')[0]; // 06/09/2024 - 05:27
        string day = dateTimeInfo.Split('|')[1];      // Thursday
        #endregion




        #region Get Resolved
        // Better clarification for true or false (Capital letters addition)
        string solutionUsedString = "False";
        if (solutionUsed == true) solutionUsedString = "True";
        #endregion





        // Bases for all the data
        string baseID = "║ Game ID       : [";
        string baseDay = "║ Game Day      : ";
        string baseLocation = "║ Location      : ";
        string baseProgramRT = "║ Program RT    : ";
        string baseResolved = "║ Game Resolved : ";
        string baseCashOnTable = "║ Cash on Table : ";

        // Completed bases ^^^ 
        // Game ID : 
        baseID += newGameID;
        baseID += "]";
        for (int i = baseID.Length + 1; i < separationLineLength; i++) // +1 for '║'
        {
            baseID += " ";
        }
        baseID += "║";
        // Game Day :
        baseDay += day;
        for (int i = baseDay.Length + 1; i < separationLineLength; i++) // +1 for '║'
        {
            baseDay += " ";
        }
        baseDay += "║";
        // Game Location :
        baseLocation += gameLocation;
        for (int i = baseLocation.Length + 1; i < separationLineLength; i++) // +1 for '║'
        {
            baseLocation += " ";
        }
        baseLocation += "║";
        // Program RT :
        baseProgramRT += dateTime;
        for (int i = baseProgramRT.Length + 1; i < separationLineLength; i++) // +1 for '║'
        {
            baseProgramRT += " ";
        }
        baseProgramRT += "║";
        // Game Resolved :
        baseResolved += solutionUsedString;
        for (int i = baseResolved.Length + 1; i < separationLineLength; i++) // +1 for '║'
        {
            baseResolved += " ";
        }
        baseResolved += "║";
        // Cash on Table : 
        double sumCashOnTable = 0;
        foreach (var player in playerStatsDic)
        {
            sumCashOnTable += player.Value.BuyIn;
        }
        baseCashOnTable += sumCashOnTable;
        baseCashOnTable += "₪";
        for (int i = baseCashOnTable.Length + 1; i < separationLineLength; i++) // +1 for '║'
        {
            baseCashOnTable += " ";
        }
        baseCashOnTable += "║";



        #endregion


        // Get spaces for each heading

        int isLineEven = separationLineLength % 2; // If line is even == 0, else == 1

        string spacesRecord1 = "";
        string spacesRecord2 = "";

        string spacesGameInformation1 = "";
        string spacesGameInformation2 = "";

        string spacesPlayerStats1 = "";
        string spacesPlayerStats2 = "";

        string spacesGameDebts1 = "";
        string spacesGameDebts2 = "";



        // Set spaces main heading lines 
        for (int i = 0; i < (separationLineLength - 20) / 2 + isLineEven; i++) // 18 == "#₪# RECORD BROKE #₪#"
        {
            spacesRecord1 += " ";
        }
        for (int i = 0; i < (separationLineLength - 16) / 2 + isLineEven; i++) // 16 == "Game Information"
        {
            spacesGameInformation1 += " ";
        }
        for (int i = 0; i < (separationLineLength - 12) / 2 + isLineEven; i++) // 12 == "Player Stats"
        {
            spacesPlayerStats1 += " ";
        }
        for (int i = 0; i < (separationLineLength - 10) / 2 + isLineEven; i++) // 10 == "Game Debts"
        {
            spacesGameDebts1 += " ";
        }
        for (int i = 0; i < (separationLineLength - 20) / 2; i++) // 18 == "#₪# RECORD BROKE #₪#"
        {
            spacesRecord2 += " ";
        }
        for (int i = 0; i < (separationLineLength - 16) / 2; i++) // 16 == "Game Information"
        {
            spacesGameInformation2 += " ";
        }
        for (int i = 0; i < (separationLineLength - 12) / 2; i++) // 12 == "Player Stats"
        {
            spacesPlayerStats2 += " ";
        }
        for (int i = 0; i < (separationLineLength - 10) / 2; i++) // 10 == "Game Debts"
        {
            spacesGameDebts2 += " ";
        }


        // Heading names
        string headingRecord = "#₪# RECORD BROKE #₪#";
        string headingGameInformation = "GAME INFORMATION";
        string headingPlayerStats = "PLAYER STATS";
        string headingGameDebts = "GAME DEBTS";


        // Heading lines

        // Record
        string lineRecordH1 = separationLine;
        lineRecordH1 = ReplaceCharAtIndex(lineRecordH1, 0, '╠');
        lineRecordH1 = ReplaceCharAtIndex(lineRecordH1, lineRecordH1.Length - 1, '╣');
        string lineRecordH2 = "";
        lineRecordH2 += spacesRecord1 += headingRecord += spacesRecord2;
        lineRecordH2 = ReplaceCharAtIndex(lineRecordH2, 0, '║');
        lineRecordH2 = ReplaceCharAtIndex(lineRecordH2, lineRecordH2.Length - 1, '║');
        string lineRecordH3 = separationLine;
        lineRecordH3 = ReplaceCharAtIndex(lineRecordH3, 0, '╠');
        lineRecordH3 = ReplaceCharAtIndex(lineRecordH3, lineRecordH3.Length - 1, '╣');
        // Game Information
        string lineInformationH1 = separationLine;
        lineInformationH1 = ReplaceCharAtIndex(lineInformationH1, 0, '╔');
        lineInformationH1 = ReplaceCharAtIndex(lineInformationH1, lineInformationH1.Length - 1, '╗');
        string lineInformationH2 = "";
        lineInformationH2 += spacesGameInformation1 += headingGameInformation += spacesGameInformation2;
        lineInformationH2 = ReplaceCharAtIndex(lineInformationH2, 0, '║');
        lineInformationH2 = ReplaceCharAtIndex(lineInformationH2, lineInformationH2.Length - 1, '║');
        string lineInformationH3 = separationLine;
        lineInformationH3 = ReplaceCharAtIndex(lineInformationH3, 0, '╠');
        lineInformationH3 = ReplaceCharAtIndex(lineInformationH3, lineInformationH3.Length - 1, '╣');
        // Player Stats
        string lineStatsH1 = separationLine;
        lineStatsH1 = ReplaceCharAtIndex(lineStatsH1, 0, '╠');
        lineStatsH1 = ReplaceCharAtIndex(lineStatsH1, lineStatsH1.Length - 1, '╣');
        string lineStatsH2 = "";
        lineStatsH2 += spacesPlayerStats1 += headingPlayerStats += spacesPlayerStats2;
        lineStatsH2 = ReplaceCharAtIndex(lineStatsH2, 0, '║');
        lineStatsH2 = ReplaceCharAtIndex(lineStatsH2, lineStatsH2.Length - 1, '║');
        string lineStatsH3 = separationLine;
        lineStatsH3 = ReplaceCharAtIndex(lineStatsH3, 0, '╠');
        lineStatsH3 = ReplaceCharAtIndex(lineStatsH3, lineStatsH3.Length - 1, '╣');
        // Get Special indexes for table (will change according to the data received by the dictionary)
        int indexReplacePlayer = 9;   // Default index
        if (longestPlayerName.Length - "Player".Length > 0) indexReplacePlayer += longestPlayerName.Length - "Player".Length;
        int indexReplaceBuyIn = indexReplacePlayer + 8;   // Default index
        if (longestPlayerBuyIn.Length + 1 - "BuyIn".Length > 0) indexReplaceBuyIn += longestPlayerBuyIn.Length + 1 - "BuyIn".Length;         //+1 for ₪
        int indexReplaceCashout = indexReplaceBuyIn + 10; // Default index
        if (longestPlayerBuyOut.Length + 1 - "Cashout".Length > 0) indexReplaceCashout += longestPlayerBuyOut.Length + 1 - "Cashout".Length; //+1 for ₪
        int indexReplaceProfitOrLoss = indexReplaceCashout + 12; // Default index
        if (longestPlayerBuyOut.Length + 2 - "Gain/Loss".Length > 0) indexReplaceProfitOrLoss += longestPlayerProfitOrLoss.Length + 2 - "Gain/Loss".Length; //+2 for '₪' & '±'


        lineStatsH3 = ReplaceCharAtIndex(lineStatsH3, indexReplacePlayer, '╦');
        lineStatsH3 = ReplaceCharAtIndex(lineStatsH3, indexReplaceBuyIn, '╦');
        lineStatsH3 = ReplaceCharAtIndex(lineStatsH3, indexReplaceCashout, '╦');

        // Player stats columns names
        string lineColumnHeaders = "║ Player ";

        // Calc spaces for each column heading

        string statsSpacesPlayer = "";
        string statsSpacesBuyIn = "";
        string statsSpacesCashout = "";
        string statsSpacesProfitOrLoss = "";


        for (int i = lineColumnHeaders.Length; i < indexReplacePlayer; i++)
        {
            statsSpacesPlayer += " ";
        }
        lineColumnHeaders += statsSpacesPlayer;
        lineColumnHeaders += "║ BuyIn ";

        for (int i = lineColumnHeaders.Length; i < indexReplaceBuyIn; i++)
        {
            statsSpacesBuyIn += " ";
        }
        lineColumnHeaders += statsSpacesBuyIn;
        lineColumnHeaders += "║ Cashout ";

        for (int i = lineColumnHeaders.Length; i < indexReplaceCashout; i++)
        {
            statsSpacesCashout += " ";
        }
        lineColumnHeaders += statsSpacesCashout;
        lineColumnHeaders += "║ Gain/Loss ";

        for (int i = lineColumnHeaders.Length; i < indexReplaceProfitOrLoss; i++)
        {
            statsSpacesProfitOrLoss += " ";
        }
        lineColumnHeaders += statsSpacesProfitOrLoss;
        lineColumnHeaders += "║";

        string lineColumnHeaders2 = lineStatsH3;

        lineColumnHeaders2 = ReplaceCharAtIndex(lineColumnHeaders2, indexReplacePlayer, '╬');
        lineColumnHeaders2 = ReplaceCharAtIndex(lineColumnHeaders2, indexReplaceBuyIn, '╬');
        lineColumnHeaders2 = ReplaceCharAtIndex(lineColumnHeaders2, indexReplaceCashout, '╬');




        // Game Debts
        // ADD THE MIDDLE LINES HERE!!!!! - Game Debts
        string lineDebtsH1 = separationLine;
        lineDebtsH1 = ReplaceCharAtIndex(lineDebtsH1, 0, '╠');
        lineDebtsH1 = ReplaceCharAtIndex(lineDebtsH1, lineDebtsH1.Length - 1, '╣');
        // Adjust special char places for 'Player Stats' table (taken from previous calc made)
        lineDebtsH1 = ReplaceCharAtIndex(lineDebtsH1, indexReplacePlayer, '╩');
        lineDebtsH1 = ReplaceCharAtIndex(lineDebtsH1, indexReplaceBuyIn, '╩');
        lineDebtsH1 = ReplaceCharAtIndex(lineDebtsH1, indexReplaceCashout, '╩');

        string lineDebtsH2 = "";
        lineDebtsH2 += spacesGameDebts1 += headingGameDebts += spacesGameDebts2;
        lineDebtsH2 = ReplaceCharAtIndex(lineDebtsH2, 0, '║');
        lineDebtsH2 = ReplaceCharAtIndex(lineDebtsH2, lineDebtsH2.Length - 1, '║');
        string lineDebtsH3 = separationLine;
        lineDebtsH3 = ReplaceCharAtIndex(lineDebtsH3, 0, '╠');
        lineDebtsH3 = ReplaceCharAtIndex(lineDebtsH3, lineDebtsH3.Length - 1, '╣');

        // End line 
        string lineEnd = separationLine;
        lineEnd = ReplaceCharAtIndex(lineEnd, 0, '╚');
        lineEnd = ReplaceCharAtIndex(lineEnd, lineEnd.Length - 1, '╝');




        using (StreamWriter writer = new StreamWriter(filePathDBGameHistory, append: true))
        {

            writer.WriteLine(lineInformationH1);
            writer.WriteLine(lineInformationH2);
            writer.WriteLine(lineInformationH3);
            writer.WriteLine(baseID);
            writer.WriteLine(baseDay);
            writer.WriteLine(baseLocation);
            writer.WriteLine(baseProgramRT);
            writer.WriteLine(baseResolved);
            writer.WriteLine(baseCashOnTable);
            writer.WriteLine(lineStatsH1);
            writer.WriteLine(lineStatsH2);
            writer.WriteLine(lineStatsH3);
            writer.WriteLine(lineColumnHeaders);
            writer.WriteLine(lineColumnHeaders2);

            // Print player stats from dictionary, dictionary is sorted when it is sent to the function.
            foreach (var player in playerStatsDic)
            {
                writer.Write($"║ {player.Key}");
                for (int i = player.Key.Length; i < indexReplacePlayer - 2; i++)
                {
                    writer.Write(" ");
                }
                writer.Write($"║ {player.Value.BuyIn}₪");

                for (int i = Convert.ToString(player.Value.BuyIn).Length + indexReplacePlayer + 3; i < indexReplaceBuyIn; i++) // +3 ' ' + '║' + '₪'
                {
                    writer.Write(" ");
                }
                writer.Write($"║ {player.Value.BuyOut}₪");

                for (int i = Convert.ToString(player.Value.BuyOut).Length + indexReplaceBuyIn + 3; i < indexReplaceCashout; i++) // +3 ' ' + '║' + '₪'
                {
                    writer.Write(" ");
                }
                if ((player.Value.BuyIn == player.Value.BuyOut) && (player.Value.BuyIn != 0 || player.Value.BuyOut != 0))
                {
                    writer.Write("║  ");
                    writer.Write("∞ ");
                }
                else
                {
                    writer.Write(Math.Round(player.Value.BuyOut - player.Value.BuyIn, 2) < 0 ? "║ -" : "║ +");
                    writer.Write($"{Math.Abs(Math.Round(player.Value.BuyOut - player.Value.BuyIn, 2))}₪");
                }




                for (int i = Convert.ToString(Math.Abs(Math.Round(player.Value.BuyOut - player.Value.BuyIn, 2))).Length + indexReplaceCashout + 4; i < indexReplaceProfitOrLoss; i++) // +3 ' ' + '±' + '║' + '₪'
                {
                    writer.Write(" ");
                }
                writer.Write($"║");

                // Go down a line
                writer.WriteLine();
            }
            // ADD RECORDS BROKEN HERE

            writer.WriteLine(lineDebtsH1);
            writer.WriteLine(lineDebtsH2);
            writer.WriteLine(lineDebtsH3);

            string debtsToString = PrintWhoOwesWhoHistoryDB(winnersList, losersList, longestPlayerName, longestLoserName, separationLineLength - 1, favPlayers);
            writer.Write(debtsToString);


            /// If any records are broken in the game, Add them under the game history
            /// Afterwords update them in the Milestones DB file.

            // Read records from MileStones DB file path and save them as variables
            double recordChipsOnTable = 0;
            double recordMaxPlayerProfit = 0;
            double recordMaxPlayerLoss = 0;

            if (File.Exists(filePathDBMilestones))
            {
                string[] milestoneLines = File.ReadAllLines(filePathDBMilestones);

                foreach (var line in milestoneLines)
                {
                    if (line.Contains("chips on table"))
                    {
                        string[] parts = line.Split(':');
                        recordChipsOnTable = Convert.ToDouble(parts[1].Trim().Substring(0, parts[1].Length - 5)); // -5 = (-1 to make it an index -3 again to remove '₪   '
                    }
                    else if (line.Contains("Highest profit"))
                    {
                        string[] parts = line.Split(':');
                        recordMaxPlayerProfit = Convert.ToDouble(parts[1].Trim().Substring(0, parts[1].Length - 5)); // -5 = (-1 to make it an index -3 again to remove '₪   '
                    }
                    else if (line.Contains("Highest loss"))
                    {
                        string[] parts = line.Split(':');
                        recordMaxPlayerLoss = Convert.ToDouble(parts[1].Trim().Substring(1, parts[1].Length - 5)); // 1 to remove '-', -4 = (-1 to make it an index -3 again to remove '₪  '
                    }
                }
            }

            // Check previous records to new ones
            // If this boolean values will be true we will adjust the table formatting for the History DB
            bool brokenRecordChipsOnTable = false;
            bool brokenRecordMaxPlayerProfit = false;
            bool brokenRecordMaxPlayerLoss = false;

            if (sumCashOnTable > recordChipsOnTable)
            {
                brokenRecordChipsOnTable = true;
            }
            if (biggestWinnerProfit > recordMaxPlayerProfit)
            {
                brokenRecordMaxPlayerProfit = true;
            }
            if (biggestLoserLoss > recordMaxPlayerLoss)
            {
                brokenRecordMaxPlayerLoss = true;
            }

            if (brokenRecordChipsOnTable == true || brokenRecordMaxPlayerProfit == true || brokenRecordMaxPlayerLoss == true)
            {
                writer.WriteLine(lineRecordH1);
                writer.WriteLine(lineRecordH2);
                writer.WriteLine(lineRecordH3);
                if (brokenRecordChipsOnTable == true)
                {
                    string recordChipsOnTablePrint = "";
                    recordChipsOnTablePrint += $"║ Highest table money :  {sumCashOnTable}₪";
                    // Add spaces
                    for (int i = recordChipsOnTablePrint.Length; i < separationLine.Length - 1; i++) // -1 for "║"
                    {
                        recordChipsOnTablePrint += " ";
                    }
                    recordChipsOnTablePrint += "║";
                    writer.WriteLine(recordChipsOnTablePrint);

                    string forMilestoneDB = $"Most chips on table in one session :  {sumCashOnTable}₪  : [{dateTime.Split('-')[0].Trim()} - {day} - GameID:{newGameID}]";
                    UpdateMileStonesDB(filePathDBMilestones, recordCashOnTable: forMilestoneDB);
                }
                if (brokenRecordMaxPlayerProfit == true)
                {
                    string recordMaxPlayerProfitPrint = "";
                    recordMaxPlayerProfitPrint += $"║ Highest profit (1P) :  {biggestWinnerProfit}₪";
                    // Add spaces
                    for (int i = recordMaxPlayerProfitPrint.Length; i < separationLine.Length - 1; i++) // -1 for "║"
                    {
                        recordMaxPlayerProfitPrint += " ";
                    }
                    recordMaxPlayerProfitPrint += "║";
                    writer.WriteLine(recordMaxPlayerProfitPrint);

                    string forMilestoneDBPlayerProfit = $"Highest profit in a single session :  {biggestWinnerProfit}₪  : [{biggestWinnerName} - {dateTime.Split('-')[0].Trim()} - {day} - GameID:{newGameID}]";
                    UpdateMileStonesDB(filePathDBMilestones, recordMaxPlayerProfit: forMilestoneDBPlayerProfit);
                }
                if (brokenRecordMaxPlayerLoss == true)
                {
                    string recordMaxPlayerLossPrint = "";
                    recordMaxPlayerLossPrint += $"║ Highest loss (1P)   : -{biggestLoserLoss}₪";
                    // Add spaces
                    for (int i = recordMaxPlayerLossPrint.Length; i < separationLine.Length - 1; i++) // -1 for "║"
                    {
                        recordMaxPlayerLossPrint += " ";
                    }
                    recordMaxPlayerLossPrint += "║";
                    writer.WriteLine(recordMaxPlayerLossPrint);

                    string forMilestoneDBPlayerLoss = $"Highest loss in a single session   : -{biggestLoserLoss}₪  : [{biggestLoserName} - {dateTime.Split('-')[0].Trim()} - {day} - GameID:{newGameID}]";
                    UpdateMileStonesDB(filePathDBMilestones, recordMaxPlayerLoss: forMilestoneDBPlayerLoss);
                }
            }





            writer.WriteLine(lineEnd);
            // Separate each game history print from another 

            writer.WriteLine("Original Game String: " + gameStatsString);
            if (gameStatsStringResolved != "") writer.WriteLine("\nResolved Game Data: \n" + "▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼\n" + gameStatsStringResolved);
            writer.WriteLine("████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████████");
        }
    }
}

static string CheckAndSetSavedLocation(string location)
{
    string[] saveLocations = { "Hadera Home", "Chips of Fury", "Pardo's Place", "Idan's Place", "Vexy's Place", "Ron's Place", "Yonatan's Place" };
    switch (location.ToLower().Trim())
    {
        case "me":
        case "mp":
        case "my":
        case "myp":
        case "hm":
        case "home":
            location = saveLocations[0];
            break;
        case "cof":
        case "cf":
        case "chips":
        case "fury":
            location = saveLocations[1];
            break;
        case "pardo":
        case "pp":
            location = saveLocations[2];
            break;
        case "idan":
        case "ip":
            location = saveLocations[3];
            break;
        case "vex":
        case "vexy":
        case "vp":
            location = saveLocations[4];
            break;
        case "ron":
        case "rp":
            location = saveLocations[5];
            break;
        case "yon":
        case "yonatan":
        case "yp":
            location = saveLocations[6];
            break;
    }
    return location;
}

// WAYPOINT: RUN Methods

// [Chip Chop] method used for splitting pots equally between players + side pots if needed.
static void RunCC(double minGameChip, int replitCharCount)
{
    PrintLandingCC();
    // The string of the pot data as instructed by the PrintLandingCC()
    string potsInput = RecordValidPotStatsString(Console.ReadLine(), replitCharCount);
    Console.Clear();
    AnalyzePotsString(potsInput, minGameChip, replitCharCount);
    PrintPressAnyToExit();
    Console.ReadKey();
}

// [Poker Cash Split] method used for quickly settling the game debts with BB (Big Brain) algorithm
static void RunPCS(bool isUsingReplit, string filePathDBGameHistory, string filePathDBMilestones, string filePathDBProfitLossTracker, double minTransfer, int replitCharCount, string[] favPlayers, bool solutionUsed = false)
{
    Dictionary<string, PlayerStats> playerStatsDic = new Dictionary<string, PlayerStats>();
    // Clear the xx every time so it will be reset and not use previous values
    ResetDic(playerStatsDic);
    AddOGPlayerToDictionary(playerStatsDic);

    PrintLandingPCS();
    // The string of the pot data as instructed by the PrintLandingCC()
    string gameStatsString = RecordValidStatsString(Console.ReadLine(), playerStatsDic, replitCharCount);
    // Get the input of the players from the user
    RecordStatsStringAndAddToDictionary(gameStatsString, playerStatsDic);
    Console.Clear();
    CalcRun(gameStatsString, playerStatsDic, isUsingReplit, filePathDBGameHistory, filePathDBMilestones, filePathDBProfitLossTracker, minTransfer, replitCharCount, favPlayers);
    PrintPressAnyToExit();
    Console.ReadKey();
}

// Timer function
static void SetTimer(int time, int replitCharCount)
{
    // Start a thread to listen for a key press
    bool stopTimer = false;
    Thread keyListenerThread = new Thread(() =>
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);  // Wait for any key press
        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
        {
            stopTimer = true;  // Set flag to stop the timer
        }
    });
    keyListenerThread.IsBackground = true;
    keyListenerThread.Start();
    for (int timeLeft = time - 1; timeLeft >= 0; timeLeft--)
    {
        if (stopTimer == true) break;
        if (timeLeft == 15) DarkGreen();
        else if (timeLeft == 10) Yellow();
        else if (timeLeft == 5) DarkRed();
        if (timeLeft != time - 1) ClearConsoleLines(0, 1, replitCharCount);
        Console.Write(timeLeft % 2 == 0 ? "Tack, " : "Tick, ");
        Console.Write($"Time left: {timeLeft}\n");
        if (timeLeft == 0) break;
        Thread.Sleep(1025);
    }
    ClearConsoleLines(0, 1, replitCharCount);
    DarkMagenta();
    Console.Write(stopTimer == true ? "Time stopped abruptly!" : "*!*TIMES IS UP*!*");
    ResetColor();
    PrintPressAnyToExit();
    Console.ReadKey();
}

// WAYPOINT: Pot Methods
#region Pot Methods
static string SplitThatPot(double potValue, int playerForSplit, double minGameChip)
{
    double remainder = 0;
    while (potValue / playerForSplit % minGameChip != 0)
    {
        remainder += minGameChip;
        potValue -= minGameChip;
        if (potValue <= 0) return "0,0";
    }

    return $"{potValue / playerForSplit},{remainder}";
}
static void PrintErrorDataEmptyOrInvalid()
{
    Red();
    Console.Write("Error:\n");
    Red();
    Console.Write("Data inputted is invalid/null");

    ResetColor();
}

static void AnalyzePotsString(string stats, double minGameChip, int replitCharCount)
{
    string[] potsStrings = stats.Split(',');
    int potCounter = 0;

    // Handle empty / invalid string
    if (potsStrings.Length == 0 || potsStrings == null || potsStrings[0] == "")
    {
        PrintGeneralError();
        PrintErrorDataEmptyOrInvalid();
    }

    for (int i = 0; i < potsStrings.Length; i++)
    {
        // Order strings
        string[] potStatsTest = TrimWordsInString(potsStrings[i]);

        // Check if length is correct, else get a proper length
        // If blank ',   ' is put into the string, skip it.
        if (potStatsTest.Length == 0) continue;

        // Set variables for the stats we need and reset them every time
        double potSize = Convert.ToDouble(potStatsTest[0]);
        int players = Convert.ToInt32(potStatsTest[1]);

        string potSplit = SplitThatPot(potSize, players, minGameChip);

        double chipsPerPlayer = Convert.ToDouble(potSplit.Split(',')[0]);
        double remainder = Convert.ToDouble(potSplit.Split(',')[1]);

        // This variable will be used to count the pots and display a proper name of the pot
        potCounter++;

        if (chipsPerPlayer == 0 && remainder == 0)
        {
            PrintPotCalcError(potSize, players);
            continue;
        }

        string potName = "";

        if (potCounter == 1) potName = "MAIN POT";
        else potName = $"Side pot {potCounter - 1}";

        Yellow();
        Console.Write($"Pot: ");
        Magenta();
        Console.Write($"{potSize}");
        Green();
        Console.Write($"₪");
        Yellow();
        Console.Write($"\nPlayers: ");
        Magenta();
        Console.Write($"{players}");
        Blue();
        Console.Write("\nEach player of ");
        if (potCounter == 1) Console.Write("the ");
        Magenta();
        Console.Write(potName);
        Blue();
        Console.Write(" gets ");
        DarkGray();
        Console.Write("'");
        Green();
        Console.Write(chipsPerPlayer);
        DarkGray();
        Console.Write("'");
        if (remainder != 0)
        {
            Red();
            Console.Write("\nHeads up! ");
            Magenta();
            Console.Write("%");
            Red();
            Console.Write("Remainder");
            Magenta();
            Console.Write("% ");
            Blue();
            Console.Write("is in the house ");
            DarkGray();
            Console.Write("'");
            Red();
            Console.Write(remainder);
            DarkGray();
            Console.Write("'");
        }
        if (i != potsStrings.Length - 2) Console.Write("\n\n"); // -2 (because the string array will always have an extra empty string because of the extra ", ")
        ResetColor();
    }
}





#endregion











#region Other Methods

static string PrintDataTime(bool isUsingReplit)
{
    DateTime nowDateTime = DateTime.Now;

    if (isUsingReplit == true) nowDateTime = ReplitTimeAdjust(nowDateTime);

    return nowDateTime.ToString("dd/MM/yyyy HH:mm");
}

static string PrintDay(bool isUsingReplit, bool previous)
{
    DateTime nowDateTime = DateTime.Now;
    if (isUsingReplit == true) nowDateTime = ReplitTimeAdjust(nowDateTime);

    if (previous == true) nowDateTime = nowDateTime.AddDays(-1);
    return nowDateTime.ToString("ddd");
}

static string ReplaceCharAtIndex(string str, int index, char newChar)
{
    // Convert the string to a char array
    char[] charArray = str.ToCharArray();

    // Replace the character at the specified index
    if (index >= 0 && index < charArray.Length)
    {
        charArray[index] = newChar;
    }

    // Convert the char array back to a string
    return new string(charArray);
}


static string GetTimeStringForDB(bool isUsingReplit)
{
    DateTime nowDateTime = DateTime.Now;
    if (isUsingReplit == true) nowDateTime = ReplitTimeAdjust(nowDateTime);

    string date = nowDateTime.ToString("dd/MM/yyyy");
    string time = nowDateTime.ToString("HH:mm");

    int timeInInt = Convert.ToInt32(time.Split(':')[0]);

    if (timeInInt >= 0 && timeInInt <= 9) nowDateTime = nowDateTime.AddDays(-1); // If game ended between 0 - 9 (show the previous day)
    string gameDay = nowDateTime.ToString("dddd"); ;

    return date + " - " + time + "|" + gameDay;
}

static int GetLongestLineLengthForGameHistoryDB(string longestPlayerName, string longestBuyIn, string longestCashout, string longestProfitOrLoss)
{
    int lineLength = 40; // Words and spaces prebuilt 

    // Calc all the differences to add spaces if need in the table so the table will be uniformed
    int longestPlayerAddition = longestPlayerName.Length - 6; // 6 = 'Player' in table heading
    int longestBuyInAddition = longestBuyIn.Length + 1 - 5;  // + 1(₪), 5 = 'BuyIn' in table heading
    int longestCashoutAddition = longestCashout.Length + 1 - 7;  // + 1(₪), 7 = 'Cashout' in table heading
    int longestProfitOrLossAddition = longestProfitOrLoss.Length + 2 - 9;  // + 2(₪,±), 9 = 'Gain/Loss' in table heading

    // If any value is negative, set it to 0
    if (longestPlayerAddition < 0) longestPlayerAddition = 0;
    if (longestBuyInAddition < 0) longestBuyInAddition = 0;
    if (longestCashoutAddition < 0) longestCashoutAddition = 0;
    if (longestProfitOrLossAddition < 0) longestProfitOrLossAddition = 0;

    return lineLength + longestPlayerAddition + longestBuyInAddition + longestCashoutAddition + longestProfitOrLossAddition;

}

// Returns the date and time with summer / winter clock for Replit application
static DateTime ReplitTimeAdjust(DateTime nowDateTime)
{
    nowDateTime = DateTime.Now;

    // Check if the current date is between October 27th of the current year and March 29th of the next year
    if (nowDateTime.Month >= 3 && nowDateTime.Month <= 10)
    {
        if (nowDateTime.Month == 3 && nowDateTime.Day <= 29) // Check first day of summer clock.
            nowDateTime = nowDateTime.AddHours(2); // If false set winter time (+2)
        else if (nowDateTime.Month == 10 && nowDateTime.Day > 27) // Check last day of summer clock
            nowDateTime = nowDateTime.AddHours(2); // If false set winter time (+2)
        else nowDateTime = nowDateTime.AddHours(3); // Else set summer time (+3)
    }
    else nowDateTime = nowDateTime.AddHours(2); // If false set winter time (+2)

    return nowDateTime;
}

#region Symbols
static void PrintSpade()
{
    char charSpade = Convert.ToChar(9824);

    Black();
    Console.Write(charSpade);
    ResetColor();
}
static void PrintClub()
{
    char charClub = Convert.ToChar(9827);

    Black();
    Console.Write(charClub);
    ResetColor();
}
static void PrintDiamond()
{
    char charDiamond = Convert.ToChar(9830);

    DarkRed();
    Console.Write(charDiamond);
    ResetColor();
}
static void PrintHeart()
{
    char charHeart = Convert.ToChar(9829);

    DarkRed();
    Console.Write(charHeart);
    ResetColor();
}

static void PrintSuitsConnected()
{
    PrintSpade();
    PrintHeart();
    PrintClub();
    PrintDiamond();
}

static void PrintSuitsConnectedReverse()
{
    PrintDiamond();
    PrintClub();
    PrintHeart();
    PrintSpade();
}
#endregion

#region Ask for STRs
static void AskForGameStatsString()
{
    Blue();
    Console.Write("Enter ");
    DarkGreen();
    Console.Write("new ");
    Blue();
    Console.Write("game stats: ");
    ResetColor();
}
static void AskForUpdatedGameStatsString()
{
    Blue();
    Console.Write("Enter ");
    DarkGreen();
    Console.Write("your changes to ");
    Blue();
    Console.Write("game stats: ");
    ResetColor();
}




#endregion









#region Printing
// WAYPOINT: PRINTING Methods
static void PrintGoodByeMessage()
{
    Console.Write("\n╔══════════════════════════════════════════╗\n");
    Console.Write("║ ");
    Blue();
    Console.Write("Hopefully you didn't lose my nibba, ");
    DarkRed();
    Console.Write("Bye!");
    ResetColor();
    Console.Write(" ║\n");
    Console.Write("╚══════════════════════════════════════════╝\n");
}
static void PrintPressAnyToExit()
{
    DarkGray();
    Console.Write("\n╔══════════════════════════════════════════╗\n");
    Console.Write("║ ");
    DarkRed();
    Console.Write("Press any key to return to the ");
    Magenta();
    Console.Write("Main Menu");
    DarkGray();
    Console.Write(" ║\n");
    Console.Write("╚══════════════════════════════════════════╝\n");
    ResetColor();
}
static void PrintMainMenu(int timerSeconds)
{
    PrintPSPHeading();
    PrintMenuHeading();
    Yellow();
    Console.Write("\n\nPlease choose your desired tool ");
    Yellow();
    Console.Write("(");
    Red();
    Console.Write("press num");
    Yellow();
    Console.Write(")");

    DarkBlue();
    Console.Write("\n\n1");
    Blue();
    Console.Write(". ");
    PrintMenuPokerCashSplit();

    DarkBlue();
    Console.Write("\n\n2");
    Blue();
    Console.Write(". ");
    PrintMenuChipChop();

    // DarkBlue();
    // Console.Write("\n\n3");
    // Blue();
    // Console.Write(". ");
    // PrintMenuTimer(timerSeconds);

    DarkBlue();
    Console.Write("\n\n0");
    Blue();
    Console.Write(". ");
    PrintMenuExit();
}
static void PrintMenuHeading()
{
    Console.Write("\n                ╔═══");
    PrintSuitsConnected();
    Console.Write("═══╗\n");
    Console.Write("                ║");
    Magenta();
    Console.Write("Main  Menu");
    ResetColor();
    Console.Write("║\n");
    Console.Write("                ╚═══");
    PrintSuitsConnectedReverse();
    Console.Write("═══╝");
    ResetColor();
}
static void PrintPSPHeading()
{
    // Top heading
    Console.Write("╔════════════════");
    Console.Write("A");
    PrintHeart();
    Console.Write("K");
    PrintHeart();
    Console.Write("Q");
    PrintHeart();
    Console.Write("J");
    PrintHeart();
    Console.Write("T");
    PrintHeart();
    Console.Write("════════════════╗\n");

    // Middle heading
    Console.Write("║   ");
    PrintDownArrow();
    Console.Write(" ");
    PrintSuitsConnected();
    Console.Write(" ");
    PrintUpArrow();
    DarkRed();
    Console.Write("   PokerSolverPro   ");
    PrintUpArrow();
    Console.Write(" ");
    PrintSuitsConnectedReverse();
    Console.Write(" ");
    PrintDownArrow();
    Console.Write("   ║\n");

    // Bottom heading
    Console.Write("╚════════════════");
    Console.Write("T");
    PrintSpade();
    Console.Write("J");
    PrintSpade();
    Console.Write("Q");
    PrintSpade();
    Console.Write("K");
    PrintSpade();
    Console.Write("A");
    PrintSpade();
    Console.Write("════════════════╝");
}
static void PrintMenuPokerCashSplit()
{
    Magenta();
    Console.Write("PokerCashSplit\n");
    Blue();
    Console.Write("Quickly settle game debts with BB algorithm");
    ResetColor();
}
static void PrintMenuChipChop()
{
    Magenta();
    Console.Write("ChipChop\n");
    Blue();
    Console.Write("Evenly splits the pot/s among players\n");
    Console.Write("accounting for any remainders if present");
    ResetColor();
}
static void PrintMenuTimer(int timerSeconds)
{
    Magenta();
    Console.Write("Timer\n");
    Blue();
    Console.Write($"Starts a {timerSeconds} second timer, press ");
    Red();
    Console.Write($"s ");
    Blue();
    Console.Write($"to ");
    Red();
    Console.Write($"stop");
    ResetColor();
}
static void PrintMenuExit()
{
    DarkRed();
    Console.Write("Exit\n");
    Red();
    Console.Write("Exits the program, What did you expect lol..");
    ResetColor();
}

static void PrintLandingPCS()
{
    PrintHeadingPCS();

    PrintSuitsConnected();
    Blue();
    Console.Write("Program ");
    Magenta();
    Console.Write("Laws");
    PrintSuitsConnectedReverse();
    DarkGray();
    Console.Write("\n=-=-=-=-=-=-=-=-=-=-");

    Magenta();
    Console.Write("\n\n→ ");
    Blue();
    Console.Write("Ignore ");
    DarkGray();
    Console.Write("+");
    Blue();
    Console.Write("'s in the String Format");

    Magenta();
    Console.Write("\n→ ");
    ResetColor();
    Console.Write("' '");
    Blue();
    Console.Write(" indicates a space is required");

    Magenta();
    Console.Write("\n→ ");
    Blue();
    Console.Write("Typing the ");
    Red();
    Console.Write("Cashout ");
    Blue();
    Console.Write("is ");
    Red();
    Console.Write("not mandatory");

    Magenta();
    Console.Write("\n→ ");
    Blue();
    Console.Write("For ");
    Green();
    Console.Write("OG players");
    Blue();
    Console.Write(": ");
    Yellow();
    Console.Write("part name ");
    Blue();
    Console.Write("= ");
    Yellow();
    Console.Write("full name\n\n");



    PrintSuitsConnected();
    Blue();
    Console.Write("String ");
    Magenta();
    Console.Write("Build");
    PrintSuitsConnectedReverse();
    DarkGray();
    Console.Write("\n=-=-=-=-=-=-=-=-=-=-");

    Yellow();
    Console.Write("\n\nName");
    DarkGray();
    Console.Write("+");
    ResetColor();
    Console.Write("' '");
    DarkGray();
    Console.Write("+");
    Green();
    Console.Write("Buyin");
    DarkGray();
    Console.Write("+");
    ResetColor();
    Console.Write("' '");
    DarkGray();
    Console.Write("+");
    Red();
    Console.Write("Cashout");

    Red();
    Console.Write("\n\n*!*");
    Blue();
    Console.Write("Between every new entry add a ");
    ResetColor();
    Console.Write("'");
    Magenta();
    Console.Write(",");
    ResetColor();
    Console.Write("'");
    Red();
    Console.Write("*!*");

    Red();
    Console.Write("\n\nGame stats");
    Magenta();
    Console.Write(": ");
    ResetColor();
}

static void PrintLandingCC()
{
    PrintHeadingCC();

    Magenta();
    Console.Write("Program Rules");
    DarkGray();
    Console.Write("\n=-=-=-=-=-=-=");

    Magenta();
    Console.Write("\n\n→ ");
    Blue();
    Console.Write("Ignore ");
    DarkGray();
    Console.Write("+");
    Blue();
    Console.Write("'s in the String Format");

    Magenta();
    Console.Write("\n→ ");
    ResetColor();
    Console.Write("' '");
    Blue();
    Console.Write(" indicates a space is required");

    Magenta();
    Console.Write("\n\nString Format");
    DarkGray();
    Console.Write("\n=-=-=-=-=-=-=");

    Green();
    Console.Write("\n\nPotSize");
    DarkGray();
    Console.Write("+");
    ResetColor();
    Console.Write("' '");
    DarkGray();
    Console.Write("+");
    Yellow();
    Console.Write("TotalPlayers");


    Red();
    Console.Write("\n\n*!*");
    Blue();
    Console.Write("Between every new entry add a ");
    ResetColor();
    Console.Write("'");
    Magenta();
    Console.Write(",");
    ResetColor();
    Console.Write("'");
    Red();
    Console.Write("*!*");

    Red();
    Console.Write("\n\nPot/s Data");
    Magenta();
    Console.Write(": ");
    ResetColor();
}

#region Remainders Print

static void PrintRemainderSolutionLosersGain(int lengthAboveRow, double remainder, string playerName)
{
    int removeOneToLine = 1;
    if (lengthAboveRow % 2 == 0) removeOneToLine = 0;

    // Line setting
    string shortLine1 = "";
    for (int i = 0; i < (lengthAboveRow - 19) / 2; i++)
    {
        shortLine1 += "─";
    }

    string shortLine2 = "";
    for (int i = 0; i < (lengthAboveRow - 19) / 2 - removeOneToLine; i++) // 19 = "% Remainder Alert %".Length | removeOneToLine to compensate for uneven sentence.Length 
    {
        shortLine2 += "─";
    }
    shortLine2 += "┐";

    string straightLine = "";
    for (int i = 0; i < lengthAboveRow - 1; i++) // -1 for "┘"
    {
        straightLine += "─";
    }
    straightLine += "┘";

    // Middle space setting
    string spacesForIfRemainder1 = "";
    string spacesForIfRemainder2 = "";
    for (int i = 0; i < lengthAboveRow - (playerName.Length + 18); i++) // 18 = Independent text for name of player
    {
        spacesForIfRemainder1 += " ";
    }
    spacesForIfRemainder1 += "│";

    for (int i = 0; i < lengthAboveRow - (Convert.ToString(remainder).Length + 17); i++) // 17 = Independent text for remainder of player
    {
        spacesForIfRemainder2 += " ";
    }
    spacesForIfRemainder2 += "│";

    string spacesForElseRemainder1 = "";
    string spacesForElseRemainder2 = "";
    for (int i = 0; i < lengthAboveRow - (playerName.Length + 19); i++) // 19 = Independent text for name of player
    {
        spacesForElseRemainder1 += " ";
    }
    spacesForElseRemainder1 += "│";

    for (int i = 0; i < lengthAboveRow - (Convert.ToString(Math.Abs(remainder)).Length + 18); i++) // 18 = Independent text for remainder of player
    {
        spacesForElseRemainder2 += " ";
    }
    spacesForElseRemainder2 += "│";


    Console.WriteLine();
    ResetColor();
    Console.Write(shortLine1);
    Green();
    Console.Write("%");
    Magenta();
    Console.Write(" Remainder Alert ");
    Red();
    Console.Write("%");
    ResetColor();
    Console.Write(shortLine2);

    // Inside Data

    if (remainder > 0)
    {
        Yellow();
        Console.Write($"\n{playerName.ToUpper()} (");
        Red();
        Console.Write("Biggest loser");
        Yellow();
        Console.Write(") ");
        ResetColor();
        Console.Write(spacesForIfRemainder1);
        Green();
        Console.Write("\nGets");
        Blue();
        Console.Write(" an extra ");
        Yellow();
        Console.Write("(");
        Cyan();
        Console.Write($"{remainder}");
        Yellow();
        Console.Write(")");
        ResetColor();
        Console.Write(spacesForIfRemainder2);
        Console.WriteLine();
    }
    else
    {
        Yellow();
        Console.Write($"\n{playerName.ToUpper()} (");
        DarkRed();
        Console.Write("Smallest loser");
        Yellow();
        Console.Write(") ");
        ResetColor();
        Console.Write(spacesForElseRemainder1);
        Red();
        Console.Write("\nMisses out");
        Blue();
        Console.Write(" on ");
        Yellow();
        Console.Write("(");
        Red();
        Console.Write("-");
        Cyan();
        Console.Write($"{Math.Abs(remainder)}");
        Yellow();
        Console.Write(")");
        ResetColor();
        Console.Write(spacesForElseRemainder2);
        Console.WriteLine();
    }
    ResetColor();
    Console.Write(straightLine);
}

static void PrintRemainderSolutionWinnersLose(int lengthAboveRow, double remainder, string playerName)
{
    int removeOneToLine = 1;
    if (lengthAboveRow % 2 == 0) removeOneToLine = 0;

    // Line setting
    string shortLine1 = "";
    for (int i = 0; i < (lengthAboveRow - 19) / 2; i++)
    {
        shortLine1 += "─";
    }

    string shortLine2 = "";
    for (int i = 0; i < (lengthAboveRow - 19) / 2 - removeOneToLine; i++) // 19 = "% Remainder Alert %".Length | removeOneToLine to compensate for uneven sentence.Length 
    {
        shortLine2 += "─";
    }
    shortLine2 += "┐";

    string straightLine = "";
    for (int i = 0; i < lengthAboveRow - 1; i++) // -1 for "┘"
    {
        straightLine += "─";
    }
    straightLine += "┘";

    // Middle space setting
    string spacesForIfRemainder1 = "";
    string spacesForIfRemainder2 = "";
    for (int i = 0; i < lengthAboveRow - (playerName.Length + 20); i++) // 20 = Independent text for name of player
    {
        spacesForIfRemainder1 += " ";
    }
    spacesForIfRemainder1 += "│";

    for (int i = 0; i < lengthAboveRow - (Convert.ToString(Math.Abs(remainder)).Length + 18); i++) // 18 = Independent text for remainder of player
    {
        spacesForIfRemainder2 += " ";
    }
    spacesForIfRemainder2 += "│";

    string spacesForElseRemainder1 = "";
    string spacesForElseRemainder2 = "";
    for (int i = 0; i < lengthAboveRow - (playerName.Length + 19); i++) // 19 = Independent text for name of player
    {
        spacesForElseRemainder1 += " ";
    }
    spacesForElseRemainder1 += "│";

    for (int i = 0; i < lengthAboveRow - (Convert.ToString(remainder).Length + 22); i++) // 22 = Independent text for remainder of player
    {
        spacesForElseRemainder2 += " ";
    }
    spacesForElseRemainder2 += "│";

    Console.WriteLine();
    ResetColor();
    Console.Write(shortLine1);
    Green();
    Console.Write("%");
    Magenta();
    Console.Write(" Remainder Alert ");
    Red();
    Console.Write("%");
    ResetColor();
    Console.Write(shortLine2);

    // Inside Data
    if (remainder < 0)
    {
        Yellow();
        Console.Write($"\n{playerName.ToUpper()} (");
        DarkGreen();
        Console.Write("Smallest winner");
        Yellow();
        Console.Write(") ");
        ResetColor();
        Console.Write(spacesForIfRemainder1);
        Green();
        Console.Write("\nKeeps");
        Blue();
        Console.Write(" an extra ");
        Yellow();
        Console.Write("(");
        Cyan();
        Console.Write($"{Math.Abs(remainder)}");
        Yellow();
        Console.Write(")");
        ResetColor();
        Console.Write(spacesForIfRemainder2);
        Console.WriteLine();
    }
    else
    {
        Yellow();
        Console.Write($"\n{playerName.ToUpper()} (");
        DarkGreen();
        Console.Write("Biggest winner");
        Yellow();
        Console.Write(") ");
        ResetColor();
        Console.Write(spacesForElseRemainder1);
        Red();
        Console.Write("\nForfeits");
        Blue();
        Console.Write(" an extra ");
        Yellow();
        Console.Write("(");
        Red();
        Console.Write("-");
        Cyan();
        Console.Write($"{remainder}");
        Yellow();
        Console.Write(")");
        ResetColor();
        Console.Write(spacesForElseRemainder2);
        Console.WriteLine();
    }
    ResetColor();
    Console.Write(straightLine);
}

#endregion

static string PrintLengthError(string[] strArr, int currentLength, int minLength, int maxLength, int replitCharCount)
{
    string str = ""; // Make the string
    for (int i = 0; i < strArr.Length; i++)
    {
        if (i == 0)
        {
            str += TrimAndCapitalize(strArr[i]);
        }
        else str += strArr[i];
        if (i < strArr.Length - 1) str += " ";
    }
    Red();
    Console.Write("\nError:\n");
    Red();
    Console.Write("Length of each string must be ");
    Magenta();
    Console.Write(minLength == maxLength ? $"{maxLength} " : $"{minLength}-{maxLength} ");
    Red();
    Console.Write("words\n");
    Blue();
    Console.Write("Your string is ");
    Magenta();
    Console.Write($"{currentLength} ");
    Red();
    Console.Write("words\n");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{str}");
    DarkGray();
    Console.Write("'");
    Blue();
    Console.Write("\nEnter a new data string: ");
    ResetColor();

    string newStr = Console.ReadLine();

    string[] newStrArray = TrimWordsInString(newStr);

    if (newStrArray.Length >= minLength && newStrArray.Length <= maxLength) return newStr;
    else
    {
        ClearConsoleLines(0, 6, replitCharCount);
        return PrintLengthError(strArr, currentLength, minLength, maxLength, replitCharCount);
    }
}

static void PrintPotCalcError(double potAmount, int playerAmount)
{
    Red();
    Console.Write("\nError:\n");
    Red();
    Console.Write("Cannot calculate pot!\n");
    Magenta();
    Console.Write($"Pot: ");
    Red();
    Console.Write($"{potAmount}");
    Yellow();
    Console.Write($" | ");
    Magenta();
    Console.Write($"Players: ");
    Red();
    Console.Write($"{playerAmount}\n");
    Red();
    Console.Write("Data received is invalid.\n\n");



    ResetColor();

}


static string PrintLengthErrorYesOrNo(string str, int minLength, int maxLength, int replitCharCount)
{

    str = TrimAndCapitalize(str);

    Red();
    Console.Write("\nError:\n");
    Red();
    Console.Write("Length of the answer must be ");
    Magenta();
    Console.Write(minLength == maxLength ? $"{maxLength} " : $"{minLength}-{maxLength} ");
    Red();
    Console.Write("chars\n");
    Console.Write("Your answer is ");
    Magenta();
    Console.Write($"{str.Length} ");
    Red();
    Console.Write("chars\n");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{str}");
    DarkGray();
    Console.Write("'");
    Blue();
    Console.Write("\nEnter a new yes or no: ");
    ResetColor();

    string newStr = Console.ReadLine();

    newStr = TrimAndCapitalize(newStr);

    if (newStr.Length >= minLength && newStr.Length <= maxLength) return newStr;
    else
    {
        ClearConsoleLines(0, 6, replitCharCount);
        return PrintLengthErrorYesOrNo(newStr, minLength, maxLength, replitCharCount);
    }
}

static string CheckAndGetNewNameString(string str, string[] arrayUsed, double playerBuyIn, double playerBuyOut, int replitCharCount)
{
    str = TrimAndCapitalize(str);
    foreach (string name in arrayUsed)
    {
        if (name.ToLower() == str.ToLower())
        {
            Red();
            Console.Write("\nError:");
            Blue();
            Console.Write("\nThe name ");
            DarkGray();
            Console.Write("'");
            Magenta();
            Console.Write($"{str}");
            DarkGray();
            Console.Write("'");
            Blue();
            Console.Write("\nWas already entered! ");
            Red();
            Console.Write($"\nBuyin: {playerBuyIn} ");
            Green();
            Console.Write($"\nCashout: {playerBuyOut} ");
            Blue();
            Console.Write("\nEnter a new name for ");
            DarkGray();
            Console.Write("'");
            Magenta();
            Console.Write($"{str}");
            DarkGray();
            Console.Write("'");
            Red();
            Console.Write(": ");
            ResetColor();

            str = CheckEnglishName(Console.ReadLine(), replitCharCount);
            ClearConsoleLines(0, 7, replitCharCount);
            return CheckAndGetNewNameString(str, arrayUsed, playerBuyIn, playerBuyOut, replitCharCount);
        }
    }
    return str;
}

static string TrimAndCapitalize(string str)
{
    str = str.Trim();
    if (str == null || str.Length == 0)
    {
        return str;
    }
    char checkFirstLetter = str[0];
    string temp = Convert.ToString(checkFirstLetter).ToUpper();
    temp += str.Substring(1).ToLower();
    return temp;
}

static string TrimAndCapitalizeSentence(string str)
{
    str = str.Trim();
    if (str == null || str.Length == 0)
    {
        return str;
    }
    string[] temp1 = str.Split(' ');
    string goodStr = "";

    foreach (string s in temp1)
    {
        if (s == " " || s == "" || s == null) continue;
        goodStr += TrimAndCapitalize(s);
        goodStr += " ";
    }
    goodStr.Trim();
    return goodStr;
}

static string[] TrimAndCapitalizeArray(string[] array)
{
    string[] temp = new string[0];
    int counter = 0;
    foreach (var item in array)
    {
        string tempItem = TrimAndCapitalize(item);
        if (tempItem == null || tempItem == "") continue;
        temp = AddOneToArray(temp);
        temp[counter++] = tempItem;
    }
    return temp;
}

static void PrintHeadingPCS()
{
    Console.Write("\n╔═══════");
    PrintSuitsConnected();
    Console.Write("═══════╗\n");

    Console.Write("║ Poker Cash Split ║\n");

    Console.Write("╚═══════");
    PrintSuitsConnectedReverse();
    Console.Write("═══════╝\n");
}

static void PrintHeadingCC()
{
    Console.Write("\n╔═══");
    PrintSpade();
    PrintHeart();
    Console.Write("═");
    PrintClub();
    PrintDiamond();
    Console.Write("═══╗\n");

    Console.Write("║ Chip Chop ║\n");

    Console.Write("╚═══");
    PrintDiamond();
    PrintClub();
    Console.Write("═");
    PrintHeart();
    PrintSpade();
    Console.Write("═══╝\n");
}

static void PrintDownArrow()
{
    Red();
    Console.Write("↓");
    ResetColor();
}
static void PrintUpArrow()
{
    Green();
    Console.Write("↑");
    ResetColor();
}

static void PrintGameDataSymbol36a()
{
    PrintDownArrow();
    Console.Write(" ");
    PrintSuitsConnected();
    Console.Write(" ");
    PrintUpArrow();
}
static void PrintGameDataSymbol36b()
{
    PrintUpArrow();
    Console.Write(" ");
    PrintSuitsConnectedReverse();
    Console.Write(" ");
    PrintDownArrow();
}


static int PrintGameDataHeading(int longestSentence)
{
    int isEven = 0;
    int sentenceLength = longestSentence;
    int count = 0;
    int returnedIndex = 0;

    // First third
    for (int i = 0; i < longestSentence - 9; i++)
    {
        count++;
        if (i == 0) Console.Write("╔");
        else if (i == longestSentence - 10) Console.Write("╗");
        else if (i == (longestSentence - 10) / 2)
        {
            Console.Write("A");
            PrintHeart();
            Console.Write("K");
            PrintHeart(); // Return this for future printing
            returnedIndex = count + 4; // For the current printing
            Console.Write("Q");
            PrintHeart();
            Console.Write("J");
            PrintHeart();
            Console.Write("T");
            PrintHeart();
        }
        else Console.Write("═");
    }
    Console.WriteLine();

    // Middle part
    Console.Write("║  ");

    if (sentenceLength >= 36) // Print with marks ↓ ♠♥♣♦ ↑
    {
        PrintGameDataSymbol36a();

        if (!(longestSentence % 2 == 0)) isEven = 1;

        for (int i = 0; i < (sentenceLength - 30) / 2; i++)
        {
            Console.Write(" ");
        }
        Console.Write("GameData");
        for (int i = 0; i < (sentenceLength - 30) / 2 + isEven; i++)
        {
            Console.Write(" ");
        }

        PrintGameDataSymbol36b();
    }
    else // Print arrows only ↓ ↑
    {
        PrintDownArrow();
        Console.Write(" ");
        PrintUpArrow();

        if (!(longestSentence % 2 == 0)) isEven = 1;

        for (int i = 0; i < (sentenceLength - 20) / 2; i++)
        {
            Console.Write(" ");
        }
        Console.Write("GameData");
        for (int i = 0; i < (sentenceLength - 20) / 2 + isEven; i++)
        {
            Console.Write(" ");
        }

        PrintUpArrow();
        Console.Write(" ");
        PrintDownArrow();
    }

    Console.Write("  ║");

    Console.WriteLine();

    // Last third
    for (int i = 0; i < longestSentence - 9; i++)
    {
        if (i == 0) Console.Write("╚");
        else if (i == longestSentence - 10) Console.Write("╝");
        else if (i == (longestSentence - 10) / 2)
        {
            Console.Write("T");
            PrintSpade();
            Console.Write("J");
            PrintSpade();
            Console.Write("Q");
            PrintSpade();
            Console.Write("K");
            PrintSpade();
            Console.Write("A");
            PrintSpade();
        }
        else Console.Write("═");
    }

    ResetColor();
    return returnedIndex;
}

static void PrintH2WithTime(int spacesFromBiggestSentence, int gameID, bool isUsingReplit)
{
    // Find the total size of the game ID text
    int totalTextSize = Convert.ToString(gameID).Length + "ID:".Length;
    int availableSpace = 18; // The space we have between the '║'

    // Create spaces for the gameID
    string spacesGameLeft = "";
    string spacesGameRight = "";
    int spaceModifier = totalTextSize % 2;

    for (int i = 0; i < (availableSpace - totalTextSize) / 2 + spaceModifier; i++)
    {
        spacesGameLeft += " ";
    }

    for (int i = 0; i < (availableSpace - totalTextSize) / 2; i++)
    {
        spacesGameRight += " ";
    }



    string currentTime = PrintDataTime(isUsingReplit);
    bool showPreviousDay = false;
    int time = Convert.ToInt32(currentTime.Substring(11, 2));
    if (time >= 0 && time <= 9) showPreviousDay = true;

    string day = PrintDay(isUsingReplit, showPreviousDay);

    string spaces = "";

    for (int i = 0; i < spacesFromBiggestSentence - 10; i++) // spacesFromBiggestSentence = number of the middle part from the player stats title
    {                                                      // 9 length of the side of this functions heading from the left
        spaces += " ";
    }




    Console.Write($"{spaces}╔═══════");
    PrintSuitsConnected();
    Console.Write("═══════╗\n");


    // Middle part
    Console.Write($"{spaces}║{spacesGameLeft}");

    Console.Write($"ID");
    DarkGray();
    Console.Write(":");
    Magenta();
    Console.Write($"{gameID}");
    ResetColor();
    Console.Write($"{spacesGameRight}║\n");


    Console.Write($"{spaces}╠══════════════════╣\n");


    Console.Write($"{spaces}║");
    DarkGray();
    Console.Write(" ATM ");
    ResetColor();
    Console.Write("DateTime");
    DarkGray();
    Console.Write(" ATM ");
    ResetColor();
    Console.Write("║\n");
    ResetColor();
    Console.Write($"{spaces}║");
    Green();
    Console.Write("    Day");
    DarkGray();
    Console.Write(" :: ");
    Green();
    Console.Write($"{day}    ");
    ResetColor();
    Console.Write("║\n");




    Console.Write($"{spaces}║");
    DarkGray();
    Console.Write(" ▼ ");
    DarkRed();
    Console.Write("Split  Stamp");
    DarkGray();
    Console.Write(" ▼ ");
    ResetColor();
    Console.Write("║\n");




    Console.Write($"{spaces}╠ ");
    Yellow();
    Console.Write(PrintDataTime(isUsingReplit));
    ResetColor();
    Console.Write(" ╣\n");


    Console.Write($"{spaces}╚═══════");
    PrintSuitsConnectedReverse();
    Console.Write("═══════╝\n");
}

static int PrintWhoOwesWhoHeading(int middleIndexOfStatsPrinting)
{
    string spaces = "";

    int counter = 0;

    for (int i = 0; i < middleIndexOfStatsPrinting - 6; i++) // -6 so it will be under the DataTime Heading
    {
        spaces += " ";
        counter++;
    }

    DarkRed();
    Console.Write($"{spaces}Who ");
    Yellow();
    Console.Write("Owes");
    Green();
    Console.Write(" Who\n");
    DarkRed();
    Console.Write($"{spaces}▬▬▬▬");
    Yellow();
    Console.Write("▬▬▬▬");
    Green();
    Console.Write("▬▬▬▬");
    ResetColor();

    return counter + 4;
}
#endregion


// WAYPOINT: WhoOwesWho
static int PrintWhoOwesWho(List<KeyValuePair<string, double>> winnersListSent, List<KeyValuePair<string, double>> losersListSent, string longestWinnerName, string longestLoserName, int whoOwesWhoIndex, double minTransfer, string[] favPlayers)
{
    // Create temp lists so they won't change when you will print the "WhoOwesWho" in the history DB

    List<KeyValuePair<string, double>> winnersList = new List<KeyValuePair<string, double>>(winnersListSent);
    List<KeyValuePair<string, double>> losersList = new List<KeyValuePair<string, double>>(losersListSent);

    int printCashIndex = 0;

    #region Sort equal pays
    // Find equal winners and losers to remove them from who owes who
    for (int loser = 0; loser < losersList.Count; loser++)
    {
        bool paymentDone = false;
        for (int winner = 0; winner < winnersList.Count; winner++)
        {
            if (winnersList[winner].Value == losersList[loser].Value && losersList[loser].Value != 0 && winnersList[winner].Value != 0)
            {
                paymentDone = true;
                printCashIndex = PrintWhoOwesWhoSentence(losersList[loser].Key, winnersList[winner].Key, winnersList[winner].Value, longestWinnerName.Length, longestLoserName.Length, whoOwesWhoIndex);
                Console.WriteLine();
                winnersList[winner] = new KeyValuePair<string, double>(winnersList[winner].Key, 0);
                losersList[loser] = new KeyValuePair<string, double>(losersList[loser].Key, 0);
                winnersList.Remove(new KeyValuePair<string, double>(winnersList[winner].Key, 0));
                losersList.Remove(new KeyValuePair<string, double>(losersList[loser].Key, 0));
            }
        }
        if (losersList.Count > 0) // To avoid when it is the only type of payment available 
        {
            if (paymentDone == true && losersList.Max(x => x.Value) > 0) Console.WriteLine();
        }
    }


    #endregion

    #region DFS settle
    // Using DFS, try and find "perfect matches" losers(1 or more).Value == winner.Value
    for (int i = 0; i < winnersList.Count; i++)
    {
        // Order lists again (ignore prioritizing favPlayers)

        // Start with the smallest losers
        losersList = losersList.OrderBy(x => x.Value).ToList();
        // Start with the biggest winners
        winnersList = winnersList.OrderByDescending(x => x.Value).ToList();

        // Make a list that will store a found 'prefect match'
        List<KeyValuePair<string, double>> foundCombo = new List<KeyValuePair<string, double>>();

        foundCombo = DFSettleWinner(losersList, winnersList[i].Value);

        if (foundCombo.Count != 0)
        {
            foreach (var loser in foundCombo)
            {
                printCashIndex = PrintWhoOwesWhoSentence(loser.Key, winnersList[i].Key, loser.Value, longestWinnerName.Length, longestLoserName.Length, whoOwesWhoIndex);
                Console.Write("\n\n");
                losersList.Remove(new KeyValuePair<string, double>(loser.Key, loser.Value));
            }
            winnersList.Remove(new KeyValuePair<string, double>(winnersList[i].Key, winnersList[i].Value));
            // if a perfect match was found i-- so it will not overflow and check the next one
            i--;
        }
        // Go next :)
    }

    #endregion


    #region Sort all else
    // Sort winners and losers lists by value in descending order while prioritizing the fav players (pay and get paid first)
    PrioritizeFavPlayers(ref losersList, ref winnersList, favPlayers);

    // Sort the rest with the following logic => Biggest loser pays Biggest winner.....
    for (int loser = 0; loser < losersList.Count; loser++)
    {
        for (int winner = 0; winner < winnersList.Count; winner++)
        {
            if (winnersList[winner].Value == 0) continue; // Skip if debt has been paid

            while (losersList[loser].Value != 0 && winnersList[winner].Value > 0)
            {
                string winnerName = winnersList[winner].Key;
                string loserName = losersList[loser].Key;
                double winnerPayAmount = winnersList[winner].Value; // How much needs to be paid
                double loserPayAmount = losersList[loser].Value; // How much loser has to pay

                bool atLeast1MoreLoserAndWinner = loser < (losersList.Count - 1) && winner < (winnersList.Count - 1);
                double rawDifferenceForMinTransfer = Math.Abs(winnerPayAmount - loserPayAmount);

                if (winnerPayAmount > loserPayAmount)
                {
                    if (loserPayAmount > minTransfer && rawDifferenceForMinTransfer < minTransfer && rawDifferenceForMinTransfer < minTransfer && atLeast1MoreLoserAndWinner == true) // If after the transfer the amount is too small for the 'Bit' minimum payment
                    {
                        if (winnersList[winner + 1].Value >= minTransfer) //  && losersList[loser+1].Value >= minTransfer
                        {
                            // Find the value until loser has 'minTransfer'₪ at least and winner has 'minTransfer'₪ at least
                            loserPayAmount -= minTransfer; // Pay one min transfer less so the next lose can cover the biggest winner with a minTransfer value.

                            printCashIndex = PrintWhoOwesWhoSentence(loserName, winnerName, loserPayAmount, longestWinnerName.Length, longestLoserName.Length, whoOwesWhoIndex);
                            Console.WriteLine();
                            winnersList[winner] = new KeyValuePair<string, double>(winnerName, winnerPayAmount - loserPayAmount);
                            losersList[loser] = new KeyValuePair<string, double>(loserName, losersList[loser].Value - loserPayAmount);
                            if (winner < winnersList.Count) winner++; // Pay the min amount to the next winner (if there is only 1 winner the issue will not occur)
                            continue;
                        }
                    }
                    printCashIndex = PrintWhoOwesWhoSentence(loserName, winnerName, loserPayAmount, longestWinnerName.Length, longestLoserName.Length, whoOwesWhoIndex);
                    winnersList[winner] = new KeyValuePair<string, double>(winnerName, winnerPayAmount - loserPayAmount);
                    losersList[loser] = new KeyValuePair<string, double>(loserName, 0);
                }
                else  // (loserPayAmount > winnerPayAmount)
                {
                    // if ((loserPayAmount - winnerPayAmount) < minTransfer && winnerPayAmount > minTransfer) // If after the transfer the amount is too small for the 'Bit' minimum payment
                    if (loserPayAmount > minTransfer && rawDifferenceForMinTransfer < minTransfer && rawDifferenceForMinTransfer < minTransfer && atLeast1MoreLoserAndWinner == true) // If after the transfer the amount is too small for the 'Bit' minimum payment
                    {
                        // Pay exact amount less so you will have a 1 minTransfer value left
                        double leftOver = minTransfer + Math.Abs(loserPayAmount - winnerPayAmount);
                        // Find the value until loser has 'minTransfer'₪ at least and winner has 'minTransfer'₪ at least
                        if (winnersList[winner + 1].Value >= leftOver) //  && losersList[loser+1].Value >= minTransfer
                        {
                            // Remove the minTransfer + leftOver so that the winner can get a minTransfer from the next loser 
                            loserPayAmount -= leftOver;
                            printCashIndex = PrintWhoOwesWhoSentence(loserName, winnerName, loserPayAmount, longestWinnerName.Length, longestLoserName.Length, whoOwesWhoIndex);
                            Console.WriteLine();
                            losersList[loser] = new KeyValuePair<string, double>(loserName, losersList[loser].Value - loserPayAmount);
                            winnersList[winner] = new KeyValuePair<string, double>(winnerName, winnersList[winner].Value - loserPayAmount);
                            if (winner < winnersList.Count) winner++;
                            continue;
                        }
                    }
                    printCashIndex = PrintWhoOwesWhoSentence(loserName, winnerName, winnerPayAmount, longestWinnerName.Length, longestLoserName.Length, whoOwesWhoIndex);
                    losersList[loser] = new KeyValuePair<string, double>(loserName, loserPayAmount - winnerPayAmount);
                    winnersList[winner] = new KeyValuePair<string, double>(winnerName, 0);
                }
                if (losersList.Max(x => x.Value) > 0) Console.WriteLine();
                if (losersList[loser].Value == 0) Console.WriteLine();
            }
        }
    }
    #endregion

    return printCashIndex;
}

static void PrioritizeFavPlayers(ref List<KeyValuePair<string, double>> losers, ref List<KeyValuePair<string, double>> winners, string[] favPlayers)
{
    favPlayers = TrimAndCapitalizeArray(favPlayers);
    List<KeyValuePair<string, double>> tempLosers = new List<KeyValuePair<string, double>>();
    List<KeyValuePair<string, double>> tempWinners = new List<KeyValuePair<string, double>>();

    // Add all of the players that are not "favPlayers"
    // Add losers
    foreach (var player in losers)
    {
        bool favPlayer = false;
        foreach (var fav in favPlayers)
        {
            if (player.Key == fav) favPlayer = true;
        }
        // Skip adding player if it is a favorite
        if (favPlayer == true) continue;
        // Add a non fav player
        tempLosers.Add(new KeyValuePair<string, double>(player.Key, player.Value));
    }
    // Add winners
    foreach (var player in winners)
    {
        bool favPlayer = false;
        foreach (var fav in favPlayers)
        {
            if (player.Key == fav) favPlayer = true;
        }
        // Skip adding player if it is a favorite
        if (favPlayer == true) continue;
        // Add a non fav player
        tempWinners.Add(new KeyValuePair<string, double>(player.Key, player.Value));
    }

    // Sort lists in a descending order
    tempLosers = tempLosers.OrderByDescending(x => x.Value).ToList();
    tempWinners = tempWinners.OrderByDescending(x => x.Value).ToList();

    // Add all of the "favPlayers" in the order they have been sent
    int orderCounterWinner = 0;
    int orderCounterLoser = 0;

    foreach (var fav in favPlayers)
    {
        bool keyFound = false;
        foreach (var loser in losers)
        {
            if (loser.Key == fav)
            {
                // Add losers to new list (first places) => Pay first
                tempLosers.Insert(orderCounterLoser++, new KeyValuePair<string, double>(fav, loser.Value));
                keyFound = true;
                break;
            }
        }
        if (keyFound == true) continue;
        foreach (var winner in winners)
        {
            if (winner.Key == fav)
            {
                // Add winners to new list (first places) => Get paid first
                tempWinners.Insert(orderCounterWinner++, new KeyValuePair<string, double>(fav, winner.Value));
                break;
            }
        }
    }

    losers = tempLosers;
    winners = tempWinners;
}

static string PrintWhoOwesWhoHistoryDB(List<KeyValuePair<string, double>> winnersListSent, List<KeyValuePair<string, double>> losersListSent, string longestWinnerName, string longestLoserName, int endIndexForDB, string[] favPlayers)
{
    List<KeyValuePair<string, double>> winnersList = new List<KeyValuePair<string, double>>(winnersListSent);
    List<KeyValuePair<string, double>> losersList = new List<KeyValuePair<string, double>>(losersListSent);

    PrioritizeFavPlayers(ref losersList, ref winnersList, favPlayers);

    string finalString = "";

    // Find equal winners and losers to remove them from who owes who
    for (int loser = 0; loser < losersList.Count; loser++)
    {
        for (int winner = 0; winner < winnersList.Count; winner++)
        {
            if (winnersList[winner].Value == losersList[loser].Value && losersList[loser].Value != 0 && winnersList[winner].Value != 0)
            {
                string debtSettle = PrintWhoOwesWhoSentenceHistoryDB(losersList[loser].Key, winnersList[winner].Key, winnersList[winner].Value, longestWinnerName.Length, longestLoserName.Length, endIndexForDB);
                finalString += debtSettle;
                finalString += "\n";
                winnersList[winner] = new KeyValuePair<string, double>(winnersList[winner].Key, 0);
                losersList[loser] = new KeyValuePair<string, double>(losersList[loser].Key, 0);
                winnersList.Remove(new KeyValuePair<string, double>(winnersList[winner].Key, 0));
                winnersList.Remove(new KeyValuePair<string, double>(losersList[loser].Key, 0));
            }
        }
    }

    // Using DFS, try and find "perfect matches" losers(1 or more).Value == winner.Value
    for (int i = 0; i < winnersList.Count; i++)
    {
        // Order lists again (ignore prioritizing favPlayers)

        // Start with the smallest losers
        losersList = losersList.OrderBy(x => x.Value).ToList();
        // Start with the biggest winners
        winnersList = winnersList.OrderByDescending(x => x.Value).ToList();

        // Make a list that will store a found 'prefect match'
        List<KeyValuePair<string, double>> foundCombo = new List<KeyValuePair<string, double>>();

        foundCombo = DFSettleWinner(losersList, winnersList[i].Value);

        if (foundCombo.Count != 0)
        {
            foreach (var loser in foundCombo)
            {
                string debtSettle = PrintWhoOwesWhoSentenceHistoryDB(loser.Key, winnersList[i].Key, winnersList[i].Value, longestWinnerName.Length, longestLoserName.Length, endIndexForDB);
                finalString += debtSettle;
                finalString += "\n";
                losersList.Remove(new KeyValuePair<string, double>(loser.Key, loser.Value));
            }
            winnersList.Remove(new KeyValuePair<string, double>(winnersList[i].Key, winnersList[i].Value));
            // if a perfect match was found i-- so it will not overflow and check the next one
            i--;
        }
        // Go next :)
    }

    // Sort winners and losers lists by value in descending order while prioritizing the fav players (pay and get paid first)
    PrioritizeFavPlayers(ref losersList, ref winnersList, favPlayers);

    // Sort the rest with the following logic => Biggest loser pays Biggest winner.....
    for (int loser = 0; loser < losersList.Count; loser++)
    {
        for (int winner = 0; winner < winnersList.Count; winner++)
        {
            if (winnersList[winner].Value == 0) continue; // Skip if debt has been paid

            while (losersList[loser].Value != 0 && winnersList[winner].Value > 0)
            {
                string winnerName = winnersList[winner].Key;
                string loserName = losersList[loser].Key;
                double winnerPayAmount = winnersList[winner].Value; // How much needs to be paid
                double loserPayAmount = losersList[loser].Value; // How much loser has to pay

                if (winnerPayAmount > loserPayAmount)
                {
                    string debtSettle = PrintWhoOwesWhoSentenceHistoryDB(loserName, winnerName, loserPayAmount, longestWinnerName.Length, longestLoserName.Length, endIndexForDB);
                    finalString += debtSettle;
                    finalString += "\n";
                    winnersList[winner] = new KeyValuePair<string, double>(winnerName, winnerPayAmount - loserPayAmount);
                    losersList[loser] = new KeyValuePair<string, double>(loserName, 0);
                }
                else
                {
                    string debtSettle = PrintWhoOwesWhoSentenceHistoryDB(loserName, winnerName, winnerPayAmount, longestWinnerName.Length, longestLoserName.Length, endIndexForDB);
                    finalString += debtSettle;
                    finalString += "\n";
                    losersList[loser] = new KeyValuePair<string, double>(loserName, loserPayAmount - winnerPayAmount);
                    winnersList[winner] = new KeyValuePair<string, double>(winnerName, 0);
                }
            }
        }
    }
    return finalString;
}

static int PrintWhoOwesWhoSentence(string loserName, string winnerName, double payment, int longestWinnerNameLength, int longestLoserNameLength, int whoOwesWhoIndex)
{

    // To change back to "owes" 1. Remove -2 from third for loop, 2. Change "→" to "owes"
    string spacesL = "";
    string spacesW = "";
    string spacesForUniformity = "";

    for (int i = 0; i < longestLoserNameLength - loserName.Length; i++)
    {
        spacesL += " ";
    }
    for (int i = 0; i < longestWinnerNameLength - winnerName.Length; i++)
    {
        spacesW += " ";
    }
    for (int i = 0; i < whoOwesWhoIndex - loserName.Length - 1 - (longestLoserNameLength - loserName.Length) - 2; i++) // -2 for '→' change (winner name starts under 'Owes' in the heading above)
    {
        spacesForUniformity += " ";
    }


    GetPlayerColor(loserName);
    Console.Write($"{spacesForUniformity}{loserName}{spacesL}");
    Yellow();
    Console.Write($" → ");
    GetPlayerColor(winnerName);
    Console.Write($"{winnerName}{spacesW}");
    Green();
    Console.Write($" {Math.Round(payment, 2)}");
    DarkGreen();
    Console.Write("₪");
    ResetColor();

    return spacesForUniformity.Length;
}

static string PrintWhoOwesWhoSentenceHistoryDB(string loserName, string winnerName, double payment, int longestWinnerNameLength, int longestLoserNameLength, int endIndexForDB)
{

    // To change back to "owes" 1. Remove -2 from third for loop, 2. Change "→" to "owes"
    string spacesL = "";
    string spacesW = "";
    string finalString = "";

    for (int i = 0; i < longestLoserNameLength - loserName.Length; i++)
    {
        spacesL += " ";
    }
    for (int i = 0; i < longestWinnerNameLength - winnerName.Length; i++)
    {
        spacesW += " ";
    }

    finalString += "║ ";
    finalString += $"{loserName}{spacesL}";
    finalString += " owes ";
    finalString += $"{winnerName}{spacesW}";
    finalString += $" {Math.Round(payment, 2)}₪";

    for (int i = finalString.Length; i < endIndexForDB; i++)
    {
        finalString += " ";
    }

    finalString += "║";

    return finalString;
}

static void PrintSolutionImplement2()
{
    Magenta();
    Console.Write("\n\nImplementation:\n");
    Blue();
    Console.Write("What solution would you like to implement?\n");
    Yellow();
    Console.Write("(");
    Magenta();
    Console.Write("1");
    Yellow();
    Console.Write("||");
    Magenta();
    Console.Write("2");
    Yellow();
    Console.Write("||");
    Magenta();
    Console.Write("3");
    Yellow();
    Console.Write("||");
    Magenta();
    Console.Write("New ");
    DarkRed();
    Console.Write("data");
    Yellow();
    Console.Write("||");
    Magenta();
    Console.Write("Alt");
    DarkRed();
    Console.Write("er data");
    DarkGray();
    Console.Write("||");
    Magenta();
    Console.Write("Ex");
    DarkRed();
    Console.Write("it");
    Yellow();
    Console.Write(")");
    Blue();
    Console.Write(": ");

    ResetColor();
}
static void PrintSolutionImplement1()
{
    Magenta();
    Console.Write("\n\nImplementation:\n");
    Blue();
    Console.Write("What solution would you like to implement?\n");
    Yellow();
    Console.Write("(");
    Magenta();
    Console.Write("1");
    DarkGray();
    Console.Write("||");
    Magenta();
    Console.Write("New ");
    DarkRed();
    Console.Write("data");
    DarkGray();
    Console.Write("||");
    Magenta();
    Console.Write("Alt");
    DarkRed();
    Console.Write("er data");
    DarkGray();
    Console.Write("||");
    Magenta();
    Console.Write("Ex");
    DarkRed();
    Console.Write("it");
    Yellow();
    Console.Write(")");
    Blue();
    Console.Write(": ");

    ResetColor();
}

// Give the only loser all the extra chips as discount 
static void PrintDiscountOnlyLoserSolution(string biggestLoserName, double biggestLoserValue, double biggestLoserCashout, double differenceChips, bool soloLoser)
{
    Yellow();
    Console.Write($"{biggestLoserName.ToUpper()}");
    Blue();
    Console.Write(soloLoser == true ? " the only " : " the biggest ");
    Red();
    Console.Write("loser ");
    Blue();
    Console.Write("who lost ");
    Yellow();
    Console.Write($"(");
    Red();
    Console.Write($"{Math.Round(biggestLoserValue, 2)}");
    Yellow();
    Console.Write($")");
    Green();
    Console.Write("\nGets");
    Blue();
    Console.Write(" all of the extra chips ");
    Yellow();
    Console.Write($"(");
    Cyan();
    Console.Write($"{Math.Round(differenceChips, 2)}");
    Yellow();
    Console.Write($")");
    Magenta();
    Console.Write("\nNew ");
    Red();
    Console.Write("cashout ");
    Magenta();
    Console.Write("of ");
    Yellow();
    Console.Write($"{biggestLoserName.ToUpper()}");
    Magenta();
    Console.Write(" = ");
    Red();
    Console.Write("*");
    Cyan();
    Console.Write($"{Math.Round(biggestLoserCashout + differenceChips, 2)}");
    Red();
    Console.Write("*");
    Magenta();
    Console.Write(".");
    ResetColor();
}

// Take the extra chips from the winners cashout
static void PrintForfeitOnlyWinnerSolution(string biggestWinnerName, double biggestWinnerValue, double biggestWinnerCashout, double differenceChips, bool soloWinner)
{
    Yellow();
    Console.Write($"{biggestWinnerName.ToUpper()}");
    Blue();
    Console.Write(soloWinner == true ? " the only " : " the biggest ");
    Green();
    Console.Write("winner ");
    Blue();
    Console.Write("who won ");
    Yellow();
    Console.Write($"(");
    Green();
    Console.Write($"{Math.Round(biggestWinnerValue, 2)}");
    Yellow();
    Console.Write($")");
    Red();
    Console.Write("\nCovers");
    Blue();
    Console.Write(" all of the missing chips ");
    Yellow();
    Console.Write($"(");
    Cyan();
    Console.Write($"{Math.Round(differenceChips, 2)}");
    Yellow();
    Console.Write($")");
    Magenta();
    Console.Write("\nNew ");
    Red();
    Console.Write("cashout ");
    Magenta();
    Console.Write("of ");
    Yellow();
    Console.Write($"{biggestWinnerName.ToUpper()}");
    Magenta();
    Console.Write(" = ");
    Red();
    Console.Write("*");
    Cyan();
    Console.Write($"{Math.Round(biggestWinnerCashout - differenceChips, 2)}");
    Red();
    Console.Write("*");
    Magenta();
    Console.Write(".");
    ResetColor();
}

static bool AskIfGameSolvedDueToAlter(int replitCharCount)
{

    Blue();
    Console.Write("Trying to ");
    Magenta();
    Console.Write("resolve ");
    Blue();
    Console.Write("the game by ");
    Magenta();
    Console.Write("alt");
    Blue();
    Console.Write("ering");
    Magenta();
    Console.Write("? ");
    ResetColor();

    bool ans = GetYesOrNo(Console.ReadLine(), replitCharCount);

    ClearConsoleLines(0, 1, replitCharCount);

    return ans;
}

static string GetSolutionResponse1(string str, int replitCharCount)
{
    int count = 0;
    str = str.Trim();
    while (!(str == "1" || str.ToLower().Contains("new") || str.ToLower().Contains("alt") || str.ToLower().Contains("ex")))
    {
        if (count++ == 0) ClearConsoleLines(0, 2, replitCharCount);
        Red();
        Console.Write("Error:\n");
        Red();
        Console.Write($"Entered invalid string: ");
        Yellow();
        Console.Write("'");
        Cyan();
        Console.Write($"{TrimAndCapitalize(str)}");
        Yellow();
        Console.Write("'\n");
        Magenta();
        Console.Write("Options: ");
        Yellow();
        Console.Write("(");
        Magenta();
        Console.Write("1");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("New ");
        DarkRed();
        Console.Write("data");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("Alt");
        DarkRed();
        Console.Write("er data");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("Ex");
        DarkRed();
        Console.Write("it");
        Yellow();
        Console.Write(")\n");
        Blue();
        Console.Write($"Enter a valid response: ");
        ResetColor();
        str = Console.ReadLine();
        if (!(str == "1" || str.ToLower().Contains("new") || str.ToLower().Contains("alt") || str.ToLower().Contains("ex"))) ClearConsoleLines(0, 4, replitCharCount);
    }
    ResetColor();
    return str;
}
static void ClearConsoleLines(int left, int top, int replitCharCount)
{
    Console.SetCursorPosition(Console.CursorLeft - left, Console.CursorTop - top);
    string clearSpace = "";
    // Set the empty row to be the exact count as the max amount of chars that fit in one line
    for (int i = 0; i < replitCharCount; i++)
    {
        clearSpace += " ";
    }
    // Empty the rows from top to bottom
    for (int i = 0; i < top; i++)
    {
        Console.Write(clearSpace);
        if (i < top) Console.WriteLine();
    }
    Console.SetCursorPosition(Console.CursorLeft - left, Console.CursorTop - top);
}


static string GetSolutionResponse2(string str, int replitCharCount)
{
    int count = 0;
    str = str.Trim();
    while (!(str == "1" || str == "2" || str == "3" || str.ToLower().Contains("new") || str.ToLower().Contains("alt") || str.ToLower().Contains("ex")))
    {
        if (count++ == 0) ClearConsoleLines(0, 2, replitCharCount);
        Red();
        Console.Write("Error:\n");
        Red();
        Console.Write($"Entered invalid string: ");
        Yellow();
        Console.Write("'");
        Cyan();
        Console.Write($"{TrimAndCapitalize(str)}");
        Yellow();
        Console.Write("'\n");
        Magenta();
        Console.Write("Options: ");
        Yellow();
        Console.Write("(");
        Magenta();
        Console.Write("1");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("2");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("3");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("New ");
        DarkRed();
        Console.Write("data");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("Alt");
        DarkRed();
        Console.Write("er data");
        DarkGray();
        Console.Write("||");
        Magenta();
        Console.Write("Ex");
        DarkRed();
        Console.Write("it");
        Yellow();
        Console.Write(")\n");
        Blue();
        Console.Write($"Enter a valid response: ");
        ResetColor();
        str = Console.ReadLine();
        if (!(str == "1" || str == "2" || str == "3" || str.ToLower().Contains("new") || str.ToLower().Contains("alt") || str.ToLower().Contains("ex"))) ClearConsoleLines(0, 4, replitCharCount);
    }
    ResetColor();
    return str;
}


static void ResetDic(Dictionary<string, PlayerStats> playerStatsDic)
{
    foreach (var player in playerStatsDic)
    {
        player.Value.BuyIn = 0;
        player.Value.BuyOut = 0;
    }
}

static void PrintLosersGain()
{
    Console.Write("\n╔═════════");
    PrintSuitsConnected();
    Console.Write("═══════╗\n");

    Console.Write("║");
    Red();
    Console.Write("  Losers ");
    Green();
    Console.Write("GAIN ");
    DarkRed();
    Console.Write("Error");
    ResetColor();
    Console.Write(" ║\n");

    Console.Write("╚═════════");
    PrintSuitsConnectedReverse();
    Console.Write("═══════╝\n");
}

static void PrintWinnersLose()
{
    Console.Write("\n╔═════════");
    PrintSuitsConnected();
    Console.Write("═══════╗\n");

    Console.Write("║");
    Green();
    Console.Write(" Winners ");
    Red();
    Console.Write("LOSE ");
    DarkRed();
    Console.Write("Error");
    ResetColor();
    Console.Write(" ║\n");

    Console.Write("╚═════════");
    PrintSuitsConnectedReverse();
    Console.Write("═══════╝\n");
}

static void PrintGeneralError()
{
    Console.Write("\n╔════════");
    PrintSuitsConnected();
    Console.Write("═══════╗\n");

    Console.Write("║ ");
    DarkRed();
    Console.Write("Calculation Error");
    ResetColor();
    Console.Write(" ║\n");

    Console.Write("╚════════");
    PrintSuitsConnectedReverse();
    Console.Write("═══════╝\n");
}

static void PrintErrorNoWinnersNoLosers()
{
    PrintGeneralError();
    Red();
    Console.Write("Error:\n");
    Red();
    Console.Write("Cannot run program when:\n");
    DarkRed();
    Console.Write("Losers  ");
    Magenta();
    Console.Write("count = ");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write("0");
    DarkGray();
    Console.Write("'\n");
    Magenta();
    Console.Write("&&\n");
    DarkGreen();
    Console.Write("Winners ");
    Magenta();
    Console.Write("count = ");
    DarkGray();
    Console.Write("'");
    DarkGreen();
    Console.Write("0");
    DarkGray();
    Console.Write("'\n");
    Red();
    Console.Write("Exiting program...");
    ResetColor();
}

static void PrintErrorNoWinners()
{
    PrintGeneralError();
    Red();
    Console.Write("Error:\n");
    Red();
    Console.Write("Cannot run program when:\n");
    DarkGreen();
    Console.Write("Winners ");
    Magenta();
    Console.Write("count = ");
    DarkGray();
    Console.Write("'");
    DarkGreen();
    Console.Write("0");
    DarkGray();
    Console.Write("'\n");
    Red();
    Console.Write("Exiting program...");
    ResetColor();
}

static void PrintErrorNoLosers()
{
    PrintGeneralError();
    Red();
    Console.Write("Error:\n");
    Red();
    Console.Write("Cannot run program when:\n");
    DarkRed();
    Console.Write("Losers ");
    Magenta();
    Console.Write("count = ");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write("0");
    DarkGray();
    Console.Write("'\n");
    Red();
    Console.Write("Exiting program...");
    ResetColor();
}


static void PrintStartWinnerError(double difference, double buyIn, double buyOut)
{
    Red();
    Console.Write("\nCash-Out");
    Magenta();
    Console.Write(" is MORE than ");
    Green();
    Console.Write("Buy-in");
    Red();
    Console.Write("\n\nReduce");
    Magenta();
    Console.Write(" the ");
    Green();
    Console.Write("winners'");
    Red();
    Console.Write(" Cashout");
    Blue();
    Console.Write("\nTotal missing chips: ");
    Yellow();
    Console.Write($"(");
    Cyan();
    Console.Write($"{difference}");
    Yellow();
    Console.Write($")");
    Red();
    Console.Write($"\n\nCashouts ({buyOut})");
    Yellow();
    Console.Write(" != ");
    Green();
    Console.Write($"Buyins ({buyIn})");
}

static void PrintStartLoserError(double difference, double buyIn, double buyOut)
{
    Green();
    Console.Write("\nBuy-in");
    Magenta();
    Console.Write(" is MORE than ");
    Red();
    Console.Write("Cash-Out");
    Green();
    Console.Write("\n\nIncrease");
    Magenta();
    Console.Write(" the ");
    Red();
    Console.Write("losers' Cashout\n");
    Blue();
    Console.Write("The extra amount is: ");
    Yellow();
    Console.Write($"(");
    Cyan();
    Console.Write($"{difference}");
    Yellow();
    Console.Write($")");
    Green();
    Console.Write($"\n\nBuyins ({buyIn})");
    Yellow();
    Console.Write(" != ");
    Red();
    Console.Write($"Cashouts ({buyOut})");
}

#region Get proper inputs

// Check user double input to avoid errors when the user inputs chars other than 0-9 & '.' instead of a number or a number that is too large
// Used for when a double is connected to a name to get a proper double input
static double GetProperDoubleForName(string input, string name, bool ifBuyInTrue, int replitCharCount)
{
    double goodInput = 0;
    bool isValid = false;
    string savedWord = "";
    if (ifBuyInTrue == true) savedWord = "Buyin";
    else savedWord = "Cashout";
    name = TrimAndCapitalize(name);

    while (!isValid)
    {
        try
        {
            goodInput = Math.Abs(Convert.ToDouble(input));
            isValid = true; // If conversion succeeds, set isValid to true
        }
        catch (FormatException)
        {
            PrintErrorDoubleWithName(input, name, savedWord, ifBuyInTrue);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (OverflowException)
        {
            PrintErrorDoubleWithName(input, name, savedWord, ifBuyInTrue);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (Exception c)
        {
            Red();
            Console.Write("An unexpected error occurred:\n");
            ResetColor();
            Console.WriteLine($"{c.Message}");
            throw; // Re-throw the exception if it's something unexpected
        }
    }
    return goodInput;
}

static double GetProperDoubleForPot(string input, int replitCharCount)
{
    double goodInput = 0;
    bool isValid = false;
    while (!isValid)
    {
        try
        {
            goodInput = Math.Abs(Convert.ToDouble(input));
            isValid = true; // If conversion succeeds, set isValid to true
        }
        catch (FormatException)
        {
            PrintErrorDoubleWithPotAmount(input);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (OverflowException)
        {
            PrintErrorDoubleWithPotAmount(input);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (Exception c)
        {
            Red();
            Console.Write("An unexpected error occurred:\n");
            ResetColor();
            Console.WriteLine($"{c.Message}");
            throw; // Re-throw the exception if it's something unexpected
        }
    }
    return goodInput;
}

static double GetProperDouble(string input, int replitCharCount)
{
    double goodInput = 0;
    bool isValid = false;
    while (!isValid)
    {
        try
        {
            goodInput = Math.Abs(Convert.ToDouble(input));
            isValid = true; // If conversion succeeds, set isValid to true
        }
        catch (FormatException)
        {
            PrintErrorDouble(input);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (OverflowException)
        {
            PrintErrorDouble(input);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (Exception c)
        {
            Red();
            Console.Write("An unexpected error occurred:\n");
            ResetColor();
            Console.WriteLine($"{c.Message}");
            throw; // Re-throw the exception if it's something unexpected
        }
    }
    return goodInput;
}

static int GetProperIntForPlayers(string input, double potValue, int replitCharCount)
{
    int goodInput = 0;
    bool isValid = false;
    while (!isValid)
    {
        try
        {
            goodInput = Math.Abs(Convert.ToInt32(input));
            isValid = true; // If conversion succeeds, set isValid to true
        }
        catch (FormatException)
        {
            PrintErrorIntPlayers(input, potValue);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 5, replitCharCount);
        }
        catch (OverflowException)
        {
            PrintErrorIntPlayers(input, potValue);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 5, replitCharCount);
        }
        catch (Exception c)
        {
            Red();
            Console.Write("An unexpected error occurred:\n");
            ResetColor();
            Console.WriteLine($"{c.Message}");
            throw; // Re-throw the exception if it's something unexpected
        }
    }
    return goodInput;
}

static int GetProperInt(string input, int replitCharCount)
{
    int goodInput = 0;
    bool isValid = false;
    while (!isValid)
    {
        try
        {
            goodInput = Math.Abs(Convert.ToInt32(input));
            isValid = true; // If conversion succeeds, set isValid to true
        }
        catch (FormatException)
        {
            PrintErrorInt(input);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (OverflowException)
        {
            PrintErrorInt(input);
            input = Console.ReadLine(); // Prompt the user to enter a valid input again
            ClearConsoleLines(0, 4, replitCharCount);
        }
        catch (Exception c)
        {
            Red();
            Console.Write("An unexpected error occurred:\n");
            ResetColor();
            Console.WriteLine($"{c.Message}");
            throw; // Re-throw the exception if it's something unexpected
        }
    }
    return goodInput;
}

static void PrintErrorDoubleWithName(string input, string name, string savedWord, bool ifBuyInTrue)
{
    Red();
    Console.Write($"\nError:");
    Magenta();
    Console.Write($"\n{name} ");
    Blue();
    Console.Write("has an ");
    Red();
    Console.Write("invalid ");
    if (ifBuyInTrue == true) Green();
    else Red();
    Console.Write($"{savedWord} ");
    Yellow();
    Console.Write("(");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write($"{input}");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(")");
    Red();
    Console.Write("!");
    Blue();
    Console.Write("\nNew ");
    if (ifBuyInTrue == true) Green();
    else Red();
    Console.Write($"{savedWord}: ");
    ResetColor();
}

static void PrintErrorDoubleWithPotAmount(string input)
{
    Red();
    Console.Write($"\nError:");
    Yellow();
    Console.Write($"\nPot ");
    Blue();
    Console.Write("has an ");
    Red();
    Console.Write("invalid value ");
    Yellow();
    Console.Write("(");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write($"{input}");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(")");
    Red();
    Console.Write("!");
    Blue();
    Console.Write("\nNew ");
    Yellow();
    Console.Write("pot ");
    Blue();
    Console.Write("value: ");
    ResetColor();
}

static void PrintErrorDouble(string input)
{
    Red();
    Console.Write($"\nError:");
    Magenta();
    Console.Write($"\nInput ");
    Blue();
    Console.Write("is ");
    Red();
    Console.Write("invalid ");
    Yellow();
    Console.Write("(");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write($"{input}");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(")");
    Red();
    Console.Write("!");
    Blue();
    Console.Write("\nNew ");
    Magenta();
    Console.Write($"double value");
    Blue();
    Console.Write(": ");
    ResetColor();
}

static void PrintErrorIntPlayers(string input, double potValue)
{
    Red();
    Console.Write($"\nError:");
    Blue();
    Console.Write("\nReferring to ");
    Yellow();
    Console.Write($"pot ");
    Yellow();
    Console.Write("(");
    DarkGray();
    Console.Write("'");
    DarkGreen();
    Console.Write($"{potValue}");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(")");
    Console.Write($"\nPlayer ");
    Blue();
    Console.Write("amount is ");
    Red();
    Console.Write("invalid ");
    Yellow();
    Console.Write("(");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write($"{input}");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(")");
    Red();
    Console.Write("!");
    Blue();
    Console.Write("\nNew ");
    Yellow();
    Console.Write("player ");
    Blue();
    Console.Write("amount: ");
    ResetColor();
}

static void PrintErrorInt(string input)
{
    Red();
    Console.Write($"\nError:");
    Magenta();
    Console.Write($"\nInput ");
    Blue();
    Console.Write("is ");
    Red();
    Console.Write("invalid ");
    Yellow();
    Console.Write("(");
    DarkGray();
    Console.Write("'");
    DarkRed();
    Console.Write($"{input}");
    DarkGray();
    Console.Write("'");
    Yellow();
    Console.Write(")");
    Red();
    Console.Write("!");
    Blue();
    Console.Write("\nNew ");
    Magenta();
    Console.Write($"integer value");
    Blue();
    Console.Write(": ");
    ResetColor();
}


static string CheckEnglishName(string str, int replitCharCount)
{
    bool validNameLetters = false;
    bool validNameLength = false;
    bool errorFirstTime = true;
    while (validNameLetters == false || validNameLength == false)
    {
        str = str.ToLower().Trim();
        validNameLetters = true;
        validNameLength = true;
        foreach (char c in str)
        {
            if (!(c >= 'a' && c <= 'z' || c >= '0' && c <= '9'))
            {
                validNameLetters = false;
                if (errorFirstTime == false) ClearConsoleLines(0, 4, replitCharCount);
                errorFirstTime = false;
                PrintEnglishNameError(str, c);
                str = Console.ReadLine();
                break;
            }
        }
        if (validNameLetters == false) continue;

        if (str.Length < 2 || str == null)
        {
            validNameLength = false;
            if (errorFirstTime == false) ClearConsoleLines(0, 4, replitCharCount);
            errorFirstTime = false;
            PrintLengthNameError(str);
            str = Console.ReadLine();
        }
        if (validNameLength == false) continue;
    }
    return str;
}

static void PrintEnglishNameError(string badName, char c)
{
    Red();
    Console.Write("Error\n");
    Blue();
    Console.Write("Name ");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{badName}");
    DarkGray();
    Console.Write("' ");
    Blue();
    Console.Write("contains invalid char ");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{c}");
    DarkGray();
    Console.Write("'\n");
    Red();
    Console.Write("Name must be English letters and or numbers\n");
    Blue();
    Console.Write("Enter a new name for ");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{badName}");
    DarkGray();
    Console.Write("'");
    Blue();
    Console.Write(": ");
    ResetColor();
}

static void PrintLengthNameError(string badName)
{
    Red();
    Console.Write("Error\n");
    Blue();
    Console.Write("Name ");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{badName}");
    DarkGray();
    Console.Write("' ");
    Blue();
    Console.Write("is invalid\n");
    Red();
    Console.Write("Name must be at least ");
    Magenta();
    Console.Write("2 ");
    Red();
    Console.Write("chars long!\n");
    Blue();
    Console.Write("Enter a new name for ");
    DarkGray();
    Console.Write("'");
    Magenta();
    Console.Write($"{badName}");
    DarkGray();
    Console.Write("'");
    Blue();
    Console.Write(": ");
    ResetColor();
}
static string GetFullNameIfAvailableOrSetName(string namePart, string[] usedNames, Dictionary<string, PlayerStats> statsDict, int replitCharCount)
{
    // Used to determine if there are a few option of the namePart inside of the players dictionary or not.
    int countPlayerAmount = 0;

    namePart = CheckEnglishName(namePart, replitCharCount);

    // Make an array that will hold all the names that are not used and contain the name part;
    // * array with all of the names that are in the statsDict that CONTAIN the namePart and are not in the usedNames array
    string[] availableNamesArr = new string[0];

    foreach (var player in statsDict)
    {
        bool playerIsUsed = false;
        if (player.Key.ToLower().Contains(namePart.ToLower()))
        {
            foreach (string name in usedNames)
            {
                if (name.ToLower() == player.Key.ToLower())
                {
                    playerIsUsed = true;
                }
            }
            if (playerIsUsed == false)
            {
                availableNamesArr = AddOneToArray(availableNamesArr);
                availableNamesArr[availableNamesArr.Length - 1] = player.Key;
                countPlayerAmount++;
            }
        }
    }

    // Will be used to identify an exact match such as 'ron' == 'Ron' in the statsDict. 
    foreach (var player in statsDict)
    {
        if (player.Key.ToLower() == namePart.ToLower())
        {
            foreach (var name in availableNamesArr)
            {
                if (player.Key.ToLower() == name.ToLower()) return player.Key;
            }
        }
    }

    // Will be used to identify shortcuts such as 'gio' == 'Giora' in the statsDict. 
    if (countPlayerAmount == 1) //  1 player was found
    {
        foreach (var player in statsDict)
        {
            if (player.Key.ToLower().Contains(namePart.ToLower()))
            {
                foreach (var name in availableNamesArr)
                {
                    if (player.Key.ToLower() == name.ToLower()) return player.Key;
                }
            }
        }
    }
    else if (countPlayerAmount > 1) //  2 or more players were found
    {
        foreach (var availName in availableNamesArr)
        {
            if (availName.ToLower().Contains(namePart.ToLower()))
            {
                // Ask if this is the player you meant, else continue
                Red();
                Console.Write("Multiple players found with: ");
                DarkGray();
                Console.Write("'");
                Cyan();
                Console.Write($"{namePart}");
                DarkGray();
                Console.Write("'");
                DarkYellow();
                Console.Write(" (");
                Cyan();
                Console.Write($"{countPlayerAmount--}");
                DarkYellow();
                Console.Write(")\n");
                Blue();
                Console.Write("Did you mean: ");
                DarkGray();
                Console.Write("'");
                Magenta();
                Console.Write($"{availName}");
                DarkGray();
                Console.Write("'");
                Blue();
                Console.Write("?");
                Blue();
                Console.Write("\nAnswer: ");
                ResetColor();
                bool correctPlayer = GetYesOrNo(Console.ReadLine(), replitCharCount);
                if (correctPlayer == true) return availName;
            }
        }
    }
    namePart = TrimAndCapitalize(namePart);
    return namePart;
}


// Get a True or False boolean from a variety of strings
static bool GetYesOrNo(string input, int replitCharCount)
{
    do
    {
        input = input.Trim();
        input = input.ToLower();
        while (input.Length > 5)
        {
            input = PrintLengthErrorYesOrNo(input, 0, 5, replitCharCount);
            input = input.Trim();
            input = input.ToLower();
        }
    } while (input.Length > 5);
    if (input.ToLower().Contains('y') || input.ToLower().Contains('t')) return true;
    return false; // Must be an error / 'n'
}

#endregion

static void GetPlayerColor(string playerName)
{
    switch (playerName)
    {
        // ~~ OG Gang ~~ \\
        case "Giora":
            DarkBlue();
            break;
        case "Ron":
            DarkRed();
            break;
        case "Idan":
            Red();
            break;
        case "Vexy":
            Green();
            break;
        case "Pardo":
            Red();
            break;
        case "Liron":
            Red();
            break;
        // Hadera Rahok Li \\
        case "Liel":
            DarkCyan();
            break;
        case "Hadar":
            DarkCyan();
            break;
        case "Yonatan": // aka Hadad (Hadars friend)
            DarkCyan();
            break;
        // Occasional Players \\
        case "Gal":
            Blue();
            break;
        case "Dana":
            DarkMagenta();
            break;
        case "Yahav":
            Blue();
            break;
        case "Lerner":
            Blue();
            break;
        default:
            ResetColor();
            break;
    }
}
#endregion

static string OrderStatsString(string statsString)
{
    // Normalize data string (readable and by value)
    // "Osher 75 247.5, Pardo 150 407, Idan 400 474, " => "Pardo 150 407 , Osher 75 247.5 , Idan 400 474"

    string temp = "";

    // // Add ',' for SortValuesInStatsString(temp) calculation as well as " , " calculation.
    // if (!statsString.Contains(',')) statsString += ",";

    string[] players = statsString.Split(',');

    for (int i = 0; i < players.Length; i++)
    {
        // Add only strings that are valid
        string[] tempPlayer = TrimWordsInString(players[i]);
        if (tempPlayer.Length != 3) continue;
        for (int j = 0; j < tempPlayer.Length; j++)
        {
            temp += tempPlayer[j];
            if (j < tempPlayer.Length - 1) temp += " ";
        }

        // Ignore the empty " ," in the original 'statsString' so in the new string you will not have a ',' in the end
        if (i != players.Length - 1 && players.Length > 1)
            //players[i+1].Length > 3 = If next player is an actual playerString and not " "
            if (players[i + 1].Length > 3) temp += " , "; // players.Length > 1 to avoid adding a " , " when there is only 1 player 
    }

    temp = SortValuesInStatsString(temp);

    return temp;
}

static string ExtractDataStringFromDictionary(Dictionary<string, PlayerStats> playerStatsDic)
{
    string temp = "";
    foreach (var player in playerStatsDic)
    {
        if (player.Value.BuyIn == 0 && player.Value.BuyOut == 0) continue;
        temp += player.Key;
        temp += " ";
        temp += player.Value.BuyIn;
        temp += " ";
        temp += player.Value.BuyOut;
        temp += " , ";
    }

    return temp.Substring(0, temp.Length - 3); // -3 removes " , "
}


static string SortValuesInStatsString(string statsString)
{
    /// Sort players in data string from top to bottom in value 100 => -100 (cashout - buyin)
    string temp = "";

    string[] players = statsString.Split(',');
    for (int i = 0; i < players.Length; i++)
    {
        players[i] = players[i].Trim();
    }
    // Go amount of times there are strings
    while (players.Length >= 1)
    {
        // If no more players, exit the loop
        if (players.Length == 1)
        {
            temp += players[0];
            break;
        }

        // // Delete a playerString if an empty playerString is given
        // {

        // }

        double largestValue = 0;
        string largestValuePlayer = "";
        // Find largest value
        for (int i = 0; i < players.Length; i++)
        {
            // Cashout - Buyin == Value;
            double value = Convert.ToDouble(players[i].Split(' ')[2]) - Convert.ToDouble(players[i].Split(' ')[1]);
            if (i == 0)
            {
                largestValue = value;
                continue;
            }
            if (value > largestValue) largestValue = value;
        }
        // Go again add largest value to temp
        for (int i = 0; i < players.Length; i++)
        {
            double value = Convert.ToDouble(players[i].Split(' ')[2]) - Convert.ToDouble(players[i].Split(' ')[1]);
            if (value == largestValue)
            {
                largestValuePlayer = players[i];
                break;
            }
        }

        // Add target value to temp
        temp += largestValuePlayer;
        temp += " , ";

        // Remove largest value
        int indexForTempPlayers = 0;
        string[] tempPlayers = new string[players.Length - 1];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == largestValuePlayer) continue;
            tempPlayers[indexForTempPlayers++] = players[i];
        }
        players = tempPlayers;
    }

    return temp;
}


static string[] TrimWordsInString(string str)
{
    // Trim all of the strings for uniformity
    str = str.Trim();                                          // Trim of each long string   "  H   200 200"   => "H   200 200"

    string[] playerStatsTest = str.Split(' ');

    string[] newArr = new string[0];

    // Trim all words for uniformity
    int trimCounter = 0;
    for (int j = 0; j < playerStatsTest.Length; j++)
    {
        if (playerStatsTest[j].Trim() == "" || playerStatsTest[j] == null) continue;
        else
        {
            newArr = AddOneToArray(newArr);
            newArr[trimCounter++] = playerStatsTest[j].Trim(); // Trim of each string inside "H" "  200" "200" => "H" "200" "200"
        }
    }
    return newArr;
}


static string AlterPlayersDataString(string originalPlayersDataString, string sentPlayersDataString)
{
    // Making the new updated string that will contain all of the players data to run the RecordStatsStringAndAddToDictionary() function.
    string updatedDataString = "";

    // Add ',' so the split(',') won't crash 
    if (!originalPlayersDataString.Contains(',')) originalPlayersDataString += ",";
    if (!sentPlayersDataString.Contains(',')) sentPlayersDataString += ",";

    // Break down the 2 strings into the player (player + buyIn + buyOut) sections
    string[] brokenDownStringOG = originalPlayersDataString.Split(',');
    string[] brokenDownStringNew = sentPlayersDataString.Split(',');

    string[] newPlayersStats = new string[0];

    // Iterate over both of them replacing or adding new players
    for (int i = 0; i < brokenDownStringNew.Length; i++)
    {
        // Ignore invalid strings such as the end of each DataString aka ", ". 
        if (brokenDownStringNew[i].Trim().Split(" ").Length != 3) continue;

        brokenDownStringNew[i] = brokenDownStringNew[i].Trim();

        string playerNewName = brokenDownStringNew[i].Split(' ')[0];
        string playerNewBuyIn = brokenDownStringNew[i].Split(' ')[1];
        string playerNewBuyOut = brokenDownStringNew[i].Split(' ')[2];

        for (int j = 0; j < brokenDownStringOG.Length; j++)
        {
            // Ignore invalid strings such as the end of each DataString aka ", ". 
            if (brokenDownStringOG[j].Trim().Split(" ").Length != 3) continue;

            brokenDownStringOG[j] = brokenDownStringOG[j].Trim();

            string playerOGName = brokenDownStringOG[j].Split(' ')[0];
            string playerOGBuyIn = brokenDownStringOG[j].Split(' ')[1];
            string playerOGBuyOut = brokenDownStringOG[j].Split(' ')[2];

            bool newPlayer = true;

            // Check if it is a new player
            foreach (string stringPartOG in brokenDownStringOG)
            {
                if (playerNewName == stringPartOG.Trim().Split(" ")[0])
                {
                    newPlayer = false;
                    break;
                }
            }
            // If it's a new player add it to the original array
            if (newPlayer == true)
            {
                newPlayersStats = AddOneToArray(newPlayersStats);
                newPlayersStats[newPlayersStats.Length - 1] = brokenDownStringNew[i];
                break;
            }

            // If both names are the same, replace the OG string with the new one
            if (playerOGName.Trim().ToLower() == playerNewName.Trim().ToLower())
            {
                brokenDownStringOG[j] = brokenDownStringNew[i];
                // Exit the loop to avoid adding additional players (duplicates)
                break;
            }
        }
    }

    // Make the new string
    foreach (string playerStats in brokenDownStringOG)
    {
        if (playerStats == " ") continue;
        updatedDataString += playerStats.Trim();
        updatedDataString += ", ";
    }
    // Add any new players 
    foreach (string playerStats in newPlayersStats)
    {
        if (playerStats == " ") continue;
        updatedDataString += playerStats.Trim();
        updatedDataString += ", ";
    }


    // Normalize data string for DB (make it more readable)
    updatedDataString = OrderStatsString(updatedDataString);

    return updatedDataString;
}

static string GetDifferenceInTwoGameStrings(string originalPlayersDataString, string sentPlayersDataString)
{
    // Making the new updated string that will contain all of the players data to run the RecordStatsStringAndAddToDictionary() function.
    string updatedDataString = "";

    // Break down the 2 strings into the player (player + buyIn + buyOut) sections
    string[] brokenDownStringOG = originalPlayersDataString.Split(',');
    string[] brokenDownStringNew = sentPlayersDataString.Split(',');

    string[] newPlayersStats = new string[0];

    // Iterate over both of them replacing or adding new players
    for (int i = 0; i < brokenDownStringNew.Length; i++)
    {
        // Ignore invalid strings such as the end of each DataString aka ", ". 
        if (brokenDownStringNew[i].Trim().Split(" ").Length != 3) continue;

        brokenDownStringNew[i] = brokenDownStringNew[i].Trim();

        string playerNewName = brokenDownStringNew[i].Split(' ')[0];
        string playerNewBuyIn = brokenDownStringNew[i].Split(' ')[1];
        string playerNewBuyOut = brokenDownStringNew[i].Split(' ')[2];

        for (int j = 0; j < brokenDownStringOG.Length; j++)
        {
            // Ignore invalid strings such as the end of each DataString aka ", ". 
            if (brokenDownStringOG[j].Trim().Split(" ").Length != 3) continue;


            brokenDownStringOG[j] = brokenDownStringOG[j].Trim();

            string playerOGName = brokenDownStringOG[j].Split(' ')[0];
            string playerOGBuyIn = brokenDownStringOG[j].Split(' ')[1];
            string playerOGBuyOut = brokenDownStringOG[j].Split(' ')[2];

            bool newPlayer = true;

            // Check if it is a new player
            foreach (string stringPartOG in brokenDownStringOG)
            {
                if (playerNewName == stringPartOG.Trim().Split(" ")[0])
                {
                    newPlayer = false;
                    break;
                }
            }
            // If it's a new player add it to the original array
            if (newPlayer == true)
            {
                updatedDataString += brokenDownStringNew[i] += ", ";
                break;
            }

            // If both names are the same, replace the OG string with the new one
            if (playerOGName.Trim().ToLower() == playerNewName.Trim().ToLower() && (playerOGBuyIn != playerNewBuyIn || playerOGBuyOut != playerNewBuyOut))
            {
                if (playerOGBuyIn != playerNewBuyIn || playerOGBuyOut != playerNewBuyOut)
                {
                    updatedDataString += brokenDownStringNew[i] += ", ";
                }
                // Exit the loop to avoid adding additional players (duplicates)
                break;
            }
        }
    }

    // Normalize data string for DB (make it more readable)
    updatedDataString = OrderStatsString(updatedDataString);

    return updatedDataString;
}

static void AddOGPlayerToDictionary(Dictionary<string, PlayerStats> statsDict)
{
    string[] ogNames =
    {
        // ~~ OG Gang ~~ \\
        "Giora",
        "Ron",
        "Vexy",
        "Idan",
        "Pardo",
        "Liron",
        // Hadera Rahok Li \\
        "Liel",
        "Hadar",
        "Yonatan",
        // Occasional Players \\
        "Gal",
        "Dana",
        "Yahav",
        "Lerner"
    };

    foreach (string ogName in ogNames)
    {
        bool playerFound = false;
        foreach (var player in statsDict)
        {
            if (player.Key == ogName) playerFound = true;
        }
        if (playerFound == false) statsDict.Add(ogName, new PlayerStats(0, 0));
    }
}
static void RecordStatsStringAndAddToDictionary(string stats, Dictionary<string, PlayerStats> statsDict)
{
    ResetDic(statsDict);

    string[] statsStrings = stats.Split(',');

    for (int i = 0; i < statsStrings.Length; i++)
    {
        // Trim the strings so the data is not ""
        statsStrings[i] = statsStrings[i].Trim();

        // Ignore invalid strings such as the end of each validDataString aka ", ". 
        if (statsStrings[i].Split(" ").Length != 3) continue;

        // Set variables for the stats we need and reset them every time
        // The data has been checked so it will be valid
        string playerName = statsStrings[i].Split(" ")[0];
        double playerBuyIn = Convert.ToDouble(statsStrings[i].Split(" ")[1]);
        double playerBuyOut = Convert.ToDouble(statsStrings[i].Split(" ")[2]);

        // Set values to the main dictionary
        bool playerChanged = false;
        foreach (var player in statsDict)
        {
            if (player.Key == playerName)
            {
                player.Value.BuyIn = playerBuyIn;
                player.Value.BuyOut = playerBuyOut;
                playerChanged = true;
                break;
            }
        }
        if (playerChanged == false) statsDict.Add(playerName, new PlayerStats(playerBuyIn, playerBuyOut));
    }
}
static string RecordValidStatsString(string stats, Dictionary<string, PlayerStats> statsDict, int replitCharCount)
{
    // Create the string that is valid and will be returned in order to alter it later if needed. 
    string validStats = "";

    string[] statsStrings = stats.Split(',');

    // Make an array for used names in order to prevent dupes
    string[] usedNames = new string[0];
    int counterUsed = 0; // For adding used player names

    for (int i = 0; i < statsStrings.Length; i++)
    {
        // Order strings
        string[] playerStatsTest = TrimWordsInString(statsStrings[i]);

        // Check if length is correct, else get a proper length

        // If blank ',   ' is put into the string, skip it.
        if (playerStatsTest.Length == 0) continue;

        if (!(playerStatsTest.Length >= 2 && playerStatsTest.Length <= 3))
        {
            string newData = PrintLengthError(playerStatsTest, playerStatsTest.Length, 2, 3, replitCharCount);
            playerStatsTest = TrimWordsInString(newData);
        }

        // Set variables for the stats we need and reset them every time
        string playerName = "";
        double playerBuyIn = 0;
        double playerBuyOut = 0;

        if (playerStatsTest.Length == 2)
        {
            playerName = GetFullNameIfAvailableOrSetName(playerStatsTest[0], usedNames, statsDict, replitCharCount);
            playerBuyIn = GetProperDoubleForName(playerStatsTest[1], playerName, true, replitCharCount);
            playerBuyOut = 0;
        }
        else if (playerStatsTest.Length == 3)
        {
            playerName = GetFullNameIfAvailableOrSetName(playerStatsTest[0], usedNames, statsDict, replitCharCount);
            playerBuyIn = GetProperDoubleForName(playerStatsTest[1], playerName, true, replitCharCount);
            playerBuyOut = GetProperDoubleForName(playerStatsTest[2], playerName, false, replitCharCount);
        }

        // After each name setting first check if name was already used, Get new name if so
        playerName = CheckAndGetNewNameString(playerName, usedNames, playerBuyIn, playerBuyOut, replitCharCount);

        // Check if name can be fitted again to an existing OG player
        playerName = GetFullNameIfAvailableOrSetName(playerName, usedNames, statsDict, replitCharCount);

        // Add name to used names and continue (To prevent dupes)
        usedNames = AddOneToArray(usedNames);
        usedNames[counterUsed++] = playerName;

        // Add stats to the string 'validStats'
        validStats += playerName + " " + playerBuyIn + " " + playerBuyOut + ", ";
    }

    // Normalize data string for DB (make it more readable)
    validStats = OrderStatsString(validStats);

    return validStats;
}

static string RecordValidPotStatsString(string stats, int replitCharCount)
{
    // Create the string that is valid and will be returned in order to alter it later if needed. 
    string validStats = "";

    string[] statsStrings = stats.Split(',');

    for (int i = 0; i < statsStrings.Length; i++)
    {
        // Order strings
        string[] potStatsTest = TrimWordsInString(statsStrings[i]);

        // Check if length is correct, else get a proper length

        // If blank ',   ' is put into the string, skip it.
        if (potStatsTest.Length == 0) continue;

        if (!(potStatsTest.Length == 2))
        {
            string newData = PrintLengthError(potStatsTest, potStatsTest.Length, 2, 2, replitCharCount);
            potStatsTest = TrimWordsInString(newData);
        }

        // Get proper values

        // Set variables for the stats we need and reset them every time
        double potAmount = GetProperDoubleForPot(potStatsTest[0], replitCharCount);
        int playerAmount = GetProperIntForPlayers(potStatsTest[1], potAmount, replitCharCount);

        // Add stats to the string 'validStats'
        validStats += potAmount + " " + playerAmount + ", ";
    }

    return validStats;
}


static void PrintPlayerStats(string playerName, string spacesP, string spacesBuyIn, double playerBuyIn, double playerBuyOut, string spacesProfitOrLoss, string spacesEven)
{

    GetPlayerColor(playerName);
    Console.Write($"{playerName}{spacesP}");
    Magenta();
    Console.Write($": ");
    DarkGreen();
    Console.Write($"BuyIn ");
    Magenta();
    Console.Write($"= ");
    DarkGreen();
    Console.Write($"{Math.Round(playerBuyIn, 2)} {spacesBuyIn}");
    DarkRed();
    Console.Write($"Cashout ");
    Magenta();
    Console.Write($"= ");
    DarkRed();
    Console.Write($"{Math.Round(playerBuyOut, 2)}\n");

    // Console.Write($": BuyIn = {playerBuyIn} {spacesBuyIn}BuyOut = {playerBuyOut}\n");

    ResetColor();


    if (playerBuyIn < playerBuyOut)
    {
        double gain = playerBuyOut - playerBuyIn;
        Green();
        Console.Write($"{spacesProfitOrLoss}Gain");
        Magenta();
        Console.Write(" => ");
        Green();
        Console.Write($"+{Math.Round(gain, 2)}\n\n");
        ResetColor();
    }
    else if (playerBuyIn > playerBuyOut)
    {
        double lose = playerBuyIn - playerBuyOut;
        Red();
        Console.Write($"{spacesProfitOrLoss}Loss");
        Magenta();
        Console.Write(" => ");
        Red();
        Console.Write($"-{Math.Round(lose, 2)}\n\n");
        ResetColor();
    }
    else
    {
        Magenta();
        Console.Write($"{spacesEven}∞");
        Yellow();
        Console.Write("Even"); // Another option is "Balanced"
        Magenta();
        Console.Write("∞\n\n");
        ResetColor();
    }
}
static Dictionary<string, PlayerStats> OrderDicForPrinting(Dictionary<string, PlayerStats> playerStatsDic)
{
    // Will return an Dictionary<string, PlayerStats> Sorted in the following format
    // Largest winner => smallest winner => Largest loser =>smallest loser => Neutral players

    Dictionary<string, PlayerStats> sorted = new Dictionary<string, PlayerStats>();

    // Make winners, losers lists
    List<KeyValuePair<string, double>> winnersList = new List<KeyValuePair<string, double>>();
    List<KeyValuePair<string, double>> losersList = new List<KeyValuePair<string, double>>();

    // Add as needed
    foreach (var player in playerStatsDic)
    {
        double value = Math.Round(Math.Abs(player.Value.BuyOut - player.Value.BuyIn), 2);
        if (player.Value.BuyOut > player.Value.BuyIn) winnersList.Add(new KeyValuePair<string, double>(player.Key, value));
        else if (player.Value.BuyOut < player.Value.BuyIn) losersList.Add(new KeyValuePair<string, double>(player.Key, value));
    }

    // Sort the lists (by VALUE)
    winnersList = winnersList.OrderByDescending(x => x.Value).ToList();
    losersList = losersList.OrderBy(x => x.Value).ToList();

    // Via KEY start from winners (Get the name and use it to add to the sorted array via looking at the CLONE ARRAY NOT THE LIST)
    foreach (var player in winnersList)
    {
        string playerKey = player.Key;
        foreach (var playerOG in playerStatsDic)
        {
            if (playerKey == playerOG.Key)
            {
                // sorted.Add(new Dictionary<string, PlayerStats>(playerOG.Key,playerOG.Value));
                sorted.Add(player.Key, playerStatsDic[playerKey]);
                break;
            }
        }
    }
    // Go over CLONE array and check if buyIn and buyOut are bigger than 0 and are EQUAL, add to sorted if so as well.
    foreach (var player in playerStatsDic)
    {
        string playerKey = player.Key;

        if (player.Value.BuyIn != 0 && player.Value.BuyOut != 0 && player.Value.BuyIn == player.Value.BuyOut) sorted.Add(player.Key, playerStatsDic[playerKey]);
    }
    // Do the same with losers
    foreach (var player in losersList)
    {
        string playerKey = player.Key;
        foreach (var playerOG in playerStatsDic)
        {
            if (playerKey == playerOG.Key)
            {
                // sorted.Add(new Dictionary<string, PlayerStats>(playerOG.Key,playerOG.Value));
                sorted.Add(player.Key, playerStatsDic[playerKey]);
                break;
            }
        }
    }

    // Return that sorted dictionary
    return sorted;
}

static void PrintCashOnTable(Dictionary<string, PlayerStats> playerStatsDic, int printCashIndex, string longestLoserName)
{
    string spaces = "";
    for (int i = 0; i < printCashIndex; i++)
    {
        spaces += " ";
    }
    string loserSpaces = "";

    for (int i = 0; i < longestLoserName.Length - 5; i++) // 5 == 'Total'
    {
        loserSpaces += " ";
    }

    double sumCashOnTable = 0;
    foreach (var player in playerStatsDic)
    {
        sumCashOnTable += player.Value.BuyIn;
    }
    sumCashOnTable = Math.Round(sumCashOnTable, 2);
    Magenta();
    Console.Write($"{spaces}Total ");
    Green();
    Console.Write($"cash"); // Add {loserSpaces} before 'cash' to align under the → in the Who owes Who
    Magenta();
    Console.Write(" on table");
    Magenta();
    Console.Write(": ");
    Green();
    Console.Write($"{sumCashOnTable}");
    DarkGreen();
    Console.Write("₪");
    ResetColor();
}


#region Sentence Printing

/// Logic - every 3 indexes belong to a category of a certain max profit (0-20, 20-50, 50-100, 100-200, 200-300.... 1000-1500, 1500-2000, 2000+)
/// Each time the max profit of a session is >= the category max amount, the category ups by 3 because every category has 3 sentences
/// When a category is set, a random number between 0-2 is chosen in order to decide what sentence of the 3 is going to be displayed.
static void PrintSentence(int charCount, string biggestWinner, double biggestWinnerProfit) //, bool withColor (If you want to do it with random colors)
{
    // Round biggestWinnerProfit, so you will get a whole value
    biggestWinnerProfit = Math.Round(biggestWinnerProfit, 2);
    string[] endSentences =
    {
        // 0 - 20
        $"Hmmm? You don't deserve a sentence {biggestWinner}.",
        $"{biggestWinnerProfit}₪? Really that's it? What do you want me to tell ya {biggestWinner}?",
        $"So so {biggestWinnerProfit}₪ is all you made {biggestWinner}? And you're the 'biggest' winner? Maybe you should go to WPT then?",
        // 20 - 50
        $"{biggestWinner} You made {biggestWinnerProfit}₪ Profit?! WOW! You can get a.... McRoyal?",
        $"Congrats {biggestWinner} bro {biggestWinnerProfit}₪ Profit!! I hope you have a job... Cuz poker won't cut it for you bro.",
        $"So so so {biggestWinnerProfit}₪ Profit {biggestWinner}? And you're the best player in the table? What stakes are these? 0.01/0.02? Zzzzz...",
        // 50 - 100
        $"Nice {biggestWinner} {biggestWinnerProfit}₪ profit.. You covered your next poker munch!",
        $"Hey {biggestWinner} did you know that if you win {biggestWinnerProfit}₪ every day you're still going to be gay?",
        $"Well, well, look who managed to earn something, It's {biggestWinner}.. Your profit (if you can call it that) is a mere {biggestWinnerProfit}₪..",
        // 100 - 200
        $"Nice work {biggestWinner} you made {biggestWinnerProfit}₪ that's over a 99.99₪ wow!",
        $"Nice winning {biggestWinner} bro {biggestWinnerProfit}₪ in total, it's almost enough to fuel your car on the way home!",
        $"{biggestWinnerProfit}₪?? You can buy a aroohat boker zoogit {biggestWinner}! Maybe take Giora for breakfast..",
        // 200 - 300
        $"{biggestWinnerProfit}₪ aa... You've almost earned enough to buy a shitty poker kit {biggestWinner}",
        $"It's worth it to play poker {biggestWinner} if you get to fill your entire fuel tank on the way home with {biggestWinnerProfit}₪ :)",
        $"Nice yomit {biggestWinner} bro, with a total winnings of {biggestWinnerProfit}₪!",
        // 300 - 400
        $"Look at you {biggestWinner}, raking in {biggestWinnerProfit}₪! Try and keep something for the rest...",
        $"{biggestWinner}, {biggestWinnerProfit}₪ in the black? Now you're making enough to seriously consider getting that yacht. \n Well, a toy one...",
        $"{biggestWinner} nice! You’re rolling in {biggestWinnerProfit}₪! Maybe now you can start tipping the dealer you asshole.",
        // 400 - 500
        $"Behold, the challenger of decent gains is {biggestWinner}! Clearly you've won some NICE pots with {biggestWinnerProfit}₪ soon to be in your BIT!!",
        $"Good job! Look who managed to scoop a pot (or 2)! It's {biggestWinner}!! With {biggestWinnerProfit}₪ in your chip stack, that means you played decently (better than Hadar for sure)! ",
        $"Wow nice haul {biggestWinner}, you're finally earning more than you lose with a total winnings of {biggestWinnerProfit}₪!!",
        // 500 - 600
        $"WP {biggestWinner} you dominated the table with {biggestWinnerProfit}₪ in winnings, too bad it's only in poker..",
        $"If you can keep wining {biggestWinnerProfit}₪ daily {biggestWinner} you can quit your job!",
        $"{biggestWinnerProfit}₪! Now that's called a win {biggestWinner}! Go and treat yourself with some new game! You deserve it lil niga.",
        // 600 - 700
        $"If you'll keep winning {biggestWinnerProfit}₪ like that you'll stop getting invites to play {biggestWinner}...",
        $"{biggestWinner}, with {biggestWinnerProfit}₪ in winnings, you’re one step closer to becoming a LEGEND... \n \n \n In your own mind.",
        $"{biggestWinner} made {biggestWinnerProfit}₪? That's some serious cash! Enough to make you think you can keep that consistent you lucky mf >:(",
        // 700 - 800
        $"{biggestWinner} you might think {biggestWinnerProfit}₪ is a nice win, However... \n \n \n Fuck you.",
        $"With {biggestWinnerProfit}₪ in winnings {biggestWinner}, you can finally afford that 'lavish' vacation... to Petah Tikva :/",
        $"{biggestWinner} made {biggestWinnerProfit}₪? That's some serious cash! Enough to make you think you can keep that consistent you lucky mf >:(",
        // 800 - 900
        $"Impressive {biggestWinner}, {biggestWinnerProfit}₪ is no joke did you consider going to the RunnerRunner? You could maybe get a Switch!",
        $"Nice! {biggestWinner} you played really well {biggestWinnerProfit}₪ is no joke, ++Respect.!!!",
        $"Bow down before GREAT WINNER {biggestWinner}!!!! Your humongous winnings of {biggestWinnerProfit}₪ clearly show that you're one of the BEST in the ~world~ (or at least Hadera)..!!",
        // 900 - 1000
        $"Behold, the king of the table is {biggestWinner}!!!! Your unimaginable winnings of {biggestWinnerProfit}₪ mark a MASSIVE win that you don't see everyday, for you are UNSTOPPABLE!",
        $"Very impressive {biggestWinner}!!! Your monumental winnings of {biggestWinnerProfit}₪ means you're a LEGEND in the making! Marvel at your own greatness!!",
        $"{biggestWinnerProfit}₪ is just shy of a 1000₪ {biggestWinner}, unlucky. \n Get good bro.",
        // 1000 - 1500
        $"{biggestWinner} did you mark the cards niga? How tf you make {biggestWinnerProfit}₪?!",
        $"All hail the table GOD, the CONQUEROR of our chips {biggestWinner}!!!! With {biggestWinnerProfit}₪ in your hands, you've won with more bad beats then I've gotten in my childhood!!!!",
        $"{biggestWinnerProfit}₪, {biggestWinner}?! I hope you're investing that before you lose it in the next poker session!!",
        // 1500 - 2000
        $"Oh oh {biggestWinner} made {biggestWinnerProfit}₪? On G? Stakes getting outta hand or the rest are fish.",
        $"Oh wow, how the poker gods smile upon {biggestWinner} as they rake in MASSIVE pots!!! With {biggestWinnerProfit}₪ in your possession, you've reached a level of poker mastery that few can match! Your success is LEGENDARY!!",
        $"Bro... {biggestWinner} is a cheater on G... With an unfathomable victory of {biggestWinnerProfit}₪!!!!! You have ROBBED and VIOLATED EVERYONE on the table! Well fucking played.",
        // 2000+
        $"Guys {biggestWinner} just won {biggestWinnerProfit}₪ I think we need to lower the stakes..",
        $"{biggestWinner} just triple that amount - {biggestWinnerProfit}₪ and you will have a nice CPC kit!",
        $"On ma mama {biggestWinner} bro is luckier than Trump... How tf you make {biggestWinnerProfit}₪ in .5/.5?!"
    };

    // Get index of the sentence
    Random rnd = new Random();

    int SentenceNum = 0;
    int valueChecker = 20;
    for (int i = 0; i < 14; i++) // 15 - number of category in sentences
    {
        if (biggestWinnerProfit >= valueChecker) SentenceNum += 3; // Up the category
        else break;
        if (i == 0) valueChecker += 30;
        else if (i == 1) valueChecker += 50;
        else if (i > 1 && i < 12) valueChecker += 100;
        else valueChecker += 500;
    }

    SentenceNum += rnd.Next(0, 3);


    string sentence = endSentences[SentenceNum].Trim();
    string[] strArr = sentence.Split(' ');
    int counter = charCount;

    // Make this boolean to avoid printing the same name with color (specific example: X₪?? You can buy a aroohat boker zoogit **Giora! Maybe take ***Giora for breakfast..)
    bool printNameOneTime = false;

    Blue();

    strArr = RemoveBlankSpaces(strArr);

    // if (withColor == false)
    // {
    for (int i = 0; i < strArr.Length; i++)
    {
        if (strArr[i] == "\n")
        {
            Console.WriteLine();
            counter = charCount;
        }
        else if (strArr[i].Length - counter == 0)
        {
            if (strArr[i].Contains(biggestWinner) && printNameOneTime == false)
            {
                PrintSpecificStringWithColors(strArr[i], biggestWinner, 1, 3);
                printNameOneTime = true;
            }
            else if (strArr[i].Contains(Convert.ToString(biggestWinnerProfit))) PrintSpecificStringWithColors(strArr[i], Convert.ToString(biggestWinnerProfit), 1, 5);
            else Console.Write(strArr[i]);
            counter -= strArr[i].Length;
            counter = charCount;
            Console.WriteLine();
        }
        else if (strArr[i].Length < counter)
        {
            if (strArr[i].Contains(biggestWinner) && printNameOneTime == false)
            {
                PrintSpecificStringWithColors(strArr[i], biggestWinner, 1, 3);
                printNameOneTime = true;
            }
            else if (strArr[i].Contains(Convert.ToString(biggestWinnerProfit))) PrintSpecificStringWithColors(strArr[i], Convert.ToString(biggestWinnerProfit), 1, 5);
            else Console.Write(strArr[i]);
            Console.Write(" ");
            counter -= strArr[i].Length + 1; // +1 for space
        }
        else
        {
            Console.WriteLine();
            counter = charCount; // Reset counter for new line
            i--;
        }
    }
    // }
    // else 
    // {
    //     int arrCounter = 0;
    //     int[] arrDoNotRepeatColor = new int[str.Length / 44 + 1]; // Divides the string into chars and counts the amount of possible sentences that will be made and colors that will be used.
    //     arrDoNotRepeatColor[arrCounter++] = GetRandomColor(arrDoNotRepeatColor);
    //     for (int i = 0; i < strArr.Length; i++)
    //     {
    //         if (strArr[i] == biggestWinner) Yellow();
    //         else if (strArr[i] == Convert.ToString(biggestWinnerProfit)) Green();

    //         if (strArr[i].Length - counter == 0)
    //         {
    //             Console.Write(strArr[i]);
    //             if (strArr[i] == biggestWinner || strArr[i] == Convert.ToString(biggestWinnerProfit)) ResetColor(); // To avoid resetting the color each time
    //             counter -= strArr[i].Length;
    //             counter = charCount;
    //             Console.WriteLine();
    //         }
    //         else if (strArr[i].Length < counter)
    //         {
    //             Console.Write(strArr[i]);
    //             Console.Write("X");
    //             if (strArr[i] == biggestWinner || strArr[i] == Convert.ToString(biggestWinnerProfit)) ResetColor(); // To avoid resetting the color each time
    //             counter -= strArr[i].Length+1; // +1 for space
    //         }
    //         else 
    //         {
    //             Console.WriteLine();
    //             counter = charCount; // Reset counter for new line
    //             i--;
    //         }
    //     }
    ResetColor();
}

static void PrintSpecificStringWithColors(string str, string whatToPrint, int regColor, int specialColor)
{
    int counter = 0;
    foreach (char c in str)
    {
        if (c == whatToPrint[counter] || c == '₪')
        {
            GetColorVoid(specialColor); // Needs to be 3 (Yellow) for name, and 5 (Green) for amount
            Console.Write(c);
            if (counter < whatToPrint.Length - 1) counter++;
            GetColorVoid(regColor);     // Needs to be 1 (Blue)
        }
        else Console.Write(c);
    }
    GetColorVoid(regColor); // Return to default printing color
}

static string[] RemoveBlankSpaces(string[] arr)
{
    string[] temp = new string[0];
    int tempCounter = 0;
    for (int i = 0; i < arr.Length; i++)
    {
        if (arr[i] != "")
        {
            temp = AddOneToArray(temp);
            temp[tempCounter++] = arr[i];
        }
    }

    return temp;
}
// T Makes the function a General function allowing it to accept types of any kind so it can be more dynamic
static T[] AddOneToArray<T>(T[] arr)
{
    T[] temp = new T[arr.Length + 1];

    for (int i = 0; i < arr.Length; i++)
    {
        temp[i] = arr[i];
    }

    return temp;
}

static int GetRandomColor(int[] arrDoNotPick)
{
    Random rnd = new Random();
    int rndNum = 0;
    int tries = 0;
    bool exitLoop = false;
    while (exitLoop == false)
    {
        rndNum = rnd.Next(1, 13);
        bool badNumFound = false;
        foreach (int num in arrDoNotPick)
        {
            if (num == rndNum)
            {
                tries++;
                badNumFound = true;
                break;
            }
        }
        if (badNumFound == false)
        {
            exitLoop = true;
        }
        else if (tries > 134232) // Try 13 times (not necessarily will try all of the colors but good enough)
        {
            exitLoop = true;
            rndNum = -1;
        }
    }
    GetColorVoid(rndNum);
    return rndNum;
}

static void GetColorVoid(int rndNum)
{
    Random rnd = new Random();
    // Blue, DarkBlue, Yellow, DarkYellow, Green, DarkGreen, Cyan, DarkCyan, Red, DarkRed, Magenta, DarkMagenta
    switch (rndNum)
    {
        case 1:
            Blue();
            break;
        case 2:
            DarkBlue();
            break;
        case 3:
            Yellow();
            break;
        case 4:
            DarkYellow();
            break;
        case 5:
            Green();
            break;
        case 6:
            DarkGreen();
            break;
        case 7:
            Cyan();
            break;
        case 8:
            DarkCyan();
            break;
        case 9:
            Red();
            break;
        case 10:
            DarkRed();
            break;
        case 11:
            Magenta();
            break;
        case 12:
            DarkMagenta();
            break;
        default:
            rndNum = rnd.Next(1, 4);
            switch (rndNum)
            {
                case 1:
                    Green();
                    break;
                case 2:
                    Yellow();
                    break;
                case 3:
                    Red();
                    break;
            }
            break;
    }
}


#endregion


