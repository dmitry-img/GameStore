namespace GameStore.BLL.Interfaces
{
    public interface IOperation<T>
    {
        T Invoke(T data);
    }
}
