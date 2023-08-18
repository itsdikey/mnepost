using CGPost.Base;
using DKIMVVM;

namespace CGPost.ViewModels.IOC
{
    public static class InstallViewModelsExtensions
    {
        public static void InstallViewModels(this IKernel kernel)
        {
            var interfaceType = typeof(IViewModel);

            var implementations = typeof(InstallViewModelsExtensions).Assembly.GetTypes().Where(
                x => !x.IsInterface
                && !x.IsGenericType
                && !x.IsGenericTypeDefinition
                && interfaceType.IsAssignableFrom(x));

            foreach (var implementation in implementations)
            {
                //var @interface = implementation.GetInterfaces().FirstOrDefault(x => interfaceType.IsAssignableFrom(x) && x != interfaceType);
                //if (@interface == null)
                //    continue;

                kernel.BindSingleton(implementation);
            }
        }
    }
}
