using System;
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

        public void RemoveOrder(int id)
        {
            orderRepository.RemoveOrder(id);
        }

        public void RemoveMeal(int mealId)
        {
            orderRepository.RemoveOrderedMeal(mealId);
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
            orderRepository.AddOrderedMeal(meal);
        }

        public int CreateOrder(Order order)
        {
            return orderRepository.AddOrder(order);
        }

        public void ChangeStatus(int orderId, OrderStatus status)
        {
            orderRepository.ChangeStatus(orderId, status);
        }

        public void SetOrderIsInDelivery(int orderId, DateTime estimatedDeliveryTime)
        {
            orderRepository.SetOrderIsInDelivery(orderId, estimatedDeliveryTime);
        }
    }
}