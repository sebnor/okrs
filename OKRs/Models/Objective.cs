using System;
using System.Collections.Generic;

namespace OKRs.Models
{
    public class Objective
    {
        public Objective(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
            Created = DateTime.Now;
            KeyResults = new List<KeyResult>();
        }

        public void AddKeyResult(KeyResult keyResult)
        {
            KeyResults.Add(keyResult);
        }

        public bool RemoveKeyResult(KeyResult keyResult)
        {
            return KeyResults.Remove(keyResult);
        }

        public List<KeyResult> KeyResults { get; private set; }
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public DateTime Created { get; private set; }
    }
}
