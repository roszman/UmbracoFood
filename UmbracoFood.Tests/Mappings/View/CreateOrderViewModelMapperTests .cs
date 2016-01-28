using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.ViewModels;
using Xunit;
using OrderViewModelMapperProfile = UmbracoFood.Mapping.OrderViewModelMapperProfile;
using UmbracoFood.Core.Extensions;

namespace UmbracoFood.Tests.Mappings
{
    public class OrderViewModelMapperTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;

        public OrderViewModelMapperTests()
        {
            lock (Sync)
            {
                if (!_configured)
                {
                    Mapper.Reset();

                    Mapper.Initialize(config => config.AddProfile(new OrderViewModelMapperProfile()));

                    _configured = true;
                    Mapper.AssertConfigurationIsValid();
                }
            }
        }

        [Fact]
        public void CreateOrderViewModelShouldBeMappedToOrder()
        {
            //Arrange
            var createOrderViewModel = new CreateOrderViewModel();
            createOrderViewModel.AccountNumber = "11 1111 1111 1111 1111";
            createOrderViewModel.Deadline = new DateTime();
            createOrderViewModel.Meals = new List<CreateOrderMeal>()
            {
                new CreateOrderMeal()
                {
                    Name = "Meal 1",
                    Count = 2,
                    Price = 10
                },
                new CreateOrderMeal()
                {
                    Name = "Meal 2",
                    Count = 1,
                    Price = 15
                }
            };
            createOrderViewModel.Owner = "Michał";
            createOrderViewModel.SelectedRestaurantId = 1;

            //Act
            var order = Mapper.DynamicMap<CreateOrderViewModel, Order>(createOrderViewModel);

            //Assert
            Assert.Equal(order.AccountNumber, createOrderViewModel.AccountNumber);
            Assert.Equal(order.Deadline, createOrderViewModel.Deadline);
            Assert.Equal(order.EstimatedDeliveryTime, null);
            Assert.Equal(order.Id, 0);
            Assert.Equal(order.Owner, createOrderViewModel.Owner);
            Assert.Equal(order.Restaurant.ID, createOrderViewModel.SelectedRestaurantId);
            Assert.Equal(order.Status, OrderStatus.InProgress);
            Assert.Equal(order.OrderedMeals.Count, createOrderViewModel.Meals.Count());
            Assert.IsType<Order>(order);
        }

        [Fact]
        public void OrderShouldBeMappedToOrderViewModel()
        {
            //arrange
            var order = new Order
            {
                Id = 569,
                AccountNumber = "111 1111 1111 1111",
                Deadline = DateTime.Now,
                EstimatedDeliveryTime = DateTime.Now,
                OrderedMeals = new List<OrderedMeal>
                 {
                     new OrderedMeal
                     {
                         Count = 4,
                         Id = 3245,
                         MealName = "meal name",
                         OrderId = 345,
                         Price = 234.23,
                         PurchaserName = "purchaser name"
                     }
                 },
                Owner = "owner",
                Restaurant = new Restaurant
                {
                    ID = 345,
                    IsActive = true,
                    MenuUrl = "menu url",
                    Name = "restaurant name",
                    Phone = "1232456",
                    WebsiteUrl = "website url"
                },
                Status = OrderStatus.InDelivery
            };

            //act
            var orderViewModel = Mapper.DynamicMap<Order, OrderViewModel>(order);

            //assert
            Assert.Equal(order.Id, orderViewModel.Id);
            Assert.Equal(order.Owner, orderViewModel.Owner);
            Assert.Equal(order.Deadline, orderViewModel.Deadline);
            Assert.Equal(order.EstimatedDeliveryTime, orderViewModel.EstitmatedDeliveryTime);
            Assert.Equal(order.OrderedMeals.Count, orderViewModel.MealsCount);
            Assert.Equal(order.Status.GetDescription(), orderViewModel.StatusName);
            Assert.Equal(order.Restaurant.Name, orderViewModel.RestaurantName);
            Assert.IsType<OrderViewModel>(orderViewModel);
        }
    }
}