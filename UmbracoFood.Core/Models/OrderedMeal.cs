namespace UmbracoFood.Core.Models
{
    public class OrderedMeal
    {
        public long Id { get; set; }
        public string MealName { get; set; }
        public double Price { get; set; }
        public string PurchaserKey { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
    }
}