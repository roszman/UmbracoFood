using System.Data.SqlServerCe;
using System.IO;

namespace UmbracoFood.Tests.Repositories.DatabaseFixtures
{
    public static class TableDataHelper
    {
        private static string _fileName;

        public static SqlCeConnection SetSqlCeConnection()
        {
            DeleteTestDb();
            string connStr = @"Data Source = " + _fileName;

            /* create Database */
            SqlCeEngine engine = new SqlCeEngine(connStr);
            engine.CreateDatabase();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            return conn;
        }

        private static void DeleteTestDb()
        {
            _fileName = "UmbracoTests.sdf";

            /* check if exists */
            if (File.Exists(_fileName))
                File.Delete(_fileName);
        }
    }
}