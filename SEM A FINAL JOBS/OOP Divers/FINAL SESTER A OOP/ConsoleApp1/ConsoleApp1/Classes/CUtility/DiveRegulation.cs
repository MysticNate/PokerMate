class DiveRegulation
{
    // Properties \\
    static int counter = 0;
    string name;
    bool atLeastTwoDivers;
    // Other regulations if any country wants to add stuff
    bool[] regulations;
    // If all regulations are passed (fast check)
    bool passed;

    // Constructor \\
    public DiveRegulation(bool atLeastTwoDivers = false, bool[] regulations = null)
    {
        counter++;
        SetName("Policy" + counter);
        SetAtLeastTwoDivers(atLeastTwoDivers);
        SetRegulations(regulations);
        SetPassed();
    }

    // Setters \\
    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetAtLeastTwoDivers(bool atLeastTwoDivers)
    {
        this.atLeastTwoDivers = atLeastTwoDivers;
    }
    public void SetRegulations(bool[] regulations)
    {
        this.regulations = regulations;
    }
    private void SetPassed()
    {
        passed = true;
        if (atLeastTwoDivers == false)
        {
            passed = false;
            return;
        }
        foreach (bool condition in regulations)
        {
            if (condition == false)
            {
                passed = false;
                return;
            }
        }
    }

    // Getters \\
    public string GetName()
    {
        return name;
    }
    public bool GetAtLeastTwoDivers()
    {
        return atLeastTwoDivers;
    }
    public bool[] GetRegulations()
    {
        return regulations;
    }
    public bool GetPassed()
    {
        return passed;
    }

    // Override ToString method
    public override string ToString()
    {
        return $"Dive Regulation: {name}, Passed: {passed}";
    }
}