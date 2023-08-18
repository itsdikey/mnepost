using CGPost.Base.Interfaces;
using CGPost.Models.Packages;
using CGPost.Models.ViewModels;

namespace CGPost.Services.Factories
{
    public interface IPackageViewModelFactory : IService
    {
        PackageEntryViewModel CreatePackageViewModel(Package package);
        void UpdatePackageViewModel(PackageEntryViewModel viewModel, Package package);
    }
}
