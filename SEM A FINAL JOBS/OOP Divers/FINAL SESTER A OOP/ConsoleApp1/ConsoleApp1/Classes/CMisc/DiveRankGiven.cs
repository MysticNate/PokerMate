class DiveRankGiven
{
    // Properties \\
    DiveRank diveRank;
    DivingClub clubReceivedFrom;
    DateTime givenTD;
    string certificate; // Link to the certificate

    // Constructor \\
    // DateTime givenTD = default == 01/01/0001 00:00:00
    public DiveRankGiven(DiveRank diveRank = null, DivingClub clubReceivedFrom = null, DateTime givenTD = default, string certificate = "undefined")
    {
        SetDiveRank(diveRank);
        SetGivenTD(givenTD);
        SetClubReceivedFrom(clubReceivedFrom);
        SetCertificate(certificate);
    }

    // Setters \\ 
    public void SetDiveRank(DiveRank diveRank)
    {
        this.diveRank = diveRank;
    }
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
    public DiveRank GetDiveRank()
    {
        return diveRank;
    }
    public DateTime GetGivenTD()
    {
        return givenTD;
    }
    public DivingClub GetClubReceivedFrom()
    {
        return clubReceivedFrom;
    }
    public string GetCertificate()
    {
        return certificate;
    }

}