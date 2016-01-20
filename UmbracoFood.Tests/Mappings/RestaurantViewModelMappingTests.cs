using AutoMapper;
using UmbracoFood.Core.Models;
using UmbracoFood.Mapping;
using UmbracoFood.ViewModels;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class RestaurantViewModelMappingTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;

        public RestaurantViewModelMappingTests()
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
            Assert.Equal(restaurant.MenuUrl, restaurantViewModel.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantViewModel.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantViewModel.Name);
            Assert.Equal(restaurant.Phone, restaurantViewModel.Phone);
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
            Assert.Equal(restaurant.ID, restaurantViewModel.ID);
            Assert.Equal(restaurant.MenuUrl, restaurantViewModel.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantViewModel.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantViewModel.Name);
            Assert.Equal(restaurant.Phone, restaurantViewModel.Phone);
            Assert.Equal(restaurant.IsActive, restaurantViewModel.IsActive);

            Assert.IsType<Restaurant>(restaurant);
        }
    }
}