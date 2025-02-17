using System.Security.Cryptography.X509Certificates;


/// <summary>
/// A helper class that will have different methods that I will use across the system
/// </summary>
class Helper
{
    // Will normalize a name ("  shay " => "Shay")
    public static string TrimAndCapitalize(string str)
    {
        str = str.Trim();
        if (str == null || str.Length == 0)
        {
            return str;
        }
        char checkFirstLetter = str[0];
        string temp = Convert.ToString(checkFirstLetter).ToUpper();
        temp += str.Substring(1).ToLower();
        return temp;
    }

    // Add one to array generic 
    public static T[] AddOneToArray<T>(T[] array)
    {
        T[] temp = new T[array.Length + 1];
        for (int i = 0; i < array.Length; i++)
        {
            temp[i] = array[i];
        }
        return temp;
    }

    // Get a True or False boolean from a variety of strings
    public static bool GetYesOrNo(string input)
    {

        input = input.Trim();
        input = input.ToLower();
        if (input.Length > 5)
            input = Validator.ValidateYesOrNo(input, 0, 5);

        if (input.ToLower().Contains('y') || input.ToLower().Contains('t')) return true;
        return false; // Must be an error / 'n'
    }


    // Program Specific Function \\ 
    public static void AssignRank(Diver diver, DivingClub[] clubs, int divesDone, DateTime time = default)
    {
        DiveRankGiven newRank;
        Console.WriteLine("Please choose what club you got your rank from: ");
        int counter = 1;
        for (int i = 0; i < clubs.Length; i++)
        {
            Color.Green();
            if (counter > 9) Console.Write($"\nT. ");
            else Console.Write($"\n{counter}. ");
            counter++;
            Color.Blue();
            Console.Write($"{clubs[i]}");
        }
        Console.WriteLine();
        Color.Magenta();
        Console.Write("Your choice: ");
        Color.ResetColor();
        int choice = Validator.GetProperInt(Console.ReadLine());

        if (choice < 1 || choice > clubs.Length)
        {
            AssignRank(diver, clubs, divesDone, time);
            return;
        }
        Color.Magenta();
        Console.WriteLine("Enter the link to the certificate: ");
        Color.ResetColor();
        string site = Validator.GetProperWWW(Console.ReadLine());
        int rankIndex = 0;
        if (divesDone > 5) rankIndex++;
        if (divesDone > 8) rankIndex++;
        if (divesDone > 60) rankIndex++;
        if (divesDone > 100) rankIndex++;
        newRank = new DiveRankGiven(DB.GetSavedDiveRank()[rankIndex], clubs[choice - 1], certificate: site);

        diver.SetRankCurrent(newRank);
    }

    public static void ChangeDiverRank(Diver diver)
    {
        Color.Blue();
        Console.Write("Please enter the new amount of dives: ");
        Color.ResetColor();
        int newDivesDone = Validator.GetProperInt(Console.ReadLine());
        Console.WriteLine();
        diver.SetDivesDone(newDivesDone);

        AssignRank(diver, DB.GetSavedDivingClub(), newDivesDone);

        Color.Magenta();
        Console.WriteLine($"\nDiver's new rank: {diver.GetRankCurrent().GetDiveRank().GetName()}");
        Color.ResetColor();
    }

    public static User IsUserOk(string email, string pass)
    {
        User u = null;
        User[] users = DB.GetSavedUser();
        foreach (User user in users)
        {
            if (email == user.GetEmail() && pass == user.GetUserPass()) u = user;
        }
        return u;
    }

    public static void SearchDivingClub(string searchTerm)
    {
        DivingClub[] clubs = DB.GetSavedDivingClub();
        List<DivingClub> results = new List<DivingClub>();

        foreach (var club in clubs)
        {
            if (club.GetLicense().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetContactPerson().GetFName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetContactPerson().GetLName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetAddress().GetStreetName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetAddress().GetCityName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetAddress().GetStateOrRegion().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetAddress().GetCountry().GetName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetPhoneNumber().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetEmail().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetDivingSite().GetName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                club.GetSiteLink().Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(club);
            }
        }

        if (results.Count > 0)
        {
            Color.Magenta();
            Console.WriteLine("Search Results:");
            foreach (var result in results)
            {
                Console.WriteLine(result.ToString());
            }
        }
        else
        {
            Color.Red();
            Console.WriteLine("No results found.");
        }
        Color.ResetColor();
    }

    public static void StartDive(DiverInstructor instructor, User active)
    {
        // Step 1: Choose from the clubs diver works at
        Color.Blue();
        Console.WriteLine("Choose a club you work at:");
        var workingClubs = instructor.GetCurrentWorkingClubs();
        Color.Green();
        for (int i = 0; i < workingClubs.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {workingClubs[i].GetName()}");
        }
        Color.ResetColor();
        int clubChoice = Validator.GetProperInt(Console.ReadLine()) - 1;
        if (clubChoice < 0 || clubChoice >= workingClubs.Length)
        {
            Color.Red();
            Console.WriteLine("Invalid choice. Please try again.");
            Color.ResetColor();
            return;
        }
        DivingClub chosenClub = workingClubs[clubChoice];

        // Step 2: Present the DiveSite.ToString() that the dive will take place in
        Color.Blue();
        Console.WriteLine("Dive will take place at:");
        Color.Green();
        Console.WriteLine(chosenClub.GetDivingSite().ToString());

        // Step 3: Choose people to go with to the dive (that are in the DB)
        List<Diver> divers = new List<Diver> { instructor }; // Add the instructor by default
        Color.Blue();
        Console.WriteLine("Choose divers to go with (enter IDs, separated by commas):");
        Color.Green();
        var savedDivers = DB.GetSavedPerson().OfType<Diver>().Where(d => d.GetId() != instructor.GetId()).ToArray(); // Exclude the instructor
        for (int i = 0; i < savedDivers.Length; i++)
        {
            Console.WriteLine($"{savedDivers[i].GetId()}: {savedDivers[i].GetFName()} {savedDivers[i].GetLName()}");
        }
        Color.ResetColor();
        string[] diverIds = Console.ReadLine().Split(',');
        foreach (var id in diverIds)
        {
            // First or Default get the ID of the driver that is saved in the DB
            var diver = savedDivers.FirstOrDefault(d => d.GetId() == id.Trim());
            if (diver != null)
            {
                divers.Add(diver);
            }
        }

        // Check if there are at least 2 divers
        if (divers.Count < 2)
        {
            Color.Red();
            Console.WriteLine("Error: There must be at least 2 divers to start a dive.");
            Color.ResetColor();
            return;
        }

        // Step 4: For each diver, choose gear from club storage and add comments
        Dictionary<Diver, Dictionary<Item, int>> diverGear = new Dictionary<Diver, Dictionary<Item, int>>();
        foreach (var diver in divers)
        {
            diverGear[diver] = ChooseGearForDiver(diver, chosenClub);
        }

        // All divers dive
        foreach (Diver diver in divers)
        {
            diver.Dive();
        }


        // Step 5: Loading...
        Color.Black();
        Console.WriteLine("Loading...");
        Thread.Sleep(2000); // Simulate loading
        Color.Blue();
        Console.WriteLine("Press any key to complete dive");
        Console.ReadKey();

        // All divers get out from the water
        foreach (Diver diver in divers)
        {
            diver.GetOutFromWater();
        }

        Color.Green();
        Console.WriteLine("Dive completed!");

        // Step 6: After the dive, all divers sign
        List<Signature> diverSignatures = new List<Signature>();
        foreach (var diver in divers)
        {
            var signature = diver.Sign();
            if (signature != null)
            {
                diverSignatures.Add(signature);
            }
        }

        // Step 7: Club signs
        var clubSignature = chosenClub.GetContactPerson().Sign();


        // Create DivingInfo and add to club logs
        Dictionary<Item, int> combinedItems = new Dictionary<Item, int>();
        foreach (var gear in diverGear.Values)
        {
            foreach (var item in gear)
            {
                if (combinedItems.ContainsKey(item.Key))
                {
                    combinedItems[item.Key] += item.Value;
                }
                else
                {
                    combinedItems[item.Key] = item.Value;
                }
            }
        }

        DivingInfo diveInfo = new DivingInfo(
            diveInfoFiller: active,
            diveDate: DateOnly.FromDateTime(DateTime.Now),
            divingClub: chosenClub,
            divingSite: chosenClub.GetDivingSite(),
            diveRegulation: chosenClub.GetAddress().GetCountry().GetDivingRegulations(),
            headDiver: instructor,
            divers: divers.ToArray(),
            itemsTaken: combinedItems,
            waterEnter: TimeOnly.FromDateTime(DateTime.Now),
            // The following data will be received before hand by automated devices / system
            waterExit: TimeOnly.FromDateTime(DateTime.Now.AddHours(2)), // Assuming a 2 hour dive
            waterTemp: 25.0, // Assuming water temperature measured at the time
            tideLevel: 0.9, // Assuming tide level measured at the time
            signatureDivers: diverSignatures.ToArray(),
            signatureClub: clubSignature
        );

        chosenClub.SetDivingLogs(chosenClub.GetDivingLogs().Append(diveInfo).ToArray());

        Color.Green();
        Console.WriteLine("Dive completed successfully!");
        Color.ResetColor();
    }

    private static Dictionary<Item, int> ChooseGearForDiver(Diver diver, DivingClub chosenClub)
    {
        Dictionary<Item, int> gear = new Dictionary<Item, int>();
        Color.Blue();
        Console.WriteLine($"Choose gear for {diver.GetFName()} {diver.GetLName()}:");
        foreach (var item in chosenClub.GetItems().ToList()) // Use ToList() to avoid modifying the collection while iterating
        {
            bool validInput = false;
            while (!validInput)
            {
                Color.Green();
                Console.WriteLine($"{item.Key.GetId()}: {item.Key.GetName()} ({item.Value} available)");
                Color.Blue();
                Console.Write($"How many {item.Key.GetName()} to take: ");
                Color.ResetColor();
                int quantity = Validator.GetProperInt(Console.ReadLine());
                if (quantity >= 0 && quantity <= item.Value)
                {
                    gear[item.Key] = quantity;
                    chosenClub.GetItems()[item.Key] -= quantity; // Update the available quantity
                    validInput = true;
                }
                else if (quantity > item.Value)
                {
                    Color.Red();
                    Console.WriteLine($"Error: Only {item.Value} {item.Key.GetName()} available.");
                    Color.ResetColor();
                }
                else
                {
                    Color.Red();
                    Console.WriteLine("Error: Invalid quantity. Please enter a positive number.");
                    Color.ResetColor();
                }
            }
        }
        return gear;
    }

    public static void UpdateUserDetails(User user)
    {
        Printer.PrintMainMenuChangeProfile();
        char key = Console.ReadKey(true).KeyChar;

        switch (key)
        {
            case '1':
                Console.Clear();
                UpdatePassword(user);
                break;
            case '2':
                Console.Clear();
                UpdateFirstName(user.GetUserDiver());
                break;
            case '3':
                Console.Clear();
                UpdateLastName(user.GetUserDiver());
                break;
            case '4':
                Console.Clear();
                ChangeDiverRank(user.GetUserDiver());
                break;
            default:
                Console.Clear();
                Color.Red();
                Console.WriteLine("\nInvalid choice. Returning...");
                break;
        }
    }

    private static void UpdatePassword(User user)
    {
        Color.Blue();
        Console.Write("Enter your new password: ");
        Color.ResetColor();
        string newPassword = Console.ReadLine();
        user.SetUserPass(newPassword);
        Color.Magenta();
        Console.WriteLine("\nPassword updated successfully!");
    }

    private static void UpdateFirstName(Diver diver)
    {
        Color.Blue();
        Console.Write("Enter your new first name: ");
        Color.ResetColor();
        string newFirstName = Console.ReadLine();
        diver.SetFName(newFirstName);
        Color.Magenta();
        Console.WriteLine("\nFirst name updated successfully!");
    }

    private static void UpdateLastName(Diver diver)
    {
        Color.Blue();
        Console.Write("Enter your new last name: ");
        Color.ResetColor();
        string newLastName = Console.ReadLine();
        diver.SetLName(newLastName);
        Color.Magenta();
        Console.WriteLine("\nLast name updated successfully!");
    }

    public static void AddNewDivingClub()
    {
        Color.Blue();
        Console.WriteLine("Enter the club license: ");
        Color.ResetColor();
        string license = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the club name: ");
        Color.ResetColor();
        string name = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the contact person's ID: ");
        Color.ResetColor();
        string contactPersonId = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the contact person's first name: ");
        Color.ResetColor();
        string contactPersonFName = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the contact person's last name: ");
        Color.ResetColor();
        string contactPersonLName = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the contact person's birth date (dd/mm/yyyy): ");
        Color.ResetColor();
        string[] birthDateParts = Console.ReadLine().Split('/');
        int day = Validator.GetProperInt(birthDateParts[0]);
        int month = Validator.GetProperInt(birthDateParts[1]);
        int year = Validator.GetProperInt(birthDateParts[2]);
        Person contactPerson = new Person(contactPersonId, contactPersonFName, contactPersonLName, day, month, year);

        Color.Blue();
        Console.WriteLine("Enter the address street name: ");
        Color.ResetColor();
        string streetName = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the address house number: ");
        Color.ResetColor();
        string houseNumber = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the address city name: ");
        Color.ResetColor();
        string cityName = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the address state or region: ");
        Color.ResetColor();
        string stateOrRegion = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the address zip code: ");
        Color.ResetColor();
        string zipCode = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the country name: ");
        Color.ResetColor();
        string countryName = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the diving regulation (true/false for at least two divers): ");
        Color.ResetColor();
        bool atLeastTwoDivers = Helper.GetYesOrNo(Console.ReadLine());

        Color.Blue();
        Console.WriteLine("Enter the diving regulations (comma separated true/false values): ");
        Color.ResetColor();
        bool[] regulations = Console.ReadLine().Split(',').Select(bool.Parse).ToArray();

        Color.Blue();
        Console.WriteLine("Enter the country languages (comma separated): ");
        Color.ResetColor();
        string[] languages = Console.ReadLine().Split(',');
        Country country = new Country(countryName, new DiveRegulation(atLeastTwoDivers, regulations), languages);

        Address address = new Address(streetName, houseNumber, cityName, stateOrRegion, zipCode, country);

        Color.Blue();
        Console.WriteLine("Enter the phone number: ");
        Color.ResetColor();
        string phoneNumber = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the email: ");
        Color.ResetColor();
        string email = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the diving site name: ");
        Color.ResetColor();
        string divingSiteName = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the diving site description: ");
        Color.ResetColor();
        string divingSiteDescription = Console.ReadLine();

        Color.Blue();
        Console.WriteLine("Enter the diving site length: ");
        Color.ResetColor();
        double length = Validator.GetProperDouble(Console.ReadLine());

        Color.Blue();
        Console.WriteLine("Enter the diving site width: ");
        Color.ResetColor();
        double width = Validator.GetProperDouble(Console.ReadLine());

        Color.Blue();
        Console.WriteLine("Enter the diving site depth: ");
        Color.ResetColor();
        double depth = Validator.GetProperDouble(Console.ReadLine());

        Color.Blue();
        Console.WriteLine("Is the water salty (true/false): ");
        Color.ResetColor();
        bool waterSalty = GetYesOrNo(Console.ReadLine());

        DivingSite divingSite = new DivingSite(divingSiteName, divingSiteDescription, length, width, depth, waterSalty, address);

        Color.Blue();
        Console.WriteLine("Enter the website link: ");
        Color.ResetColor();
        string siteLink = Console.ReadLine();

        Dictionary<Item, int> items = new Dictionary<Item, int>();
        Color.Blue();
        Console.WriteLine("Enter the number of items: ");
        Color.ResetColor();
        int itemCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < itemCount; i++)
        {
            Color.Blue();
            Console.WriteLine($"Enter item {i + 1} ID: ");
            Color.ResetColor();
            string itemId = Console.ReadLine();

            Color.Blue();
            Console.WriteLine($"Enter item {i + 1} name: ");
            Color.ResetColor();
            string itemName = Console.ReadLine();

            Color.Blue();
            Console.WriteLine($"Enter item {i + 1} description: ");
            Color.ResetColor();
            string itemDescription = Console.ReadLine();

            Color.Blue();
            Console.WriteLine($"Enter item {i + 1} type (number): ");
            Color.ResetColor();
            int itemType = Validator.GetProperInt(Console.ReadLine());

            Color.Blue();
            Console.WriteLine($"Enter item {i + 1} quantity: ");
            Color.ResetColor();
            int itemQuantity = Validator.GetProperInt(Console.ReadLine());

            Item item = new Item(itemId, itemName, itemDescription, itemType);
            items.Add(item, itemQuantity);
        }

        DivingInfo[] divingLogs = new DivingInfo[0]; // Assuming no diving logs initially

        DivingClub newClub = new DivingClub(license, name, contactPerson, address, phoneNumber, email, divingSite, siteLink, items, divingLogs);
        DB.AddSavedDivingClub(newClub);
        Color.Magenta();
        Console.WriteLine("New diving club added successfully!");
        Color.ResetColor();
    }
}