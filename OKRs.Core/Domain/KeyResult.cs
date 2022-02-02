namespace OKRs.Core.Domain
{
    public class KeyResult
    {
        public KeyResult()
        {
        }

        public void Touch()
        {
            LastUpdated = DateTime.Now; //TODO: solve with EF on add and update trigger
        }

        public Guid Id { get; private set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime LastUpdated { get; private set; } = DateTime.Now;
    }
}
