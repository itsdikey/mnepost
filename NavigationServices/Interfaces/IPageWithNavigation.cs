using DKIMVVM;
namespace NavigationServices.Interfaces
{
    public interface IPageWithNavigation
    {

    }
    public interface IPageWithNavigation<T> : IPageWithNavigation where T : class
    {
        void Navigate(T parameter);
    }

    public interface IPageNavigationWithViewModel<T> : IPageWithNavigation where T : ViewModelBase
    {

    }
}
