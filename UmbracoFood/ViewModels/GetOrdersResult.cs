using System.Collections.Generic;

namespace UmbracoFood.ViewModels
{
    public class GetOrdersResult
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}