/// <summary>
/// A helper class that will have different methods that I will use across the system
/// </summary>
class Helper
{
    // Will normalize a name ("  shay " => "Shay")
    public static string TrimAndCapitalize(string str)
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
}