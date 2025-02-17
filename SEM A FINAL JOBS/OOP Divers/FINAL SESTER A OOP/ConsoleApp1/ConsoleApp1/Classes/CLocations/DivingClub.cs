
using System.Text.RegularExpressions;
class DivingClub
{
    // Properties \\ 
    static List<string> licensesMade = new List<string>();
    string license;
    string name;
    Person contactPerson;
    Address address;
    string phoneNumber;
    string email;
    DivingSite divingSite;
    bool hasWebsite;
    string siteLink;
    Dictionary<Item, int> items = new Dictionary<Item, int>(); // Array of items that can be borrowed (initialized in the properties)
    DivingInfo[] divingLogs; // Array of the diving logs of the club

    // Constructor \\
    public DivingClub(string license = "-1", string name = "undefined", Person contactPerson = null, Address address = null, string phoneNumber = "undefined", string email = "undefined", DivingSite divingSite = null, string siteLink = "undefined", Dictionary<Item, int> items = null, DivingInfo[] divingLogs = null)
    {
        SetLicense(license);
        SetName(name);
        SetContactPerson(contactPerson);
        SetAddress(address);
        SetPhoneNumber(phoneNumber);
        SetEmail(email);
        SetSiteLink(siteLink);
        SetItems(items);
        SetDivingLogs(divingLogs);
        SetDivingSite(divingSite);

        // Check if site exists
        SetHasWebsite();
        DB.AddSavedDivingClub(this);
    }

    // Setters \\
    
    public void SetLicense(string license)
    {
        if (licensesMade.Contains(license) || !Regex.IsMatch(license, @"^[a-zA-Z0-9]{2,}$"))
        {
            // Print Error and ask for new license
            Printer.PrintAskForNewLicense(license);
            // Rerun the function to check validity again
            SetLicense(Console.ReadLine());
        }
        licensesMade.Add(license);
        this.license = license;
    }
    public void SetName(string name)
    {
        name = Validator.GetProperEnglishName(name);
        name = Helper.TrimAndCapitalize(name);
        this.name = name;
    }
    public void SetContactPerson(Person contactPerson)
    {
        this.contactPerson = contactPerson;
    }
    public void SetAddress(Address address)
    {
        this.address = address;
    }
    public void SetPhoneNumber(string phoneNumber)
    {
        this.phoneNumber = phoneNumber;
    }
    public void SetEmail(string email)
    {
        email = Validator.GetProperEmail(email);
        this.email = email;
    }
    public void SetDivingSite(DivingSite divingSite)
    {
        this.divingSite = divingSite;
    }
    public void SetHasWebsite()
    {
        if (siteLink != "undefined")
        {
            hasWebsite = true;
            return;
        }
        hasWebsite = false;
    }
    public void SetSiteLink(string siteLink)
    {
        siteLink = Validator.GetProperWWW(siteLink);
        this.siteLink = siteLink;
    }
    public void SetItems(Dictionary<Item, int> items)
    {
        this.items = items;
    }
    public void SetDivingLogs(DivingInfo[] divingLogs)
    {
        if (divingLogs == null) divingLogs = new DivingInfo[0];
        this.divingLogs = divingLogs;
    }

    // Getters \\
    public string GetLicense()
    {
        return license;
    }
    public string GetName()
    {
        return name;
    }
    public Person GetContactPerson()
    {
        return contactPerson;
    }
    public Address GetAddress()
    {
        return address;
    }
    public string GetPhoneNumber()
    {
        return phoneNumber;
    }
    public string GetEmail()
    {
        return email;
    }
    public DivingSite GetDivingSite()
    {
        return divingSite;
    }
    public bool GetHasWebsite()
    {
        return hasWebsite;
    }
    public string GetSiteLink()
    {
        return siteLink;
    }
    public Dictionary<Item, int> GetItems()
    {
        return items;
    }
    public DivingInfo[] GetDivingLogs()
    {
        return divingLogs;
    }

    // Other \\
    public override string ToString()
    {
        return $"License: {license}, Name: {name}, Contact Person: {contactPerson.GetFName()} {contactPerson.GetLName()}, Address: {address.GetStreetName()}, {address.GetCityName()}, {address.GetCountry().GetName()}, Phone: {phoneNumber}, Email: {email}, Diving Site: {divingSite.GetName()}, Website: {siteLink}, Diving Logs: {divingLogs.Length}";
    }



}