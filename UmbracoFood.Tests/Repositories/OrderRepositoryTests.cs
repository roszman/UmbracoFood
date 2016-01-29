using System.Linq;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Infrastructure.Repositories;
using UmbracoFood.Tests.Repositories.DatabaseFixtures;
using Xunit;
using Umbraco.Core.Persistence;
using System;
using System.Collections.Generic;

namespace UmbracoFood.Tests.Repositories
{
    [Collection("Database collection")]
    public class OrderRepositoryTests
    {
        private readonly DatababaseFixture _databaseFixture;
        private readonly Mock<IModelMapper<Order, OrderPoco>> _orderMapper;
        private readonly OrderRepository _repo;
        private readonly Mock<IModelMapper<OrderedMeal, OrderedMealPoco>> _mealMapper;

        public OrderRepositoryTests(DatababaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            var dataBaseProvider = new Mock<IDatabaseProvider>();
            dataBaseProvider.Setup(dbp => dbp.Db).Returns(_databaseFixture.Db);
            _orderMapper = new Mock<IModelMapper<Order, OrderPoco>>();
            _mealMapper = new Mock<IModelMapper<OrderedMeal, OrderedMealPoco>>();

            _repo = new OrderRepository(
                dataBaseProvider.Object,
                _orderMapper.Object,
                _mealMapper.Object);
        }

        [Fact]
        public void RemoveOrderShouldRemoveFromDbOrderAndOrderedMeals()
        {
            //arrange
            var existingOrder = GetOrderesPocoFromDb().Skip(1).Take(1).FirstOrDefault();

            //act
            _repo.RemoveOrder(existingOrder.Id);

            //assert
            var orderFromDb = GetOrderPocoFromDbById(existingOrder.Id);
            Assert.Null(orderFromDb);
        }

        [Fact]
        public void AddOrderShouldAddOrderToDb()
        {
            IList<OrderedMealPoco> orderedMeals = new List<OrderedMealPoco>()
            {
                new OrderedMealPoco
                {
                    MealName = "meal name",
                    Price = 14.2,
                    PurchaserName = "purchaser name",
                }
            };
            RestaurantPoco restaurant = new RestaurantPoco()
            {
                ID = 1,
                IsActive = true,
                MenuUrl = "menu url",
                Name = "name",
                Phone = "123445670",
                WebsiteUrl = "website url"
            };
            //arrange
            var orderPocoBeforDbInsert = new OrderPoco
            {
                AccountNumber = "1324564362367",
                Deadline = new DateTime(2016, 01, 28, 12, 00, 00),
                EstimatedDeliveryTime = new DateTime(2016, 01, 28, 13, 00, 00),
                OrderedMeals = orderedMeals,
                Owner = "owner",
                RestaurantId = restaurant.ID,
                StatusId = (int)OrderStatus.InProgress
            };

            _orderMapper.Setup(m => m.MapToPoco(It.IsAny<Order>())).Returns(orderPocoBeforDbInsert);
            //act
            var newOrderId =_repo.AddOrder(new Order());

            //assert
            var orderFromDb = GetOrderPocoFromDbById(newOrderId);
            Assert.NotNull(orderFromDb);
            Assert.Equal(orderPocoBeforDbInsert.Deadline, orderFromDb.Deadline);
            Assert.Equal(orderPocoBeforDbInsert.EstimatedDeliveryTime, orderFromDb.EstimatedDeliveryTime);
            Assert.Equal(orderPocoBeforDbInsert.OrderedMeals.Count, orderFromDb.OrderedMeals.Count);
            Assert.Equal(orderPocoBeforDbInsert.Owner, orderFromDb.Owner);
            Assert.Equal(orderPocoBeforDbInsert.StatusId, orderFromDb.Status.Id);
            Assert.Equal(orderPocoBeforDbInsert.RestaurantId, orderFromDb.Restaurant.ID);
        }

        [Fact]
        public void GetOrderShouldExecuteMapperWithCorrectData()
        {
            //arrange
            OrderPoco orderPocoFromDb = GetOrderPocoFromDbById(1);

            OrderPoco mapperArgument = new OrderPoco();
            _orderMapper.Setup(c => c.MapToDomain(It.IsAny<OrderPoco>()))
                    .Callback<OrderPoco>(o => mapperArgument = o)
                    .Returns(new Order());

            //act
            var orderFromRepo = _repo.GetOrder(orderPocoFromDb.Id);

            //assert
            _orderMapper.Verify(m => m.MapToDomain(It.IsAny<OrderPoco>()), Times.Once);
            Assert.Equal(orderPocoFromDb.Id, mapperArgument.Id);
            Assert.Equal(orderPocoFromDb.OrderedMeals.Count(), mapperArgument.OrderedMeals.Count());
            Assert.Equal(orderPocoFromDb.Owner, mapperArgument.Owner);
            Assert.Equal(orderPocoFromDb.EstimatedDeliveryTime, mapperArgument.EstimatedDeliveryTime);
            Assert.Equal(orderPocoFromDb.Deadline, mapperArgument.Deadline);


            Assert.Equal(orderPocoFromDb.Status.Id, mapperArgument.Status.Id);
            Assert.Equal(orderPocoFromDb.Status.Name, mapperArgument.Status.Name);

            Assert.Equal(orderPocoFromDb.RestaurantId, mapperArgument.RestaurantId);
            Assert.Equal(orderPocoFromDb.Restaurant.ID, mapperArgument.Restaurant.ID);
            Assert.Equal(orderPocoFromDb.Restaurant.IsActive, mapperArgument.Restaurant.IsActive);
            Assert.Equal(orderPocoFromDb.Restaurant.MenuUrl, mapperArgument.Restaurant.MenuUrl);
            Assert.Equal(orderPocoFromDb.Restaurant.Phone, mapperArgument.Restaurant.Phone);
            Assert.Equal(orderPocoFromDb.Restaurant.Name, mapperArgument.Restaurant.Name);
            Assert.Equal(orderPocoFromDb.Restaurant.WebsiteUrl, mapperArgument.Restaurant.WebsiteUrl);

        }

        [Fact]
        public void GetOrdersShouldReturnRightAmountOfOrders()
        {
            //arrange
            var ordersFromDb = GetOrderesPocoFromDb();

            _orderMapper.Setup(m => m.MapToDomain(It.IsAny<OrderPoco>())).Returns(new Order());

            //act
            var orders = _repo.GetOrders();

            //assert
            _orderMapper.Verify(m => m.MapToDomain(It.IsAny<OrderPoco>()), Times.Exactly(orders.Count()));
            Assert.Equal(ordersFromDb.Count(), orders.Count());
        }

        [Fact]
        public void AddOrderMealSholudAddMealToOrder()
        {
            //arrange
            var orderedMealPoco = new OrderedMealPoco
            {
                Count = 1,
                MealName = "special meal name",
                OrderId = 3,
                Price = 15.56,
                PurchaserName = "purchaser name"
            };
            _mealMapper
                .Setup(m => m.MapToPoco(It.IsAny<OrderedMeal>()))
                .Returns(orderedMealPoco);

            //act
            _repo.AddOrderedMeal(new OrderedMeal());

            //assert
            _mealMapper.Verify(m => m.MapToPoco(It.IsAny<OrderedMeal>()),
                Times.Once);
            var orderFromDb = GetOrderPocoFromDbById(3);
            var orderedMeal = orderFromDb.OrderedMeals
                .FirstOrDefault(om => om.MealName == orderedMealPoco.MealName);
            Assert.NotNull(orderFromDb);
        }

        [Fact]
        public void ChangeStatusShouldChangeStatusInDb()
        {
            //arrange

            //act
            _repo.ChangeStatus(5, OrderStatus.InDelivery);

            //assert
            var orderFromDb = GetOrderPocoFromDbById(5);
            Assert.Equal((int)OrderStatus.InDelivery, orderFromDb.Status.Id);
            Assert.Equal((int)OrderStatus.InDelivery, orderFromDb.StatusId);
        }

        [Fact]
        public void SetOrderIsInDeliveryShouldChangeEstimatedDeliveryTimeAdStatusInDb()
        {
            //arrange
            var date = new DateTime(2015, 12, 09, 11, 00, 00);

            //act
            _repo.SetOrderIsInDelivery(5, date);

            //assert
            var orderFromDb = GetOrderPocoFromDbById(5);
            Assert.Equal(date, orderFromDb.EstimatedDeliveryTime);
            Assert.Equal((int)OrderStatus.InDelivery, orderFromDb.StatusId);
        }

        [Fact]
        public void RemoveOrderedMealShouldRemoveOrderedMealFromRepo()
        {
            //arrange
            int id = 1;


            //act
            _repo.RemoveOrderedMeal(id);

            //assert
            var orderedMealFromDb = _databaseFixture.Db.Query<OrderedMeal>("SELECT * FROM OrderedMeals WHERE Id=@0", id)
                .FirstOrDefault();
            Assert.Null(orderedMealFromDb);

        }

        private OrderPoco GetOrderPocoFromDbById(int id)
        {
            return _databaseFixture.Db
                            .Fetch<OrderPoco, OrderedMealPoco, StatusPoco, RestaurantPoco, OrderPoco>(
                            new OrderRelator().MapIt,
                            "SELECT * FROM Orders"
                            + " LEFT JOIN OrderedMeals ON OrderedMeals.OrderId = Orders.Id"
                            + " LEFT JOIN Statuses ON Statuses.Id = Orders.StatusId"
                            + " LEFT JOIN Restaurants ON Restaurants.Id = Orders.RestaurantId"
                            + " WHERE Orders.Id = @0"
                            , id
                            ).FirstOrDefault();
        }

        private IEnumerable<OrderPoco> GetOrderesPocoFromDb()
        {
            return _databaseFixture.Db
                .Fetch<OrderPoco, OrderedMealPoco, StatusPoco, RestaurantPoco, OrderPoco>(
                    new OrderRelator().MapIt,
                    "SELECT * FROM Orders"
                    + " LEFT JOIN OrderedMeals ON OrderedMeals.OrderId = Orders.Id"
                    + " LEFT JOIN Statuses ON Statuses.Id = Orders.StatusId"
                    + " LEFT JOIN Restaurants ON Restaurants.Id = Orders.RestaurantId"
                    + " WHERE DATEADD(dd,0,DATEDIFF(dd,0,Orders.Deadline)) = DATEADD(dd,0,DATEDIFF(dd,0,GETDATE()))");
        }
    }
}
