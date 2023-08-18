using CGPost.Models.Packages;
using CGPost.Services.DAO;
using System.ComponentModel;

namespace CGPost.Services.Services.Implementations
{
    public sealed class UserPackageService : IUserPackagesService
    {
        private readonly IPackagesDAOSqLite _packageDAO;
        private readonly Dictionary<int, Package> _packagesById;


        public UserPackageService(IPackagesDAOSqLite packageDAO)
        {
            _packageDAO = packageDAO;
            _packagesById = new Dictionary<int, Package>();
        }

        public event Action<Package>? PackageCreated;
        public event Action<Package>? PackageUpdated;
        public event Action<int>? PackageDeleted;
        public event Action<int>? PackageArchived;

        public Package AddPackage(Package package)
        {
            var packageResponse = _packageDAO.Create(package);
            PackageCreated?.Invoke(packageResponse!);
            return packageResponse!;
        }

        public List<Package>? GetPackages()
        {
            var packages = _packageDAO.ReadAll();
            _packagesById.Clear();

            foreach(var package in packages!)
            {
                _packagesById.Add(package.id, package);
            }

            return packages;
        }

        public void RemovePackage(int id)
        {
            _packageDAO.RemovePackage(id);
            _packagesById.Remove(id);
            PackageDeleted?.Invoke(id);
        }

        public void UpdatePackage(Package package)
        {
            _packageDAO.Update(package);
            _packagesById[package.id] = package;
            PackageUpdated?.Invoke(package);
        }

        public void ArchivePackage(int id)
        {
            var package = _packagesById[id];
            package.is_archived = 1;
            _packageDAO.Update(package);
            _packagesById.Remove(package.id);
            PackageArchived?.Invoke(package.id);
        }
    }
}
