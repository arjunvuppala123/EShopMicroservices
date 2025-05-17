namespace Ordering.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string Message) : base($"Domain Exception: \"{Message} \" throws from Domain Layer")
        { }
    }
}
