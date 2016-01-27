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

        void AddOrderMeal(OrderedMeal orderedMeal);

        void ChangeStatus(OrderStatus newStatus, int orderId);

        void SetOrderIsInDelivery(DateTime estimatedDeliveryTime, int orderId);
    }
}
