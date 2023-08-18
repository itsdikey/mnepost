using CGPost.Models.Packages;
using CGPost.Services;
using CGPost.Services.DAO;
using CGPost.Services.Repositories;
using CGPost.Services.Services;
using DKIMVVM;
using NavigationServices.Services;
using System.Windows.Input;

namespace CGPost.ViewModels
{
    public sealed class EditPackageViewModel : ViewModelBase, IViewModel
    {
        private string? _name;
        private string? _trackingNumber;
        private bool _isButtonEnabled;
        private IUserPackagesService _userPackagesService;
        private INavigationService _navigationService;
        private ISelectedPackageRepositoryService _packageSelectedRepository;
        private IPackagesDAOSqLite _packageDAO;
        private IToastService _toastService;
        private Package? _package;

        public EditPackageViewModel(
            IUserPackagesService userPackagesService,
            INavigationService navigationService,
            ISelectedPackageRepositoryService packageSelectedRepository,
            IPackagesDAOSqLite packageDAO,
            IToastService toastService)
        {
            this._userPackagesService = userPackagesService;
            this._navigationService = navigationService;
            this._packageSelectedRepository = packageSelectedRepository;
            _packageDAO = packageDAO;
            _toastService = toastService;

            _packageSelectedRepository.PackageChanged += _packageSelectedRepository_PackageChanged;
            _packageSelectedRepository_PackageChanged(_packageSelectedRepository.Package);

            SavePackageCommand = new ActionCommandWithCheck((_) =>
            {
                if (_package != null)
                {
                    _package.title = Name;
                    _package.tracking_number = TrackingNumber;

                    _userPackagesService.UpdatePackage(_package);

                    _packageSelectedRepository.Package = _package.id;

                    _toastService.ShowToast("Changes Saved", Models.Enums.ToastDurations.Short);
                }

                _navigationService.NavigateWithViewModel<TrackingPageViewModel>();
            }, (_) =>
            {
                return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(TrackingNumber);
            });

            BackButtonCommand = new ActionCommand((_) =>
            {
                GoBack();
            });

            OnValidateButtonEnabled();
        }

        private void GoBack()
        {
            _navigationService.NavigateWithViewModel<TrackingPageViewModel>();
        }

        private void _packageSelectedRepository_PackageChanged(int id)
        {
            _package = _packageDAO.Read(_packageSelectedRepository.Package);

            if (_package != null)
            {
                TrackingNumber = _package.tracking_number;
                Name = _package.title;
            }
        }

        public ICommand SavePackageCommand { get; private set; }
        public ICommand BackButtonCommand { get; private set; }

        public string? Name { get => _name; set { SetProperty(ref _name, value); OnValidateButtonEnabled(); } }
        public string? TrackingNumber { get => _trackingNumber; set { SetProperty(ref _trackingNumber, value); OnValidateButtonEnabled(); } }

        private void OnValidateButtonEnabled()
        {
            IsButtonEnabled = !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(TrackingNumber);
        }

        public bool IsButtonEnabled { get => _isButtonEnabled; set => SetProperty(ref _isButtonEnabled, value); }
    }
}
