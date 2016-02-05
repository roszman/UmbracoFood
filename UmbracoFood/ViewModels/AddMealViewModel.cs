namespace UmbracoFood.ViewModels
{
    public class AddMealViewModel
    {
        public int OrderId { get; set; }

        public string MealName { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }

        public string PurchaserName { get; set; }
    }
}