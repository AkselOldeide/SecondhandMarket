using SecondhandMarket.Enums;
using SecondhandMarket.Models;

namespace SecondhandMarket.Services
{
    /// <summary>
    /// Manages all marketplace data and business logic.
    /// </summary>
    public class Marketplace
    {
        /// <summary>
        /// All registered users.
        /// </summary>
        public List<User> Users { get; } = new();

        /// <summary>
        /// All listings in the marketplace.
        /// </summary>
        public List<Listing> Listings { get; } = new();

        /// <summary>
        /// All completed transactions.
        /// </summary>
        public List<Transaction> Transactions { get; } = new();

        /// <summary>
        /// Registers a new user with a username and password.
        /// </summary>
        public User RegisterUser(string username, string password)
        {
            if (Users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException("Username is already taken.");
            }

            var user = new User(username, password);
            Users.Add(user);
            return user;
        }
        /// <summary>
        /// Logs in a user with a username and password.
        /// </summary>
        public User Login(string username, string password)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (!user.CheckPassword(password))
            {
                throw new InvalidOperationException("Incorrect password.");
            }

            return user;
        }
        /// <summary>
        /// Creates a new listing for the given seller.
        /// </summary>
        public Listing CreateListing(User seller, string title, string description, Category category, Condition condition, decimal price)
        {
            if (price <= 0)
            {
                throw new InvalidOperationException("Price must be greater than zero.");
            }

            var listing = new Listing(title, description, category, condition, price, seller);
            Listings.Add(listing);
            seller.Listings.Add(listing);
            return listing;
        }

        /// <summary>
        /// Purchases a listing for the given buyer.
        /// </summary>
        public Transaction PurchaseListing(User buyer, Listing listing)
        {
            if (listing.Status == ListingStatus.Sold)
            {
                throw new InvalidOperationException("This listing has already been sold.");
            }

            if (listing.Seller.Username == buyer.Username)
            {
                throw new InvalidOperationException("You cannot purchase your own listing.");
            }

            listing.MarkAsSold();

            var transaction = new Transaction(listing, buyer, listing.Seller);
            Transactions.Add(transaction);
            buyer.Transactions.Add(transaction);
            listing.Seller.Transactions.Add(transaction);

            return transaction;
        }
        /// <summary>
        /// Leaves a review for the seller of a completed transaction.
        /// </summary>
        public Review LeaveReview(User buyer, Transaction transaction, int rating, string comment)
        {
            if (transaction.Buyer.Username != buyer.Username)
            {
                throw new InvalidOperationException("You can only review your own purchases.");
            }

            if (transaction.HasReview())
            {
                throw new InvalidOperationException("You have already reviewed this transaction.");
            }

            if (rating < 1 || rating > 6)
            {
                throw new InvalidOperationException("Rating must be between 1 and 6.");
            }

            var review = new Review(buyer.Username, rating, comment);
            transaction.AddReview(review);
            transaction.Seller.Reviews.Add(review);
            return review;
        }
        /// <summary>
        /// Returns all available listings matching the given keyword.
        /// </summary>
        public List<Listing> SearchListings(string keyword)
        {
            return Listings
                .Where(l => l.Status == ListingStatus.Available &&
                    (l.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                        l.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        /// <summary>
        /// Returns all available listings in the given category.
        /// </summary>
        public List<Listing> FilterByCategory(Category category)
        {
            return Listings
                .Where(l => l.Status == ListingStatus.Available && l.Category == category)
                .ToList();
        }
    }
}