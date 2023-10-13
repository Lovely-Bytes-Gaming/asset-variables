using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    internal class SetOperationValidator<TType>
    {
        private readonly string _ownerName;
        private readonly Action<TType> _setOperation;
        private readonly Queue<TType> _pendingOperations = new();
        
        private bool _isRecursiveCallback;
        private int _recursionDepth;

        public SetOperationValidator(string ownerName, System.Action<TType> setOperation)
        {
            _ownerName = ownerName;
            _setOperation = setOperation;

            _ = MainThread.ID;
        }

        public void Reset()
        {
            _pendingOperations.Clear();
            _isRecursiveCallback = false;
            _recursionDepth = 0;
        }

        public void PerformSetOperation(TType value)
        {
            if (Thread.CurrentThread.ManagedThreadId != MainThread.ID)
            {
                Debug.LogError($"{GetType().Name}.Value can only be set on the main thread!");
                return;
            }
            
            while (true)
            {
                if (_isRecursiveCallback)
                {
                    DelayRecursiveSet(value);
                    return;
                }

                SetNext(value);

                if (_pendingOperations.Count > 0)
                {
                    value = _pendingOperations.Dequeue();
                    continue;
                }

                _recursionDepth = 0;
                break;
            }
        }

        private void DelayRecursiveSet(TType value)
        {
            if (_recursionDepth < AssetVariableConstants.MaxSetValueRecursionDepth)
            {
                ++_recursionDepth;
                _pendingOperations.Enqueue(value);
            }
            else
            {
                Debug.LogError($"Exceeded the recursion limit of {AssetVariableConstants.MaxSetValueRecursionDepth} " 
                               + $"while setting the Value of {_ownerName}! Maybe you are writing to it from within a method that listens to the OnValueChanged callback?");
            }
        }

        private void SetNext(TType value)
        {
            try
            {
                _isRecursiveCallback = true;
                _setOperation?.Invoke(value);
            }
            catch (Exception)
            {
                _recursionDepth = 0;
                _pendingOperations.Clear();
                throw;
            }
            finally
            {
                _isRecursiveCallback = false;
            }
        }
    }
}
