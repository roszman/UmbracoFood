using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class OrderDomainPocoMapperTests
    {
        private static readonly object sync = new object();
        private static bool configured;
        private OrderMapper mapper;

        public OrderDomainPocoMapperTests()
        {
            lock (sync)
            {
                if (!configured)
                {
                    Mapper.Reset();
                    Mapper.Initialize(config =>
                    {
                        config.AddProfile(new OrderMappingProfile());
                        config.AddProfile(new OrderedMealMappingProfile());
                        config.AddProfile(new RestaurantMappingProfile());
                        config.AddProfile(new StatusMappingProfile());
                    });
                    configured = true;
                    Mapper.AssertConfigurationIsValid();
                }
            }
            mapper = new OrderMapper();
        }

        [Fact]
        public void OrderPocoShouldBeMappedToOrder()
        {
            //Arrange
            var orderPoco = new OrderPoco();
            orderPoco.AccountNumber = "11 1111 1111 1111 1111";
            orderPoco.Deadline = DateTime.Now;
            orderPoco.EstimatedDeliveryTime = DateTime.Now.AddHours(1);
            orderPoco.Id = 999;
            orderPoco.OrderedMeals = new List<OrderedMealPoco>()
            {
                new OrderedMealPoco()
                {
                    Id = 10,
                    Count = 2,
                    Price = 21.32,
                    MealName = "Meal 1",
                    OrderId = 999,
                    PurchaserName = "Johnny Bravo"
                },
                new OrderedMealPoco()
                {
                    Id = 11,
                    Count = 3,
                    Price = 31.32,
                    MealName = "Meal 2",
                    OrderId = 999,
                    PurchaserName = "Voldemort"
                }
            };
            orderPoco.Owner = "Papa Smurf";
            orderPoco.Restaurant = new RestaurantPoco()
            {
                ID = 1
            };
            orderPoco.RestaurantId = 1;
            orderPoco.Status = new StatusPoco()
            {
                Id = 1
            };
            orderPoco.StatusId = 1;


            //Act
            var order = mapper.MapToDomain(orderPoco);

            //Assert
            Assert.Equal(order.Id, orderPoco.Id);
            Assert.Equal(order.Owner, orderPoco.Owner);
            Assert.Equal(order.Status, (OrderStatus) orderPoco.StatusId);
            Assert.Equal(order.AccountNumber, orderPoco.AccountNumber);
            Assert.Equal(order.Restaurant.ID, orderPoco.RestaurantId);
            Assert.Equal(order.Deadline, orderPoco.Deadline);
            Assert.Equal(order.EstimatedDeliveryTime, orderPoco.EstimatedDeliveryTime);
            Assert.Equal(order.OrderedMeals.Count, orderPoco.OrderedMeals.Count);
            Assert.IsType<Order>(order);
        }

        [Fact]
        public void OrderShouldBeMappedToOrderPooc()
        {
            //Arrange
            var order = new Order();
            order.AccountNumber = "11 1111 1111 1111 1111";
            order.Deadline = DateTime.Now;
            order.EstimatedDeliveryTime = DateTime.Now.AddHours(1);
            order.Id = 999;
            order.OrderedMeals = new List<OrderedMeal>()
            {
                new OrderedMeal()
                {
                    Id = 10,
                    Count = 2,
                    Price = 21.32,
                    MealName = "Meal 1",
                    OrderId = 999,
                    PurchaserName = "Johnny Bravo"
                },
                new OrderedMeal()
                {
                    Id = 11,
                    Count = 3,
                    Price = 31.32,
                    MealName = "Meal 2",
                    OrderId = 999,
                    PurchaserName = "Voldemort"
                }
            };
            order.Owner = "Papa Smurf";
            order.Restaurant = new Restaurant()
            {
                ID = 1
            };
            order.Status = OrderStatus.InKitchen;

            //Act
            var orderPoco = mapper.MapToPoco(order);

            //Assert
            Assert.Equal(orderPoco.Id, order.Id);
            Assert.Equal(orderPoco.Owner, order.Owner);
            Assert.Equal((OrderStatus) orderPoco.Status.Id, order.Status);
            Assert.Equal(orderPoco.AccountNumber, order.AccountNumber);
            Assert.Equal(orderPoco.Restaurant.ID, order.Restaurant.ID);
            Assert.Equal(orderPoco.Deadline, order.Deadline);
            Assert.Equal(orderPoco.EstimatedDeliveryTime, order.EstimatedDeliveryTime);
            Assert.Equal(orderPoco.OrderedMeals.Count, order.OrderedMeals.Count);
            Assert.IsType<OrderPoco>(orderPoco);
        }
    }
}