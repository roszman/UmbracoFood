using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class RestaurantRepositoryProfileTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;

        public RestaurantRepositoryProfileTests()
        {
            lock (Sync)
            {
                if (!_configured)
                {
                    Mapper.Reset();
                    Mapper.Initialize(config => config.AddProfile(new RestaurantRepositoryMappingProfile()));
                    _configured = true;
                    Mapper.AssertConfigurationIsValid();
                }
            }
        }

        [Fact]
        public void RestaurantPocoShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantPoco = new RestaurantPoco();
            restaurantPoco.Id = 1;
            restaurantPoco.IsActive = true;
            restaurantPoco.MenuUrl = "http://menumock.url";
            restaurantPoco.Url = "http://mock.url";
            restaurantPoco.Name = "MockName";
            restaurantPoco.Phone = "123456789";


            //Act
            var restaurant = Mapper.DynamicMap<RestaurantPoco, Restaurant>(restaurantPoco);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.Id);
            Assert.IsType<Restaurant>(restaurant);
        } 
    }
}