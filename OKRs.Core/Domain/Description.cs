
namespace OKRs.Core.Domain
{
    public record Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Value = value;
        }

        public static implicit operator string(Description description) => description.Value;
    }
}
