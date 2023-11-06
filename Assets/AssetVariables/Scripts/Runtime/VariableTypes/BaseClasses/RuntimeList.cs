using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Scriptable object wrapper around a list, supporting all common list operations
    /// </summary>
    public class RuntimeList<TType> : ScriptableObject, IList<TType>, IReadOnlyList<TType>
    {
        protected readonly List<TType> List = new();

        public IEnumerator<TType> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TType item)
            => List.Add(item);

        public void Clear()
            => List.Clear();

        public bool Contains(TType item) => List.Contains(item);

        public void CopyTo(TType[] array, int arrayIndex)
            => List.CopyTo(array, arrayIndex);

        public bool Remove(TType item)
            => List.Remove(item);

        public int Count => List.Count;

        public bool IsReadOnly => false;

        public int IndexOf(TType item)
            => List.IndexOf(item);

        public void Insert(int index, TType item)
            => List.Insert(index, item);

        public void RemoveAt(int index)
            => List.RemoveAt(index);

        public TType this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }
    }
}
