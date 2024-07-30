namespace nuget_class_library.class_library.data.core
{
	/// <summary>
	/// Holds data regarding the vehicle insured on a policy for a customer object.
	/// </summary>
	public class Vehicle
	{
		/// <summary>
		/// Gets the VehicleRegistration.
		/// </summary>
		public string VehicleRegistration { get; private set; }

		/// <summary>
		/// Gets the Make.
		/// </summary>
		public string Make { get; private set; }

		/// <summary>
		/// Gets the Model.
		/// </summary>
		public string Model { get; private set; }

		/// <summary>
		/// Initialises a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		/// <param name="vehicleRegistration">The vehicle registration to set.</param>
		/// <param name="make">The make to set.</param>
		/// <param name="model">The model to set.</param>
		public Vehicle(
			string vehicleRegistration,
			string make,
			string model)
		{
			VehicleRegistration = vehicleRegistration;
			Make = make;
			Model = model;
		}
	}
}
