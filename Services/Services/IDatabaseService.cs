using CGPost.Base.Interfaces;

namespace CGPost.Services.Services
{
    public interface IDatabaseService : IService
    {
        void CreateOrUpgradeDatabase();
    }
}
