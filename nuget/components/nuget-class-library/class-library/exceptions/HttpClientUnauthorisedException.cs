namespace nuget_class_library.class_library.exception
{
    public class HttpClientUnauthorisedException : Exception
    {
        public HttpClientUnauthorisedException()
        {
        }

        public HttpClientUnauthorisedException(string message) : base(message)
        {
        }

        public HttpClientUnauthorisedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}

