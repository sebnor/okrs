namespace OKRs.Core.Domain
{
    public class KeyResult
    {
        public KeyResult(Description description)
        {
            Description = description;
        }

        public void Touch()
        {
            LastUpdated = DateTime.Now; //TODO: solve with EF on add and update trigger
        }

        public Guid Id { get; init; }
        public Description Description { get; private set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime LastUpdated { get; private set; } = DateTime.Now;

        public void ChangeDescriptionTo(Description newDescription)
        {
            Description = newDescription;
            Touch();
        }
    }
}
