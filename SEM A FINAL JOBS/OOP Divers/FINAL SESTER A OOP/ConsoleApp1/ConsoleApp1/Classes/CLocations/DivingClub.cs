
using System.Text.RegularExpressions;
class DivingClub
{
    // Properties \\
    static string[] licensesMade;
    string license;
    string name;
    Person contactPerson;
    Address address;
    string phoneNumber;
    string email;
    DivingSite divingSite;
    bool hasWebsite;
    string siteLink;
    DivingInfo[] divingLogs; // Array of the diving logs of the club

    // Constructor \\
    public DivingClub(string license = "-1", string name = "undefined", Person contactPerson = null, Address address = null, string phoneNumber = "undefined", string email = "undefined", DivingSite divingSite = null, bool hasWebsite = false, string siteLink = "undefined", DivingInfo[] divingLogs = null)
    {
        SetLicense(license);
        SetName(name);
        SetContactPerson(contactPerson);
        SetAddress(address);
        SetPhoneNumber(phoneNumber);
        SetEmail(email);
        SetHasWebsite(hasWebsite);
        SetSiteLink(siteLink);
        SetDivingLogs(divingLogs);
    }

    // Setters \\
    public void SetLicense(string license)
    {
        this.license = license;
    }
    public void SetName(string name)
    {
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
        this.email = email;
    }
    public void SetDivingSite(DivingSite divingSite)
    {
        this.divingSite = divingSite;
    }
    public void SetHasWebsite(bool hasWebsite)
    {
        this.hasWebsite = hasWebsite;
    }
    public void SetSiteLink(string siteLink)
    {
        this.siteLink = siteLink;
    }
    public void SetDivingLogs(DivingInfo[] divingLogs)
    {
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
    public DivingInfo[] GetDivingLogs()
    {
        return divingLogs;
    }





}