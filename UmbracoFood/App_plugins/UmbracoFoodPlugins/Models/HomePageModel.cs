using Umbraco.Web;
using Umbraco.Web.Models;

namespace UmbracoFood.App_plugins.UmbracoFoodPlugins.Models
{
    public class HomePageModel : RenderModel
    {
        public HomePageModel()
            :base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            
        }

        public string Message { get; set; }
    }
}