using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using UmbracoFood.Core.Extensions;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Interfaces;
using UmbracoFood.Mapping;
using UmbracoFood.ViewModels;
using Xunit;
using Xunit.Sdk;

namespace UmbracoFood.Tests.Mappings.Infrastructure
{
    public class MapperTests
    {
        private static readonly object sync = new object();
        private static bool configured;
        private OrderMapper _orderMapper;
        private OrderedMealMapper _orderdedMealMapper;
        private RestaurantMapper _restaurantMapper;
        private CreateOrderViewModel _createOrderViewModel;
        private Mock<IUserDetailsService> _userDeatilsService;
        private Mock<IMappingEngine> _mappingEngineMock;

        public MapperTests()
        {
            _userDeatilsService = new Mock<IUserDetailsService>();
            _mappingEngineMock = new Mock<IMappingEngine>();
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
                        config.AddProfile(new OrderViewModelMapperProfile(_userDeatilsService.Object));
                    });
                    configured = true;
                    Mapper.AssertConfigurationIsValid();
                }
            }

            _orderMapper = new OrderMapper();
            _orderdedMealMapper = new OrderedMealMapper();
            _restaurantMapper = new RestaurantMapper(_mappingEngineMock.Object);
            _createOrderViewModel = new CreateOrderViewModel();
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
                    PurchaserKey = "Johnny Bravo"
                },
                new OrderedMealPoco()
                {
                    Id = 11,
                    Count = 3,
                    Price = 31.32,
                    MealName = "Meal 2",
                    OrderId = 999,
                    PurchaserKey = "Voldemort"
                }
            };
            orderPoco.OwnerKey = "Papa Smurf";
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
            var order = _orderMapper.MapToDomain(orderPoco);

            //Assert
            Assert.Equal(order.Id, orderPoco.Id);
            Assert.Equal(order.OwnerKey, orderPoco.OwnerKey);
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
                    PurchaserKey = "Johnny Bravo"
                },
                new OrderedMeal()
                {
                    Id = 11,
                    Count = 3,
                    Price = 31.32,
                    MealName = "Meal 2",
                    OrderId = 999,
                    PurchaserKey = "Voldemort"
                }
            };
            order.OwnerKey = "Papa Smurf";
            order.Restaurant = new Restaurant()
            {
                ID = 1
            };
            order.Status = OrderStatus.InKitchen;
            var orderedMealKey = "ordered meal key";

            //Act
            var orderPoco = _orderMapper.MapToPoco(order);

            //Assert
            Assert.Equal(orderPoco.Id, order.Id);
            Assert.Equal(orderPoco.OwnerKey, order.OwnerKey);
            Assert.Equal((OrderStatus) orderPoco.Status.Id, order.Status);
            Assert.Equal(orderPoco.AccountNumber, order.AccountNumber);
            Assert.Equal(orderPoco.Restaurant.ID, order.Restaurant.ID);
            Assert.Equal(orderPoco.Deadline, order.Deadline);
            Assert.Equal(orderPoco.EstimatedDeliveryTime, order.EstimatedDeliveryTime);
            Assert.Equal(orderPoco.OrderedMeals.Count, order.OrderedMeals.Count);
            Assert.IsType<OrderPoco>(orderPoco);
        }


        [Fact]
        public void OrderedMealShouldBeMappedToOrderedMealPoco()
        {
            //Arrange
            var orderedMeal = new OrderedMeal
            {
                Count = 6,
                MealName = "meal name",
                OrderId = 145,
                Id = 1765,
                Price = 234.67,
                PurchaserKey = "purchaser name"

            };

            //Act
            var mappedOrderedMeal = _orderdedMealMapper.MapToPoco(orderedMeal);

            //Assert
            Assert.Equal(mappedOrderedMeal.Id, mappedOrderedMeal.Id);
            Assert.Equal(mappedOrderedMeal.MealName, mappedOrderedMeal.MealName);
            Assert.Equal(mappedOrderedMeal.Count, mappedOrderedMeal.Count);
            Assert.Equal(mappedOrderedMeal.OrderId, mappedOrderedMeal.OrderId);
            Assert.Equal(mappedOrderedMeal.Price, mappedOrderedMeal.Price);
            Assert.Equal(mappedOrderedMeal.PurchaserKey, mappedOrderedMeal.PurchaserKey);
            Assert.IsType<OrderedMealPoco>(mappedOrderedMeal);
        }

        [Fact]
        public void OrderedMealPocoShouldBeMappedToOrderedMeal()
        {
            //Arrange
            var orderedMealPoco = new OrderedMealPoco
            {
                Count = 6,
                MealName = "meal name",
                OrderId = 145,
                Id = 1765,
                Price = 234.67,
                PurchaserKey = "purchaser key"

            };

            //Act
            var mappedOrderedMeal = _orderdedMealMapper.MapToDomain(orderedMealPoco);

            //Assert
            Assert.Equal(mappedOrderedMeal.Id, mappedOrderedMeal.Id);
            Assert.Equal(mappedOrderedMeal.MealName, mappedOrderedMeal.MealName);
            Assert.Equal(mappedOrderedMeal.Count, mappedOrderedMeal.Count);
            Assert.Equal(mappedOrderedMeal.OrderId, mappedOrderedMeal.OrderId);
            Assert.Equal(mappedOrderedMeal.Price, mappedOrderedMeal.Price);
            Assert.IsType<OrderedMeal>(mappedOrderedMeal);
        }

        [Fact]
        public void RestaurantPocoShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantPoco = new RestaurantPoco();
            restaurantPoco.ID = 1;
            restaurantPoco.IsActive = true;
            restaurantPoco.MenuUrl = "http://menumock.url";
            restaurantPoco.WebsiteUrl = "http://mock.url";
            restaurantPoco.Name = "MockName";
            restaurantPoco.Phone = "123456789";
            restaurantPoco.ShippingRate = 14.54;
            restaurantPoco.FreeShippingThreshold = 124.53;

            //Act
            var restaurant = _restaurantMapper.MapToDomain(restaurantPoco);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.ID);
            Assert.Equal(restaurant.IsActive, restaurantPoco.IsActive);
            Assert.Equal(restaurant.MenuUrl, restaurantPoco.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantPoco.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantPoco.Name);
            Assert.Equal(restaurant.Phone, restaurantPoco.Phone);
            Assert.IsType<Restaurant>(restaurant);
        }
        [Fact]
        public void RestaurantShouldBeMappedToRestaurantPoco()
        {
            //Arrange
            var restaurant = new Restaurant();
            restaurant.ID = 1;
            restaurant.IsActive = true;
            restaurant.MenuUrl = "http://menumock.url";
            restaurant.WebsiteUrl = "http://mock.url";
            restaurant.Name = "MockName";
            restaurant.Phone = "123456789";

            //Act
            var restaurantPoco = _restaurantMapper.MapToPoco(restaurant);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.ID);
            Assert.Equal(restaurant.IsActive, restaurantPoco.IsActive);
            Assert.Equal(restaurant.MenuUrl, restaurantPoco.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantPoco.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantPoco.Name);
            Assert.Equal(restaurant.Phone, restaurantPoco.Phone);
            Assert.IsType<RestaurantPoco>(restaurantPoco);
        }
        [Fact]
        public void CreateOrderViewModelShouldBeMappedToOrder()
        {
            //Arrange
            var createOrderViewModel = new CreateOrderViewModel();
            createOrderViewModel.AccountNumber = "11 1111 1111 1111 1111";
            createOrderViewModel.Deadline = new DateTime();
            createOrderViewModel.Meals = new List<CreateOrderMealViewModel>()
            {
                new CreateOrderMealViewModel()
                {
                    Name = "Meal 1",
                    Count = 2,
                    Price = 10
                },
                new CreateOrderMealViewModel()
                {
                    Name = "Meal 2",
                    Count = 1,
                    Price = 15
                }
            };
            createOrderViewModel.Owner = "Michał";
            createOrderViewModel.SelectedRestaurantId = 1;
            var userKey = "userKey";
            _userDeatilsService.Setup(u => u.GetUserKey(It.IsAny<string>())).Returns(userKey);

            //Act
            var order = Mapper.DynamicMap<CreateOrderViewModel, Order>(createOrderViewModel);

            //Assert
            Assert.Equal(order.AccountNumber, createOrderViewModel.AccountNumber);
            Assert.Equal(order.Deadline, createOrderViewModel.Deadline);
            Assert.Equal(order.EstimatedDeliveryTime, null);
            Assert.Equal(order.Id, 0);
            Assert.Equal(order.OwnerKey, userKey);
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
                         PurchaserKey = "purchaser name"
                     }
                 },
                OwnerKey = "owner",
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

            var userName = "user name";
            _userDeatilsService.Setup(u => u.GetUserName(It.IsAny<string>())).Returns(userName);

            //act
            var orderViewModel = Mapper.DynamicMap<Order, OrderViewModel>(order);

            //assert
            _userDeatilsService.Verify(u => u.GetUserName(It.IsAny<string>()), Times.Once);
            Assert.Equal(order.Id, orderViewModel.Id);
            Assert.Equal(userName, orderViewModel.Owner);
            Assert.Equal(order.Deadline, orderViewModel.Deadline);
            Assert.Equal(order.EstimatedDeliveryTime, orderViewModel.EstimatedDeliveryTime);
            Assert.Equal(order.OrderedMeals.Sum(m => m.Count), orderViewModel.MealsCount);
            Assert.Equal(order.Status.GetDescription(), orderViewModel.StatusName);
            Assert.Equal(order.Restaurant.Name, orderViewModel.RestaurantName);
            Assert.IsType<OrderViewModel>(orderViewModel);
        }

        [Fact]
        public void AddRestaurantViewModelShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantViewModel = new AddRestaurantViewModel();
            restaurantViewModel.MenuUrl = "http://menumock.url";
            restaurantViewModel.WebsiteUrl = "http://mock.url";
            restaurantViewModel.Name = "MockName";
            restaurantViewModel.Phone = "123456789";


            //Act
            var restaurant = Mapper.DynamicMap<AddRestaurantViewModel, Restaurant>(restaurantViewModel);

            //Assert
            Assert.Equal(restaurant.MenuUrl, restaurantViewModel.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantViewModel.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantViewModel.Name);
            Assert.Equal(restaurant.Phone, restaurantViewModel.Phone);
            Assert.IsType<Restaurant>(restaurant);
        }

        [Fact]
        public void EditRestaurantViewModelShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantViewModel = new EditRestaurantViewModel();
            restaurantViewModel.ID = 999;
            restaurantViewModel.MenuUrl = "http://menumock.url";
            restaurantViewModel.WebsiteUrl = "http://mock.url";
            restaurantViewModel.Name = "MockName";
            restaurantViewModel.Phone = "123456789";

            //Act
            var restaurant = Mapper.DynamicMap<EditRestaurantViewModel, Restaurant>(restaurantViewModel);

            //Assert
            Assert.Equal(restaurant.ID, restaurantViewModel.ID);
            Assert.Equal(restaurant.MenuUrl, restaurantViewModel.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantViewModel.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantViewModel.Name);
            Assert.Equal(restaurant.Phone, restaurantViewModel.Phone);

            Assert.IsType<Restaurant>(restaurant);
        }
    }
}