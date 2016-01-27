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

        IEnumerable<Order> GetOrders();

        IEnumerable<Status> GetStatuses();

        void AddMeal(OrderedMeal meal);

        void ChangeOrderStatus(Order order);
    }
}