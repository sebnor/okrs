
namespace OKRs.Core.Domain
{
    public class Description
    {
        private Description()
        {
            Value = string.Empty;
        }

        public Description(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentOutOfRangeException(nameof(description));
            }

            Value = description;
        }

        public string Value { get; internal set; }
    }
}
