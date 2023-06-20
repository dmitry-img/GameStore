namespace GameStore.BLL.Interfaces
{
    public interface IConfigurationWrapper
    {
        string GetValue(string key);

        bool HasKey(string key);
    }
}
