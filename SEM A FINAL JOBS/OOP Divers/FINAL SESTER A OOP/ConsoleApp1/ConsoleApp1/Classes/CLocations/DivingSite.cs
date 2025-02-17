class DivingSite
{
    // Properties \\
    string name;
    string description;
    double length;
    double width;
    double depth;
    bool waterSalty;
    Address address;

    // Constructor \\
    public DivingSite(string name = "undefined", string description = "undefined", double length = -1, double width = -1, double depth = -1, bool waterSalty = false, Address address = null)
    {
        SetName(name);
        SetDescription(description);
        SetLength(length);
        SetWidth(width);
        SetDepth(depth);
        SetWaterSalty(waterSalty);
        SetAddress(address);
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
    public void SetAddress(Address address)
    {
        this.address = address;
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
    public Address GetAddress()
    {
        return address;
    }

    // Override ToString method
    public override string ToString()
    {
        return $"Name: {name}, Description: {description}, Length: {length}m, Width: {width}m, Depth: {depth}m, Salty: {waterSalty}, Address: {address}";
    }
}