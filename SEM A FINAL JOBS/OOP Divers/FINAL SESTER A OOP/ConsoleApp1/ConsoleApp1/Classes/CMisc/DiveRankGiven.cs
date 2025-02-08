class DiveRankGiven
{
    // Properties \\
    DivingClub clubReceivedFrom;
    DateTime givenTD;
    string certificate; // Link to the certificate

    // Constructor \\
    public DiveRankGiven(DivingClub clubReceivedFrom = null, DateTime givenTD = default, string certificate = "undefined")
    {
        SetGivenTD(givenTD);
        SetClubReceivedFrom(clubReceivedFrom);
        SetCertificate(certificate);
    }

    // Setters \\ 
    public void SetGivenTD(DateTime givenTD)
    {
        this.givenTD = givenTD;
    }
    public void SetClubReceivedFrom(DivingClub clubReceivedFrom)
    {
        this.clubReceivedFrom = clubReceivedFrom;
    }
    public void SetCertificate(string certificate)
    {
        this.certificate = certificate;
    }

    // Getters \\
    public DateTime GetGivenTD()
    {
        return givenTD;
    }
    public DivingClub GetName()
    {
        return clubReceivedFrom;
    }
    public string GetCertificate()
    {
        return certificate;
    }

}