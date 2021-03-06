﻿using System;
using System.Web.Http;
using AutoMapper;
using Umbraco.Web.Models.ContentEditing;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using System.Linq;
using UmbracoFood.ViewModels;

namespace UmbracoFood.Controllers.Api
{
    [Umbraco.Web.Mvc.PluginController("UmbracoFoodApi")]
    public class RestaurantApiController : UmbracoApiController
    {
        private readonly IRestaurantService restaurantService;

        public RestaurantApiController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        [HttpGet]
        public GetRestaurantsResult GetRestaurants()
        {
            var restaurants = restaurantService.GetActiveRestaurants();

            var getRestaurantsResult = new GetRestaurantsResult()
            {
                Restaurants = restaurants.Select(Mapper.Map<Core.Models.Restaurant, RestaurantViewModel>)
            };

            return getRestaurantsResult;
        }

        [HttpGet]
        public RestaurantViewModel GetRestaurant(int id)
        {
            if (id < 1)
            {
                throw new Exception("ID nie może być niższe niż 1");
            }

            var restaurant = restaurantService.GetRestaurant(id);
            if (restaurant == null)
            {
                throw new Exception("Restaurant doesn't exist.");
            }

            return Mapper.Map<Core.Models.Restaurant, RestaurantViewModel>(restaurant);
        }

        [HttpGet]
        public GetSelectRestaurantsItemsResult GetSelectRestaurantsItems()
        {
            var restaurants = restaurantService.GetActiveRestaurants();

            var result = new GetSelectRestaurantsItemsResult()
            {
                Restaurants = restaurants.Select(Mapper.Map<Core.Models.Restaurant, SelectRestaurantItem>)
            };

            return result;
        }

        [HttpPost]
        public void PostRestaurant(AddRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Couldn't add a restaurant");
            }
            var restaurant = Mapper.Map<AddRestaurantViewModel, Restaurant>(model);
            restaurantService.AddRestaurant(restaurant);
        }

        [HttpPut]
        public void PutRestaurant(EditRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Nie udało się zedytować restauracji");
            }

            var restaurant = restaurantService.GetRestaurant(model.ID);
            if (restaurant == null)
            {
                throw new Exception("Restauracja nie istnieje");
            }

            var updatedRestaurant = Mapper.Map<EditRestaurantViewModel, Restaurant>(model, restaurant);
            restaurantService.EditRestaurant(updatedRestaurant);
        }

        [HttpDelete]
        public void DeleteRestaurant(int id)
        {
            if (id < 1)
            {
                throw new Exception("ID nie może być niższe niż 1");
            }

            restaurantService.RemoveRestaurant(id);
        }
    }
}