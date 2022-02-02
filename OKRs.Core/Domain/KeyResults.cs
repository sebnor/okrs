using System.Collections;

namespace OKRs.Core.Domain
{
    public class KeyResults : IEnumerable<KeyResult>
    {
        private HashSet<KeyResult> _keyResults;

        public KeyResults(HashSet<KeyResult> keyResults)
        {
            _keyResults = keyResults;
        }

        public KeyResults(IEnumerable<KeyResult> keyResults)
        {
            _keyResults = keyResults.ToHashSet();
        }

        public IEnumerator<KeyResult> GetEnumerator()
        {
            return _keyResults.GetEnumerator();
        }

        public void Add(KeyResult keyResult)
        {
            _keyResults.Add(keyResult);
        }

        public bool Remove(KeyResult keyResult)
        {
            return _keyResults.Remove(keyResult);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
