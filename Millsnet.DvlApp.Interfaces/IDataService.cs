namespace Millsnet.DvlApp.Interfaces
{
    public interface IDataService
    {
        T Load<T>() where T : class;
        T Load<T>(string name) where T : class;
        string Load(string name);
        void Save<T>(T data);
        void Save<T>(T data, string name);
    }
}