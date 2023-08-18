using Microsoft.VisualBasic;
using SQLite;
using System.Diagnostics;

namespace CGPost.Services.Services.Implementations
{
    public sealed class DatabaseService : IDatabaseService
    {

        private readonly IStorageService _storageService;
        private readonly IFileService _fileService;
        private readonly IDatabaseLocationService _databaseLocationService;

        private string DBPath => _databaseLocationService.DBPath;

        public DatabaseService(
            IStorageService storageService, 
            IDatabaseLocationService databaseLocationService, 
            IFileService fileService)
        {
            _storageService = storageService;
            _databaseLocationService = databaseLocationService;
            _fileService = fileService;
        }

        public void CreateOrUpgradeDatabase()
        {
            var version = _storageService.GetValueSync<int>("sqlVersion");
            try
            {
                if (File.Exists(_databaseLocationService.DBPath))
                {
                    if (version >= _databaseLocationService.SQLVersion)
                    {
                        return;
                    }
                    try
                    {
                        //upgrade routine
                        return;
                    }
                    catch
                    {
                        DeleteDatabaseFolder();
                        CreateDatabase();
                    }
                }
                else
                {
                    CreateDatabase();
                }
            }
            catch
            {
                DeleteDatabaseFolder();
                CreateDatabase();
            }
        }

        private void CreateDatabase()
        {
            var directory = Path.GetDirectoryName(DBPath);
            if (directory == null)
            {
                throw new DirectoryNotFoundException(DBPath);
            }
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            var result = ExecuteSQLFile("db/db.sql", SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.ReadWrite);

            _storageService.SetValueSync<int>("sqlVersion", _databaseLocationService.SQLVersion);
        }

        private bool ExecuteSQLFile(string resourcePath, SQLiteOpenFlags flags)
        {
            try
            {
                using var stream = _fileService.OpenPackageFile(resourcePath).Result;
                using var reader = new StreamReader(stream);
                var sql = reader.ReadToEnd();

                var commands = sql.Split(";");

                var db = new SQLiteConnection(DBPath, flags);

                foreach (var command in commands)
                {
                    if (string.IsNullOrWhiteSpace(command))
                    {
                        continue;
                    }
                    var cmd = db.CreateCommand(command);
                    cmd.ExecuteNonQuery();
                }
                db.Close();
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        private void DeleteDatabaseFolder()
        {
            var directory = Path.GetDirectoryName(DBPath);
            if (directory == null)
            {
                return;
            }
            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(directory);
        }
    }
}
