using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public int AddOrder(Order order)
        {
            var id = db.Insert("Orders", "Id", Mapper.Map<Order, OrderPoco>(order));
            return decimal.ToInt32((decimal)id);
        }

        public void EditOrder(Order order)
        {
            var updateOrder = db.SingleOrDefault<OrderPoco>("SELECT * FROM Orders WHERE Id = @0", order.Id);
            if (updateOrder == null)
            {
                throw new KeyNotFoundException("Order has not been found.");
            }

            db.Update("Order", "Id", Mapper.Map(order, updateOrder));
        }

        public void RemoveOrder(int id)
        {
            db.Execute("DELETE FROM Orders WHERE Id = @0", id);
        }

        public Order GetOrder(int id)
        {
            var oreder = db.SingleOrDefault<OrderPoco>("SELECT * FROM Orders WHERE Id = @0", id);
            if (oreder == null)
            {
                throw new KeyNotFoundException("Order has not been found.");
            }
            return Mapper.Map<Order>(oreder);
        }

        public IEnumerable<Order> GetOrders()
        {
            //var order = db.Query<OrderPoco>("SELECT * FROM Orders WHERE Active = 1");
            var orders = db.Fetch<Order, OrderedMeal, Order>(
                new OrderMealRelator().MapIt,
                "SELECT * FROM Orders LEFT JOIN OrderedMeals ON OrderedMeals.OrderId = Order.Id ORDER BY OrderedMeals.Id"
                );
            return orders.Select(Mapper.Map<Order>);
        }
    }
}