using CGPost.Base.Interfaces;

namespace CGPost.Services.Services
{
    public interface IDatabaseLocationService : IService
    {
        public string DBPath { get; }
        public int SQLVersion { get; }
    }
}
