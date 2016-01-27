using System;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order);

        void AddMeal(OrderedMeal meal);

        void ChangeStatus(OrderStatus order);

        Order GetOrder(int id);

        void ChangeStatus(OrderStatus newStatus, int orderId);

        void SetOrderIsInDelivery(DateTime estimatedDeliveryTime, int orderId);

    }
}