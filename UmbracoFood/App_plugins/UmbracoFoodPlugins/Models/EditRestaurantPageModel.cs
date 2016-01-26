using Umbraco.Web;
using Umbraco.Web.Models;

namespace UmbracoFood.App_plugins.UmbracoFoodPlugins.Models
{
    public class EditRestaurantPageModel : RenderModel
    {
        public EditRestaurantPageModel()
            :base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            
        }

        public int? Id { get; set; }
    }
}