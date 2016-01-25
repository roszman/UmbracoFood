using AutoMapper;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Mapping;
using UmbracoFood.ViewModels;
using Xunit;

namespace UmbracoFood.Tests.Mappings
{
    public class OrderProfileTests
    {
        private static readonly object Sync = new object();
        private static bool _configured;

        public OrderProfileTests()
        {
            lock (Sync)
            {
                if (!_configured)
                {
                    Mapper.Reset();

                    Mapper.Initialize(config => config.AddProfile(new OrderMappingProfile()));

                    _configured = true;
                    Mapper.AssertConfigurationIsValid();                   
                }
            }
        }

        [Fact]
        public void AddRestaurantViewModelShouldBeMappedToRestaurant()
        {

        }


    }
}