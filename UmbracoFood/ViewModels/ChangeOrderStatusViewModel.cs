namespace UmbracoFood.ViewModels
{
    public class ChangeOrderStatusViewModel
    {
        public int OrderId { get; set; }

        public int StatusId { get; set; }

        public int? EstitmatedDeliveryTime { get; set; }
    }
}