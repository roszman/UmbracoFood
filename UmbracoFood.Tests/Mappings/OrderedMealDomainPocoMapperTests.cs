using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class OrderedMealDomainPocoMapperTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;
        private OrderedMealMapper _mapper;

        public OrderedMealDomainPocoMapperTests()
        {
            lock (Sync)
            {
                if (!_configured)
                {
                    Mapper.Reset();
                    Mapper.Initialize(config => config.AddProfile(new OrderedMealMappingProfile()));
                    _configured = true;
                    Mapper.AssertConfigurationIsValid();
                }
            }
            _mapper = new OrderedMealMapper();
        }

        [Fact]
        public void OrderedMealShouldBeMappedToOrderedMealPoco()
        {
            //Arrange
            var orderedMeal = new OrderedMeal
            {
                Count = 6,
                MealName = "meal name",
                OrderId = 145,
                Id = 1765,
                Price = 234.67,
                PurchaserName = "purchaser name"

            };

            //Act
            var mappedOrderedMeal = _mapper.MapToPoco(orderedMeal);

            //Assert
            Assert.Equal(mappedOrderedMeal.Id, mappedOrderedMeal.Id);
            Assert.Equal(mappedOrderedMeal.MealName, mappedOrderedMeal.MealName);
            Assert.Equal(mappedOrderedMeal.Count, mappedOrderedMeal.Count);
            Assert.Equal(mappedOrderedMeal.OrderId, mappedOrderedMeal.OrderId);
            Assert.Equal(mappedOrderedMeal.Price, mappedOrderedMeal.Price);
            Assert.Equal(mappedOrderedMeal.PurchaserName, mappedOrderedMeal.PurchaserName);
            Assert.IsType<OrderedMealPoco>(mappedOrderedMeal);
        }

        [Fact]
        public void OrderedMealPocoShouldBeMappedToOrderedMeal()
        {
            //Arrange
            var orderedMealPoco = new OrderedMealPoco
            {
                Count = 6,
                MealName = "meal name",
                OrderId = 145,
                Id = 1765,
                Price = 234.67,
                PurchaserName = "purchaser name"

            };

            //Act
            var mappedOrderedMeal = _mapper.MapToDomain(orderedMealPoco);

            //Assert
            Assert.Equal(mappedOrderedMeal.Id, mappedOrderedMeal.Id);
            Assert.Equal(mappedOrderedMeal.MealName, mappedOrderedMeal.MealName);
            Assert.Equal(mappedOrderedMeal.Count, mappedOrderedMeal.Count);
            Assert.Equal(mappedOrderedMeal.OrderId, mappedOrderedMeal.OrderId);
            Assert.Equal(mappedOrderedMeal.Price, mappedOrderedMeal.Price);
            Assert.Equal(mappedOrderedMeal.PurchaserName, mappedOrderedMeal.PurchaserName);
            Assert.IsType<OrderedMeal>(mappedOrderedMeal);
        }
    }
}