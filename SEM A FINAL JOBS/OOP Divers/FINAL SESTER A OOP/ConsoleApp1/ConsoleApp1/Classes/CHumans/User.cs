class User
{
    // Properties \\ 
    Diver diver;
    string email;
    string password;

    // Constructor \\
    public User(Diver diver, string email, string password)
    {
        this.diver = diver;
        SetEmail(email);
        SetUserPass(password);
        DB.AddSavedUser(this);
    }

    // Setters \\
    public void SetUserDiver(Diver diver)
    {
        this.diver = diver;
    }
    public void SetEmail(string email)
    {
        email = Validator.GetProperEmail(email);
        this.email = email;
    }
    public void SetUserPass(string password)
    {
        // Will follow the story guide = (REGEX: 8 digits exactly [a-z0-9] ONLY!)
        password = Validator.GetProperPassword(password);
        this.password = password;
    }

    // Getters \\ 
    public Diver GetUserDiver()
    {
        return diver;
    }
    public string GetEmail()
    {
        return email;
    }
    public string GetUserPass()
    {
        return password;
    }
}