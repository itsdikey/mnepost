using CGPost.Base.Interfaces;
using CGPost.Models.Packages;

namespace CGPost.Services.Services
{
    public interface IUserPackagesService : IService
    {
        event Action<Package> PackageCreated;
        event Action<Package> PackageUpdated;
        event Action<int> PackageDeleted;
        event Action<int>? PackageArchived;

        Package AddPackage(Package package);
        void ArchivePackage(int id);
        List<Package>? GetPackages();
        void RemovePackage(int id);
        void UpdatePackage(Package package);
    }
}
