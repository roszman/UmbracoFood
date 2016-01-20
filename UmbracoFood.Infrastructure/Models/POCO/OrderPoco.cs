using System;
using System.Collections.Generic;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using UmbracoFood.Core.Models;

namespace UmbracoFood.Infrastructure.Models.POCO
{
    [TableName("Orders")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class OrderPoco
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Owner")]
        public string Owner { get; set; }

        [ForeignKey(typeof(StatusPoco))]
        [Column("StatusId")]
        public OrderStatus Status { get; set; }

        [ForeignKey(typeof(RestaurantPoco))]
        [Column("RestaurantId")]
        public RestaurantPoco Restaurant { get; set; }

        [Column("Deadline")]
        public DateTime Deadline { get; set; }

        [Column("EstimatedDeliveryTime")]
        public DateTime? EstimatedDeliveryTime { get; set; }

        [Column("OrderedMeals")]
        public IList<OrderedMealPoco> OrderedMeals { get; set; }
    }
}