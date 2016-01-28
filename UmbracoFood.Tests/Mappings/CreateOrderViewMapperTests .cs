using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Mapping;
using UmbracoFood.ViewModels;
using Xunit;
using OrderMappingProfile = UmbracoFood.Mapping.OrderMappingProfile;

namespace UmbracoFood.Tests.Mappings
{
    public class CreateOrderViewMapperTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;

        public CreateOrderViewMapperTests()
        {
            lock (Sync)
            {
                if (!_configured)
                {
                    Mapper.Reset();

                    Mapper.Initialize(config => config.AddProfile(new OrderMappingProfile()));

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




        }


    }
}