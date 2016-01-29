using System;
using System.Collections.Generic;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);

        void RemoveOrder(int id);

        Order GetOrder(int id);

        IEnumerable<Order> GetOrders();

        IEnumerable<Status> GetStatuses();

        void AddOrderedMeal(OrderedMeal orderedMeal);

        void ChangeStatus(int orderId, OrderStatus newStatus);

        void SetOrderIsInDelivery(int orderId, DateTime estimatedDeliveryTime);

        void RemoveOrderedMeal(int orderedMealId);
    }
}
