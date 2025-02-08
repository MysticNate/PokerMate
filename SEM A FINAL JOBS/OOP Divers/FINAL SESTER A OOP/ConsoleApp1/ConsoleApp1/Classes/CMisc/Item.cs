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
    EType type;

    // Constructor \\
    public Item(string id = "0", string name = "undefined", int type = 0)
    {
        SetId(id);
        SetName(name);
        SetType(type);
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
    public EType GetItemType()
    {
        return type;
    }
}