using SecondhandMarket.Enums;

namespace SecondhandMarket.Models
{
    /// <summary>
    /// Represents an item listed for sale in the marketplace.
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// The title of the listing.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A detailed description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The category the item belongs to.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// The physical condition of the item.
        /// </summary>
        public Condition Condition { get; set; }

        /// <summary>
        /// The asking price in NOK.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The current status of the listing.
        /// </summary>
        public ListingStatus Status { get; private set; }

        /// <summary>
        /// The seller who created this listing.
        /// </summary>
        public User Seller { get; }

        /// <summary>
        /// The date this listing was created.
        /// </summary>
        public DateTime DateCreated { get; }

        /// <summary>
        /// Creates a new listing.
        /// </summary>
        public Listing(string title, string description, Category category, Condition condition, decimal price, User seller)
        {
            Title = title;
            Description = description;
            Category = category;
            Condition = condition;
            Price = price;
            Seller = seller;
            Status = ListingStatus.Available;
            DateCreated = DateTime.Now;
        }

        /// <summary>
        /// Marks this listing as sold.
        /// </summary>
        public void MarkAsSold()
        {
            Status = ListingStatus.Sold;
        }
    }
}