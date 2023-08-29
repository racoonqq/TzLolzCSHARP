using System.Data.SQLite;
using System.Data.Common;

namespace Database
{
    public class DbManager {
        private static string DataBaseName;
        public static string connection_string;
        public static SQLiteConnection GamesConnect;
        public DbManager(string DbPath)
        {
            DataBaseName = DbPath;
        }

        public void Init()
        {
            //SQLiteConnection.CreateFile(DataBaseName);

            SQLiteFactory Factory = (SQLiteFactory)DbProviderFactories.GetFactory("System.Data.SQLite");

            using (SQLiteConnection Connection = (SQLiteConnection)Factory.CreateConnection())
            {
                Connection.ConnectionString = "Data Source=" + DataBaseName;
                connection_string = Connection.ConnectionString;
                GamesConnect = Connection;

                Connection.Open();

                using(SQLiteCommand Query = new SQLiteCommand(Connection))
                {
                    Query.CommandText = @"CREATE TABLE IF NOT EXISTS all_games (id INTEGER PRIMARY KEY, name TEXT NOT NULL UNIQUE, companyName TEXT, CreatedAt TEXT)";

                    Query.ExecuteNonQuery();
                }
                Connection.Close();
            }
        }

        public static SQLiteConnection GetConnection()
        {
            GamesConnect = new SQLiteConnection("Data Source=" + DataBaseName);
            GamesConnect.Open();

            return GamesConnect;
        }
    }
}
