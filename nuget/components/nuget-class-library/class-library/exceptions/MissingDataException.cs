namespace nuget_class_library.class_library.exception
{
    /// <summary>
    /// Custom exception for missing customer data.
    /// </summary>
    public class MissingDataException : Exception
    {
        public MissingDataException()
        {
        }

        public MissingDataException(string message) : base(message)
        {
        }

        public MissingDataException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
