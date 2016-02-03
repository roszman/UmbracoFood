using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoFood.App_plugins.UmbracoFoodPlugins.Models;

namespace UmbracoFood.Controllers.RenderMvc
{
    [Authorize]
    public class EditRestaurantPageController : RenderMvcController
    {
        public ActionResult EditRestaurantPage(RenderModel model, int? id)
        {
            var renderModel = new EditRestaurantPageModel();
            renderModel.Id = id;

            return base.Index(renderModel);
        }
    }
}