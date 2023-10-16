using System;
using System.Threading;

namespace LovelyBytes.AssetVariables
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message)
            : base(message) {}
    }
    
    internal class OperationValidator
    {
        private static class MainThread
        {
            // Explicit static constructor to prevent compiler from marking this type as 'beforefieldinit'.
            // This ensures that all field initializers will only be called on first access 
            static MainThread() 
            {  }
        
            internal static readonly int ID = Thread.CurrentThread.ManagedThreadId;
        }
        
        private readonly string _ownerName;
        private bool _isExecuting;

        public OperationValidator(string ownerName)
        {
            _ownerName = ownerName;
            _ = MainThread.ID;
        }

        public void PerformOperation(Action operation)
        {
            if (Thread.CurrentThread.ManagedThreadId != MainThread.ID)
                throw new ValidatorException($"{_ownerName}.Value can only be set on the main thread!");
            
            if (_isExecuting)
                throw new ValidatorException($"{_ownerName}.Value is set recursively. " +
                                             $"Did you try to set it in a method that listens to 'OnValueChanged'?");

            try
            {
                _isExecuting = true;
                operation?.Invoke();
            }
            finally
            {
                _isExecuting = false;
            }
        }
        
    }
}
