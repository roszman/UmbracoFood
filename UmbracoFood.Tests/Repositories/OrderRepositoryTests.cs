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
    public class OrderRepositoryTests : IClassFixture<OrdersDatabaseFixture>
    {
        private readonly OrdersDatabaseFixture _databaseFixture;
        private Mock<IModelMapper<Order, OrderPoco>> _mapper;
        private OrderRepository _repo;

        public OrderRepositoryTests(OrdersDatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
            var dataBaseProvider = new Mock<IDatabaseProvider>();
            dataBaseProvider.Setup(dbp => dbp.Db).Returns(_databaseFixture.Db);
            _mapper = new Mock<IModelMapper<Order, OrderPoco>>();

            _repo = new OrderRepository(dataBaseProvider.Object, _mapper.Object);
        }

        [Fact]
        public void GetOrderShouldReturnOrder()
        {
            //arrange
            var orderPoco =
                _databaseFixture.Db
                .Query<OrderPoco>("SELECT * FROM Orders").First();
            var order = new Order()
            {
                Id = orderPoco.Id
            };

            _mapper.Setup(m => m.MapToDomain(It.IsAny<OrderPoco>())).Returns(order);

            //act
            var orderFromRepo = _repo.GetOrder(orderPoco.Id);

            //assert
            Assert.NotNull(orderFromRepo);
            Assert.Equal(orderPoco.Id, orderFromRepo.Id);
        }
    }
}
