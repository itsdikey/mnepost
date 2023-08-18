using CGPost.Base;

namespace CGPOSTMAUI.Kernel
{
    public sealed class Kernel : IKernel
    {
        private readonly IServiceCollection _serviceCollection;

        public Kernel(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void BindSingleton<Base, Implementation>() where Base : class where Implementation : class, Base
        {
            _serviceCollection.AddSingleton<Base, Implementation>();
        }

        public void BindSingleton(Type implementation)
        {
            _serviceCollection.AddSingleton(implementation);
        }

        public void BindSingleton(Type @interface, Type implementation)
        {
            _serviceCollection.AddSingleton(@interface, implementation);
        }

        public void BindSingleton(Type @interface, object implementation)
        {
            _serviceCollection.AddSingleton(@interface, implementation);
        }

        public void BindSingleton<Base>(object implementation)
        {
            _serviceCollection.AddSingleton(typeof(Base), implementation);
        }

        public object Resolve<T>()
        {
            return _serviceCollection.BuildServiceProvider().GetService<T>();
        }
    }
}
