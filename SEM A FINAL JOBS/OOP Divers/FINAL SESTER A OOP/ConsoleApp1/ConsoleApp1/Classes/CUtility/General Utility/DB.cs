/// <summary>
/// This will act as the DB of the system
/// </summary>
class DB
{
    static Person[] savedPerson = new Person[0];
    static Country[] savedCountry = new Country[0];
    static Address[] savedAddress = new Address[0];
    static DiveRank[] savedDiveRank = new DiveRank[0];
    static DivingClub[] savedDivingClub = new DivingClub[0];
    static Item[] savedItem = new Item[0];
    static User[] savedUser = new User[0];

    // Getters \\
    public static Person[] GetSavedPerson()
    {
        return savedPerson;
    }
    public static Country[] GetSavedCountry()
    {
        return savedCountry;
    }
    public static Address[] GetSavedAddress()
    {
        return savedAddress;
    }
    public static DiveRank[] GetSavedDiveRank()
    {
        return savedDiveRank;
    }
    public static DivingClub[] GetSavedDivingClub()
    {
        return savedDivingClub;
    }
    public static Item[] GetSavedItem()
    {
        return savedItem;
    }
    public static User[] GetSavedUser()
    {
        return savedUser;
    }

    // Adders \\
    public static void AddSavedPerson(Person person)
    {
        if (person == null) return;
        savedPerson = Helper.AddOneToArray(savedPerson);
        savedPerson[savedPerson.Length - 1] = person;
    }
    public static void AddSavedCountry(Country country)
    {
        if (country == null) return;
        savedCountry = Helper.AddOneToArray(savedCountry);
        savedCountry[savedCountry.Length - 1] = country;
    }
    public static void AddSavedAddress(Address address)
    {
        if (address == null) return;
        savedAddress = Helper.AddOneToArray(savedAddress);
        savedAddress[savedAddress.Length - 1] = address;
    }
    public static void AddSavedDiveRank(DiveRank diveRank)
    {
        if (diveRank == null) return;
        savedDiveRank = Helper.AddOneToArray(savedDiveRank);
        savedDiveRank[savedDiveRank.Length - 1] = diveRank;
    }
    public static void AddSavedDivingClub(DivingClub divingClub)
    {
        if (divingClub == null) return;
        savedDivingClub = Helper.AddOneToArray(savedDivingClub);
        savedDivingClub[savedDivingClub.Length - 1] = divingClub;
    }
    public static void AddSavedItem(Item item)
    {
        if (item == null) return;
        savedItem = Helper.AddOneToArray(savedItem);
        savedItem[savedItem.Length - 1] = item;
    }
    public static void AddSavedUser(User user)
    {
        if (user == null) return;
        savedUser = Helper.AddOneToArray(savedUser);
        savedUser[savedUser.Length - 1] = user;
    }
}