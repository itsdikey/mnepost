using CGPost.Base.Interfaces;
using DKIMVVM;
using NavigationServices.Interfaces;

namespace NavigationServices.Services
{
    public interface INavigationService : IService
    {
        Task Navigate<T>() where T : IPageWithNavigation;
        Task Navigate<T, K>(K arg)
            where T : IPageWithNavigation<K>
            where K : class;
        Task NavigateWithViewModel<T>() where T : ViewModelBase;
    }
}
