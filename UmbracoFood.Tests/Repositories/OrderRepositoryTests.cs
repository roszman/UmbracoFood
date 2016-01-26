﻿using System.Linq;
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
    public class OrderRepositoryTests : IClassFixture<OrdersDatabaseFixture>
    {
        private readonly OrdersDatabaseFixture _databaseFixture;
        private readonly Mock<IModelMapper<Order, OrderPoco>> _mapper;
        private readonly OrderRepository _repo;

        public OrderRepositoryTests(OrdersDatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            var dataBaseProvider = new Mock<IDatabaseProvider>();
            dataBaseProvider.Setup(dbp => dbp.Db).Returns(_databaseFixture.Db);
            _mapper = new Mock<IModelMapper<Order, OrderPoco>>();

            _repo = new OrderRepository(dataBaseProvider.Object, _mapper.Object);
        }

        [Fact]
        public void RemoveOrderShouldremoveOrderAndOrderedMeals()
        {
            //arrange


            //act
            _repo.RemoveOrder(1);

            //assert
            var orderFromDb = GetOrderPocoFromDbById(1);
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
                    PurchaserName = "purchaser name"
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
                Deadline = new DateTime(2016, 01, 28, 12, 00, 00),
                EstimatedDeliveryTime = new DateTime(2016, 01, 28, 13, 00, 00),
                OrderedMeals = orderedMeals,
                Owner = "owner",
                RestaurantId = restaurant.ID,
                StatusId = (int)OrderStatus.InProgress
            };

            _mapper.Setup(m => m.MapToPoco(It.IsAny<Order>())).Returns(orderPocoBeforDbInsert);
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
            _mapper.Setup(c => c.MapToDomain(It.IsAny<OrderPoco>()))
                    .Callback<OrderPoco>(o => mapperArgument = o)
                    .Returns(new Order());

            //act
            var orderFromRepo = _repo.GetOrder(orderPocoFromDb.Id);

            //assert
            _mapper.Verify(m => m.MapToDomain(It.IsAny<OrderPoco>()), Times.Once);
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
    }
}
