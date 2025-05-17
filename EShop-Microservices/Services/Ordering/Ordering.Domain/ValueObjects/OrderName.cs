namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        public const int DefaultLength = 5;
        public string Value { get; set; }
        private OrderName(string value) => Value = value;
        private static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

            return new OrderName(value);
        }
    }
}
