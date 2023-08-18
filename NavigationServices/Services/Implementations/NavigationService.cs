using CGPost.Base;
using DKIMVVM;
using NavigationServices.Interfaces;
using System.Reflection;

namespace NavigationServices.Services.Implementations
{
    public sealed class NavigationService : INavigationService
    {
        private Dictionary<Type,string> _routes= new Dictionary<Type,string>();
        private Dictionary<Type,string> _viewModelRoutes= new Dictionary<Type,string>();
        private Dictionary<string,bool> _needNavigationCalls = new Dictionary<string, bool>();
        private IKernel _kernel;

        public NavigationService(IKernel kernel, params Assembly[] assemblies)
        {
            var types = new List<Type>();
            var pageWithNavigationType = typeof(IPageWithNavigation);
            var pageWithNavigationGenericType = typeof(IPageWithNavigation<>);
            var pageWithViewModelGenericType = typeof(IPageNavigationWithViewModel<>);
            foreach (var assembly in assemblies)
            {
                var targetTypes = assembly.GetTypes().Where(x => !x.IsInterface && x.IsAssignableTo(pageWithNavigationType));
                types.AddRange(targetTypes);
            }

            foreach(var type in types)
            {
                var path = $"//{type.FullName}";
                _routes.Add(type, path);
                Routing.RegisterRoute(path, type);

                var genericInterface = type.GetInterfaces().Any(x=>x.IsGenericType && x.GetGenericTypeDefinition() == pageWithNavigationGenericType);
                if(genericInterface)
                {
                    _needNavigationCalls.Add(path, true);
                }
                else
                {
                    _needNavigationCalls.Add(path, false);
                }

                var genericViewModelInterface = type.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == pageWithViewModelGenericType);
                if (genericViewModelInterface!=null)
                {
                    var viewModel = genericViewModelInterface.GetGenericArguments()[0];
                    _viewModelRoutes.Add(viewModel, path);  
                }
            }
        }

        public async Task Navigate<T>() where T : IPageWithNavigation
        {
            var type = typeof(T);
            if(!_routes.ContainsKey(type))
            {
                throw new Exception($"No route registered for {type.Name}");
            }
            var route = _routes[type];
            await Shell.Current.GoToAsync(route);
        }

        public async Task Navigate<T,K>(K arg) where T : IPageWithNavigation<K> where K:class
        {
            var type = typeof(T);
            if (!_routes.ContainsKey(type))
            {
                throw new Exception($"No route registered for {type.Name}");
            }
            var route = _routes[type];
            var dictionary = new Dictionary<string, object>() { { "args", arg } };
            await Shell.Current.GoToAsync(route, true, dictionary);
        }

        public async Task NavigateWithViewModel<T>() where T : ViewModelBase
        {
            var type = typeof(T);
            if (!_viewModelRoutes.ContainsKey(type))
            {
                throw new Exception($"No route registered for {type.Name}");
            }
            var route = _viewModelRoutes[type];
            await Shell.Current.GoToAsync(route, true);
        }
    }
}
