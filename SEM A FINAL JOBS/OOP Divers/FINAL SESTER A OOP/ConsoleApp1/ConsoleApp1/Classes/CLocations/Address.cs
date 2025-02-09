class Address
{
    // Properties \\ 
    string streetName;
    string houseNumber;
    string cityName;
    string stateOrRegion;
    string zipCode;
    Country country;

    public Address(string streetName = "undefined", string houseNumber = "undefined", string cityName = "undefined", string stateOrRegion = "undefined", string zipCode = "undefined", Country country = null)
    {
        SetStreetName(streetName);
        SetHouseNumber(houseNumber);
        SetCityName(cityName);
        SetStateOrRegion(stateOrRegion);
        SetZipCode(zipCode);
        SetCountry(country);
    }
    // Setters \\
    public void SetStreetName(string streetName)
    {
        this.streetName = streetName;
    }
    public void SetHouseNumber(string houseNumber)
    {
        this.houseNumber = houseNumber;
    }
    public void SetCityName(string cityName)
    {
        this.cityName = cityName;
    }
    public void SetStateOrRegion(string stateOrRegion)
    {
        this.stateOrRegion = stateOrRegion;
    }
    public void SetZipCode(string zipCode)
    {
        this.zipCode = zipCode;
    }
    public void SetCountry(Country country)
    {
        this.country = country;
    }
    // Getters \\
    public string GetStreetName()
    {
        return streetName;
    }
    public string GetHouseNumber()
    {
        return houseNumber;
    }
    public string GetCityName()
    {
        return cityName;
    }
    public string GetStateOrRegion()
    {
        return stateOrRegion;
    }
    public string GetZipCode()
    {
        return zipCode;
    }
    public Country GetCountry()
    {
        return country;
    }

}