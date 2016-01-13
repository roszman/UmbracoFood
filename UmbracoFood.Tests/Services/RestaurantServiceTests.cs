using System;
using System.Collections.Generic;
using Moq;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Services;
using Xunit;

namespace UmbracoFood.Tests.Services
{
    public class RestaurantServiceTests 
    {
        private IRestaurantService restaurantService;
        private Mock<IRestaurantRepository> restaurantRepositoryMock;

        public RestaurantServiceTests()
        {
            restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            restaurantService = new RestaurantService(restaurantRepositoryMock.Object);
        }

        [Fact]
        public void AddRestaurant_Given_Restaurant_Then_AddRestaurantIsCalled()
        {
            //Arrange
            restaurantRepositoryMock.Setup(r => r.AddRestaurant(It.Is<Restaurant>(obj => obj != null)));
            var restaurant = new Restaurant()
            {
                ID = 1,
                MenuUrl = "http://menu.url",
                WebsiteUrl = "http://site.url",
                Name = "Restaurant name",
                Phone = "01203243240"
            };

            //Act
            restaurantService.AddRestaurant(restaurant);

            //Assert
            restaurantRepositoryMock.Verify(r => r.AddRestaurant(It.Is<Restaurant>(obj => obj != null)),Times.Once);
        }

        [Fact]
        public void EditRestaurant_Given_Restaurant_Then_EditRestaurantIsCalled()
        {
            //Arrange
            restaurantRepositoryMock.Setup(r => r.EditRestaurant(It.Is<Restaurant>(obj => obj != null)));
            var restaurant = new Restaurant()
            {
                ID = 1,
                MenuUrl = "http://menu.url",
                WebsiteUrl = "http://site.url",
                Name = "Restaurant name",
                Phone = "01203243240"
            };

            //Act
            restaurantService.EditRestaurant(restaurant);

            //Assert
            restaurantRepositoryMock.Verify(r => r.EditRestaurant(It.Is<Restaurant>(obj => obj != null)), Times.Once);
        }

        [Fact]
        public void RemoveRestaurant_When_RestaurantExists_Then_RemoveRestaurantIsCalled()
        {
            //Arrange
            restaurantRepositoryMock.Setup(r => r.RemoveRestaurant(It.IsAny<int>()));

            //Act
            restaurantService.RemoveRestaurant(1);

            //Assert
            restaurantRepositoryMock.Verify(r => r.RemoveRestaurant(1), Times.Once);

        }

        [Fact]
        public void GetRestaurant_When_RestaurantExists_Then_GetRestaurantIsCalled()
        {
            //Arrange
            restaurantRepositoryMock.Setup(r => r.GetRestaurant(It.IsAny<int>()));

            //Act
            restaurantService.GetRestaurant(1);

            //Assert
            restaurantRepositoryMock.Verify(r => r.GetRestaurant(1), Times.Once);

        }

        [Fact]
        public void GetActiveRestaurans_When_RestaurantExists_Then_GetActiveRestaurantsIsCalled()
        {
            //Arrange
            restaurantRepositoryMock.Setup(r => r.GetActiveRestaurants());

            //Act
            restaurantService.GetActiveRestaurants();

            //Assert
            restaurantRepositoryMock.Verify(r => r.GetActiveRestaurants(), Times.Once);
        }

        [Fact]
        public void GetInactiveRestaurans_When_RestaurantExists_Then_GetInactiveRestaurantsIsCalled()
        {
            //Arrange
            restaurantRepositoryMock.Setup(r => r.GetInactiveRestaurants());

            //Act
            restaurantService.GetInactiveRestaurants();

            //Assert
            restaurantRepositoryMock.Verify(r => r.GetInactiveRestaurants(), Times.Once);
        }  
    }
}