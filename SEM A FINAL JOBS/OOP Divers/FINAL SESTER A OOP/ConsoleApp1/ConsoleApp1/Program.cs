/// <note from Giora and Nate>
/// 1. Because this system is an international system, language will be only in English (input + output)
/// 2. For all classes users will not have access to (for example Dive Ranks will be made by us as they are pre-defined by us [no user access])
/// </note from Giora and Nate>

// IMPORTANT: We have decided that only Divers will be able to access the system.

// Pre made Object Creation \\ 
// Create DiveRank objects
DiveRank openWaterDiver = new DiveRank("1", "Open Water Diver", "Entry-level certification for recreational divers.", 4);
DiveRank advancedOpenWaterDiver = new DiveRank("2", "Advanced Open Water Diver", "Certification for divers to explore deeper waters and more challenging environments.", 5);
DiveRank rescueDiver = new DiveRank("3", "Rescue Diver", "Certification for divers to learn rescue skills and emergency management.", 8);
DiveRank diveMaster = new DiveRank("4", "Dive Master", "Professional-level certification for leading and assisting dive activities.", 60);
DiveRank instructor = new DiveRank("5", "Instructor", "Certification for teaching and certifying new divers.", 100);

// Create Country objects
Country usa = new Country("US", new DiveRegulation(true, new bool[] { true, true, false }), new string[] { "English" });
Country australia = new Country("Australia", new DiveRegulation(true, new bool[] { true, true, true }), new string[] { "English" });
Country egypt = new Country("EG", new DiveRegulation(true, new bool[] { true, false, true }), new string[] { "Arabic", "English" });

// Create Address objects
Address address1 = new Address("Ocean Drive", "3", "Miami", "Florida", "222", usa);
Address address2 = new Address("Reef Road", "12", "Cairns", "Queensland", "333", australia);
Address address3 = new Address("Coral Street", "14", "Pyramids", "South Sinai", "444", egypt);

// Create DivingClub objects
DivingClub club1 = new DivingClub("CLUB001", "Miami Dive Club", new Person("234463669", "John", "Doe", 1, 1, 1980), address1, "305-555-1234", "info@miamidiveclub.com", new DivingSite("Miami Beach", "Popular diving site in Miami.", 1000, 500, 30, true, address1), "www.miamidiveclub.com", new Dictionary<Item, int>(), new DivingInfo[0]);
DivingClub club2 = new DivingClub("CLUB002", "Great Barrier Reef Dive Club", new Person("330106857", "Jane", "Smith", 2, 2, 1985), address2, "07-555-5678", "info@gbrdiveclub.com", new DivingSite("Great Barrier Reef", "World-famous diving site in Australia.", 2000, 1000, 40, true, address2), "www.gbrdiveclub.com", new Dictionary<Item, int>(), new DivingInfo[0]);
DivingClub club3 = new DivingClub("CLUB003", "Red Sea Dive Club", new Person("230565228", "Ahmed", "Ali", 3, 3, 1990), address3, "069-555-9101", "info@redseadiveclub.com", new DivingSite("Red Sea", "Popular diving site in Egypt.", 1500, 700, 35, true, address3), "www.redseadiveclub.com", new Dictionary<Item, int>(), new DivingInfo[0]);

// Create Item objects
Item diveComputer = new Item("ITEM001", "Dive Computer", "Advanced dive computer for monitoring dive data.", 1);
Item mask = new Item("ITEM002", "Mask", "High-quality diving mask.", 2);
Item fins = new Item("ITEM003", "Fins", "Durable diving fins.", 4);
Item wetsuit = new Item("ITEM004", "Wetsuit", "Comfortable wetsuit for diving.", 5);
Item diveLight = new Item("ITEM005", "Dive Light", "Bright dive light for underwater visibility.", 7);

// Add items to clubs
club1.SetItems(new Dictionary<Item, int> { { diveComputer, 5 }, { mask, 10 }, { fins, 10 }, { wetsuit, 7 }, { diveLight, 3 } });
club2.SetItems(new Dictionary<Item, int> { { diveComputer, 8 }, { mask, 15 }, { fins, 15 }, { wetsuit, 10 }, { diveLight, 5 } });
club3.SetItems(new Dictionary<Item, int> { { diveComputer, 6 }, { mask, 12 }, { fins, 12 }, { wetsuit, 8 }, { diveLight, 4 } });


// Pre made User
DiveRankGiven yourRank = new DiveRankGiven(instructor, club1);
DiverInstructor adminDiver = new DiverInstructor("319250973", "James", "Bond", 1, 1, 1999, 123, rankCurrent: yourRank);
WorkStamp workStamp = new WorkStamp(endDate: DateOnly.MaxValue); // Just default for demonstrating purposes
adminDiver.AddWorkingClub(club1, workStamp);
User admin = new User(adminDiver, "justblood99@gmail.com", "asdfghjk");

// Pre made divers (for test dives)
Diver diver1 = new Diver("321196578", "James", "Johnson", 5, 5, 1995, 10);
Diver diver2 = new Diver("321797086", "Games", "Smith", 10, 10, 1990, 20);
Diver diver3 = new Diver("335066874", "Lames", "Brown", 15, 8, 1985, 30);
Diver diver4 = new Diver("337396691", "Chris", "Williams", 20, 12, 1980, 40);
Diver diver5 = new Diver("327252177", "Michel", "Davis", 25, 7, 1975, 50);


// A little bit of fake booting
Printer.LoadBar();

// Menu \\
bool loggedIn = false;
while (true)
{
    Console.Clear();
    Printer.PrintMainMenu();
    char key = Console.ReadKey(true).KeyChar; // Console.ReadKey(true).KeyChar; // Read key without displaying it in console 
    Console.Clear();
    if (key == '1')
    {
        // Register
        string id, fName, lName, email, password;
        int day, month, year, divesDone;
        Console.WriteLine("What is your ID?");
        id = Console.ReadLine();
        Console.WriteLine("What is your first name?");
        fName = Console.ReadLine();
        Console.WriteLine("What is your last name?");
        lName = Console.ReadLine();
        Console.WriteLine("What is your email?");
        email = Console.ReadLine();
        Console.WriteLine("What is your password?");
        password = Console.ReadLine();
        Console.WriteLine("Please enter the day you were born: ");
        day = Validator.GetProperInt(Console.ReadLine());
        Console.WriteLine("Please enter the month you were born: ");
        month = Validator.GetProperInt(Console.ReadLine());
        Console.WriteLine("Please enter the year you were born: ");
        year = Validator.GetProperInt(Console.ReadLine());
        Console.WriteLine("Please enter the amount of your total dives: ");
        divesDone = Validator.GetProperInt(Console.ReadLine());

        // Get rank data 
        if (divesDone >= 60)
        {
            DiverInstructor you = new DiverInstructor(id, fName, lName, day, month, year, divesDone);
            Helper.AssignRank(you, DB.GetSavedDivingClub(), divesDone);
            User youUser = new User(you, email, password);
        }
        else
        {
            Diver you = new Diver(id, fName, lName, day, month, year, divesDone);
            Helper.AssignRank(you, DB.GetSavedDivingClub(), divesDone);
            // Create the user
            User youUser = new User(you, email, password);
        }
        Console.Clear();
        Console.WriteLine("Register successful!");
        Printer.PrintPressAnyToContinue();
    }
    else if (key == '2')
    {
        // LogIn
        User active = null;
        if (!loggedIn)
        {
            Color.Blue();
            Console.WriteLine("Enter your email: ");
            Color.ResetColor();
            string email = Console.ReadLine();
            Color.Blue();
            Console.WriteLine("Enter your password: ");
            Color.ResetColor();
            string pass = Console.ReadLine();

            // Try and find a user in the DB class
            active = Helper.IsUserOk(email, pass);
            if (active != null) loggedIn = true;
            if (loggedIn)
            {
                Color.Green();
                Console.WriteLine("Login Successful!");
            }
            else
            {
                Color.Red();
                Console.WriteLine("Login Failed!");
            }
            Printer.PrintPressAnyToContinue();
            Console.Clear();
        }
        while (loggedIn)
        {
            Console.Clear();
            Printer.PrintMainMenuLogged(active.GetUserDiver().GetFName(), active.GetUserDiver().GetLName());
            key = Console.ReadKey(true).KeyChar; // Console.ReadKey(true).KeyChar;
            Console.Clear();
            switch (key)
            {
                case '1':
                    Color.Blue();
                    Console.Write("Input any keyword from the club you're trying to find: ");
                    Color.ResetColor();
                    string word = Console.ReadLine();
                    Console.WriteLine();
                    Helper.SearchDivingClub(word);
                    break;
                case '2':
                    // Only diving instructor can start a dive...
                    if (!(active.GetUserDiver() is DiverInstructor))
                    {
                        // Driver is not an diving instructor and will be denied
                        Printer.PrintCantStartDive("Only instructors are allowed to start a dive...");
                        break;
                    }
                    Helper.StartDive((DiverInstructor)active.GetUserDiver(), active);
                    break;
                case '3':
                    Helper.UpdateUserDetails(active);
                    break;
                case '4':
                    Helper.AddNewDivingClub();
                    break;
                case '0':
                    loggedIn = false;
                    Console.WriteLine("You've logged out successfully!");
                    active = null;
                    Console.WriteLine("Current user: 'null'");
                    break;
            }
            Printer.PrintPressAnyToContinue();
        }
    }
    // Exit
    else if (key == '0')
    {
        Console.Clear();
        Printer.PrintAreYouSure();
        bool asking = Helper.GetYesOrNo(Console.ReadLine());
        if (asking)
        {
            Console.Clear();
            Printer.PrintGoodByeMessage();
            return;
        }

    }
    // Else -> wait for proper key press (Do nothing)

    Console.Clear();
}