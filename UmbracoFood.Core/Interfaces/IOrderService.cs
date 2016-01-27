using System;
using System.Collections.Generic;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IOrderService
    {
        int AddOrder(Order order);

        void EditOrder(Order order);

        void RemoveOrder(int id);

        Order GetOrder(int id);

        void ChangeStatus(OrderStatus newStatus, int orderId);

        void SetOrderIsInDelivery(int orderId, DateTime estimatedDeliveryTime);
    }
}
