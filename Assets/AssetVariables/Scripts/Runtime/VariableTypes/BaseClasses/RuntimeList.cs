using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Scriptable object wrapper around a list, supporting all common list operations
    /// </summary>
    public class RuntimeList<TType> : ScriptableObject, IList<TType>
    {
        private readonly List<TType> _list = new();

        public IEnumerator<TType> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TType item)
            => _list.Add(item);

        public void Clear()
            => _list.Clear();

        public bool Contains(TType item) => _list.Contains(item);

        public void CopyTo(TType[] array, int arrayIndex)
            => _list.CopyTo(array, arrayIndex);

        public bool Remove(TType item)
            => _list.Remove(item);

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public int IndexOf(TType item)
            => _list.IndexOf(item);

        public void Insert(int index, TType item)
            => _list.Insert(index, item);

        public void RemoveAt(int index)
            => _list.RemoveAt(index);

        public TType this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
    }
}
