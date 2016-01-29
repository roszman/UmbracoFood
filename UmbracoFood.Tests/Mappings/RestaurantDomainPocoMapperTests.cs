﻿using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class RestaurantDomainPocoMapperTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;
        private RestaurantMapper _mapper;

        public RestaurantDomainPocoMapperTests()
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
            _mapper = new RestaurantMapper();
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
            restaurantPoco.Phone = "123456789";

            //Act
            var restaurant = _mapper.MapToDomain(restaurantPoco);

            //Assert
            Assert.Equal(restaurant.ID, restaurantPoco.ID);
            Assert.Equal(restaurant.IsActive, restaurantPoco.IsActive);
            Assert.Equal(restaurant.MenuUrl, restaurantPoco.MenuUrl);
            Assert.Equal(restaurant.WebsiteUrl, restaurantPoco.WebsiteUrl);
            Assert.Equal(restaurant.Name, restaurantPoco.Name);
            Assert.Equal(restaurant.Phone, restaurantPoco.Phone);
            Assert.IsType<Restaurant>(restaurant);
        }
    }
}