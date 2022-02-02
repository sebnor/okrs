namespace OKRs.Core.Domain
{
    public record UserId
    {
        public UserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException(nameof(value));
            }

            Value = value;
        }

        public Guid Value { get; }
    }
}
