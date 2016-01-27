using System;
using System.Collections.Generic;

namespace UmbracoFood.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public OrderStatus Status { get; set; }
        public string AccountNumber { get; set; }
        public Restaurant Restaurant { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public List<OrderedMeal> OrderedMeals { get; set; }
    }
}