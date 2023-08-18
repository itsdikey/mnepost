using CGPost.Services.Services;

namespace CGPost.Services
{
    public class DatabaseLocationService : IDatabaseLocationService
    {
        private string _sqlPath;
        private int _sqlVersion;
        public string DBPath => _sqlPath;

        public int SQLVersion => _sqlVersion;

        public DatabaseLocationService()
        {
            _sqlPath = Path.Combine(FileSystem.Current.AppDataDirectory, "sqlite/packages.db");
            _sqlVersion = 1;
        }
    }
}
