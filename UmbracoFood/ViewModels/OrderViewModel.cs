using System;
using UmbracoFood.Core.Models;

namespace UmbracoFood.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string Owner { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime? EstitmatedDeliveryTime { get; set; }

        public int MealsCount { get; set; }

        public string StatusName { get; set; }

        public string RestaurantName { get; set; }
    }
}