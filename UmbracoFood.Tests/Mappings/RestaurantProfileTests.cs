using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Mapping;
using UmbracoFood.ViewModels;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class RestaurantProfileTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;

        public RestaurantProfileTests()
        {
            lock (Sync)
            {
                if (!_configured)
                {
                    Mapper.Reset();

                    Mapper.Initialize(config => config.AddProfile(new RestaurantMappingProfile()));

                    _configured = true;
                    Mapper.AssertConfigurationIsValid();                   
                }
            }
        }

        [Fact]
        public void AddRestaurantViewModelShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantViewModel = new AddRestaurantViewModel();
            restaurantViewModel.MenuUrl = "http://menumock.url";
            restaurantViewModel.WebsiteUrl = "http://mock.url";
            restaurantViewModel.Name = "MockName";
            restaurantViewModel.Phone = "123456789";


            //Act
            var restaurant = Mapper.DynamicMap<AddRestaurantViewModel, Restaurant>(restaurantViewModel);

            //Assert
            Assert.Equal(restaurant.Name, restaurantViewModel.Name);
            Assert.IsType<Restaurant>(restaurant);
        }

        [Fact]
        public void EditRestaurantViewModelShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantViewModel = new EditRestaurantViewModel();
            restaurantViewModel.ID = 999;
            restaurantViewModel.MenuUrl = "http://menumock.url";
            restaurantViewModel.WebsiteUrl = "http://mock.url";
            restaurantViewModel.Name = "MockName";
            restaurantViewModel.Phone = "123456789";
            restaurantViewModel.IsActive = true;

            //Act
            var restaurant = Mapper.DynamicMap<EditRestaurantViewModel, Restaurant>(restaurantViewModel);

            //Assert
            Assert.Equal(restaurant.Name, restaurantViewModel.Name);
            Assert.IsType<Restaurant>(restaurant);
        }
    }
}