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
            var restaurantPoco = new Mock<RestaurantPoco>();
            restaurantPoco.Object.Id = 1;
            restaurantPoco.Object.Active = true;
            restaurantPoco.Object.MenuUrl = "http://menumock.url";
            restaurantPoco.Object.Url = "http://mock.url";
            restaurantPoco.Object.Name = "MockName";
            restaurantPoco.Object.Phone = "123456789";


            //Act
            var restaurant = Mapper.Map<RestaurantPoco, Restaurant>(restaurantPoco.Object);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.Object.Id);
            Assert.IsType<Restaurant>(restaurant);
        } 
    }
}