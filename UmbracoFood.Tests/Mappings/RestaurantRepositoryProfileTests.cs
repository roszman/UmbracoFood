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
            restaurantPoco.Id = 1;
            restaurantPoco.Active = true;
            restaurantPoco.MenuUrl = "http://menumock.url";
            restaurantPoco.Url = "http://mock.url";
            restaurantPoco.Name = "MockName";
            restaurantPoco.Phone = "123456789";


            //Act
            var restaurant = Mapper.Map<RestaurantPoco, Restaurant>(restaurantPoco);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.Id);
            Assert.IsType<Restaurant>(restaurant);
        } 
    }
}