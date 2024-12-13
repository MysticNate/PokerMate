class GeneralFunctions
{
    static Random rnd = new Random();

    // T Makes the function a General function allowing it to accept types of any kind so it can be more dynamic
    public static T[] AddOneToArray<T>(T[] arr)
    {
        T[] temp = new T[arr.Length+1];
        
        for (int i = 0; i < arr.Length; i++)
        {
            temp[i] = arr[i];
        }

        return temp;
    }

    public static int GetRandomNumber(int startRange, int endRange)
    {
        return rnd.Next(startRange, endRange+1);
    }

    public static double CheckErrorMargin(double sentErrorMargin)
    {
        if (sentErrorMargin < 0 || sentErrorMargin > 99.99)
        {
            Console.WriteLine("Enter new Error Margin (0-99.99)");
            sentErrorMargin = GetProperDouble(Console.ReadLine());
            CheckErrorMargin(sentErrorMargin);
        }
        return sentErrorMargin;
    }

    public static double GetProperDouble(string input)
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
            catch (Exception c)
            {   Console.WriteLine(c.Message);
                Console.Write("\nEnter a new double value:");
                input = Console.ReadLine(); // Prompt the user to enter a valid input again
            }
        }
        return goodInput;
    }

    public static void ClearConsoleLines(int left, int top, int replitCharCount = 44)
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

}