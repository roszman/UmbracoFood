using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoFood.App_plugins.UmbracoFoodPlugins.Models;
using UmbracoFood.Core.Interfaces;

namespace UmbracoFood.Controllers.RenderMvc
{
    [Authorize]
    public class HomePageController : RenderMvcController
    {
        public ActionResult HomePage(RenderModel model, string message)
        {
            var renderModel = new HomePageModel();
            renderModel.Message = message;


            return base.Index(renderModel);
        }
    }
}