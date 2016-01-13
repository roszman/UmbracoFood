﻿using System.Web.Mvc;
using Umbraco.Web.Mvc;
using UmbracoFood.Core.Models;

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

        [HttpPost]
        public ActionResult AddRestaurant(RestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //ADD RESTAURANT HERE


            var homePage = UmbracoContext.ContentCache.GetById(1052);
            return Redirect(homePage.Url);
        }
    }
}