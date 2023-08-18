using CGPost.Base.Interfaces;

namespace CGPost.Services.Services
{
    public interface IStorageService : IService
    {
        Task SetValue<T>(string key, T value);
        Task<T?> GetValue<T>(string key);
        T? GetValueSync<T>(string key);
        void SetValueSync<T>(string key, T value);
    }
}
