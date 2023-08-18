using System.Reflection;

namespace CGPost.Base
{
    public class AssemblyPool
    {
        private List<Type> _types;
        public List<Assembly> Assemblies { get; private set; }

        public AssemblyPool(params Assembly[] assemblies)
        {
            _types = new List<Type>();
            Assemblies = new List<Assembly>(assemblies);
        }

        public void LoadTypes()
        {
            foreach (var assembly in Assemblies)
            {
                _types.AddRange(assembly.GetTypes());
            }
        }

        public IReadOnlyList<Type> GetTypes() => _types;
    }
}
