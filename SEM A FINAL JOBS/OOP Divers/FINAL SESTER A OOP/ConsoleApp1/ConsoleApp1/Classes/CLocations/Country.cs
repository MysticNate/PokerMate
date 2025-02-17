class Country
{
    // Properties \\
    string name;
    DiveRegulation divingRegulations;
    Node<string> languages;

    public Country(string name = "undefined", DiveRegulation divingRegulations = null, string[] languages = null)
    {
        SetName(name);
        SetDivingRegulations(divingRegulations);
        SetLanguages(languages);
        DB.AddSavedCountry(this);
    }

    // Setters \\ 
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetDivingRegulations(DiveRegulation divingRegulations)
    {
        this.divingRegulations = divingRegulations;
    }

    public void SetLanguages(string[] languages)
    {
        this.languages = Node<string>.MakeLLFromArr(languages);
    }

    // Getters \\
    public string GetName()
    {
        return name;
    }
    public DiveRegulation GetDivingRegulations()
    {
        return divingRegulations;
    }
    public Node<string> GetLanguages()
    {
        return languages;
    }
}