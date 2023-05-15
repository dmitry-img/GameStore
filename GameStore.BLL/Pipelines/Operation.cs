using System;
using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Pipelines
{
    public class Operation<T> : IOperation<T>
    {
        private readonly Func<T, T> _func;

        public Operation(Func<T, T> func)
        {
            _func = func;
        }

        public T Invoke(T data) => _func(data);
    }
}
