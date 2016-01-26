using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using Moq;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Tests.Repositories.DatabaseFixtures
{
    public class RestaurantsDatabaseFixture : IDisposable
    {
        private DatabaseSchemaHelper _dbSchemaHelper;
        public UmbracoDatabase Db { get; set; }
        private SqlCeConnection _sqlCeConnection { get; set; }

        public RestaurantsDatabaseFixture()
        {
            _sqlCeConnection = TableDataHelper.SetSqlCeConnection();

            Db = new UmbracoDatabase(_sqlCeConnection, Mock.Of<ILogger>());

            _dbSchemaHelper = new DatabaseSchemaHelper(Db, Mock.Of<ILogger>(), new SqlCeSyntaxProvider());

            PrepareRestaurantTable();

        }

        private void PrepareRestaurantTable()
        {
            if (_dbSchemaHelper.TableExist("Restaurants"))
            {
                _dbSchemaHelper.DropTable<RestaurantPoco>();
            }

            List<RestaurantPoco> restaurantPocos = new List<RestaurantPoco>();
            for (int i = 0; i < 10; i++)
            {
                restaurantPocos.Add(new RestaurantPoco
                {
                    IsActive = i % 2 == 0,
                    MenuUrl = "http://restauracja" + i + ".pl/menu",
                    WebsiteUrl = "http://restauracja" + i + ".pl",
                    Name = "restauracja " + i,
                    Phone = string.Concat(Enumerable.Repeat(i.ToString(), 9))
                });
            }

            using (var db = new Database(Db.Connection.ConnectionString, "System.Data.SqlServerCe.4.0"))
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
        public void Dispose()
        {
            _dbSchemaHelper.DropTable<RestaurantPoco>();
        }
    }
}
