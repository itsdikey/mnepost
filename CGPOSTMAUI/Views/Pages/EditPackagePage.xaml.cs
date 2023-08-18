using CGPost.ViewModels;
using NavigationServices.Interfaces;

namespace CGPost.Views.Pages;

public partial class EditPackagePage : ContentPage, IPage, IPageNavigationWithViewModel<EditPackageViewModel>
{
	public EditPackagePage(EditPackageViewModel editPackageViewModel)
	{
		InitializeComponent();
		BindingContext = editPackageViewModel;
	}
}