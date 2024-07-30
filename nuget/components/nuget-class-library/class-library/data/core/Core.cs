using nuget_class_library.class_library.data.enums;
using nuget_class_library.class_library.exception;

namespace nuget_class_library.class_library.data.core
{
    public class Core
    {
        ///<summary>
        /// Gets the ReferenceId
        ///</summary>
        public long ReferenceId { get; private set; }

        /// <summary>
        /// Gets the Reference.
        /// </summary>
        public string Reference { get; private set; }

        ///<summary>
        /// Gets the brand
        ///</summary>
        public Brand Brand { get; private set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Core"/> class.
        /// </summary>
        /// <param name="referenceId">The reference id to set.</param>
        /// <param name="reference">The reference to set.</param>
        /// <param name="brand">The brand to set.</param>
        public Core(
            long referenceId,
            string reference, 
            string brand)
        {
            ReferenceId = referenceId;
            Reference = reference;
            try
            {
                Brand = (Brand)Enum.Parse(typeof(Brand), brand, true);
            }
            catch (Exception)
            {
                throw new EnumParseNotFoundException(brand);
            }
        }
    }
}
