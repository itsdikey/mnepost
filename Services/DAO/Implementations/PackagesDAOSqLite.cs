using CGPost.Models.Packages;
using CGPost.Services.Services;

namespace CGPost.Services.DAO.Implementations
{
    public class PackagesDAOSqLite : DAOSqLiteBase, IPackagesDAOSqLite
    {
        public PackagesDAOSqLite(IDatabaseLocationService locationService) : base(locationService)
        {

        }

        public Package? Create(Package? package)
        {
            using(var db = OpenConnection())
            {
                var cmd = GetInsertCommand(db, package, "items", new HashSet<string>() { "id" });
                cmd?.ExecuteNonQuery();
                cmd = db.CreateCommand("SELECT last_insert_rowid()");
                long lastRowID = cmd.ExecuteScalar<long>();
                package!.id = (int)lastRowID;
                db.Close();
                return package;
            }
        }

        public Package? Read(int id)
        {
            using (var db = OpenConnection())
            {
                var cmd = db.CreateCommand("SELECT * FROM items WHERE id=@id");
                cmd.Bind("@id", id);
                var list = cmd.ExecuteQuery<Package>();
                db.Close();
                return list[0];
            }
        }

        public List<Package>? ReadAll()
        {
            using (var db = OpenConnection())
            {
                var cmd = db.CreateCommand("SELECT * FROM items WHERE is_archived = 0");
                var list = cmd.ExecuteQuery<Package>();                
                db.Close();
                return list;
            }
        }

        public void RemovePackage(int id)
        {
            using (var db = OpenConnection())
            {
                var cmd = db.CreateCommand("DELETE FROM items WHERE id = @id");
                cmd.Bind("@id", id);
                cmd.ExecuteNonQuery();
                db.Close();
            }
        }

        public Package? Update(Package? package)
        {
            using (var db = OpenConnection())
            {
                var cmd = GetUpdateCommand(db, package, "items", new HashSet<string> { "id" });
                if (cmd == null)
                {
                    db.Close();
                    return package;
                }
                cmd.ExecuteNonQuery();
                db.Close();
                return package;
            }
        }
    }
}
