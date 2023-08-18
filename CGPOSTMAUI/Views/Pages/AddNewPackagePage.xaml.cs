using CGPost.ViewModels;
using NavigationServices.Interfaces;

namespace CGPost.Views.Pages;

public partial class AddNewPackagePage : ContentPage, IPage, IPageNavigationWithViewModel<AddPackageViewModel>
{
    public AddNewPackagePage(AddPackageViewModel addPackageViewModel)
    {
        InitializeComponent();
        BindingContext = addPackageViewModel;
    }
}