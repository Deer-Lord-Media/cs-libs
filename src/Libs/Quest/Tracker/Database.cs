using System;
using System.Collections;
using System.Collections.Generic;

namespace Quest.Tracker
{
    public class Database<T> : IDictionary<Guid, T>
    {
        private readonly Dictionary<Guid, T> _dict = new Dictionary<Guid, T>();

        // --- Indexer ---
        public T this[Guid key]
        {
            get => GetOrLoad(key);
            set => SetAndSave(key, value);
        }

        // --- Keys / Values ---
        public ICollection<Guid> Keys => _dict.Keys;
        public ICollection<T> Values => _dict.Values;

        // --- Count / IsReadOnly ---
        public int Count => _dict.Count;
        public bool IsReadOnly => false;

        // --- Core dictionary operations ---
        public void Add(Guid key, T value) => _dict.Add(key, value);
        public bool ContainsKey(Guid key) => _dict.ContainsKey(key);
        public bool Remove(Guid key) => _dict.Remove(key);
        public bool TryGetValue(Guid key, out T value)
        {
            return _dict.TryGetValue(key, out value);
        }

        // --- ICollection<KeyValuePair<TKey,TValue>> members (required by IDictionary) ---
        public void Add(KeyValuePair<Guid, T> item) =>
            ((ICollection<KeyValuePair<Guid, T>>)_dict).Add(item);

        public void Clear() => _dict.Clear();

        public bool Contains(KeyValuePair<Guid, T> item) =>
            ((ICollection<KeyValuePair<Guid, T>>)_dict).Contains(item);

        public void CopyTo(KeyValuePair<Guid, T>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<Guid, T>>)_dict).CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<Guid, T> item) =>
            ((ICollection<KeyValuePair<Guid, T>>)_dict).Remove(item);

        // --- Enumeration ---
        public IEnumerator<KeyValuePair<Guid, T>> GetEnumerator() => _dict.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private readonly Func<Guid, T> load;
        private readonly Func<T, bool> save;

        private T GetOrLoad(Guid id)
        {
            var loaded = _dict.TryGetValue(id, out T value);
            if (loaded) return _dict[id];
            _dict[id] = load(id);
            return _dict[id];
        }

        private void SetAndSave(Guid id, T value)
        {
            _dict[id] = value;
            var success = Save(id);
            // ensure update, this is important!
        }

        private bool Save(Guid id)
        {
            // TODO: handle this in the background
            return save(_dict[id]);
        }

        public Database(Func<Guid, T> load, Func<T, bool> save)
        {
            this.load = load;
            this.save = save;
        }
    }
}
