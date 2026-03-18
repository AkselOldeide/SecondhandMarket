using SecondhandMarket.Enums;

namespace SecondhandMarket.Models
{
    /// <summary>
    /// Represents a registered user in the marketplace.
    /// </summary>
    public class User
    {
        private string _password;

        /// <summary>
        /// The user's unique username.
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Listings created by this user.
        /// </summary>
        public List<Listing> Listings { get; } = new();

        /// <summary>
        /// All transactions this user is involved in.
        /// </summary>
        public List<Transaction> Transactions { get; } = new();

        /// <summary>
        /// Reviews received by this user as a seller.
        /// </summary>
        public List<Review> Reviews { get; } = new();

        /// <summary>
        /// The average rating this user has received.
        /// </summary>
        public double AverageRating => Reviews.Count == 0 ? 0 : Reviews.Average(r => r.Rating);

        /// <summary>
        /// Creates a new user with a username and password.
        /// </summary>
        public User(string username, string password)
        {
            Username = username;
            _password = password;
        }

        /// <summary>
        /// Checks whether the provided password matches.
        /// </summary>
        public bool CheckPassword(string input) => _password == input;
    }
}