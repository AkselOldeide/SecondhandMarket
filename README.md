# Second Hand Market

A command-line marketplace application for buying and selling second-hand items, inspired by Finn.no.

## Description

This C# console application simulates an online second-hand marketplace. Users can register accounts, list items for sale, browse and search listings, purchase items, and leave reviews for sellers. All data is stored in memory for the duration of the session.

## How to Build and Run

1. Clone the repository
2. Make sure you have .NET 10 installed — verify with `dotnet --version`
3. Navigate to the project folder in your terminal
4. Run the application:

```bash
dotnet run
```

## Features

- Register and log in with a username and password
- Create, edit and delete your own listings
- Browse all available listings
- Search listings by keyword (case insensitive, matches title and description)
- Filter listings by category
- Purchase items from other users
- Leave a rating (1–6) and optional comment after purchasing
- View your profile with listings, purchase history, sales history and average rating

## Project Structure

```
SecondhandMarket/
├── Enums/
│   ├── Category.cs         # Electronics, ClothingAndAccessories, FurnitureAndHome, BooksAndMedia, SportsAndOutdoors, Other
│   ├── Condition.cs        # New, LikeNew, Good, Fair
│   └── ListingStatus.cs    # Available, Sold
├── Models/
│   ├── User.cs             # Registered user with listings, transactions and reviews
│   ├── Listing.cs          # Item listed for sale
│   ├── Transaction.cs      # Completed purchase between buyer and seller
│   └── Review.cs           # Buyer review left for a seller
├── Services/
│   └── Marketplace.cs      # All business logic and data management
├── Program.cs              # Entry point, all console input/output and menus
└── README.md
```

## Design Decisions

### OOP Concepts Used

**Classes and Objects**
All core entities are modelled as classes: `User`, `Listing`, `Transaction` and `Review`. Each has private fields, public properties and a constructor that initialises all required data.

**Encapsulation**
Private fields are exposed through public properties. Most properties are read-only (`get` only) to prevent outside code from directly modifying internal state. For example, `Listing.Status` can only be changed through `MarkAsSold()`, and `Transaction.Review` can only be set through `AddReview()`. This ensures business rules are always respected.

**Inheritance**
Not used in this project. There were no natural "is-a" relationships between the core entities that would benefit from a shared base class without adding unnecessary complexity.

**Interfaces**
Not used in this project. The classes serve distinct purposes and share no common contract that would benefit from an interface in a project of this scope.

**Enums**
Used for `Category`, `Condition` and `ListingStatus` to represent fixed sets of values safely without magic strings. This prevents typos and makes comparisons reliable.

**Generic Collections**
`List<T>` is used throughout to store users, listings, transactions and reviews. No arrays or untyped collections are used.

**LINQ**
Used for filtering listings by category, searching by keyword, calculating average ratings, filtering purchase and sales history, and checking listing availability.

**Exception Handling**
`try/catch` blocks are used in all UI methods in `Program.cs`. The `Marketplace` service throws `InvalidOperationException` for business rule violations such as buying your own listing, duplicate usernames, invalid ratings, and reviewing the same transaction twice.

### Separation of Concerns

All console input/output is kept entirely in `Program.cs`. Model classes contain only data and simple helper methods. Business logic lives in `Marketplace.cs`. This means if the interface ever changed, the underlying logic would not need to be touched.

### AI
This project was developed with assistance from Claude (Anthropic). Helping design the class structure, explaining concepts like encapsulation, LINQ and exception handling, and guiding the implementation. All prompts and a full breakdown of AI usage can be found in [AI_USAGE.md](AI_USAGE.md).


## Author

**Aksel Oldeide**
Backend Programming, 1st Year — Gokstad Akademiet
