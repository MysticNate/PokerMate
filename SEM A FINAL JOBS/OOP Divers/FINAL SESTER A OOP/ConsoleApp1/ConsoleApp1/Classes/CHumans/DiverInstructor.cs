class DiverInstructor : Diver
{
    // Properties \\
    // This will be a dictionary of all the clubs the instructor is working at, If the end date is null, the instructor is still working there, else, he worked there before.
    Dictionary<DivingClub, WorkStamp> workingClubs = new Dictionary<DivingClub, WorkStamp>();
    // new Dictionary<DivingClub, WorkStamp>();

    // Constructor \\
    public DiverInstructor(string id, string fName = "undefined", string lName = "undefined", int day = 1, int month = 1, int year = 1900, int divesDone = -1, bool currentlyDiving = false, Dictionary<Item, int> items = null, DiveRankGiven rankCurrent = null, DiveRankGiven[] rankHistory = null, Dictionary<DivingClub, WorkStamp> workingClubs = null) : base(id, fName, lName, day, month, year, divesDone, currentlyDiving, items, rankCurrent, rankHistory)
    {
        SetWorkingClubs(workingClubs);
    }

    // Setters \\
    public void SetWorkingClubs(Dictionary<DivingClub, WorkStamp> workingClubs)
    {
        if (workingClubs != null)
        {
            this.workingClubs = workingClubs;
        }
        else
        {
            this.workingClubs = new Dictionary<DivingClub, WorkStamp>();
        }
    }

    // Misc \\ 
    public void AddWorkingClub(DivingClub club, WorkStamp workStamp)
    {
        workingClubs.Add(club, workStamp);
    }

    // Getters \\
    public DivingClub[] GetCurrentWorkingClubs()
    {
        DivingClub[] currentWorkingClubs = new DivingClub[0];
        foreach (var club in workingClubs)
        {
            if (club.Value.endDate != default) // default = 1/1/0001
            {
                currentWorkingClubs = Helper.AddOneToArray(currentWorkingClubs);
                currentWorkingClubs[currentWorkingClubs.Length - 1] = club.Key;
            }
        }
        return currentWorkingClubs;
    }
    public DivingClub[] GetAllClubsWorked()
    {
        return workingClubs.Keys.ToArray();
    }
    public Dictionary<DivingClub, WorkStamp> GetClubWorkingDictionary()
    {
        return workingClubs;
    }

    // Other \\
    public override string ToString()
    {
        string clubsToString = "\n";
        DivingClub[] clubsWorkedOrWorking = GetAllClubsWorked();
        foreach (var c in clubsWorkedOrWorking)
        {
            clubsToString += c;
            clubsToString += "\n";
        }
        return base.ToString() + $"\nI've worked/working at {clubsToString}";
    }

}