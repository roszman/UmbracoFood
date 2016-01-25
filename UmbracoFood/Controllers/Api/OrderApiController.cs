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
        //private readonly IOrderService orderService;

        //public OrderApiController(IOrderService orderService)
        //{
        //    this.orderService = orderService;
        //}

        [HttpGet]
        public GetOrdersResult GetOrders()
        {
            //var orders = orderService.GetActiveRestaurants();
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

        //[HttpGet]
        //public RestaurantViewModel GetRestaurant(int id)
        //{
        //    if (id < 1)
        //    {
        //        throw new Exception("ID nie może być niższe niż 1");
        //    }

        //    var restaurant = orderService.GetRestaurant(id);
        //    if (restaurant == null)
        //    {
        //        throw new Exception("Restaurant doesn't exist.");
        //    }

        //    return Mapper.Map<Core.Models.Restaurant, RestaurantViewModel>(restaurant);
        //}

        [HttpPost]
        public void PostOrder(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't create an order");
            }

            var restaurant = Mapper.Map<CreateOrderViewModel, Order>(model);

            //orderService.AddRestaurant(restaurant);
        }

        //[HttpPut]
        //public void PutRestaurant(EditRestaurantViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        throw new Exception("Nie udało się zedytować restauracji");
        //    }

        //    var restaurant = orderService.GetRestaurant(model.ID);
        //    if (restaurant == null)
        //    {
        //        throw new Exception("Restauracja nie istnieje");
        //    }

        //    var updatedRestaurant = Mapper.Map<EditRestaurantViewModel, Restaurant>(model, restaurant);
        //    orderService.EditRestaurant(updatedRestaurant);
        //}

        //[HttpDelete]
        //public void DeleteRestaurant(int id)
        //{
        //    if (id < 1)
        //    {
        //        throw new Exception("ID nie może być niższe niż 1");
        //    }
            
        //    var restaurant = orderService.GetRestaurant(id);
        //    if (restaurant == null)
        //    {
        //        throw new Exception("Restauracja nie istnieje");
        //    }

        //    orderService.RemoveRestaurant(id);
        //}
    }
}