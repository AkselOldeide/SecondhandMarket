namespace SecondhandMarket.Models
{
    /// <summary>
    /// Represents a completed purchase between a buyer and a seller.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// The listing that was purchased.
        /// </summary>
        public Listing Listing { get; }

        /// <summary>
        /// The user who bought the item.
        /// </summary>
        public User Buyer { get; }

        /// <summary>
        /// The user who sold the item.
        /// </summary>
        public User Seller { get; }

        /// <summary>
        /// The price paid for the item.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// The date the transaction was completed.
        /// </summary>
        public DateTime DateCompleted { get; }

        /// <summary>
        /// The review left for the seller, if any.
        /// </summary>
        public Review? Review { get; private set; }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        public Transaction(Listing listing, User buyer, User seller)
        {
            Listing = listing;
            Buyer = buyer;
            Seller = seller;
            Price = listing.Price;
            DateCompleted = DateTime.Now;
        }

        /// <summary>
        /// Attaches a review to this transaction.
        /// </summary>
        public void AddReview(Review review)
        {
            Review = review;
        }

        /// <summary>
        /// Returns true if a review has already been left for this transaction.
        /// </summary>
        public bool HasReview() => Review != null;
    }
}