using CGPost.ViewModels;
using NavigationServices.Interfaces;

namespace CGPost.Views.Pages;

public partial class TrackingPage : ContentPage, IPage, IPageNavigationWithViewModel<TrackingPageViewModel>
{
    public TrackingPage(TrackingPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}