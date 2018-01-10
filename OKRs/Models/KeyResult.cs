using System;

namespace OKRs.Models
{
    public class KeyResult
    {
        public KeyResult()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}