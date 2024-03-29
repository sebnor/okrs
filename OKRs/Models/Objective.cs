﻿namespace OKRs.Models
{
    public class Objective
    {
        public Objective() { }

        public Objective(string title, Guid userId)
        {
            Title = title;
            UserId = userId;
            LastUpdated = DateTime.Now;
        }

        public void AddKeyResult(KeyResult keyResult)
        {
            KeyResults.Add(keyResult);
        }

        public bool RemoveKeyResult(KeyResult keyResult)
        {
            return KeyResults.Remove(keyResult);
        }

        public void Touch()
        {
            LastUpdated = DateTime.Now;
        }

        public string Title { get; set; }
        public Guid UserId { get; private set; }
        public Guid Id { get; private set; }
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime LastUpdated { get; private set; }
        public List<KeyResult> KeyResults { get; private set; } = new List<KeyResult>();
    }
}
