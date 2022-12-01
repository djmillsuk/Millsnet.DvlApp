using Millsnet.DvlApp.Interfaces;
using System.Text.Json;

namespace Millsnet.DvlApp.Services
{
    public class DataService : IDataService
    {
        public DataService() { }
        public T Load<T>() where T : class
        {
            return Load<T>(typeof(T).Name);
        }
        public T Load<T>(string name) where T:class
        {
            string dataJson = Preferences.Get(name, JsonSerializer.Serialize(default(T)));
            return JsonSerializer.Deserialize<T>(dataJson);
        }
        public string Load(string name)
        {
            return Preferences.Get(name, JsonSerializer.Serialize(string.Empty));
        }
        public void Save<T>(T data)
        {
            Save<T>(data, typeof(T).Name);
        }
        public void Save<T>(T data, string name)
        {
            Preferences.Set(name, JsonSerializer.Serialize(data));
        }
    }
}