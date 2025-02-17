/// <summary>
/// This class will help us validate variable / methods across our system
/// </summary>
using System.Text.RegularExpressions;



class Validator
{
    // Person Validators \\
    // Method to check if ID (Israeli) is legal or not.
    // Source = https://www.upnext.co.il/articles/israeli-id-numer-validation/
    public static bool IsIDLegal(string id)
    {
        if (id.Length < 1 || id.Length > 9) return false;
        else if (id.Length < 9)
        {
            int needsZeros = 9 - id.Length;
            string addedZeros = "";
            for (int i = 0; i < needsZeros; i++)
            {
                addedZeros += "0";
            }
            addedZeros += id; // Add the zeros (must be 9 to the ID of the object)
            int validID = int.Parse(addedZeros);
            int[] validIDNums = new int[9];
            int counter = 0; // To iterate between 1 and 2.
            while (validID != 0)
            {
                validIDNums[counter] = validID % 10;
                validID /= 10;
                counter++;
            }
            counter = 0;
            int[] idAfterMult = (int[])validIDNums.Clone();
            foreach (int c in validIDNums)
            {
                if (counter % 2 == 0)
                {
                    idAfterMult[counter] = c * 1;
                }
                else
                {
                    idAfterMult[counter] = c * 2;
                }
                counter++;
            }
            counter = 0;
            foreach (int num in idAfterMult)
            {
                if (num > 9)
                {
                    int temp = num;
                    int newNum = 0;
                    while (temp != 0)
                    {
                        newNum += temp % 10;
                        temp /= 10;
                    }
                    idAfterMult[counter] = newNum;
                }
                counter++;
            }
            int sumOfID = 0; // To check ID
            foreach (int num in idAfterMult)
            {
                sumOfID += num;
            }
            if (sumOfID % 10 == 0) return true;
            return false;
        }
        else
        {
            int counter = 0; // Making an int array with the ID inside of it
            int[] idAfterMult = new int[9];
            foreach (char c in id)
            {
                string s = Convert.ToString(c);
                int num = Convert.ToInt32(s);
                idAfterMult[counter] = num;
                counter++;
            }
            counter = 0;
            foreach (int c in idAfterMult) // Multiplying each digit by 1,2,1...
            {
                if (counter % 2 == 0)
                {
                    idAfterMult[counter] = c * 1;
                }
                else
                {
                    idAfterMult[counter] = c * 2;
                }
                counter++;
            }
            counter = 0;
            foreach (int num in idAfterMult) // Changing the numbers bigger than 10
            {
                if (num > 9)
                {
                    int temp = num;
                    int newNum = 0;
                    while (temp != 0)
                    {
                        newNum += temp % 10;
                        temp /= 10;
                    }
                    idAfterMult[counter] = newNum;
                }
                counter++;
            }
            int sumOfID = 0; // To check ID
            foreach (int num in idAfterMult)
            {
                sumOfID += num;
            }
            if (sumOfID % 10 == 0) return true;
            return false;
        }
    }

    // Valid Number Getters \\
    public static int GetProperInt(string input)
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
                Printer.PrintErrorInt(input);
                input = Console.ReadLine(); // Prompt the user to enter a valid input again
                Printer.ClearConsoleLines(0, 4);
            }
            catch (OverflowException)
            {
                Printer.PrintErrorInt(input);
                input = Console.ReadLine(); // Prompt the user to enter a valid input again
                Printer.ClearConsoleLines(0, 4);
            }
            catch (Exception c)
            {
                Color.Red();
                Console.Write("An unexpected error occurred:\n");
                Color.ResetColor();
                Console.WriteLine($"{c.Message}");
                throw; // Re-throw the exception if it's something unexpected
            }
        }
        return goodInput;
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
            catch (FormatException)
            {
                Printer.PrintErrorDouble(input);
                input = Console.ReadLine(); // Prompt the user to enter a valid input again
                Printer.ClearConsoleLines(0, 4);
            }
            catch (OverflowException)
            {
                Printer.PrintErrorDouble(input);
                input = Console.ReadLine(); // Prompt the user to enter a valid input again
                Printer.ClearConsoleLines(0, 4);
            }
            catch (Exception c)
            {
                Color.Red();
                Console.Write("An unexpected error occurred:\n");
                Color.ResetColor();
                Console.WriteLine($"{c.Message}");
                throw; // Re-throw the exception if it's something unexpected
            }
        }
        return goodInput;
    }
    public static double GetValid01Double(string input)
    {
        while (true)
        {
            double value = GetProperDouble(input);
            if (value >= 0 && value <= 1)
            {
                return value;
            }
            Printer.Print01Error();
            input = Console.ReadLine();
        }
    }

    // Valid Name Getter \\
    public static string GetProperEnglishName(string str)
    {
        bool errorFirstTime = true;
        Regex nameRegex = new Regex("^[a-z ]{2,}$"); // Only lowercase letters, at least 2 characters

        while (!nameRegex.IsMatch(str.ToLower().Trim()))
        {
            str = str.ToLower().Trim();

            if (errorFirstTime == false)
                Printer.ClearConsoleLines(0, 4);
            errorFirstTime = false;

            if (str.Length < 2)
                Printer.PrintLengthNameError(str);
            else
            {
                char invalidChar = str.FirstOrDefault(c => !char.IsLetter(c));
                Printer.PrintEnglishNameError(str, invalidChar);
            }

            str = Console.ReadLine();
        }

        return str;
    }

    // Valid Phone Number Getter \\
    public static string GetProperEmail(string str)
    {
        bool errorFirstTime = true;
        Regex emailRegex = new Regex(@"^[\d\w\-\.]+@[aA-zZ]{2,15}(\.[aA-zZ]+|\.[aA-zZ]+\.[aA-zZ]+)$"); // Email regex from Secured Development 

        while (!emailRegex.IsMatch(str.ToLower().Trim()))
        {
            str = str.ToLower().Trim();

            if (errorFirstTime == false)
                Printer.ClearConsoleLines(0, 4);
            errorFirstTime = false;

            Printer.PrintRegExError(str, "email", emailRegex.ToString());


            str = Console.ReadLine();
        }

        return str;
    }

    public static string GetProperPassword(string str)
    {
        bool errorFirstTime = true;
        Regex passwordRegex = new Regex(@"^[a-z0-9]{8}$"); // Email regex from Secured Development 

        while (!passwordRegex.IsMatch(str.ToLower().Trim()))
        {
            str = str.ToLower().Trim();

            if (errorFirstTime == false)
                Printer.ClearConsoleLines(0, 4);
            errorFirstTime = false;

            Printer.PrintRegExError(str, "password", passwordRegex.ToString());


            str = Console.ReadLine();
        }

        return str;
    }

    // Valid Website Getter \\
    public static string GetProperWWW(string str)
    {
        bool errorFirstTime = true;
        Regex wwwRegex = new Regex(@"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$"); // Website regex from https://stackoverflow.com/questions/42618872/regex-for-website-or-url-validation 

        while (!wwwRegex.IsMatch(str.ToLower().Trim()))
        {
            str = str.ToLower().Trim();

            if (errorFirstTime == false)
                Printer.ClearConsoleLines(0, 4);
            errorFirstTime = false;

            Printer.PrintRegExError(str, "website", wwwRegex.ToString());


            str = Console.ReadLine();
        }

        return str;
    }


    // // Valid Country Name / Symbol Check \\
    // // Using an external API to check if the country exists
    // public async static Task<bool> CheckIfCountryExists(string country)
    // {
    //     using (HttpClient client = new HttpClient())
    //     {
    //         Uri endPoint = new Uri("https://www.ipqualityscore.com/api/raw/country/list");
    //         HttpResponseMessage response = await client.GetAsync(endPoint);
    //         string result = response.Content.ReadAsStringAsync().Result;
    //         result = result.Replace("\n", " ");
    //         string[] listCount = result.Split(':');

    //         foreach (var item in listCount)
    //         {
    //             if (item.Trim().ToLower() == country.ToLower())
    //             {
    //                 return true;
    //             }
    //         }
    //         return false;
    //    }
    // }

    public static string ValidateYesOrNo(string str, int minLength, int maxLength)
    {
        str = Helper.TrimAndCapitalize(str);

        // Print error
        Printer.PrintLengthErrorYesOrNo(str, minLength, maxLength);

        string newStr = Console.ReadLine();

        newStr = Helper.TrimAndCapitalize(newStr);

        if (newStr.Length >= minLength && newStr.Length <= maxLength) return newStr;
        else
        {
            Printer.ClearConsoleLines(0, 6);
            return ValidateYesOrNo(newStr, minLength, maxLength);
        }
    }


}
