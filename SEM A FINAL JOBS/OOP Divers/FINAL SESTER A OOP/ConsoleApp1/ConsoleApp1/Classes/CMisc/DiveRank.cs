class DiveRank
{
    // Properties \\
    string id;
    string name;
    string description;
    int divesNeeded;

    // Constructor \\
    public DiveRank(string id = "0", string name = "undefined", string description = "undefined", int divesNeeded = -1)
    {
        SetId(id);
        SetName(name);
        SetDescription(description);
        SetDivesNeeded(divesNeeded);
    }

    // Setters \\ 
    public void SetId(string id)
    {
        this.id = id;
    }
    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetDescription(string description)
    {
        this.description = description;
    }
    public void SetDivesNeeded(int divesNeeded)
    {
        this.divesNeeded = divesNeeded;
    }

    // Getters \\
    public string GetId()
    {
        return id;
    }
    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetDivesNeeded()
    {
        return divesNeeded;
    }    

}