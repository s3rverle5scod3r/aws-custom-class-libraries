namespace nuget_class_library.class_library.exception
{
    public class RequestHandlerException : Exception
    {
        public RequestHandlerException()
        {
        }

        public RequestHandlerException(string message) : base(message)
        {
        }

        public RequestHandlerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
