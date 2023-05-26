using System.Collections.Generic;
using GameStore.BLL.Interfaces;

namespace GameStore.BLL.Pipelines
{
    public class Pipeline<T> : IOperation<T>
    {
        private readonly List<IOperation<T>> _operations = new List<IOperation<T>>();

        public void Register(IOperation<T> operation)
        {
            _operations.Add(operation);
        }

        public T Invoke(T data)
        {
            foreach (var operation in _operations)
            {
                data = operation.Invoke(data);
            }

            return data;
        }
    }
}
