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
                var r = new RestaurantPoco
                {
                    IsActive = i % 2 == 0,
                    MenuUrl = "http://restauracja" + i + ".pl/menu",
                    WebsiteUrl = "http://restauracja" + i + ".pl",
                    Name = "restauracja " + i,
                    Phone = string.Concat(Enumerable.Repeat(i.ToString(), 9))
                };
                restaurantPocos.Add(r);
            }

            using (var db = new Database(Db.Connection.ConnectionString, "System.Data.SqlServerCe.4.0"))
            {
                db.Execute(@"CREATE TABLE [Statuses] (
                              [Id] int IDENTITY (4,1) NOT NULL
                            , [Name] nvarchar(50) NOT NULL
                            );");
                db.Execute(@" CREATE TABLE [Restaurants] (
                              [Id] int IDENTITY (2,1) NOT NULL
                            , [Name] nvarchar(255) NOT NULL
                            , [Phone] nvarchar(50) NOT NULL
                            , [Url] ntext NOT NULL
                            , [MenuUrl] ntext NOT NULL
                            , [Active] bit NOT NULL
                            );");
                db.Execute(@"CREATE TABLE [Orders] (
                              [Id] int IDENTITY (2,1) NOT NULL
                            , [Owner] nvarchar(100) NOT NULL
                            , [StatusId] int NOT NULL
                            , [RestaurantId] int NOT NULL
                            , [Deadline] datetime NOT NULL
                            , [EstimatedDeliveryTime] datetime NULL
                            );");
                db.Execute(@"CREATE TABLE [OrderedMeals] (
                              [Id] int IDENTITY (2,1) NOT NULL
                            , [Price] numeric(18,0) NOT NULL
                            , [MealName] nvarchar(255) NOT NULL
                            , [OrderId] int NOT NULL
                            , [PurchaserName] nvarchar(100) NOT NULL
                            );");
                db.Execute(@"SET IDENTITY_INSERT [Statuses] ON;");
                //db.Execute(@"INSERT INTO [Statuses] ([Id],[Name]) VALUES (1,N'InProgress');");
                //db.Execute(@"INSERT INTO [Statuses] ([Id],[Name]) VALUES (2,N'InDelivery');");
                //db.Execute(@"INSERT INTO [Statuses] ([Id],[Name]) VALUES (3,N'InKitchen');");
                db.Execute(@"SET IDENTITY_INSERT [Statuses] OFF;");
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

                foreach (var restaurantPoco in restaurantPocos)
                {
                    db.Insert(restaurantPoco);
                }

                var savedRestaurantPocos = db.Query<RestaurantPoco>("SELECT * FROM Restaurants");
                List<OrderPoco> orderPocos = new List<OrderPoco>();
                foreach (var restaurantPoco in savedRestaurantPocos)
                {
                    var o = new OrderPoco
                    {
                        Restaurant = restaurantPoco,
                        Deadline = DateTime.Now,
                        EstimatedDeliveryTime = DateTime.Now,
                        OrderedMeals = new List<OrderedMealPoco>(),
                        Owner = "user " + restaurantPoco.ID,
                        Status = OrderStatus.InDelivery
                    };
                    orderPocos.Add(o);
                }
                foreach (var orderPoco in orderPocos)
                {
                    db.Insert(orderPoco);
                }

            }
        }
        public void Dispose()
        {
            _dbSchemaHelper.DropTable<RestaurantPoco>();
            _dbSchemaHelper.DropTable<OrderPoco>();
            _dbSchemaHelper.DropTable<OrderedMealPoco>();
            _dbSchemaHelper.DropTable<StatusPoco>();
        }
    }
}
