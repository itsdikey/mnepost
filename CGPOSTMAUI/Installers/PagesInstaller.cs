using CGPost.Base;
using CGPost.Views;

namespace CGPost.Installers
{
    public static class PagesInstaller
    {
        public static IKernel AddPages(this IKernel kernel)
        {
            var interfaceType = typeof(IPage);

            var implementations = interfaceType.Assembly.GetTypes().Where(
                x => !x.IsInterface
                && !x.IsGenericType
                && !x.IsGenericTypeDefinition
                && interfaceType.IsAssignableFrom(x));

            foreach(var implementation in implementations)
            {
                kernel.BindSingleton(implementation);
            }

            return kernel;
        }
    }
}
