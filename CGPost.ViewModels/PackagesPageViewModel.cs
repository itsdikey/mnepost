using CGPost.Models.Packages;
using CGPost.Models.ViewModels;
using CGPost.Services;
using CGPost.Services.Factories;
using CGPost.Services.Repositories;
using CGPost.Services.Services;
using DKIMVVM;
using NavigationServices.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CGPost.ViewModels
{
    public sealed class PackagesPageViewModel : ViewModelBase, IViewModel
    {
        #region Properties

        private ObservableCollection<PackageEntryViewModel>? _packages;
        public ObservableCollection<PackageEntryViewModel>? Packages
        {
            get => _packages;
            set => SetProperty(ref _packages, value);
        }

        private Dictionary<int, PackageEntryViewModel> _packagesById;

        #endregion
        public ICommand ClickedOnItemCommand { get; private set; }
        public ICommand AddPackageCommand { get; set; }
        public ICommand DeletePackageCommand { get; private set; }
        public ICommand ArchivePackageCommand { get; private set; }

        private readonly IUserPackagesService _userPackagesService;
        private readonly IPackageViewModelFactory _packageViewModelFactory;
        private readonly INavigationService _navigationService;
        private readonly IToastService _toastService;

        public PackagesPageViewModel(
            ISelectedPackageRepositoryService selectedPackageRepositoryService,
            IUserPackagesService userPackagesService,
            INavigationService navigationService,
            IPackageViewModelFactory packageViewModelFactory,
            IToastService toastService)
        {
            _userPackagesService = userPackagesService;
            _navigationService = navigationService;
            _packageViewModelFactory = packageViewModelFactory;
            _toastService = toastService;

            _packagesById = new Dictionary<int, PackageEntryViewModel>();

            ClickedOnItemCommand = new ActionCommand<PackageEntryViewModel>((p) =>
            {
                if (p != null)
                {
                    selectedPackageRepositoryService.Package = p.Id;
                    navigationService.NavigateWithViewModel<TrackingPageViewModel>();
                }
            });

            AddPackageCommand = new ActionCommandAsync(async () =>
            {
                await navigationService.NavigateWithViewModel<AddPackageViewModel>();
            });

            DeletePackageCommand = new ActionCommandAsync<PackageEntryViewModel>(async (p) =>
            {
                if (p == null)
                    return;
                userPackagesService.RemovePackage(p.Id);
                await _toastService.ShowToast("Deleted package", Models.Enums.ToastDurations.Short);
            });

            ArchivePackageCommand = new ActionCommandAsync<PackageEntryViewModel>(async (p) =>
            {
                if (p == null)
                    return;
                _userPackagesService.ArchivePackage(p.Id);
                await _toastService.ShowToast("Archived package", Models.Enums.ToastDurations.Short);
            });

            _userPackagesService.PackageCreated += UserPackagesService_PackageCreated;
            _userPackagesService.PackageUpdated += UserPackagesService_PackageUpdated;
            _userPackagesService.PackageDeleted += UserPackagesService_PackageDeleted;
            _userPackagesService.PackageArchived += _userPackagesService_PackageArchived;

            LoadData();
        }

        private void _userPackagesService_PackageArchived(int id)
        {
            if (Packages != null)
            {
                for (int i = 0; i < Packages.Count; i++)
                {
                    if (Packages[i].Id == id)
                    {
                        Packages.RemoveAt(i);
                        break;
                    }
                }
            }

            _packagesById.Remove(id);
        }

        private void UserPackagesService_PackageDeleted(int id)
        {
            if (Packages != null)
            {
                for (int i = 0; i < Packages.Count; i++)
                {
                    if (Packages[i].Id == id)
                    {
                        Packages.RemoveAt(i);
                        break;
                    }
                }
            }

            _packagesById.Remove(id);
        }

        private void UserPackagesService_PackageUpdated(Package package)
        {
            _packageViewModelFactory.UpdatePackageViewModel(_packagesById[package.id], package);
        }

        private void UserPackagesService_PackageCreated(Package package)
        {
            AddPackageViewModel(package);
        }

        public void LoadData()
        {
            if (Packages == null)
            {
                Packages = new ObservableCollection<PackageEntryViewModel>();
            }

            var packages = _userPackagesService.GetPackages();

            if(packages != null)
            {
                _packagesById.Clear();
                Packages.Clear();
                foreach (var package in packages)
                {
                    AddPackageViewModel(package);
                }
            }
        }

        private void AddPackageViewModel(Package package)
        {
            var packageViewModel = _packageViewModelFactory.CreatePackageViewModel(package);
            Packages?.Add(packageViewModel);
            _packagesById.Add(package.id, packageViewModel);
        }
    }
}
