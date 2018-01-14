using System;
using System.Collections.Generic;

namespace OKRs.Models
{
    public class Objective
    {
        public Objective(string title, Guid userId)
        {
            Title = title;
            UserId = userId;
        }

        public void AddKeyResult(KeyResult keyResult)
        {
            KeyResults.Add(keyResult);
        }

        public bool RemoveKeyResult(KeyResult keyResult)
        {
            return KeyResults.Remove(keyResult);
        }

        public string Title { get; set; }
        public Guid UserId { get; private set; }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime Created { get; private set; } = DateTime.Now;
        public List<KeyResult> KeyResults { get; private set; } = new List<KeyResult>();
    }
}
