using System;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    public enum WrapMode
    {
        Repeat = 0,
        Clamp = 1
    }  
    
    public class OptionsList<TType> : RuntimeList<TType>
    {
        public event Action<TType, TType> OnSelectionChanged;
        public WrapMode WrapMode = WrapMode.Repeat;
        
        public int CurrentIndex
        {
            get => _currentIndex;
            private set => ChangeSelection(Wrapped(value));
        }

        public TType Current => Count > 0 ? this[_currentIndex] : default;

        public void MoveRight()
        {
            ++_currentIndex;
            _currentIndex = Wrapped(_currentIndex);
        }

        public void MoveLeft()
        {
            --_currentIndex;
            _currentIndex = Wrapped(_currentIndex);
        }

        private int _currentIndex = 0;
        
        private int Wrapped(int index)
        {
            if (Count < 1)
                return 0;
            
            return WrapMode switch
            {
                WrapMode.Clamp => Mathf.Clamp(_currentIndex, 0, Count),
                _ => RepeatIndex(_currentIndex)
            };
        }

        private void ChangeSelection(int newIndex)
        {
            if (_currentIndex == newIndex)
                return;
            
            TType oldValue = this[_currentIndex];
            TType newValue = this[newIndex];

            _currentIndex = newIndex;
            OnSelectionChanged?.Invoke(oldValue, newValue);
        }
        
        private int RepeatIndex(int index)
        {
            index %= Count;
            
            if (index < 0)
                index += Count;

            return index;
        }        
    }
}
