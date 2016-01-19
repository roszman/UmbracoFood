using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Mapping;
using UmbracoFood.Models;
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
                    Mapper.Initialize(config => config.AddProfile(new RestaurantMappingProfile()));

                    _configured = true;
                    Mapper.AssertConfigurationIsValid();                   
                }
            }
        }

        [Fact]
        public void RestaurantViewModelShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantViewModel = new RestaurantViewModel();
            restaurantViewModel.ID = 1;
            restaurantViewModel.MenuUrl = "http://menumock.url";
            restaurantViewModel.WebsiteUrl = "http://mock.url";
            restaurantViewModel.Name = "MockName";
            restaurantViewModel.Phone = "123456789";


            //Act
            var restaurant = Mapper.DynamicMap<RestaurantViewModel, Restaurant>(restaurantViewModel);

            //Assert
            Assert.Equal(restaurant.ID, restaurantViewModel.ID);
            Assert.IsType<Restaurant>(restaurant);
        }
    }
}