using OOP_LAB_1;
// Driver code for class Cat
// Not the best code, but it works ¯\_(ツ)_/¯

static Cat CreateCat(int id) {
    string? input;

    // Name
    while (true) {
        Console.Write("Cat name: ");
        input = Console.ReadLine();
        if (input != null) { break; }
    }
    string catName = Utils.Capitalize(input.Trim());

    // Age
    while (true) {
        Console.Write("Cat age (in years): ");
        input = Console.ReadLine();
        if (Double.TryParse(input, out double tmp) && tmp > 0) { break; }
    }
    double catAge = double.Parse(input);

    // Gender
    while (true) {
        Console.Write("Cat gender (M or F): ");
        input = Console.ReadLine();
        if (input != null) {
            input = input.Trim().ToLower();
            if (input == "f" || input == "m") { break; }
        }
    }
    Gender catGender = (input == "m") ? Gender.Male : Gender.Female;

    // Type
    while (true) {
        Console.Write("Cat type (British, Jellie, Persian, Ragdoll, Siamese, Tabby, Tuxedo): ");
        input = Console.ReadLine();
        if (input != null) {
            // Do not allow typing in numbers
            if (int.TryParse(input, out _)) { continue; }
            input = Utils.Capitalize(input.Trim());
            if (Enum.TryParse(typeof(CatType), input, true, out _)) { break; }
        }
    }
    CatType catType = (CatType) Enum.Parse(typeof(CatType), input);


    return new Cat(catName, id, catAge, catGender, catType);
}

string? input;
while (true) {
    Console.Write("# of objects: ");
    input = Console.ReadLine();
    if (int.TryParse(input, out int tmp) && tmp > 0) { break; }
}
int N = int.Parse(input);

List<Cat> cats = new(N);
int i = 1;

Console.WriteLine("Commands: add, list, find, delete, exit");
while (true) {
    Console.Write("> ");
    input = Console.ReadLine();
    if (input == null) { continue; }

    // Some variables
    string filter;
    Cat[] result;

    var cmdArgs = input.Trim().Split(' ', 2);
    if (input != null) {
        switch (cmdArgs[0]) {
            case "+":
            case "add":
                cats.Add(CreateCat(i++));
                break;
            case "ls":
            case "list":
                // List all cats
                Console.WriteLine($"Cats ({cats.Count}):");
                foreach (Cat cat in cats) {
                    Console.WriteLine($"{cat.Id} | {cat}");
                }
                break;
            case "show":
            case "find":
                // Find cat(s) by Name/Id
                if (cmdArgs.Length != 2) { Console.WriteLine("Usage: find <Id | Name>"); break; }
                filter = cmdArgs[1].ToLower();
                result = cats.Where(x => x.Name.ToLower() == filter || x.Id.ToString() == filter).ToArray();
                if (result.Length != 0) {
                    Console.WriteLine($"Cats ({cats.Count}):");
                    foreach (Cat cat in result) {
                        Console.WriteLine($"{cat.Id} | {cat}");
                    }
                }
                else {
                    Console.WriteLine("No results for given Id/Name");
                }
                break;
            case "-":
            case "rm":
            case "del":
            case "delete":
                // Delete cat(s) by Name/Id
                if (cmdArgs.Length != 2) { Console.WriteLine("Usage: delete <Id | Name>"); break; }
                filter = cmdArgs[1].ToLower();

                result = cats.Where(x => (x.Name.ToLower() == filter || x.Id.ToString() == filter)).ToArray();
                if (result.Length != 0) {
                    Console.WriteLine($"The following entries were deleted:");
                    foreach (Cat cat in result) {
                        Console.WriteLine($"{cat.Id} | {cat}");
                    }
                }
                else {
                    Console.WriteLine("No results for given Id/Name");
                }
                // Actually remove from the list
                cats.RemoveAll(x => (x.Name.ToLower() == filter || x.Id.ToString() == filter));

                break;
            case "exit":
                Console.WriteLine("Bye bye.");
                Environment.Exit(0);
                // Fall through case after exit? What? VS, you good?
                break;
            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
}