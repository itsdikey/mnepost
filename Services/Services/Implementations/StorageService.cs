using Newtonsoft.Json;

namespace CGPost.Services.Services.Implementations
{
    public sealed class StorageService : IStorageService
    {
        private string _folderPath;

        public StorageService()
        {
            _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User Store");
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        public async Task<T?> GetValue<T>(string key)
        {
          
            string filePath = Path.Combine(_folderPath, $"{key}.dat");
            if(!File.Exists(filePath))
            {
                return default(T);
            }
           
            return JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(filePath));
        }

        public T? GetValueSync<T>(string key)
        {

            string filePath = Path.Combine(_folderPath, $"{key}.dat");
            if (!File.Exists(filePath))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
        }

        public async Task SetValue<T>(string key, T value)
        {
            string filePath = Path.Combine(_folderPath, $"{key}.dat");
            
            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(value));
        }

        public void SetValueSync<T>(string key, T value)
        {
            string filePath = Path.Combine(_folderPath, $"{key}.dat");

            File.WriteAllText(filePath, JsonConvert.SerializeObject(value));
        }
    }
}
