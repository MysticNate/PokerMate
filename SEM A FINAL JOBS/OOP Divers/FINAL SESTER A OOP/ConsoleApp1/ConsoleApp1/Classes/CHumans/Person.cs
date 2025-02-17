class Person : Human, ISigner
{
    // Constructor \\
    // A person can't be more than MAX_AGE (140) years old.
    public Person(string id, string fName = "undefined", string lName = "undefined", int day = 1, int month = 1, int year = 1900)
        : base(id, fName, lName, day, month, year)
    {
        DB.AddSavedPerson(this);
    }

    // Setters \\ 

    public virtual Signature Sign()
    {
        Signature signature = null;
        string name = fName + " " + lName;
        // Ask if wants to sign
        Printer.PrintAskForSignature(name);
        if (Helper.GetYesOrNo(Console.ReadLine()))
            signature = new Signature(this);
        return signature;
    }
}