using CGPost.Base.Interfaces;
using CGPost.Models.ViewModels;

namespace CGPost.Services.Repositories
{
    public interface ISelectedPackageRepositoryService : IService
    {
        int Package { get; set; }

        event Action<int>? PackageChanged;
    }
}
