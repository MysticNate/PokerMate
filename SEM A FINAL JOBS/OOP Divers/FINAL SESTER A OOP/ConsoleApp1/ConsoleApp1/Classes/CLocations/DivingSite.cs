class DivingSite : Address
{
    // Properties \\
    string name;
    string description;
    double length;
    double width;
    double depth;
    bool waterSalty;

    // Constructor \\
    public DivingSite(string streetName = "undefined", string houseNumber = "undefined", string cityName = "undefined", string stateOrRegion = "undefined", string zipCode = "undefined", Country country = null, string name = "undefined", string description = "undefined", double length = -1, double width = -1, double depth = -1, bool waterSalty = false) : base(streetName, houseNumber, cityName, stateOrRegion, zipCode, country)
    {
        SetName(name);
        SetDescription(description);
        SetLength(length);
        SetWidth(width);
        SetDepth(depth);
        SetWaterSalty(waterSalty);
    }

    // Setters \\
    public void SetName(string name)
    {
        this.name = name;
    }
    public void SetDescription(string description)
    {
        this.description = description;
    }
    public void SetLength(double length)
    {
        this.length = length;
    }
    public void SetWidth(double width)
    {
        this.width = width;
    }
    public void SetDepth(double depth)
    {
        this.depth = depth;
    }
    public void SetWaterSalty(bool waterSalty)
    {
        this.waterSalty = waterSalty;
    }

    // Getters \\
    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }
    public double GetLength()
    {
        return length;
    }
    public double GetWidth()
    {
        return width;
    }
    public double GetDepth()
    {
        return depth;
    }
    public bool GetWaterSalty()
    {
        return waterSalty;
    }

}