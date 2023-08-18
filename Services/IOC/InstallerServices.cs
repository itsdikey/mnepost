using CGPost.Base;
using CGPost.Base.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace CGPost.Services.IOC
{
    public static class InstallerServices
    {
        public static void InstallServices(this IKernel kernel)
        {
            var interfaceType = typeof(IService);

            var implementations = typeof(InstallerServices).Assembly.GetTypes().Where(
                x => !x.IsInterface
                && !x.IsGenericType
                && !x.IsGenericTypeDefinition
                && interfaceType.IsAssignableFrom(x));

            foreach(var implementation in implementations) 
            {
                var @interface = implementation.GetInterfaces().FirstOrDefault(x=>interfaceType.IsAssignableFrom(x) && x!=interfaceType);
                if (@interface == null)
                    continue;

                kernel.BindSingleton(@interface, implementation);
            }
        }

        public static void InstallServicesFromAssemblyPool(this IKernel kernel, AssemblyPool assemblyPool)
        {
            var interfaceType = typeof(IService);

            var implementations = assemblyPool.GetTypes().Where(
                x => !x.IsInterface
                && !x.IsGenericType
                && !x.IsGenericTypeDefinition
                && interfaceType.IsAssignableFrom(x));

            foreach (var implementation in implementations)
            {
                var @interface = implementation.GetInterfaces().FirstOrDefault(x => interfaceType.IsAssignableFrom(x) && x != interfaceType);
                if (@interface == null)
                    continue;

                kernel.BindSingleton(@interface, implementation);
            }
        }

        public static void AddServices(this AssemblyPool assemblyPool)
        {
            assemblyPool.Assemblies.Add(typeof(InstallerServices).Assembly);
        }
    }
}
