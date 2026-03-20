using SecondhandMarket.Enums;
using SecondhandMarket.Models;
using SecondhandMarket.Services;

var marketplace = new Marketplace();
User? currentUser = null;

while (true)
{
    if (currentUser == null)
    {
        ShowMainMenu();
    }
    else
    {
        ShowLoggedInMenu();
    }
}

void ShowMainMenu()
{
    Console.WriteLine("\n=== Second Hand Market ===");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Log In");
    Console.WriteLine("3. Exit");
    Console.Write("Select an option: ");

    switch (Console.ReadLine())
    {
        case "1":
            Register();
            break;
        case "2":
            Login();
            break;
        case "3":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}

void ShowLoggedInMenu()
{
    Console.WriteLine($"\n=== Main Menu === (Logged in as {currentUser!.Username})");
    Console.WriteLine("1. Create Listing");
    Console.WriteLine("2. Browse Listings");
    Console.WriteLine("3. Search Listings");
    Console.WriteLine("4. Filter by Category");
    Console.WriteLine("5. My Profile");
    Console.WriteLine("6. Log Out");
    Console.Write("Select an option: ");

    switch (Console.ReadLine())
    {
        case "1":
            CreateListing();
            break;
        case "2":
            BrowseListings();
            break;
        case "3":
            SearchListings();
            break;
        case "4":
            FilterByCategory();
            break;
        case "5":
            ShowProfile();
            break;
        case "6":
            currentUser = null;
            Console.WriteLine("You have been logged out.");
            break;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}

void Register()
{
    Console.Write("Choose a username: ");
    string username = Console.ReadLine() ?? "";

    Console.Write("Choose a password: ");
    string password = Console.ReadLine() ?? "";

    try
    {
        currentUser = marketplace.RegisterUser(username, password);
        Console.WriteLine($"Welcome, {currentUser.Username}! You are now registered and logged in.");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}

void Login()
{
    Console.Write("Username: ");
    string username = Console.ReadLine() ?? "";

    Console.Write("Password: ");
    string password = Console.ReadLine() ?? "";

    try
    {
        currentUser = marketplace.Login(username, password);
        Console.WriteLine($"Welcome back, {currentUser.Username}!");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}

void CreateListing()
{
    Console.Write("Title: ");
    string title = Console.ReadLine() ?? "";

    Console.Write("Description: ");
    string description = Console.ReadLine() ?? "";

    Console.WriteLine("Select a category:");
    var categories = Enum.GetValues<Category>();
    for (int i = 0; i < categories.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {categories[i]}");
    }
    Console.Write("Select: ");
    if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1 || catIndex > categories.Length)
    {
        Console.WriteLine("Invalid category.");
        return;
    }
    var category = categories[catIndex - 1];

    Console.WriteLine("Select a condition:");
    var conditions = Enum.GetValues<Condition>();
    for (int i = 0; i < conditions.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {conditions[i]}");
    }
    Console.Write("Select: ");
    if (!int.TryParse(Console.ReadLine(), out int condIndex) || condIndex < 1 || condIndex > conditions.Length)
    {
        Console.WriteLine("Invalid condition.");
        return;
    }
    var condition = conditions[condIndex - 1];

    Console.Write("Price (NOK): ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        Console.WriteLine("Invalid price.");
        return;
    }

    try
    {
        var listing = marketplace.CreateListing(currentUser!, title, description, category, condition, price);
        Console.WriteLine($"Listing \"{listing.Title}\" created successfully!");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}

void BrowseListings()
{
    var listings = marketplace.Listings
        .Where(l => l.Status == ListingStatus.Available)
        .ToList();

    if (listings.Count == 0)
    {
        Console.WriteLine("No listings available.");
        return;
    }

    Console.WriteLine("\n=== Available Listings ===");
    for (int i = 0; i < listings.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {listings[i].Title} - {listings[i].Category} - {listings[i].Condition} - {listings[i].Price} NOK");
    }

    Console.Write("Select a listing to view (0 to go back): ");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > listings.Count)
    {
        Console.WriteLine("Invalid selection.");
        return;
    }
    if (choice == 0) return;

    ShowListingDetails(listings[choice - 1]);
}

void SearchListings()
{
    Console.Write("Enter keyword: ");
    string keyword = Console.ReadLine() ?? "";

    var results = marketplace.SearchListings(keyword);

    if (results.Count == 0)
    {
        Console.WriteLine("No listings found.");
        return;
    }

    Console.WriteLine($"\n=== Search Results for \"{keyword}\" ===");
    for (int i = 0; i < results.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {results[i].Title} - {results[i].Category} - {results[i].Price} NOK");
    }

    Console.Write("Select a listing to view (0 to go back): ");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > results.Count)
    {
        Console.WriteLine("Invalid selection.");
        return;
    }
    if (choice == 0) return;

    ShowListingDetails(results[choice - 1]);
}

void FilterByCategory()
{
    Console.WriteLine("Select a category:");
    var categories = Enum.GetValues<Category>();
    for (int i = 0; i < categories.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {categories[i]}");
    }
    Console.Write("Select: ");
    if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1 || catIndex > categories.Length)
    {
        Console.WriteLine("Invalid category.");
        return;
    }

    var category = categories[catIndex - 1];
    var results = marketplace.FilterByCategory(category);

    if (results.Count == 0)
    {
        Console.WriteLine("No listings found in this category.");
        return;
    }

    Console.WriteLine($"\n=== Listings in {category} ===");
    for (int i = 0; i < results.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {results[i].Title} - {results[i].Condition} - {results[i].Price} NOK");
    }

    Console.Write("Select a listing to view (0 to go back): ");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > results.Count)
    {
        Console.WriteLine("Invalid selection.");
        return;
    }
    if (choice == 0) return;

    ShowListingDetails(results[choice - 1]);
}

void ShowListingDetails(Listing listing)
{
    Console.WriteLine($"\n=== {listing.Title} ===");
    Console.WriteLine($"Seller: {listing.Seller.Username}");
    Console.WriteLine($"Category: {listing.Category}");
    Console.WriteLine($"Condition: {listing.Condition}");
    Console.WriteLine($"Price: {listing.Price} NOK");
    Console.WriteLine($"Description: {listing.Description}");
    Console.WriteLine($"Listed: {listing.DateCreated:dd.MM.yyyy}");

    if (listing.Seller.Username == currentUser!.Username)
    {
        Console.WriteLine("\nThis is your own listing.");
        EditOrDeleteListing(listing);
        return;
    }

    Console.WriteLine("\n1. Buy this item");
    Console.WriteLine("2. Go back");
    Console.Write("Select an option: ");

    if (Console.ReadLine() == "1")
    {
        Purchase(listing);
    }
}

void EditOrDeleteListing(Listing listing)
{
    Console.WriteLine("1. Edit listing");
    Console.WriteLine("2. Delete listing");
    Console.WriteLine("3. Go back");
    Console.Write("Select an option: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Write($"New title ({listing.Title}): ");
            string title = Console.ReadLine() ?? listing.Title;
            if (!string.IsNullOrWhiteSpace(title)) listing.Title = title;

            Console.Write($"New description ({listing.Description}): ");
            string description = Console.ReadLine() ?? listing.Description;
            if (!string.IsNullOrWhiteSpace(description)) listing.Description = description;

            Console.Write($"New price ({listing.Price}): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
                listing.Price = price;

            Console.WriteLine("Listing updated.");
            break;
        case "2":
            marketplace.Listings.Remove(listing);
            currentUser!.Listings.Remove(listing);
            Console.WriteLine("Listing deleted.");
            break;
    }
}

void Purchase(Listing listing)
{
    try
    {
        var transaction = marketplace.PurchaseListing(currentUser!, listing);
        Console.WriteLine($"Purchase complete! You bought \"{listing.Title}\" from {listing.Seller.Username}.");

        Console.Write("Would you like to leave a review? (Y/N): ");
        if (Console.ReadLine()?.ToUpper() == "Y")
        {
            LeaveReview(transaction);
        }
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}

void LeaveReview(Transaction transaction)
{
    Console.Write("Rating (1-6): ");
    if (!int.TryParse(Console.ReadLine(), out int rating))
    {
        Console.WriteLine("Invalid rating.");
        return;
    }

    Console.Write("Comment (or press Enter to skip): ");
    string comment = Console.ReadLine() ?? "";

    try
    {
        marketplace.LeaveReview(currentUser!, transaction, rating, comment);
        Console.WriteLine("Review submitted. Thank you!");
    }
    catch (InvalidOperationException e)
    {
        Console.WriteLine($"Error: {e.Message}");
    }
}

void ShowProfile()
{
    Console.WriteLine($"\n=== {currentUser!.Username}'s Profile ===");
    Console.WriteLine($"Average Rating: {currentUser.AverageRating:F1} / 6");

    Console.WriteLine("\n--- My Listings ---");
    if (currentUser.Listings.Count == 0)
    {
        Console.WriteLine("No listings.");
    }
    else
    {
        foreach (var l in currentUser.Listings)
        {
            Console.WriteLine($"- {l.Title} | {l.Price} NOK | {l.Status}");
        }
    }

    Console.WriteLine("\n--- Purchase History ---");
    var purchases = currentUser.Transactions.Where(t => t.Buyer.Username == currentUser.Username).ToList();
    if (purchases.Count == 0)
    {
        Console.WriteLine("No purchases.");
    }
    else
    {
        foreach (var t in purchases)
        {
            Console.WriteLine($"- {t.Listing.Title} | {t.Price} NOK | {t.DateCompleted:dd.MM.yyyy} | Seller: {t.Seller.Username}");
        }
    }

    Console.WriteLine("\n--- Sales History ---");
    var sales = currentUser.Transactions.Where(t => t.Seller.Username == currentUser.Username).ToList();
    if (sales.Count == 0)
    {
        Console.WriteLine("No sales.");
    }
    else
    {
        foreach (var t in sales)
        {
            Console.WriteLine($"- {t.Listing.Title} | {t.Price} NOK | {t.DateCompleted:dd.MM.yyyy} | Buyer: {t.Buyer.Username}");
        }
    }

    Console.WriteLine("\n--- Reviews Received ---");
    if (currentUser.Reviews.Count == 0)
    {
        Console.WriteLine("No reviews yet.");
    }
    else
    {
        foreach (var r in currentUser.Reviews)
        {
            Console.WriteLine($"- {r.Rating}/6 by {r.ReviewerUsername} on {r.DatePosted:dd.MM.yyyy}: {r.Comment}");
        }
    }
}