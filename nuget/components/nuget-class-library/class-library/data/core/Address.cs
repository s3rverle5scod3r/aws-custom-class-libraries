namespace nuget_class_library.class_library.data.core
{
    /// <summary>
    /// Holds data concerning the address details of a customer for a customer object.
    /// </summary>
    /// <remarks>
    /// Initialises a new instance of the <see cref="Address"/> class.
    /// </remarks>
    /// <param name="houseNumber">The house number to set.</param>
    /// <param name="addressLine1">First address line to set.</param>
    /// <param name="addressLine2">Second address line to set.</param>
    /// <param name="addressLine3">Third address line to set.</param>
    /// <param name="addressLine4">Fourth address line to set.</param>
    /// <param name="postcode">The postcode to set.</param>
    public class Address(
        string houseNumber,
        string addressLine1,
        string addressLine2,
        string addressLine3,
        string addressLine4,
        string postcode)
    {
        /// <summary>
        /// Gets the HouseNumber.
        /// </summary>
        public string? HouseNumber { get; private set; } = houseNumber;

        /// <summary>
        /// Gets the AddressLine1.
        /// </summary>
        public string? AddressLine1 { get; private set; } = addressLine1;

        /// <summary>
        /// Gets the AddressLine2.
        /// </summary>
        public string? AddressLine2 { get; private set; } = addressLine2;

        /// <summary>
        /// Gets the AddressLine3.
        /// </summary>
        public string? AddressLine3 { get; private set; } = addressLine3;

        /// <summary>
        /// Gets the AddressLine4.
        /// </summary>
        public string? AddressLine4 { get; private set; } = addressLine4;

        /// <summary>
        /// Gets the Postcode.
        /// </summary>
        public string? Postcode { get; private set; } = postcode;
    }
}
