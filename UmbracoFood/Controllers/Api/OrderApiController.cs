using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using AutoMapper.Mappers;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using System.Linq;
using UmbracoFood.Extensions;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Controllers.Api
{
    [Umbraco.Web.Mvc.PluginController("UmbracoFoodApi")]
    public class OrderApiController : UmbracoApiController
    {
        private readonly IOrderService orderService;

        [HttpGet]
        public GetOrdersResult GetOrders()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Restaurant = new Restaurant() {Name = "Restaurant 1"},
                    Deadline = DateTime.Now.AddHours(1),
                    EstitmatedDeliveryTime = null,
                    OrderedMeals = new List<OrderedMeal>()
                    {
                        new OrderedMeal(),
                        new OrderedMeal(),
                        new OrderedMeal()
                    },
                    Owner = "Michal",
                    Status = OrderStatus.InProgress
                },
                new Order()
                {
                    Id = 1,
                    Restaurant = new Restaurant() {Name = "Restaurant 2"},
                    Deadline = DateTime.Now.AddMinutes(-10),
                    EstitmatedDeliveryTime = DateTime.Now.AddHours(2),
                    OrderedMeals = new List<OrderedMeal>()
                    {
                        new OrderedMeal(),
                        new OrderedMeal(),
                    },
                    Owner = "Piotr",
                    Status = OrderStatus.InDelivery
                },
                new Order()
                {
                    Id = 1,
                    Restaurant = new Restaurant() {Name = "Restaurant 3"},
                    Deadline = DateTime.Now.AddHours(-1),
                    EstitmatedDeliveryTime = DateTime.Now.AddMinutes(-15),
                    OrderedMeals = new List<OrderedMeal>()
                    {
                        new OrderedMeal(),
                        new OrderedMeal(),
                        new OrderedMeal(),
                        new OrderedMeal(),
                        new OrderedMeal(),
                    },
                    Owner = "Piotr",
                    Status = OrderStatus.InKitchen
                }
            };


            var getOrdersResult = new GetOrdersResult()
            {
                Orders = orders.Select(Mapper.Map<Core.Models.Order, OrderViewModel>)
            };

            return getOrdersResult;
        }

        [HttpPost]
        public void PostOrder(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't create an order");
            }

            var order = Mapper.Map<CreateOrderViewModel, Order>(model);

            orderService.CreateOrder(order);
        }

        [HttpPut]
        public void PutOrderMeal(AddMealViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't add meal to order");
            }

            var meal = Mapper.Map<AddMealViewModel, OrderedMeal>(model);

            //todo orderService.AddMeal(meal);
        }

        [HttpGet]
        public GetOrderResult GetOrder(int id)
        {
            return new GetOrderResult()
            {
                Deadline = new DateTime().AddHours(0.5),
                Owner = "JA owner",
                RestaurantId = 1,
                RestaurantName = "Da restaurand",
                StatusId = 1,
                OrderId = 1,
                EstitmatedDeliveryTime = null,
                RestaurantMenuUrl = "http://alizze.pl/menu.php",
                Meals = new List<MealViewModel>()
                {
                    new MealViewModel()
                    {
                        Count = 1,
                        Price = 10,
                        MealName = "Meal name 1",
                        Person = "Person name 1"
                    },
                    new MealViewModel()
                    {
                        Count = 2,
                        Price = 15,
                        MealName = "Meal name 2",
                        Person = "Person name 2"
                    },
                },
                AvailableStatuses = new List<StatusItem>()
                {
                    new StatusItem()
                    {
                        Id = 1,
                        Name = "W trakcie zamawiania"
                    },
                    new StatusItem()
                    {
                        Id = 2,
                        Name = "W drodze"
                    },
                    new StatusItem()
                    {
                        Id = 3,
                        Name = "W kuchni"
                    },
                },
                AccountNumber = "11 1222 3333 4444 5555"
            };
        }


        [HttpPut]
        public void PutChangeOrderStatus(ChangeOrderStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't change order status");
            }

            var orderStatus = Mapper.Map<ChangeOrderStatusViewModel, Order>(model);

       }

    }
}