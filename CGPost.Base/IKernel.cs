namespace CGPost.Base
{
    public interface IKernel
    {
        void BindSingleton<Base, Implementation>() where Base:class where Implementation : class, Base;
        void BindSingleton(Type implementation);
        void BindSingleton(Type @interface, Type implementation);
        void BindSingleton(Type @interface, object implementation);
        void BindSingleton<Base>(object implementation);
        object Resolve<T>();
    }
}
