using CGPost.Base.Interfaces;
using CGPost.Models.Packages;

namespace CGPost.Services.DAO
{
    public interface IPackagesDAOSqLite : IService
    {
        public List<Package>? ReadAll();
        public Package? Read(int id);
        public Package? Update(Package? package);
        public Package? Create(Package? package);
        void RemovePackage(int id);
    }
}
