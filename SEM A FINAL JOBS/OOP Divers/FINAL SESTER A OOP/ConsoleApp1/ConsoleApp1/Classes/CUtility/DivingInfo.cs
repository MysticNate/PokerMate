class DivingInfo
{
    // Properties \\
    // Universal ID for each dice regardless of club the dive was made in / diving site, etc...
    static int idCounter = 0;
    int diveID;
    User diveInfoFiller;
    DateOnly diveDate;
    DivingClub divingClub;
    DivingSite divingSite;
    DiveRegulation diveRegulation;
    DiverInstructor headDiver;
    // Instructor divers and Regular divers
    Diver[] divers;
    Dictionary<Item, int> itemsTaken;
    TimeOnly waterEnter;
    TimeOnly waterExit;
    double waterTemp;
    double tideLevel;
    Signature[] signatureDivers;
    Signature signatureClub;

    // Constructor \\



    // Constructor \\
    public DivingInfo(User diveInfoFiller, DateOnly diveDate = default, DivingClub divingClub = null, DivingSite divingSite = null, DiveRegulation diveRegulation = null, DiverInstructor headDiver = null, Diver[] divers = null, Dictionary<Item, int> itemsTaken = null, TimeOnly waterEnter = default, TimeOnly waterExit = default, double waterTemp = -1, double tideLevel = -1, Signature[] signatureDivers = null, Signature signatureClub = null)
    {
        idCounter++;
        diveID = idCounter;
        SetDiveInfoFiller(diveInfoFiller);
        SetDiveDate(diveDate);
        SetDivingClub(divingClub);
        SetDivingSite(divingSite);
        SetDiveRegulation(diveRegulation);
        SetHeadDiver(headDiver);
        SetDivers(divers);
        SetItemsTaken(itemsTaken);
        SetWaterEnter(waterEnter);
        SetWaterExit(waterExit);
        SetWaterTemp(waterTemp);
        SetTideLevel(tideLevel);
        SetSignatureDivers(signatureDivers);
        SetSignatureClub(signatureClub);
    }

    // Setters \\
    public void SetDiveInfoFiller(User diveInfoFiller)
    {
        this.diveInfoFiller = diveInfoFiller;
    }
    public void SetDiveDate(DateOnly diveDate)
    {
        this.diveDate = diveDate;
    }
    public void SetDivingClub(DivingClub divingClub)
    {
        this.divingClub = divingClub;
    }
    public void SetDivingSite(DivingSite divingSite)
    {
        this.divingSite = divingSite;
    }
    public void SetDiveRegulation(DiveRegulation diveRegulation)
    {
        this.diveRegulation = diveRegulation;
    }
    public void SetHeadDiver(DiverInstructor headDiver)
    {
        this.headDiver = headDiver;
    }
    public void SetDivers(Diver[] divers)
    {
        this.divers = divers;
    }
    public void SetItemsTaken(Dictionary<Item, int> itemsTaken)
    {
        this.itemsTaken = itemsTaken;
    }
    public void SetWaterEnter(TimeOnly waterEnter)
    {
        this.waterEnter = waterEnter;
    }
    public void SetWaterExit(TimeOnly waterExit)
    {
        this.waterExit = waterExit;
    }
    public void SetWaterTemp(double waterTemp)
    {
        Validator.GetValid01Double(waterTemp.ToString());
        this.waterTemp = waterTemp;
    }
    public void SetTideLevel(double tideLevel)
    {
        Validator.GetValid01Double(tideLevel.ToString());
        this.tideLevel = tideLevel;
    }
    public void SetSignatureDivers(Signature[] signatureDivers)
    {
        this.signatureDivers = signatureDivers;
    }
    public void SetSignatureClub(Signature signatureClub)
    {
        this.signatureClub = signatureClub;
    }
    // Getters \\
    public User GetDiveInfoFiller()
    {
        return diveInfoFiller;
    }
    public DateOnly GetDiveDate()
    {
        return diveDate;
    }
    public DivingClub GetDivingClub()
    {
        return divingClub;
    }
    public DivingSite GetDivingSite()
    {
        return divingSite;
    }
    public DiveRegulation GetDiveRegulation()
    {
        return diveRegulation;
    }
    public DiverInstructor GetHeadDiver()
    {
        return headDiver;
    }
    public Diver[] GetDivers()
    {
        return divers;
    }
    public Dictionary<Item, int> GetItemsTaken()
    {
        return itemsTaken;
    }
    public TimeOnly GetWaterEnter()
    {
        return waterEnter;
    }
    public TimeOnly GetWaterExit()
    {
        return waterExit;
    }
    public double GetWaterTemp()
    {
        return waterTemp;
    }
    public double GetTideLevel()
    {
        return tideLevel;
    }
    public Signature[] GetSignatureDivers()
    {
        return signatureDivers;
    }
    public Signature GetSignatureClub()
    {
        return signatureClub;
    }

    // Other \\ 
    // Followed by the story provided \\
    public bool CheckIfDiveValid()
    {
        // a. Diving regulations – ALL VALID?
        bool con1 = diveRegulation.GetPassed();
        // b. Head diver works currently?
        DivingClub[] divingClubWorkingOfInstructor = headDiver.GetCurrentWorkingClubs();
        bool con2 = divingClubWorkingOfInstructor.Contains(divingClub);
        // c. All divers signed?
        bool con3 = divers.Length == signatureDivers.Length;
        // d. Is the diver that submitted the information is a head diver?
        bool con4 = diveInfoFiller.GetUserDiver() != null && diveInfoFiller.GetUserDiver().Equals(headDiver);
        return con1 && con2 && con3 && con4;

        // If we get a false result the secretaries should check why :)
    }

}