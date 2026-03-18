namespace SecondhandMarket.Models
{
    /// <summary>
    /// Represents a review left by a buyer for a seller.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// The rating given, from 1 to 6.
        /// </summary>
        public int Rating { get; }

        /// <summary>
        /// An optional comment left by the buyer.
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// The username of the person who left the review.
        /// </summary>
        public string ReviewerUsername { get; }

        /// <summary>
        /// The date the review was posted.
        /// </summary>
        public DateTime DatePosted { get; }

        /// <summary>
        /// Creates a new review.
        /// </summary>
        public Review(string reviewerUsername, int rating, string comment)
        {
            ReviewerUsername = reviewerUsername;
            Rating = rating;
            Comment = comment;
            DatePosted = DateTime.Now;
        }
    }
}