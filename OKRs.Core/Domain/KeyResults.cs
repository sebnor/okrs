using System.Collections;

namespace OKRs.Core.Domain
{
    public class KeyResults : ICollection<KeyResult>
    {
        private readonly HashSet<KeyResult> _keyResults;

        public KeyResults(HashSet<KeyResult> keyResults)
        {
            _keyResults = keyResults;
        }

        public KeyResults(IEnumerable<KeyResult> keyResults)
        {
            _keyResults = keyResults.ToHashSet();
        }

        public int Count => _keyResults.Count;

        public bool IsReadOnly => true;

        public void Add(KeyResult item) => _keyResults.Add(item);

        public void Clear() => _keyResults.Clear();

        public bool Contains(KeyResult item) => _keyResults.Contains(item);

        public void CopyTo(KeyResult[] array, int arrayIndex) => _keyResults.CopyTo(array, arrayIndex);

        public IEnumerator<KeyResult> GetEnumerator() => _keyResults.GetEnumerator();

        public bool Remove(KeyResult item) => _keyResults.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => _keyResults.GetEnumerator();
    }
}
