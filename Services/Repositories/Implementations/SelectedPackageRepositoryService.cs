using CGPost.Models.Packages;
using CGPost.Models.ViewModels;

namespace CGPost.Services.Repositories.Implementations
{
    public sealed class SelectedPackageRepositoryService : ISelectedPackageRepositoryService
    {
        private int _packageId;

        public event Action<int>? PackageChanged;
        public void OnPackageChanged(int id)
        {
            PackageChanged?.Invoke(id);
        }
        public int Package 
        { 
            get => _packageId; 
            set 
            {
                _packageId = value; 
                OnPackageChanged(_packageId); 
            }
        }
    }
}
