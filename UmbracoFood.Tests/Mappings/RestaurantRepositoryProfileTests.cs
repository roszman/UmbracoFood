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


        public RestaurantRepositoryProfileTests()
        {
           Mapper.Initialize(config => config.AddProfile(new RestaurantRepositoryMappingProfile()));
           Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void RestaurantPocoShouldBeMappedToRestaurant()
        {
            //Arrange
            var restaurantPoco = new RestaurantPoco();
            restaurantPoco.ID = 1;
            restaurantPoco.IsActive = true;
            restaurantPoco.MenuUrl = "http://menumock.url";
            restaurantPoco.WebsiteUrl = "http://mock.url";
            restaurantPoco.Name = "MockName";
            restaurantPoco.Phone = "restaurantPoco23456789";


            //Act
            var restaurant = Mapper.Map<RestaurantPoco, Restaurant>(restaurantPoco);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.ID);
            Assert.Equal(restaurant.IsActive, restaurantPoco.IsActive);
            Assert.Equal(restaurant.MenuUrl, restaurantPoco.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantPoco.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantPoco.Name);
            Assert.Equal(restaurant.Phone, restaurantPoco.Phone);
            Assert.IsType<Restaurant>(restaurant);
        }

        [Fact]
        public void RestaurantShouldBeMappedToRestaurantPoco()
        {
            //Arrange
            var restaurant = new Restaurant();
            restaurant.ID = 1;
            restaurant.IsActive = true;
            restaurant.MenuUrl = "http://menumock.url";
            restaurant.WebsiteUrl = "http://mock.url";
            restaurant.Name = "MockName";
            restaurant.Phone = "restaurantPoco23456789";


            //Act
            var restaurantPoco = Mapper.Map<Restaurant, RestaurantPoco>(restaurant);

            //Assert
            Assert.Equal(restaurantPoco.ID, restaurant.ID);
            Assert.Equal(restaurantPoco.IsActive, restaurant.IsActive);
            Assert.Equal(restaurantPoco.MenuUrl, restaurant.MenuUrl);
            Assert.Equal(restaurantPoco.WebsiteUrl, restaurant.WebsiteUrl);
            Assert.Equal(restaurantPoco.Name, restaurant.Name);
            Assert.Equal(restaurantPoco.Phone, restaurant.Phone);
            Assert.IsType<RestaurantPoco>(restaurantPoco);
        }
    }
}