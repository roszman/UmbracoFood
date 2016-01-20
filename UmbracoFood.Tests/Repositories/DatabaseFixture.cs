using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using Moq;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Tests.Repositories
{
    public class DatabaseFixture : IDisposable
    {
        private DatabaseSchemaHelper _dbSchemaHelper;
        private string _fileName;
        public UmbracoDatabase Db { get; set; }

        public DatabaseFixture()
        {
            var conn = SqlCeConnection();

            Db = new UmbracoDatabase(conn, Mock.Of<ILogger>()); ;

            PrepareRestaurantTable(conn);
        }

        private void PrepareRestaurantTable(SqlCeConnection conn)
        {
            _dbSchemaHelper = new DatabaseSchemaHelper(Db, Mock.Of<ILogger>(), new SqlCeSyntaxProvider());
            if (_dbSchemaHelper.TableExist("Restaurants"))
            {
                _dbSchemaHelper.DropTable<RestaurantPoco>();
            }

            List<RestaurantPoco> restaurantPocos = new List<RestaurantPoco>();
            for (int i = 0; i < 10; i++)
            {
                restaurantPocos.Add(new RestaurantPoco
                {
                    IsActive = i%2 == 0,
                    MenuUrl = "http://restauracja" + i + ".pl/menu",
                    WebsiteUrl = "http://restauracja" + i + ".pl",
                    Name = "restauracja " + i,
                    Phone = string.Concat(Enumerable.Repeat(i.ToString(), 9))
                });
            }

            using (var db = new Database(conn.ConnectionString, "System.Data.SqlServerCe.4.0"))
            {
                db.Execute(@"CREATE TABLE [Restaurants] (
                          [Id] int IDENTITY(2, 1) NOT NULL
                        , [Name] nvarchar(255) NOT NULL
                        , [Phone] nvarchar(50) NOT NULL
                        , [Url] ntext NOT NULL
                        , [MenuUrl] ntext NOT NULL
                        , [Active] bit NOT NULL
                        ); ");

                foreach (var restaurantPoco in restaurantPocos)
                {
                    db.Insert(restaurantPoco);
                }
            }
        }

        private SqlCeConnection SqlCeConnection()
        {
            _fileName = System.IO.Path.Combine("./", "UmbracoTests.sdf");

            /* check if exists */
            if (File.Exists(_fileName))
                File.Delete(_fileName);
            string connStr = @"Data Source = " + _fileName;

            /* create Database */
            SqlCeEngine engine = new SqlCeEngine(connStr);
            engine.CreateDatabase();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            return conn;
        }

        public void Dispose()
        {
            _dbSchemaHelper.DropTable<RestaurantPoco>();
        }
    }
}
