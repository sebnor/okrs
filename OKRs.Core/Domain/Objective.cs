namespace OKRs.Core.Domain
{
    public class Objective
    {
        public string Title { get; set; }
        public Guid UserId { get; private set; }
        public Guid Id { get; private set; }
        public DateTime Created { get; init; }
        public DateTime LastUpdated { get; private set; }
        public KeyResults KeyResults { get; init; }

        public Objective(string title, Guid userId)
        {
            Title = title;
            UserId = userId;
            KeyResults = new KeyResults(new HashSet<KeyResult>());
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
    }
}
