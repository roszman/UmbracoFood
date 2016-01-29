using System;
using UmbracoFood.Core.Models;

namespace UmbracoFood.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime? EstimatedDeliveryTime { get; set; }

        public int MealsCount { get; set; }

        public string StatusName { get; set; }

        public int StatusId { get; set; }

        public string RestaurantName { get; set; }
    }
}