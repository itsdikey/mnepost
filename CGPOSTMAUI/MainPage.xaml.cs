using CGPost.ViewModels;
using CGPost.Views;
using NavigationServices.Interfaces;
using NavigationServices.Services;

namespace CGPOSTMAUI;

public partial class MainPage : ContentPage, IPage, IPageNavigationWithViewModel<PackagesPageViewModel>
{
	private INavigationService _navigationService;
    private PackagesPageViewModel _trackingViewModel;
    public MainPage(PackagesPageViewModel trackingViewModel)
    {
        Loaded += MainPage_Loaded;
        InitializeComponent();
        _trackingViewModel = trackingViewModel;
        this.BindingContext= _trackingViewModel;
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        _trackingViewModel.LoadData();
    }
}

