﻿using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using UmbracoFood.Core.Interfaces;

namespace UmbracoFood.Controllers
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
            var test = restaurantService.GetAllRestaurants();

            return base.Index(model);
        }
    }
}