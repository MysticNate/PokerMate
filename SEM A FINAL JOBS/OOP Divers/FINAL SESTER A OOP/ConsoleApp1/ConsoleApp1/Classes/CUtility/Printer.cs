


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

    // Error for Person Class \\
    // ID Error
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

}