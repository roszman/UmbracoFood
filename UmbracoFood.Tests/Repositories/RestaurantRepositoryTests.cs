﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Infrastructure.Repositories;
using UmbracoFood.Tests.Repositories.DatabaseFixtures;
using Xunit;

namespace UmbracoFood.Tests.Repositories
{
    [Collection("Database collection")]
    public class RestaurantRepositoryTests
    {
        private readonly DatababaseFixture _databaseFixture;
        private RestaurantRepository _repo;
        private Mock<IModelMapper<Restaurant, RestaurantPoco>> _mapper;

        public RestaurantRepositoryTests(DatababaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;

            var dataBaseProvider = new Mock<IDatabaseProvider>();
            dataBaseProvider.Setup(dbp => dbp.Db).Returns(_databaseFixture.Db);
            _mapper = new Mock<IModelMapper<Restaurant, RestaurantPoco>>();

            _repo = new RestaurantRepository(dataBaseProvider.Object, _mapper.Object);
        }

        [Fact]
        public void AddRestaurantShouldSaveRestaurantInDb()
        {
            //arrange
            var restaurantPoco = new RestaurantPoco
            {
                WebsiteUrl = "website url",
                MenuUrl = "menu url",
                Name = "name",
                Phone = "1234356",
                IsActive = true,
                FreeShippingThreshold = 543.24,
                ShippingRate = 14.56
            };

            _mapper.Setup(m => m.MapToPoco(It.IsAny<Restaurant>())).Returns(restaurantPoco);

            //act
            var id = _repo.AddRestaurant(new Restaurant());

            //assert
            var addedRestaurant = _databaseFixture.Db.SingleOrDefault<RestaurantPoco>("SELECT * FROM Restaurants WHERE Id = @0", id);
            _mapper.Verify(m => m.MapToPoco(It.IsAny<Restaurant>()), Times.Once);
            Assert.NotNull(addedRestaurant);
            Assert.Equal(restaurantPoco.WebsiteUrl, addedRestaurant.WebsiteUrl);
            Assert.Equal(restaurantPoco.MenuUrl, addedRestaurant.MenuUrl);
            Assert.Equal(restaurantPoco.Name, addedRestaurant.Name);
            Assert.Equal(restaurantPoco.Phone, addedRestaurant.Phone);
            Assert.Equal(restaurantPoco.IsActive, addedRestaurant.IsActive);
            Assert.Equal(restaurantPoco.FreeShippingThreshold, addedRestaurant.FreeShippingThreshold);
            Assert.Equal(restaurantPoco.ShippingRate, addedRestaurant.ShippingRate);
        }

        [Fact]
        public void EditRestaurantShouldUpdateRestaurantInDb()
        {
            //arrange
            var restaurantId = _databaseFixture.Db.Query<RestaurantPoco>("SELECT * FROM Restaurants").First().ID;
            var updatedRestaurantPoco = new RestaurantPoco
            {
                ID = restaurantId,
                IsActive = false,
                MenuUrl = "nowe menu url",
                WebsiteUrl = "nowy website url",
                Name = "nowy name",
                Phone = "12345676",
                ShippingRate = 123.67,
                FreeShippingThreshold = 2345435634.76
            };
            _mapper.Setup(m => m.MapToPoco(It.IsAny<Restaurant>())).Returns(updatedRestaurantPoco);
            //act
            _repo.EditRestaurant(new Restaurant() {ID = restaurantId});

            //assert
            var updatedRestaurant =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Id = @0", restaurantId)
                .FirstOrDefault();

            Assert.NotNull(updatedRestaurant);
            Assert.Equal(updatedRestaurantPoco.WebsiteUrl, updatedRestaurant.WebsiteUrl);
            Assert.Equal(updatedRestaurantPoco.MenuUrl, updatedRestaurant.MenuUrl);
            Assert.Equal(updatedRestaurantPoco.Name, updatedRestaurant.Name);
            Assert.Equal(updatedRestaurantPoco.Phone, updatedRestaurant.Phone);
            Assert.Equal(updatedRestaurantPoco.IsActive, updatedRestaurant.IsActive);
            Assert.Equal(updatedRestaurantPoco.FreeShippingThreshold, updatedRestaurant.FreeShippingThreshold);
            Assert.Equal(updatedRestaurantPoco.ShippingRate, updatedRestaurant.ShippingRate);
        }

        [Fact]
        public void RemoveRestaurantShouldSetIsActiveToFalse()
        {
            //arrange
            var restaurantPoco =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 1").First();

            //act
            _repo.RemoveRestaurant(restaurantPoco.ID);

            //assert
            var inactiveRestaurant =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Id = @0", restaurantPoco.ID)
                .FirstOrDefault();

            Assert.NotNull(inactiveRestaurant);
            Assert.False(inactiveRestaurant.IsActive);
        }

        [Fact]
        public void RemoveOrderShouldThrowExceptionIfOrderDoesNotExistInDb()
        {
            //arrange

            //act
            Exception ex = Assert.Throws<KeyNotFoundException>(() => _repo.RemoveRestaurant(1234566));

            //assert
            Assert.Equal("Restaurant 1234566 not found in database", ex.Message);
        }

        [Fact]
        public void GetRestaurantShouldCallRestaurantMapperWithValidArgument()
        {
            //arrange
            var restaurantPoco =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 1").First();
            var restaurant = new Restaurant
            {
                ID = restaurantPoco.ID,
                IsActive = false,
                MenuUrl = "nowe menu url",
                WebsiteUrl = "nowy website url",
                Name = "nowy name",
                Phone = "12345676",
                ShippingRate = 123.67,
                FreeShippingThreshold = 2345435634.76
            };

            _mapper.Setup(m => m.MapToDomain(It.IsAny<RestaurantPoco>())).Returns(restaurant);
            RestaurantPoco mapperArgument = new RestaurantPoco();
            _mapper.Setup(c => c.MapToDomain(It.IsAny<RestaurantPoco>()))
                    .Callback<RestaurantPoco>(o => mapperArgument = o)
                    .Returns(new Restaurant());

            //act
            var restaurantFromRepo = _repo.GetRestaurant(restaurantPoco.ID);

            //assert
            Assert.NotNull(restaurantFromRepo);
            Assert.Equal(restaurantPoco.ID, mapperArgument.ID);
            Assert.Equal(restaurantPoco.WebsiteUrl, mapperArgument.WebsiteUrl);
            Assert.Equal(restaurantPoco.MenuUrl, mapperArgument.MenuUrl);
            Assert.Equal(restaurantPoco.Name, mapperArgument.Name);
            Assert.Equal(restaurantPoco.Phone, mapperArgument.Phone);
            Assert.Equal(restaurantPoco.IsActive, mapperArgument.IsActive);
            Assert.Equal(restaurantPoco.FreeShippingThreshold, mapperArgument.FreeShippingThreshold);
            Assert.Equal(restaurantPoco.ShippingRate, mapperArgument.ShippingRate);
        }

        [Fact]
        public void GetActiveRestaurantsShouldReturnActiveRestaurants()
        {
            //arrange
            var restaurants =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 1");

            //act
            var activeRestaurants = _repo.GetActiveRestaurants();

            //assert
            Assert.Equal(restaurants.Count(), activeRestaurants.Count());
        }

        [Fact]
        public void GetInactiveRestaurantsShouldReturnInactiveRestaurants()
        {
            //arrange
            var restaurants =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 0");

            //act
            var inactiveRestaurants = _repo.GetInactiveRestaurants();

            //assert
            Assert.Equal(restaurants.Count(), inactiveRestaurants.Count());
        }
    }
}