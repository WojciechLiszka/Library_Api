namespace Library_Api.Exceptions
{
    public class SystemError : Exception
    {
        public SystemError(string message) : base(message)
        {
        }
    }
}