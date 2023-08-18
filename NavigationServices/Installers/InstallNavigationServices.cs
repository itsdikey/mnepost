using CGPost.Base;
using NavigationServices.Services;
using NavigationServices.Services.Implementations;
using System.Reflection;

namespace NavigationServices.Installers
{
    public static class InstallNavigationServicesExt
    {
        public static void InstallNavigationServices(this IKernel kernel, params Assembly[] pageLookUpAssemblies)
        {
            var navigationService = new NavigationService(kernel, pageLookUpAssemblies);
            kernel.BindSingleton<INavigationService>(navigationService);
        }
    }
}
