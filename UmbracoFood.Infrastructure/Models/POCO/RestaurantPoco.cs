﻿using Umbraco.Core.Persistence;

namespace UmbracoFood.Infrastructure.Models.POCO
{

    [TableName("Restaurants")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class RestaurantPoco
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column("MenuUrl")]
        public string MenuUrl { get; set; }

        [Column("Active")]
        public bool IsActive { get; set; }
    }
}