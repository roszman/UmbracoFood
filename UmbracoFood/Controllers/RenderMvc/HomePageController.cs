using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoFood.Core.Interfaces;

namespace UmbracoFood.Controllers.RenderMvc
{
    public class HomePageController : RenderMvcController
    {
        private readonly IRestaurantService restaurantService;

        public HomePageController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public ActionResult HomePage(RenderModel model)
        {
            var test = restaurantService.GetActiveRestaurants();

            return base.Index(model);
        }
    }
}