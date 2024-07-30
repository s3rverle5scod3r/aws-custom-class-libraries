using nuget_class_library.class_library.data.enums;
using nuget_class_library.class_library.exception;

namespace nuget_class_library.class_library.data.core
{
    /// <summary>
    /// Holds data describing an individual customer for a customer object.
    /// </summary>
    public abstract class Customer
    {
        /// <summary>
        /// Gets the Reference.
        /// </summary>
        public string Reference { get; private set; }

        /// <summary>
        /// Gets the Web Reference.
        /// </summary>
        public string WebReference { get; private set; }

        /// <summary>
        /// Gets the Email.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the FirstName.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the Surname.
        /// </summary>
        public string Surname { get; private set; }

        /// <summary>
        /// Gets the Address.
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// Gets the Phone.
        /// </summary>
        public string Phone { get; private set; }

        /// <summary>
        /// Gets the Brand.
        /// </summary>
        public Brand Brand { get; protected set; }

        /// <summary>
		/// Gets the Active state of the customer.
		/// </summary>
		public bool Active { get; private set; }

        /// <summary>
		/// Gets the PaymentType; describing the payment type eg payment in full or direct debit.
		/// </summary>
		public string PaymentType { get; private set; }

        /// <summary>
        /// Gets the Premium details associated with the business event.
        /// </summary>
        public Premium Premium { get; private set; }

        /// <summary>
        /// Gets the Finance details, such as installments, etc.
        /// </summary>
        public Finance Finance { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="reference">The client reference to set.</param>
        /// <param name="webReference">The web reference to set.</param>
        /// <param name="email">The email address to set.</param>
        /// <param name="title">The title to set.</param>
        /// <param name="firstName">The first name to set.</param>
        /// <param name="surname">The surname to set.</param>
        /// <param name="address">The address to set.</param>
        /// <param name="phone">The phone number to set.</param>
        /// <param name="brand">The brand to set.</param>
        /// <param name="active">The active state to set.</param>
        /// <param name="paymentType">The payment type to set.</param>
        /// <param name="premium">The premium to set.</param>
        /// <param name="finance">The finance to set.</param>
        public Customer(
            string reference,
            string webReference,
            string email,
            string title,
            string firstName,
            string surname,
            Address address,
            string phone,
            string brand,
            bool active,
            string paymentType,
            Premium premium,
            Finance finance)
		{
            Reference = reference;
            WebReference = webReference;
            Email = email;
            Title = title;
            FirstName = firstName;
            Surname = surname;
            Address = address;
            Phone = phone;
            try
            {
                Brand = (Brand)Enum.Parse(typeof(Brand), brand, true);
            }
            catch (Exception)
            {
                throw new EnumParseNotFoundException(brand);
            }
            Active = active;
            PaymentType = paymentType;
            Premium = premium;
            Finance = finance;
        }
    }
}
