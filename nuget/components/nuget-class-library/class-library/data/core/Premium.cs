namespace nuget_class_library.class_library.data.core
{
    /// <summary>
    /// Holds data concerning the premuim details of a customer for a customer object.
    /// </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="Premium"/> class.
    /// </remarks>
    /// <param name="totalSellingPrice">The total selling price to set.</param>
    /// <param name="nettPremium">The nett premium to set.</param>
    /// <param name="grossPremium">The gross premium to set.</param>
    /// <param name="outstandingBalance">The outstanding balance to set.</param>
    /// <param name="cardNumber">The card number to set.</param>
    public class Premium(
        decimal totalSellingPrice,
        decimal nettPremium,
        decimal grossPremium,
        decimal outstandingBalance,
        string cardNumber)
    {
        /// <summary>
        /// Gets the TotalSellingPrice.
        /// </summary>
        public decimal TotalSellingPrice { get; private set; } = totalSellingPrice;

        /// <summary>
        /// Gets the NettPremium.
        /// </summary>
        public decimal NettPremium { get; private set; } = nettPremium;

        /// <summary>
        /// Gets the GrossPremium.
        /// </summary>
        public decimal GrossPremium { get; private set; } = grossPremium;

        /// <summary>
        /// Gets the OutstandingBalance.
        /// </summary>
        public decimal OutstandingBalance { get; private set; } = outstandingBalance;

        /// <summary>
        /// Gets the CardNumber.
        /// </summary>
        public string? CardNumber { get; private set; } = cardNumber;
    }
}

