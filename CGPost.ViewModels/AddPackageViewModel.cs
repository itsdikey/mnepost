using CGPost.Services.Repositories;
using CGPost.Services.Services;
using DKIMVVM;
using NavigationServices.Services;
using System.Windows.Input;

namespace CGPost.ViewModels
{
    public sealed class AddPackageViewModel : ViewModelBase, IViewModel
    {
        private string? _name;
        private string? _trackingNumber;
        private bool _isButtonEnabled;
        private IUserPackagesService _userPackagesService;
        private INavigationService _navigationService;
        private ISelectedPackageRepositoryService _packageSelectedRepository;

        public AddPackageViewModel(IUserPackagesService userPackagesService, INavigationService navigationService, ISelectedPackageRepositoryService packageSelectedRepository)
        {
            this._userPackagesService = userPackagesService;
            this._navigationService = navigationService;
            this._packageSelectedRepository = packageSelectedRepository;

            AddPackageCommand = new ActionCommandWithCheck((_) =>
            {
                var package = _userPackagesService.AddPackage(new Models.Packages.Package()
                {
                    title = _name,
                    tracking_number = _trackingNumber
                });

                _packageSelectedRepository.Package = package.id;
                _navigationService.NavigateWithViewModel<TrackingPageViewModel>();
                Name = "";
                TrackingNumber = "";
            }, (_) =>
            {
                return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(TrackingNumber);
            }); 
            
            BackButtonCommand = new ActionCommand((_) =>
            {
                GoBack();
            });

            Name = "";
            TrackingNumber = "";
            OnValidateButtonEnabled();
        }
        private void GoBack()
        {
            _navigationService.NavigateWithViewModel<PackagesPageViewModel>();
        }
        public ICommand AddPackageCommand { get; private set; }
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
