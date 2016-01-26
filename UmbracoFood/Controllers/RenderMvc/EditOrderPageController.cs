using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoFood.App_plugins.UmbracoFoodPlugins.Models;

namespace UmbracoFood.Controllers.RenderMvc
{
    public class EditOrderPageController : RenderMvcController
    {
        public ActionResult EditOrderPage(RenderModel model, int? id)
        {
            var renderModel = new EditOrderPageModel();
            renderModel.Id = id;

            return base.Index(renderModel);
        } 
    }
}