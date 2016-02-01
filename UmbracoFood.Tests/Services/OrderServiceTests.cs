using Moq;
using System;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Services;
using Xunit;

namespace UmbracoFood.Tests.Services
{
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> orderRepositoryMock;
        private OrderService orderService;

        public OrderServiceTests()
        {
            orderRepositoryMock = new Mock<IOrderRepository>();
            orderService = new OrderService(orderRepositoryMock.Object);
        }

        [Fact]
        public void RemoveOrderShoudlCallRemoveOrderInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.RemoveOrder(It.IsAny<int>()));

            //act
            orderService.RemoveOrder(1);

            //assert
            orderRepositoryMock.Verify(r => r.RemoveOrder(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GetOrderShoudlCallGetOrderInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.GetOrder(It.IsAny<int>()));

            //act
            orderService.GetOrder(1);

            //assert
            orderRepositoryMock.Verify(r => r.GetOrder(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void GetOrdersShoudlCallGetOrdersInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.GetOrders());

            //act
            orderService.GetOrders();

            //assert
            orderRepositoryMock.Verify(r => r.GetOrders(), Times.Once);
        }

        [Fact]
        public void GetStatusesShoudlCallGetStatusesInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.GetStatuses());

            //act
            orderService.GetStatuses();

            //assert
            orderRepositoryMock.Verify(r => r.GetStatuses(), Times.Once);
        }
        [Fact]
        public void AddMealShoudlCallAddMealInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.AddOrderedMeal(It.IsAny<OrderedMeal>()));

            //act
            orderService.AddMeal(new OrderedMeal());

            //assert
            orderRepositoryMock.Verify(r => r.AddOrderedMeal(It.IsAny<OrderedMeal>()), Times.Once);
        }
        [Fact]
        public void CreateOrderShoudlCallAddOrderInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.AddOrder(It.IsAny<Order>()));

            //act
            orderService.CreateOrder(new Order());

            //assert
            orderRepositoryMock.Verify(r => r.AddOrder(It.IsAny<Order>()), Times.Once);
        }
        [Fact]
        public void ChangeStatusShoudlCallChangeStatusInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.ChangeStatus(It.IsAny<int>(), It.IsAny<OrderStatus>()));

            //act
            orderService.ChangeStatus(1, OrderStatus.InKitchen);

            //assert
            orderRepositoryMock.Verify(r => r.ChangeStatus(It.IsAny<int>(), It.IsAny<OrderStatus>()), Times.Once);
        }

        [Fact]
        public void SetOrderIsInDeliveryShoudlCallSetOrderIsInDeliveryInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.SetOrderIsInDelivery(It.IsAny<int>(), It.IsAny<DateTime>()));

            //act
            orderService.SetOrderIsInDelivery(1, DateTime.Now);

            //assert
            orderRepositoryMock.Verify(r => r.SetOrderIsInDelivery(It.IsAny<int>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public void RemoveMealShouldCallRemoveOrderedMealInRepo()
        {
            //arrange
            orderRepositoryMock.Setup(r => r.RemoveOrderedMeal(1));

            //act
            orderService.RemoveMeal(1);

            //assert
            orderRepositoryMock.Verify(r => r.RemoveOrderedMeal(1), Times.Once);
        }
    }
}
