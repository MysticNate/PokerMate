






/// <summary>
/// This class will help print stuff that are being repeated / long so that we will have a cleaner code across our system
/// </summary>
class Printer
{
    // Console.Clear()
    // A function that will be used to clear the console
    public static void ClearConsoleLines(int left, int top, int charCount = 50)
    {
        Console.SetCursorPosition(Console.CursorLeft - left, Console.CursorTop - top);
        string clearSpace = "";
        // Set the empty row to be the exact count as the max amount of chars that fit in one line
        for (int i = 0; i < charCount; i++)
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

    // Errors for number validators \\
    public static void PrintErrorDouble(string input)
    {
        Color.Red();
        Console.Write($"\nError:");
        Color.Magenta();
        Console.Write($"\nInput ");
        Color.Blue();
        Console.Write("is ");
        Color.Red();
        Console.Write("invalid ");
        Color.Yellow();
        Console.Write("(");
        Color.DarkGray();
        Console.Write("'");
        Color.DarkRed();
        Console.Write($"{input}");
        Color.DarkGray();
        Console.Write("'");
        Color.Yellow();
        Console.Write(")");
        Color.Red();
        Console.Write("!");
        Color.Blue();
        Console.Write("\nNew ");
        Color.Magenta();
        Console.Write($"double value");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }
    public static void PrintErrorInt(string input)
    {
        Color.Red();
        Console.Write($"\nError:");
        Color.Magenta();
        Console.Write($"\nInput ");
        Color.Blue();
        Console.Write("is ");
        Color.Red();
        Console.Write("invalid ");
        Color.Yellow();
        Console.Write("(");
        Color.DarkGray();
        Console.Write("'");
        Color.DarkRed();
        Console.Write($"{input}");
        Color.DarkGray();
        Console.Write("'");
        Color.Yellow();
        Console.Write(")");
        Color.Red();
        Console.Write("!");
        Color.Blue();
        Console.Write("\nNew ");
        Color.Magenta();
        Console.Write($"integer value");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }

    // Errors for name validator \\ 
    public static void PrintEnglishNameError(string badName, char c)
    {
        Color.Red();
        Console.Write("Error\n");
        Color.Blue();
        Console.Write("Name ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badName}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("contains invalid char ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{c}");
        Color.DarkGray();
        Console.Write("'\n");
        Color.Red();
        Console.Write("Name must be English letters and or numbers\n");
        Color.Blue();
        Console.Write("Enter a new name for ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badName}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }
    public static void PrintLengthNameError(string badName)
    {
        Color.Red();
        Console.Write("Error:\n");
        Color.Blue();
        Console.Write("Name ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badName}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("is invalid\n");
        Color.Red();
        Console.Write("Name must be at least ");
        Color.Magenta();
        Console.Write("2 ");
        Color.Red();
        Console.Write("chars long!\n");
        Color.Blue();
        Console.Write("Enter a new name for ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badName}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }

    // Regex Error Printer \\ 
    public static void PrintRegExError(string errorString, string typeOfProperty, string regexPattern)
    {
        Color.Red();
        Console.Write("RegEx Error:\n");
        Color.Blue();
        Console.Write("String ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{errorString}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("is invalid by RegEx Check\n");
        Color.Red();
        Console.Write("Ensure you follow RegEx rules: ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{regexPattern}");
        Color.DarkGray();
        Console.Write("'\n");
        Color.Blue();
        Console.Write("Enter a new string for ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{typeOfProperty}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }

    // Error for Person Class \\
    // ID Error invalid
    public static void PrintAskForNewID(string badId)
    {
        Color.Red();
        Console.Write("Error:\n");
        Color.Blue();
        Console.Write("ID ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badId}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("is invalid\n");
        Color.Red();
        Console.Write("ID must be a ");
        Color.Magenta();
        Console.Write("valid Israeli ");
        Color.Red();
        Console.Write("ID!\n");
        Color.Blue();
        Console.Write("Enter a new ID for ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badId}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }
    // ID Error exists
    public static void PrintAskForNewIDExists(string badId)
    {
        Color.Red();
        Console.Write("Error:\n");
        Color.Blue();
        Console.Write("ID ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badId}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("is invalid\n");
        Color.Red();
        Console.Write("ID already ");
        Color.Magenta();
        Console.Write("in use");
        Color.Red();
        Console.Write("!\n");
        Color.Blue();
        Console.Write("Enter a new ID for ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badId}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }

    // ID Error exists
    public static void PrintAskForNewLicense(string badLicense)
    {
        Color.Red();
        Console.Write("Error:\n");
        Color.Blue();
        Console.Write("License ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badLicense}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("is invalid\n");
        Color.Red();
        Console.Write("License is in use / invalid!\n");
        Color.Blue();
        Console.Write("Enter a new license for ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{badLicense}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }

    // Date Error 
    // Depending on the boolean sent, error will be printed as follows
    public static void PrintErrorDate(string number, string reason, bool year = false, bool month = false)
    {
        // "Year is invalid, Cannot be more than 140 years old, Cannot be born in the future");
        // Console.WriteLine("Please enter a new year: ");
        string date = "";
        if (year == true) date = "Year";
        else if (month == true) date = "Month";
        else date = "Day";

        Color.Red();
        Console.Write("Error:\n");
        Color.Blue();
        Console.Write($"{date} ");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{number}");
        Color.DarkGray();
        Console.Write("' ");
        Color.Blue();
        Console.Write("is invalid\n");
        Color.Red();
        Console.Write($"{reason} \n");
        Color.Blue();
        Console.Write($"Enter a new {date} ");
        Color.Blue();
        Console.Write(": ");
        Color.ResetColor();
    }

    // Error for Item Class \\
    public static void PrintAskForNewType(int badType, string itemName)
    {
        Color.Red();
        Console.WriteLine($"Error:\nNo such item type '({badType})' in Enum EType\nCan be Min:'0' Max:'9'\n");
        Color.ResetColor();
        Console.Write($"Enter a type (0 - 9) for {itemName}: ");
    }

    internal static void PrintAskForNewCountry(string name)
    {
        Color.Red();
        Console.WriteLine($"Error:\nNo such country / country code '({name})'\nSee 'www.ipqualityscore.com/documentation/countries/overview' for reference\n");
        Color.ResetColor();
        Console.Write($"Enter a new country / country code: ");
    }

    // // AskForASignature \\ 
    // public static void 

    // YesOrNoErrorPrint \\ 
    public static void PrintLengthErrorYesOrNo(string str, int minLength, int maxLength)
    {
        // Print error
        Color.Red();
        Console.Write("\nError:\n");
        Color.Red();
        Console.Write("Length of the answer must be ");
        Color.Magenta();
        Console.Write(minLength == maxLength ? $"{maxLength} " : $"{minLength}-{maxLength} ");
        Color.Red();
        Console.Write("chars\n");
        Console.Write("Your answer is ");
        Color.Magenta();
        Console.Write($"{str.Length} ");
        Color.Red();
        Console.Write("chars\n");
        Color.DarkGray();
        Console.Write("'");
        Color.Magenta();
        Console.Write($"{str}");
        Color.DarkGray();
        Console.Write("'");
        Color.Blue();
        Console.Write("\nEnter a new yes or no: ");
        Color.ResetColor();
    }

    public static void PrintAskForSignature(string signer)
    {
        Color.Magenta();
        Console.WriteLine($"{signer}, Please approve digital signature!\n");
        Color.Blue();
        Console.Write("Do you approve: ");
        Color.ResetColor();
    }

    internal static void Print01Error()
    {
        Color.Red();
        Console.Write("\nError:\n");
        Color.Magenta();
        Console.WriteLine($"Value must be between 0 and 1.\nEnter a new value: ");
        Color.ResetColor();
    }

    public static void PrintPressAnyToContinue()
    {
        Color.DarkGray();
        Console.Write("\n╔════════════════════════════════╗\n");
        Console.Write("║ ");
        Color.DarkRed();
        Console.Write("Press any key to ");
        Color.Magenta();
        Console.Write("continue...  ");
        Color.DarkGray();
        Console.Write(" ║\n");
        Console.Write("╚════════════════════════════════╝\n");
        Color.ResetColor();
        // TODO: ENABLE ME
        // Console.ReadKey();
    }
    public static void PrintAreYouSure()
    {
        Color.Magenta();
        Console.WriteLine("Are you sure you want to ");
        Color.Red();
        Console.WriteLine("exit?");
        Color.ResetColor();
    }

    public static void PrintGoodByeMessage()
    {
        Console.Write("\n╔═════════════════════════════════════════════╗\n");
        Console.Write("║ ");
        Color.Blue();
        Console.Write("Hopefully you enjoyed our program! ");
        Color.DarkRed();
        Console.Write("GoodBye!");
        Color.ResetColor();
        Console.Write(" ║\n");
        Console.Write("╚═════════════════════════════════════════════╝\n");
    }

    private static void PrintNavMenu()
    {
        Color.Yellow();
        Console.Write("Please navigate to desired section ");
        Color.Yellow();
        Console.Write("(");
        Color.Red();
        Console.Write("press num");
        Color.Yellow();
        Console.Write(")");
    }


    // Menu Printing \\
    public static void PrintMainMenu()
    {
        Color.Magenta();
        Console.Write("Welcome to the 'Dive Master Program'!\n-------------------------------------\n"); ;

        PrintNavMenu();

        Color.Green();
        Console.Write("\n\n1. ");
        Color.Blue();
        Console.Write("Register"); ;
        Color.Green();
        Console.Write("\n\n2. ");
        Color.Blue();
        Console.Write("LogIn");

        Color.Red();
        Console.Write("\n\n0. ");
        PrintMenuExit();
    }

    public static void PrintMainMenuLogged(string fName, string lName)
    {
        Color.Magenta();
        Console.Write($"Good to see you back! {fName} {lName}!\nWhat would you like to do today? {DateTimeOffset.Now:dd/MM/yyyy}\n"); ;

        PrintNavMenu();

        Color.Green();
        Console.Write("\n\n1. ");
        Color.Blue();
        Console.Write("Search a club"); ;
        Color.Green();
        Console.Write("\n\n2. ");
        Color.Blue();
        Console.Write("Start a new dive");
        Color.Green();
        Console.Write("\n\n3. ");
        Color.Blue();
        Console.Write("Update profile");
        Color.Green();
        Console.Write("\n\n4. ");
        Color.Blue();
        Console.Write("Add a club");


        Color.Red();
        Console.Write("\n\n0. ");
        Color.DarkRed();
        Console.Write("Log out!");
    }

    public static void PrintMainMenuChangeProfile()
    {
        Color.Magenta();
        Console.Write($"What would you like to update?\n"); ;

        PrintNavMenu();

        Color.Green();
        Console.Write("\n\n1. ");
        Color.Blue();
        Console.Write("Password"); ;
        Color.Green();
        Console.Write("\n\n2. ");
        Color.Blue();
        Console.Write("First Name");
        Color.Green();
        Console.Write("\n\n3. ");
        Color.Blue();
        Console.Write("Last Name");
        Color.Green();
        Console.Write("\n\n4. ");
        Color.Blue();
        Console.Write("Diving Rank");

    }
       

    private static void PrintMenuExit()
    {
        Color.DarkRed();
        Console.Write("Exit\n");
        Color.Red();
        Console.Write("Exits the program..");
        Color.ResetColor();
    }

    public static void LoadBar()
    {
        int dotCounter = 0;
        string whatIsHappeningRow = "";
        string dots = "";
        string underLine = "";
        string middleRow = "|";
        for (int i = 0; i < 49; i++)
        {
            switch (dotCounter)
            {
                case 0:
                    dots = "";
                    underLine = "-=-=-=-=-=-=-=";
                    dotCounter++;
                    break;
                case 1:
                    dots = ".";
                    underLine = "-=-=-=-=-=-=-=-";
                    dotCounter++;
                    break;
                case 2:
                    dots = "..";
                    underLine = "-=-=-=-=-=-=-=-=";
                    dotCounter++;
                    break;
                case 3:
                    dots = "...";
                    underLine = "-=-=-=-=-=-=-=-=-";
                    dotCounter = 0;
                    break;
            }

            // Add text to whatIsHappeningRow depending on 'i' (iteration) 

            if (i >= 0 && i <= 10)
            {
                whatIsHappeningRow = "Booting Program.cs";
            }
            else if (i > 10 && i <= 20)
            {
                whatIsHappeningRow = "Loading Divers folder Classes";
            }
            else if (i > 20 && i <= 30)
            {
                whatIsHappeningRow = "Loading Locations folder Classes";
            }
            else if (i > 30 && i <= 40)
            {
                whatIsHappeningRow = "Loading Interfaces";
            }
            else whatIsHappeningRow = "Finishing up";

            Console.Write($"Booting System{dots}\n{underLine}\n\n");


            // Add the loading box
            Console.Write("|-------------------------------------------------|\n");
            middleRow += "="; // Add a space bar to represent loading
            if (i == 48) middleRow += "|"; // Add ending only when loading is complete
            Console.WriteLine(middleRow);
            Console.WriteLine("|-------------------------------------------------|\n");
            Console.WriteLine(i == 48 ? "Setup completed!\n\n                   100% complete" : $"{whatIsHappeningRow + dots}\n\n                    {i * 2 + 1}% complete"); // While it is not on the last iteration (48) show 'i*2+1' else show '100'. 
            Random rnd = new Random();
            int waitTimeRnd = rnd.Next(0, 300);
            Thread.Sleep(waitTimeRnd); // Wait a random amount of time in milliseconds to make it look even more real
                                       // Wait 0.05 seconds and clear console while it is not on the last iteration (48)
            if (i != 48)
            {
                Thread.Sleep(50);
                Console.Clear();
            }
        }
        Color.Magenta();
        PrintPressAnyToContinue();
        Color.ResetColor();
    }

    internal static void PrintCantStartDive(string custom)
    {
        Color.Red();
        Console.WriteLine($"Error\nCannot start a dive!\n{custom}\n");
        Color.ResetColor();
    }

}