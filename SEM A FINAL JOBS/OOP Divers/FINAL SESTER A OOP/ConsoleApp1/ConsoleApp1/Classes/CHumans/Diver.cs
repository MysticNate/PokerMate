class Diver : Person, IDiveable, IBorrow
{
    // Properties \\
    int divesDone;
    bool currentlyDiving; // If diver is actively diving right now 
    Dictionary<Item, int> items;
    DiveRankGiven rankCurrent;
    DiveRankGiven[] rankHistory;

    // Constructor \\
    // A person can't be more than MAX_AGE (140) years old.
    public Diver(string id, string fName = "undefined", string lName = "undefined", int day = 1, int month = 1, int year = 1900, int divesDone = -1, bool currentlyDiving = false, Dictionary<Item, int> items = null, DiveRankGiven rankCurrent = null, DiveRankGiven[] rankHistory = null) : base(id, fName, lName, day, month, year)
    {
        SetDivesDone(divesDone);
        SetCurrentlyDiving(currentlyDiving);
        SetItems(items);
        SetRankCurrent(rankCurrent);
        SetRankHistory(rankHistory);
    }

    // Setters \\ 
    public void SetDivesDone(int divesDone)
    {
        this.divesDone = divesDone;
    }
    public void SetCurrentlyDiving(bool currentlyDiving)
    {
        this.currentlyDiving = currentlyDiving;
    }
    public void SetItems(Dictionary<Item, int> items)
    {
        if (items != null)
        {
            this.items = items;
        }
        else
        {
            this.items = new Dictionary<Item, int>();
        }
    }
    // This function will update the rank history as well as the current rank
    public void SetRankCurrent(DiveRankGiven rankCurrent)
    {
        // Save the history of the diver's ranks
        if (rankHistory == null)
        {
            this.rankHistory = new DiveRankGiven[0];
        }
        rankHistory = Helper.AddOneToArray(rankHistory);
        rankHistory[rankHistory.Length - 1] = rankCurrent;
        // Update current rank
        this.rankCurrent = rankCurrent;
    }
    // For if you want to change 'rankHistory' completely
    public void SetRankHistory(DiveRankGiven[] rankHistory)
    {
        if (rankHistory != null)
        {
            this.rankHistory = rankHistory;
        }
        else
        {
            this.rankHistory = new DiveRankGiven[0];
        }
    }

    // Getters \\
    public int GetDivesDone()
    {
        return divesDone;
    }
    public bool GetCurrentlyDiving()
    {
        return currentlyDiving;
    }
    public Dictionary<Item, int> GetItems()
    {
        return items;
    }
    public DiveRankGiven GetRankCurrent()
    {
        return rankCurrent;
    }
    public DiveRankGiven[] GetRankHistory()
    {
        return rankHistory;
    }


    // Other \\
    public override string ToString()
    {
        return base.ToString() + $"\nI've done {divesDone} dives, I'm currently diving: {currentlyDiving} My current rank is {rankCurrent.GetDiveRank().GetName()}";
    }
    public void Dive()
    {
        currentlyDiving = true;
        divesDone++;
    }
    public void GetOutFromWater()
    {
        currentlyDiving = false;
    }

    // Items \\
    public void BorrowItem(Diver diver, Item item, DivingClub divingClub)
    {
        // Check if the item is available
        if (divingClub.GetItems().ContainsKey(item))
        {
            // Check if the item is available
            if (divingClub.GetItems()[item] > 0)
            {
                // Check if the diver has the item
                if (diver.GetItems().ContainsKey(item))
                {
                    diver.GetItems()[item]++;
                }
                else
                {
                    diver.GetItems().Add(item, 1);
                }
                divingClub.GetItems()[item]--;
            }
            else
            {
                Console.WriteLine("The item is not available at the moment.");
            }
        }
        else
        {
            Console.WriteLine("The item is not available at the moment.");
        }
    }
    public void ReturnItem(Diver diver, Item item, DivingClub divingClub)
    {
        // Check if the diver has the item
        if (diver.GetItems().ContainsKey(item))
        {
            // Check if the diver has the item
            if (diver.GetItems()[item] > 0)
            {
                // Check if the item is available
                if (divingClub.GetItems().ContainsKey(item))
                {
                    divingClub.GetItems()[item]++;
                }
                else
                {
                    divingClub.GetItems().Add(item, 1);
                }
                diver.GetItems()[item]--;
            }
            else
            {
                Console.WriteLine("The diver doesn't have the item.");
            }
        }
        else
        {
            Console.WriteLine("The diver doesn't have the item.");
        }

    }


}