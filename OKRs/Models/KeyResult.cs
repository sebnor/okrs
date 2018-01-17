using System;

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
            LastUpdated = DateTime.Now;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Description { get; set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime LastUpdated { get; private set; }
    }
}