using UnityEngine;
using UnityEngine.Events;

namespace LovelyBytes.AssetVariables
{
    public abstract class MatchListener<T> : MonoBehaviour
    {
        public enum OperationType
        {
            Equal,
            Greater,
            Less,
            GreaterEqual,
            LessEqual,
            Always,
            Never
        }

        [SerializeField] 
        private OperationType _operation;

        [SerializeField] 
        private Variable<T> _source;

        [SerializeField] 
        private T _targetValue;

        [SerializeField]
        private UnityEvent _onMatch; 
        
        [SerializeField]
        private UnityEvent _onMismatch; 

        protected abstract int Compare(T a, T b);

        private void Awake()
        {
            _source.OnValueChanged += OnValueChanged;
        }

        private void OnDestroy()
        {
            _source.OnValueChanged -= OnValueChanged;
        }

        private void Start()
        {
            OnValueChanged(default, _source.Value);
        }

        private void OnValueChanged(T _, T value)
        {
            UnityEvent result = IsMatch(_targetValue, value) 
                ? _onMatch : _onMismatch;
            
            result?.Invoke();
        }

        private bool IsMatch(T a, T b)
        {
            int compare = Compare(a, b);

            return _operation switch
            {
                OperationType.Equal => compare == 0,
                OperationType.Greater => compare > 0,
                OperationType.Less => compare < 0,
                OperationType.GreaterEqual => compare >= 0,
                OperationType.LessEqual => compare <= 0,
                OperationType.Always => true,
                OperationType.Never => false,
                _ => false
            };
        }
    }
}
