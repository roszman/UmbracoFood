using Umbraco.Core.Persistence;

namespace UmbracoFood.Infrastructure.Models.POCO
{

    [TableName("Restaurants")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class RestaurantPoco
    {
        [Column("Id")]
        public long Id { get; set; }

        [Column("Name")]
        public double Name { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column("MenuUrl")]
        public string MenuUrl { get; set; }

        [Column("Active")]
        public bool Active { get; set; }
    }
}