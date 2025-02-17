abstract class Human
{
    // Properties \\
    protected string id;
    protected string fName;
    protected string lName;
    protected DateOnly dateOfBirth;
    protected static List<string> idsInUse = new List<string>();
    protected const int MAX_AGE = 140;

    // Constructor \\
    public Human(string id, string fName = "undefined", string lName = "undefined", int day = 1, int month = 1, int year = 1900)
    {
        SetBirthDay(day, month, year);
        SetId(id);
        SetFName(fName);
        SetLName(lName);
    }

    // Setters \\
    public void SetId(string id)
    {
        while (Validator.IsIDLegal(id) == false)
        {
            Printer.PrintAskForNewID(id);
            id = Console.ReadLine();
            id = id.Trim();
        }
        while (idsInUse.Contains(id))
        {
            Printer.PrintAskForNewIDExists(id);
            // Rerun the function to check validity again
            SetId(Console.ReadLine());
            return;
        }

        // Add id to the in-use list so we can check if it's already in use
        idsInUse.Add(id);
        this.id = id;
    }
    public void SetFName(string fName)
    {
        fName = Validator.GetProperEnglishName(fName);
        fName = Helper.TrimAndCapitalize(fName);
        this.fName = fName;
    }
    public void SetLName(string lName)
    {
        lName = Validator.GetProperEnglishName(lName);
        lName = Helper.TrimAndCapitalize(lName);
        this.lName = lName;
    }
    public void SetBirthDay(int day, int month, int year)
    {
        year = ValidateYear(year);             // Will check if year is valid (not 140 years back & not in the future)
        month = ValidateMonth(year, month);     // Will check if we are past this month or not (after validating the year given)
        day = ValidateDay(year, month, day);  // Will check if the date exists in that specific year & month && that we did not pass this day

        dateOfBirth = new DateOnly(year, month, day);
    }

    // Getters \\
    public string GetId()
    {
        return id;
    }
    public string GetFName()
    {
        return fName;
    }
    public string GetLName()
    {
        return lName;
    }
    public DateOnly GetDateOfBirth()
    {
        return dateOfBirth;
    }

    // Specific Class Helper Methods \\
    private int ValidateYear(int year)
    {
        int currentYear = DateTime.Now.Year;

        if (year < currentYear - MAX_AGE)
        {
            Printer.PrintErrorDate(year.ToString(), reason: "Cannot be more than 140 years old", year: true);
            return ValidateYear(Validator.GetProperInt(Console.ReadLine()));
        }

        if (year > currentYear)
        {
            Printer.PrintErrorDate(year.ToString(), reason: "Cannot be born in the future", year: true);
            return ValidateYear(Validator.GetProperInt(Console.ReadLine()));
        }

        return year;
    }

    private int ValidateMonth(int year, int month)
    {
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;

        if (month < 1 || month > 12)
        {
            Printer.PrintErrorDate(month.ToString(), reason: "No such month", month: true);
            return ValidateMonth(year, Validator.GetProperInt(Console.ReadLine()));
        }

        if (year == currentYear && month > currentMonth)
        {
            Printer.PrintErrorDate(month.ToString(), reason: "Cannot be born in the future", month: true);
            return ValidateMonth(year, Validator.GetProperInt(Console.ReadLine()));
        }

        return month;
    }

    private int ValidateDay(int year, int month, int day)
    {
        int currentDay = DateTime.Now.Day;
        int currentMonth = DateTime.Now.Month;
        int currentYear = DateTime.Now.Year;

        if (day < 1 || day > DateTime.DaysInMonth(year, month))
        {
            Printer.PrintErrorDate(day.ToString(), reason: "No such day in that date");
            return ValidateDay(year, month, Validator.GetProperInt(Console.ReadLine()));
        }

        if (year == currentYear && month == currentMonth && day > currentDay)
        {
            Printer.PrintErrorDate(day.ToString(), reason: "Cannot be born in the future");
            return ValidateDay(year, month, Validator.GetProperInt(Console.ReadLine()));
        }

        return day;
    }

    // Other \\
    public override bool Equals(object obj)
    {
        if (obj is Human other)
        {
            return id == other.GetId();
        }
        return false;
    }
}