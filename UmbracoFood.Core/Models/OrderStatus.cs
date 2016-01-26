using System.ComponentModel;

namespace UmbracoFood.Core.Models
{
    public enum OrderStatus
    {
        [Description("W trakcie zamawiania")]
        InProgress = 1,

        [Description("W drodze")]
        InDelivery = 2,

        [Description("W kuchni")]
        InKitchen = 3
    }
}