using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    /// <summary>
    /// Scriptable object wrapper around a list, supporting all common list operations
    /// </summary>
    public class ListObject<TType> : ScriptableObject, IList<TType>, IReadOnlyList<TType>
    {
        [SerializeField]
        protected List<TType> List;

        public IEnumerator<TType> GetEnumerator() => List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public virtual void Add(TType item)
            => List.Add(item);

        public virtual void Clear()
            => List.Clear();

        public bool Contains(TType item) => List.Contains(item);

        public void CopyTo(TType[] array, int arrayIndex)
            => List.CopyTo(array, arrayIndex);

        public virtual bool Remove(TType item)
            => List.Remove(item);

        public int Count => List.Count;

        public bool IsReadOnly => false;

        public int IndexOf(TType item)
            => List.IndexOf(item);

        public virtual void Insert(int index, TType item)
            => List.Insert(index, item);

        public virtual void RemoveAt(int index)
            => List.RemoveAt(index);

        public virtual TType this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }
    }
}
