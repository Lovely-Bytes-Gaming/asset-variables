using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public enum WrapMode
    {
        Repeat = 0,
        Clamp = 1
    }
    
    public class OptionsList<TType> : ListObject<TType>
        where TType : IComparable<TType>
    {
        private const int DefaultSelection = 0;
        
        public event Action<TType, TType> OnSelectionChanged;
        public WrapMode WrapMode = WrapMode.Repeat;
        
        public int Index
        {
            get => _index;
            set => ChangeSelection(Wrapped(value));
        }

        public TType Current => Count > 0 ? this[_index] : default;
        
        [SerializeField, GetSet(nameof(Index))]
        private int _index = DefaultSelection;
        
        public void MoveToNext()
        {
            ++Index;
        }

        public void MoveToPrevious()
        {
            --Index;
        }

        public override void Add(TType item)
        {
            base.Add(item);

            // Auto select item if it is the only element in the list
            if (Count == 1)
                OnSelectionChanged?.Invoke(default, Current);
        }

        public override bool Remove(TType item)
        {
            int index = IndexOf(item);
            
            if (index > -1)
                RemoveAt(index);

            return index > -1;
        }
        
        public override void Insert(int index, TType item)
        {
            base.Insert(index, item);

            // Selection stays the same, but is moved one position to the right
            if (_index >= index)
                ++_index;
        }

        public override void RemoveAt(int index)
        {
            TType oldValue = Current;
            bool selectionWasRemoved = index == _index;
            
            base.RemoveAt(index);

            // Selection is moved one position to the left
            if (_index >= index)
                _index = Mathf.Max(0, _index - 1);

            // Notify listeners if the currently selected item has been removed
            if (selectionWasRemoved)
                OnSelectionChanged?.Invoke(oldValue, Current);
        }

        public override TType this[int index]
        {
            get => base[index];
            set
            {
                TType oldSelection = base[index];
                base[index] = value;
                
                // Notify listeners if the current selection was swapped out
                if (index == _index)
                    OnSelectionChanged?.Invoke(oldSelection, value);
            }
        }

        public override void Sort()
        {
            TType current = Current;
            base.Sort();
            _index = IndexOf(current);
        }

        public virtual void AddInOrder(TType item)
        {
            int index = List.BinarySearch(item);
            if (index < 0) index = ~1;
            List.Insert(index, item);
        }
        
        private int Wrapped(int index)
        {
            if (Count < 1)
                return 0;
            
            return WrapMode switch
            {
                WrapMode.Clamp => Mathf.Clamp(index, 0, Count-1),
                _ => HelperFunctions.RepeatIndex(index, Count)
            };
        }

        private void ChangeSelection(int newIndex)
        {
            TType oldValue = Current;
            _index = newIndex;
            TType newValue = Current;

            if(oldValue.CompareTo(newValue) != 0)
                OnSelectionChanged?.Invoke(oldValue, newValue);
        }
    }
}
