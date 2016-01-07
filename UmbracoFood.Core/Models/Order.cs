using System.Collections.Generic;

namespace UmbracoFood.Core.Models
{
    public class Order
    {
        public IEnumerable<OrderedMeal> OrderedMeals { get; set; }
        public string Owner { get; set; }
        public OrderStatus Status { get; set; }
    }
}