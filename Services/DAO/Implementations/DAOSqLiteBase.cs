using CGPost.Services.Services;
using SQLite;
using System.Reflection;
using System.Text;

namespace CGPost.Services.DAO.Implementations
{
    public abstract class DAOSqLiteBase
    {
        private IDatabaseLocationService _databaseLocationService;

        public DAOSqLiteBase(IDatabaseLocationService databaseLocationService)
        {
            _databaseLocationService = databaseLocationService;
        }

        private string DBPath => _databaseLocationService.DBPath;

        protected SQLiteConnection OpenConnection()
        {
            return new SQLiteConnection(DBPath, SQLiteOpenFlags.ReadWrite);
        }

        protected SQLiteCommand? GetUpdateCommand<T>(SQLiteConnection db, T model, string table, HashSet<string> selectionProperties)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if(properties.Length <= 0)
            {
                return null;
            }            
            var querryArguments = new Dictionary<string, string>();            
            string propertyGroup = string.Empty;
            var selectionClause = string.Empty;
            for (int i = 0; i < properties.Length; i++)
            {
                var escapedName = "@val" + i;
                var value = properties[i].GetValue(model);
                if (value != null)
                {
                    querryArguments.Add(escapedName, value.ToString()!);
                }
                else
                {
                    querryArguments.Add(escapedName, string.Empty);
                }
                if (selectionProperties.Contains(properties[i].Name))
                {
                    selectionClause += $"{properties[i].Name} = {escapedName} AND ";
                }
                else
                {
                    propertyGroup += $"{properties[i].Name} = {escapedName}, ";                    
                }                                                            
            }
            propertyGroup = propertyGroup.Substring(0, propertyGroup.Length - 2);            
            selectionClause = selectionClause.Substring(0, selectionClause.Length - 5);

            var command = db.CreateCommand($"UPDATE {table} SET {propertyGroup} WHERE {selectionClause}");
            foreach(var arg in querryArguments)
            {
                command.Bind(arg.Key, arg.Value);
            }            
            return command;
        }

        protected SQLiteCommand? GetInsertCommand<T>(SQLiteConnection db, T model, string table, HashSet<string>? excludeColumns = null)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return null;
            }
            var querryArguments = new Dictionary<string, string>();
            var columns = new StringBuilder();
            var values = new StringBuilder();
            for (int i = 0; i < properties.Length; i++)
            {
                if (excludeColumns!=null && excludeColumns.Contains(properties[i].Name))
                {
                    continue;
                }
                columns.Append(properties[i].Name);
                if(i<properties.Length - 1)
                {
                    columns.Append(',');
                }

                var escapedName = "@val" + i;

                values.Append(escapedName);
                if (i < properties.Length - 1)
                {
                    values.Append(',');
                }

                var value = properties[i].GetValue(model);
                if (value != null)
                {
                    querryArguments.Add(escapedName, value.ToString()!);
                }
                else
                {
                    querryArguments.Add(escapedName, string.Empty);
                } 
            }

            var command = db.CreateCommand($"INSERT INTO {table} ({columns}) VALUES ({values})");
            foreach (var arg in querryArguments)
            {
                command.Bind(arg.Key, arg.Value);
            }
            return command;
        }
    }
}
