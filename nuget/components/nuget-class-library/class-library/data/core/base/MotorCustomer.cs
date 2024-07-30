namespace nuget_class_library.class_library.data.core
{
    public class MotorCustomer : Customer
    {
        /// <summary>
        /// Gets the Vehicle details, such as reg, etc.
        /// </summary>
        public Vehicle Vehicle { get; private set; }

        public MotorCustomer(
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
            Finance finance,
            Vehicle vehicle) 
            : base(reference, webReference, email, title, firstName, surname, address, phone, brand, active, paymentType, premium, finance)
        {
            Vehicle = vehicle;
        }
    }
}
