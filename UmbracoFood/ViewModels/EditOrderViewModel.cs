using System;

namespace UmbracoFood.ViewModels
{
    public class EditOrderViewModel
    {
        public int OrderId { get; set; }

        public int Status { get; set; }

        public DateTime? EstitmatedDeliveryTime { get; set; }
    }
}