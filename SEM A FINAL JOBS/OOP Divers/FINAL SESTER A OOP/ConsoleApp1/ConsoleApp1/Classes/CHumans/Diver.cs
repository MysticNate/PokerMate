class Diver : Person
{
    // Properties \\
    string diverId;
    int divesDone;
    DiveRankGiven rankCurrent;
    DiveRankGiven[] rankHistory;

    // Constructor \\
    // A person can't be more than MAX_AGE (140) years old.
    public Diver(string id = "0", string fName = "undefined", string lName = "undefined", DateOnly dateOfBirth) : base()
    {

    }

    // Setters \\ 
    public void SetDiverId(string id)
    {
        while (Validator.IsIDLegal(id) == false)
        {
            Printer.PrintAskForNewID(id);
            id = Console.ReadLine(); ;
        }
        this.id = id;
    }
    public void SetDivesDone(int divesDone)
    {
        this.divesDone = divesDone;
    }
    public void SetLName(string lName)
    {
        lName = Validator.GetProperEnglishName(lName);
        fName = Helper.TrimAndCapitalize(fName);
        this.lName = lName;
    }
    public void SetBirthDay(int day, int month, int year)
    {
        ValidateYear(year);             // Will check if year is valid (not 140 years back & not in the future)
        ValidateMonth(year, month);     // WIll check if we are past this month or not (after validating the year given)
        ValidateDay(year, month, day);  // Will check if the date exists in that specific year & month && that we did not pass this day
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
}