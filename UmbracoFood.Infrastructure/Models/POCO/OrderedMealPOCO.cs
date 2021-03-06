﻿using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace UmbracoFood.Infrastructure.Models.POCO
{

    [TableName("OrderedMeals")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class OrderedMealPoco
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Price")]
        public double Price { get; set; }

        [Column("MealName")]
        public string MealName { get; set; }

        [Column("OrderId")]
        [ForeignKey(typeof(OrderPoco))]
        public int OrderId { get; set; }

        [Column("PurchaserName")]
        public string PurchaserName { get; set; }

        [Column("Count")]
        public int Count { get; set; }
    }
}