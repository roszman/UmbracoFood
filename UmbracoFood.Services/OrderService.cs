using System.Collections.Generic;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public int AddOrder(Order order)
        {
            return orderRepository.AddOrder(order);
        }

        public void EditOrder(Order order)
        {
            orderRepository.EditOrder(order);

        }

        public void RemoveOrder(int id)
        {
            orderRepository.RemoveOrder(id);

        }

        public Order GetOrder(int id)
        {
            return orderRepository.GetOrder(id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return orderRepository.GetOrders();
        }

        public IEnumerable<Status> GetStatuses()
        {
            return orderRepository.GetStatuses();

        }

        public void AddMeal(OrderedMeal meal)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeOrderStatus(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}