using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UmbracoFood.Infrastructure.Models.POCO
{
    [TableName("Orders")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class OrderPoco
    {
        [Column("Id")]
        public long Id { get; set; }

        [Column("Owner")]
        public string Owner { get; set; }

        [ForeignKey(typeof(StatusPoco))]
        [Column("StatusId")]
        public int StatusId { get; set; }

        [ForeignKey(typeof(RestaurantPoco))]
        [Column("RestaurantId")]
        public int RestaurantId { get; set; }

        [Column("Deadline")]
        public DateTime Deadline { get; set; }

        [Column("EstimatedDeliveryTime")]
        public DateTime? EstimatedDeliveryTime { get; set; } 
    }
}