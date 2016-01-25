using System.Collections.Generic;
using Umbraco.Web.Models.ContentEditing;
using UmbracoFood.Core.Models;

namespace UmbracoFood.ViewModels
{
    public class GetSelectRestaurantsItemsResult
    {
        public IEnumerable<SelectRestaurantItem> Restaurants { get; set; } 
    }
}