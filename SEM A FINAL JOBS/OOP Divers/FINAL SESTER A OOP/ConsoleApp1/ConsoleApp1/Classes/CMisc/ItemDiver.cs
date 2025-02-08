class ItemDiver
{

    // Properties \\
    Item item;
    string description;
    int amount;

    // Constructor \\
    public ItemDiver(Item item = null, string description = "undefined", int amount = 0)
    {
        SetItem(item);
        SetDescription(description);
        SetAmount(amount);
    }

    // Setters \\ 
    public void SetItem(Item item)
    {
        this.item = item;
    }
    public void SetDescription(string description)
    {
        this.description = description;
    }
    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    // Getters \\
    public Item GetItem()
    {
        return item;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetAmount()
    {
        return amount;
    }
}