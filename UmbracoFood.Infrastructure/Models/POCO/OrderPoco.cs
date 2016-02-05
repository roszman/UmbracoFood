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


        [Column("OwnerKey")]
        public string OwnerKey { get; set; }

        [ForeignKey(typeof(StatusPoco),Column ="StatusId")]
        [Column("StatusId")]
        public int StatusId { get; set; }

        [ResultColumn]
        public StatusPoco Status { get; set; }

        [ForeignKey(typeof(RestaurantPoco))]
        [Column("RestaurantId")]
        public int RestaurantId { get; set; }

        [ResultColumn]
        public RestaurantPoco Restaurant { get; set; }

        [Column("Deadline")]
        public DateTime Deadline { get; set; }

        [Column("EstimatedDeliveryTime")]
        public DateTime? EstimatedDeliveryTime { get; set; }

        [ResultColumn]
        public IList<OrderedMealPoco> OrderedMeals { get; set; }

        [Column("AccountNumber")]
        public string AccountNumber { get; set; }
    }
}