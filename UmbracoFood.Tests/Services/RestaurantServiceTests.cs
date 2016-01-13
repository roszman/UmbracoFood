using Moq;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Services;

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



    }
}