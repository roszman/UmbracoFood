using System;
using System.Collections.Generic;
using UmbracoFood.Core.Models;

namespace UmbracoFood.ViewModels
{
    public class GetOrderResult
    {
        public int OrderId { get; set; }

        public string Owner { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime? EstimatedDeliveryTime { get; set; }

        public OrderStatus Status { get; set; }

        public int RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string RestaurantMenuUrl { get; set; }

        public string AccountNumber { get; set; }

        public IEnumerable<MealViewModel> Meals { get; set; }

        public IEnumerable<StatusItem> AvailableStatuses { get; set; }
    }
}