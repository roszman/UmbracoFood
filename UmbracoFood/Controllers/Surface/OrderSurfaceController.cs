using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoFood.Core.Models;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Controllers.Surface
{
    [Authorize]
    [PluginController("UmbracoFoodPlugins")]
    public class OrderSurfaceController : SurfaceController
    {

        [HttpGet]
        public ActionResult CreateOrder()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            return PartialView(id);
        }
    }
}