using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Extensions
{
    public static class AsyncOperationExtension
    {
        public static AsyncOperationAwaiter GetAwaiter(this AsyncOperation asyncOperation)
            => new AsyncOperationAwaiter(asyncOperation);
    }

    public struct AsyncOperationAwaiter : INotifyCompletion
    {
        private readonly AsyncOperation _asyncOperation;

        public AsyncOperationAwaiter(AsyncOperation asyncOperation) : this() => 
            _asyncOperation = asyncOperation;

        public bool IsCompleted => _asyncOperation.isDone;

        public AsyncOperation GetResult() => _asyncOperation;

        public void OnCompleted(Action continuation) => 
            _asyncOperation.completed += _ => continuation?.Invoke();
    }
}
