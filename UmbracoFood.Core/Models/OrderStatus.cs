using System.ComponentModel;

namespace UmbracoFood.Core.Models
{
    public enum OrderStatus
    {
        [Description("W trakcie zamawiania")]
        InProgress,

        [Description("W drodze")]
        InDelivery,

        [Description("W kuchni")]
        InKitchen
    }
}