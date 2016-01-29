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

        public OrderApiController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public GetOrdersResult GetOrders()
        {
            var orders = orderService.GetOrders();

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

            orderService.AddMeal(meal);
        }

        [HttpGet]
        public GetOrderResult GetOrder(int id)
        {
            var order = orderService.GetOrder(id);
            var availableStatuses = orderService.GetStatuses();

            return new GetOrderResult()
            {
                AccountNumber = order.AccountNumber,
                AvailableStatuses = availableStatuses.Select(Mapper.Map<Status, StatusItem>),
                Deadline = order.Deadline,
                EstimatedDeliveryTime = order.EstimatedDeliveryTime,
                Owner = order.Owner,
                RestaurantId = order.Restaurant.ID,
                OrderId = order.Id,
                RestaurantMenuUrl = order.Restaurant.MenuUrl,
                RestaurantName = order.Restaurant.Name,
                Status = order.Status,
                Meals = order.OrderedMeals.Select(Mapper.Map<OrderedMeal, MealViewModel>)
            };
        }

        [HttpPut]
        public void PutEditOrder(EditOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't change order status");
            }

            if ((OrderStatus) model.Status == OrderStatus.InDelivery)
            {
                orderService.SetOrderIsInDelivery(model.OrderId, model.EstimatedDeliveryTime.Value);
            }
            else
            {
                orderService.ChangeStatus(model.OrderId, (OrderStatus) model.Status);
            }

        }
    }
}