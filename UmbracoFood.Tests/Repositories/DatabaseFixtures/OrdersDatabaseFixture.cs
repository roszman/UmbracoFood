using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using Moq;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Tests.Repositories.DatabaseFixtures
{
    public class OrdersDatabaseFixture : IDisposable
    {
        private DatabaseSchemaHelper _dbSchemaHelper;
        public UmbracoDatabase Db { get; set; }
        private SqlCeConnection _sqlCeConnection { get; set; }

        public OrdersDatabaseFixture()
        {
            _sqlCeConnection = TableDataHelper.SetSqlCeConnection();

            Db = new UmbracoDatabase(_sqlCeConnection, Mock.Of<ILogger>());

            _dbSchemaHelper = new DatabaseSchemaHelper(Db, Mock.Of<ILogger>(), new SqlCeSyntaxProvider());

            PrepareTables();
            FillTablesWithTestData();

        }

        private void PrepareTables()
        {
            DeleteTables();

            using (var db = new Database(Db.Connection.ConnectionString, "System.Data.SqlServerCe.4.0"))
            {
                db.Execute(@"CREATE TABLE [Statuses] (
                              [Id] int IDENTITY (1,1) NOT NULL
                            , [Name] nvarchar(50) NOT NULL
                            );");
                db.Execute(@" CREATE TABLE [Restaurants] (
                              [Id] int IDENTITY (1,1) NOT NULL
                            , [Name] nvarchar(255) NOT NULL
                            , [Phone] nvarchar(50) NOT NULL
                            , [Url] ntext NOT NULL
                            , [MenuUrl] ntext NOT NULL
                            , [Active] bit NOT NULL
                            );");
                db.Execute(@"CREATE TABLE [Orders] (
                              [Id] int IDENTITY (1,1) NOT NULL
                            , [Owner] nvarchar(100) NOT NULL
                            , [StatusId] int NOT NULL
                            , [RestaurantId] int NOT NULL
                            , [Deadline] datetime NOT NULL
                            , [EstimatedDeliveryTime] datetime NULL
                            );");
                db.Execute(@"CREATE TABLE [OrderedMeals] (
                              [Id] int IDENTITY (1,1) NOT NULL
                            , [Price] numeric(18,0) NOT NULL
                            , [MealName] nvarchar(255) NOT NULL
                            , [OrderId] int NOT NULL
                            , [Count] int NOT NULL
                            , [PurchaserName] nvarchar(100) NOT NULL
                            );");
                db.Execute(@"INSERT INTO [Statuses] ([Name]) VALUES (N'InProgress');");
                db.Execute(@"INSERT INTO [Statuses] ([Name]) VALUES (N'InDelivery');");
                db.Execute(@"INSERT INTO [Statuses] ([Name]) VALUES (N'InKitchen');");
                db.Execute(@"SET IDENTITY_INSERT [Restaurants] ON;");
                //db.Execute(@"INSERT INTO [Restaurants] ([Id],[Name],[Phone],[Url],[MenuUrl],[Active]) VALUES (1,N'Restauracja Michała',N'123456789',N'url',N'menu url',1);");
                db.Execute(@"SET IDENTITY_INSERT [Restaurants] OFF;");
                db.Execute(@"SET IDENTITY_INSERT [Orders] OFF;");
                db.Execute(@"SET IDENTITY_INSERT [OrderedMeals] OFF;");
                db.Execute(@"ALTER TABLE [Statuses] ADD CONSTRAINT [PK_Statuses] PRIMARY KEY ([Id]);");
                db.Execute(@"ALTER TABLE [Restaurants] ADD CONSTRAINT [PK_Restaurants] PRIMARY KEY ([Id]);");
                db.Execute(@"ALTER TABLE [Orders] ADD CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]);");
                db.Execute(@"ALTER TABLE [OrderedMeals] ADD CONSTRAINT [PK_OrderedMeals] PRIMARY KEY ([Id]);");
                db.Execute(@"ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Orders] FOREIGN KEY ([Id]) REFERENCES [Orders]([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;");
                db.Execute(@"ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Restaurants] FOREIGN KEY ([RestaurantId]) REFERENCES [Restaurants]([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;");
                db.Execute(@"ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Statuses] FOREIGN KEY ([StatusId]) REFERENCES [Statuses]([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;");
                db.Execute(@"ALTER TABLE [OrderedMeals] ADD CONSTRAINT [FK_OrderedMeals_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;");
            }
        }

        private void DeleteTables()
        {
            if (_dbSchemaHelper.TableExist("OrderedMeals"))
                _dbSchemaHelper.DropTable<OrderedMealPoco>();
            if (_dbSchemaHelper.TableExist("Orders"))
                _dbSchemaHelper.DropTable<OrderPoco>();
            if (_dbSchemaHelper.TableExist("Restaurants"))
                _dbSchemaHelper.DropTable<RestaurantPoco>();
            if (_dbSchemaHelper.TableExist("Statuses"))
                _dbSchemaHelper.DropTable<StatusPoco>();
        }

        private void FillTablesWithTestData()
        {
            using (var db = new Database(Db.Connection.ConnectionString, "System.Data.SqlServerCe.4.0"))
            {
                InsertRestaurants(db);

                var savedRestaurantPocos = db.Query<RestaurantPoco>("SELECT * FROM Restaurants");
                var statuses = db.Query<StatusPoco>("SELECT * FROM Statuses");
                InsertOrders(db, savedRestaurantPocos, statuses);

            }
        }

        private static void InsertOrders(Database db, IEnumerable<RestaurantPoco> savedRestaurantPocos, IEnumerable<StatusPoco> statuses)
        {
            List<OrderPoco> orderPocos = new List<OrderPoco>();
            foreach (var restaurantPoco in savedRestaurantPocos)
            {
                var o = new OrderPoco
                {
                    RestaurantId = restaurantPoco.ID,
                    Deadline = DateTime.Now,
                    EstimatedDeliveryTime = DateTime.Now,
                    OrderedMeals = null,
                    Owner = "user " + restaurantPoco.ID,
                    StatusId = statuses.First().Id
                };
                orderPocos.Add(o);
            }
            foreach (var orderPoco in orderPocos)
            {
                db.Insert(orderPoco);
            }

            var orders = db.Query<OrderPoco>("SELECT * FROM Orders");
            AddOrderedMealsToOrders(db, orders);
        }

        private static void AddOrderedMealsToOrders(Database db, IEnumerable<OrderPoco> orders)
        {
            foreach (var o in orders)
            {
                db.Insert("OrderedMeals", "Id", new OrderedMeal { MealName = "nazwa posiłku", Price = 2.5, PurchaserName = "Piotrek", OrderId = o.Id, Count = 1});
            }
        }

        private static void InsertRestaurants(Database db)
        {
            List<RestaurantPoco> restaurantPocos = new List<RestaurantPoco>();
            for (int i = 0; i < 10; i++)
            {
                var r = new RestaurantPoco
                {
                    IsActive = i % 2 == 0,
                    MenuUrl = "http://restauracja" + i + ".pl/menu",
                    WebsiteUrl = "http://restauracja" + i + ".pl",
                    Name = "restauracja " + i,
                    Phone = string.Concat(Enumerable.Repeat(i.ToString(), 9))
                };
                restaurantPocos.Add(r);
                //db.Execute(@"INSERT INTO [Restaurants] ([Id],[Name],[Phone],[Url],[MenuUrl],[Active]) VALUES (1,N'Restauracja Michała',N'123456789',N'url',N'menu url',1);");
            }

            foreach (var restaurantPoco in restaurantPocos)
            {
                db.Insert(restaurantPoco);
            }
        }

        public void Dispose()
        {
            DeleteTables();
        }
    }
}
