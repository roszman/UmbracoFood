using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoFood.Core.Models;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Controllers.Surface
{
    [PluginController("UmbracoFoodPlugins")]
    public class RestaurantSurfaceController : SurfaceController
    {

        [HttpGet]
        public ActionResult AddRestaurant()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Restaurants()
        {
            return PartialView();
        }
    }
}