using System;
using System.Net;
using System.Net.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Filters;

namespace UmbracoFood.Controllers.Api
{
    [PluginController("UmbracoFoodApi")]
    public class RestaurantApiController : UmbracoApiController
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantApiController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        public string GetTest()
        {
            return "test";
        }

        public void PostRestaurant(Restaurant model)
        {
            restaurantService.AddRestaurant(model);
        }
    }
} ;