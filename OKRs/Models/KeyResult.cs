namespace OKRs.Models
{
    public class KeyResult
    {
        public KeyResult()
        {
            LastUpdated = DateTime.Now;
        }

        public void Touch()
        {
            LastUpdated = DateTime.Now; //TODO: solve with EF on add and update trigger
        }

        public Guid Id { get; private set; } // = Guid.NewGuid();
        public string Description { get; set; }
        public decimal CompletionRate { get; set; } = 0.0m;
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime LastUpdated { get; private set; }
    }
}