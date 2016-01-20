using System.Linq;
using Moq;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using UmbracoFood.Infrastructure.Repositories;
using Xunit;

namespace UmbracoFood.Tests.Repositories
{
    public class RestaurantRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _databaseFixture;
        private RestaurantRepository _repo;
        private Mock<IModelMapper<Restaurant, RestaurantPoco>> _mapper;

        public RestaurantRepositoryTests(DatabaseFixture databaseFixture)
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
                IsActive = true
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
                Phone = "12345676"
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
        public void GetRestaurantShouldReturnRestaurant()
        {
            //arrange
            var restaurantPoco =
                _databaseFixture.Db
                .Query<RestaurantPoco>("SELECT * FROM Restaurants WHERE Active = 1").First();
            var restaurant = new Restaurant
            {
                ID = restaurantPoco.ID
            };

            _mapper.Setup(m => m.MapToDomain(It.IsAny<RestaurantPoco>())).Returns(restaurant);

            //act
            var restaurantFromRepo = _repo.GetRestaurant(restaurantPoco.ID);

            //assert
            Assert.NotNull(restaurantFromRepo);
            Assert.Equal(restaurantPoco.ID, restaurantFromRepo.ID);
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