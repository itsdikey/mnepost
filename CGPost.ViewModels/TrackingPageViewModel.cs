using CGPost.Models.Packages;
using CGPost.Models.Tracking;
using CGPost.Models.ViewModels;
using CGPost.Services.DAO;
using CGPost.Services.Factories;
using CGPost.Services.Repositories;
using CGPost.Services.Services;
using DKIMVVM;
using NavigationServices.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CGPost.ViewModels
{
    public sealed class TrackingPageViewModel : ViewModelBase, IViewModel
    {
        private bool _isRefreshing;

        public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }

        private PackageEntryViewModel? _package;

        public PackageEntryViewModel? Package
        {
            get => _package;
            set
            {
                SetProperty(ref _package, value);
                OnPackageUpdated();
            }
        }

        private Package? _packageModel; 

        private async void OnPackageUpdated()
        {
            _data.Clear();
            if (_package == null || string.IsNullOrEmpty(_package.TrackingNumber))
            {
                _data.Add(TrackingDataPoint.Empty);
                return;
            }
            var data = await _trackingService.GetTrackingInfo(_package.TrackingNumber);
            if (data == null)
            {
                _data.Add(TrackingDataPoint.Empty);
                return;
            }
            if (data.TrackingEvents.Count == 0)
            {
                _data.Add(TrackingDataPoint.Empty);
                return;
            }

            foreach (var dataPoint in data.TrackingEvents)
            {
                _data.Add(dataPoint);
            }
            var lastData = _data.First();
            lastData.IsLast = true;
            if (_packageModel != null)
            {
                _packageModel.last_tracking_info = lastData.Description;
                _userPackagesService.UpdatePackage(_packageModel);
            }
        }

        private ObservableCollection<TrackingDataPoint> _data;

        public ObservableCollection<TrackingDataPoint> Data { get => _data; set => SetProperty(ref _data, value); }

        private readonly ITrackingService _trackingService;
        private readonly IPackagesDAOSqLite _packagesDAO;
        private readonly IPackageViewModelFactory _packageViewModelFactory;
        private readonly IUserPackagesService _userPackagesService;
        private readonly INavigationService _navigationService;

        public TrackingPageViewModel(
            ITrackingService trackingService,
            ISelectedPackageRepositoryService selectedPackageRepositoryService,
            IPackagesDAOSqLite packagesDAO,
            IPackageViewModelFactory packageViewModelFactory,
            IUserPackagesService userPackagesService,
            INavigationService navigationService)
        {
            _packagesDAO = packagesDAO;
            _packageViewModelFactory = packageViewModelFactory;
            _trackingService = trackingService;
            _userPackagesService = userPackagesService;
            _navigationService = navigationService;

            EditButtonCommand = new ActionCommand((_) =>
            {
                selectedPackageRepositoryService.Package = _packageModel!.id;
                navigationService.NavigateWithViewModel<EditPackageViewModel>();
            });

            BackButtonCommand = new ActionCommand((_) =>
            {
                GoBack();
            });

            _data = new ObservableCollection<TrackingDataPoint>();
            Package = null;
            selectedPackageRepositoryService.PackageChanged += SelectedPackageRepositoryService_PackageChanged;

            SelectedPackageRepositoryService_PackageChanged(selectedPackageRepositoryService.Package);
        }



        private void GoBack()
        {
            _navigationService.NavigateWithViewModel<PackagesPageViewModel>();
        }

        private void SelectedPackageRepositoryService_PackageChanged(int packageId)
        {
            var package = _packagesDAO.Read(packageId);
            _packageModel = package;
            if (package != null)
            {
                Package = _packageViewModelFactory.CreatePackageViewModel(package);
            }
        }

        public ICommand EditButtonCommand { get; private set; }

        public ICommand BackButtonCommand { get; private set; }
    }
}
