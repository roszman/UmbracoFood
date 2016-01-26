using Umbraco.Web;
using Umbraco.Web.Models;

namespace UmbracoFood.App_plugins.UmbracoFoodPlugins.Models
{
    public class EditOrderPageModel : RenderModel
    {
        public EditOrderPageModel()
            :base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            
        }

        public int? Id { get; set; }
    }
}