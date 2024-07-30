namespace nuget_class_library.class_library.exception
{
    public class EnumParseNotFoundException : Exception
    {
        public EnumParseNotFoundException(string enumValue) : base($"A matching enum value could not be found for this record during transform. Value received was {enumValue}.")
        {
        }
    }
}
