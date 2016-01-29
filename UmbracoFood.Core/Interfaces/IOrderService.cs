using System;
using System.Collections.Generic;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IOrderService
    {
        int CreateOrder(Order order);

        void RemoveOrder(int orderId);

        void AddMeal(OrderedMeal meal);

        void RemoveMeal(int mealId);

        Order GetOrder(int id);

        IEnumerable<Order> GetOrders();

        IEnumerable<Status> GetStatuses();

        void ChangeStatus(int orderId, OrderStatus newStatus);

        void SetOrderIsInDelivery(int orderId, DateTime estimatedDeliveryTime);
    }
}