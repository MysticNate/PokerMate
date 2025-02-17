// EType of divers equipment (10 general items)
enum EType
{
    Unknown = 0,
    DiveSystemComputer,
    Mask,
    Snorkel,
    Fins,
    Wetsuit,
    Compass,
    DiveLight,
    DiveKnife,
    Camera
}
class Item
{
    // Properties \\
    string id;
    string name;
    string description;
    EType type;

    // Constructor \\
    public Item(string id = "0", string name = "undefined", string description = "undefined", int type = 0)
    {
        SetId(id);
        SetName(name);
        SetDescription(description);
        SetType(type);
        DB.AddSavedItem(this);
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
    public void SetType(int type)
    {
        while (type < 0 || type > 9) // Error handling
        {
            Printer.PrintAskForNewType(type, name);
            type = Validator.GetProperInt(Console.ReadLine());
        }
        this.type = (EType)type;
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
    public EType GetItemType()
    {
        return type;
    }

    // Other \\
    public override string ToString()
    {
        return $"Item: {GetName()}";
    }
}