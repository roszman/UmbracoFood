using Umbraco.Core.Persistence;

namespace UmbracoFood.Infrastructure2.Models.POCO
{
    [TableName("Statuses")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class StatusPoco
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}