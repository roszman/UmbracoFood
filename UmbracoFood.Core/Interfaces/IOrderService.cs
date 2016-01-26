﻿using UmbracoFood.Core.Models;

namespace UmbracoFood.Core.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(Order order);

        void AddMeal(OrderedMeal meal);

        void ChangeStatus(OrderStatus order);
    }
}