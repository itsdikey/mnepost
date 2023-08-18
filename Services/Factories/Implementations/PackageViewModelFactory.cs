using CGPost.Models.Packages;
using CGPost.Models.ViewModels;

namespace CGPost.Services.Factories.Implementations
{
    public sealed class PackageViewModelFactory : IPackageViewModelFactory
    {
        public PackageEntryViewModel CreatePackageViewModel(Package package)
        {
            var entryVM = new PackageEntryViewModel();
            UpdatePackageViewModel(entryVM, package);
            return entryVM;
        }

        public void UpdatePackageViewModel(PackageEntryViewModel viewModel, Package package)
        {
            viewModel.Id = package.id;
            viewModel.TrackingNumber = package.tracking_number;
            viewModel.Name = package.title;
            viewModel.LastTrackingInfo = package.last_tracking_info;
        }
    }
}
