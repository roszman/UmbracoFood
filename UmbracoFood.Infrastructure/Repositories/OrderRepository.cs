using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Umbraco.Core.Persistence;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDatabaseProvider _databaseProvider;
        private readonly IModelMapper<Order, OrderPoco> _mapper;
        private UmbracoDatabase _db;

        public OrderRepository(IDatabaseProvider databaseProvider, IModelMapper<Order, OrderPoco> mapper )
        {
            _databaseProvider = databaseProvider;
            _mapper = mapper;
            _db = _databaseProvider.Db;
        }

        public int AddOrder(Order order)
        {
            var id = _db.Insert("Orders", "Id", _mapper.MapToPoco(order));
            return decimal.ToInt32((decimal)id);
        }

        public void EditOrder(Order order)
        {
            //var updateOrder = _db.SingleOrDefault<OrderPoco>("SELECT * FROM Orders WHERE Id = @0", order.Id);
            //if (updateOrder == null)
            //{
            //    throw new KeyNotFoundException("Order has not been found.");
            //}

            _db.Update("Order", "Id", _mapper.MapToPoco(order));
        }

        public void RemoveOrder(int id)
        {
            _db.Execute("DELETE FROM Orders WHERE Id = @0", id);
        }

        public Order GetOrder(int id)
        {
            var order = _db.SingleOrDefault<OrderPoco>("SELECT * FROM Orders WHERE Id = @0", id);
            if (order == null)
            {
                throw new KeyNotFoundException("Order has not been found.");
            }
            return _mapper.MapToDomain(order);
        }

        public IEnumerable<Order> GetOrders()
        {
            //var order = db.Query<OrderPoco>("SELECT * FROM Orders WHERE Active = 1");
            var orders = _db.Fetch<OrderPoco, OrderedMealPoco, OrderPoco>(
                new OrderMealRelator().MapIt,
                "SELECT * FROM Orders LEFT JOIN OrderedMeals ON OrderedMeals.OrderId = Order.Id ORDER BY OrderedMeals.Id"
                );
            return orders.Select(o => _mapper.MapToDomain(o));
        }
    }
}