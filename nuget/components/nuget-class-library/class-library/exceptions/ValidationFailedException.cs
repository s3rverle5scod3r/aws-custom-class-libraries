namespace nuget_class_library.class_library.exception
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException()
        {
        }

        public ValidationFailedException(string message) : base(message)
        {
        }

        public ValidationFailedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
