using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Umbraco.Core.Persistence;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Models.POCO;
using System;

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
            try
            {
                _db.BeginTransaction();
                // Do transacted updates here
                var poco = _mapper.MapToPoco(order);
                var insertResult = _db.Insert("Orders", "Id", poco);
                var id = Convert.ToInt32(insertResult);
                foreach(var orderedMeal in poco.OrderedMeals)
                {
                    orderedMeal.OrderId = id;
                    _db.Insert("OrderedMeals", "Id", orderedMeal);
                }

                // Commit
                _db.CompleteTransaction();
                return id;
            }
            catch(Exception)
            {
                _db.AbortTransaction();
                throw;
            }
        }

        public void EditOrder(Order order)
        {
            var orderPoco = _mapper.MapToPoco(order);
            try
            {
                _db.BeginTransaction();

                _db.Execute("DELETE FROM OrderedMeals Where OrderId = @0", orderPoco.Id);
                foreach (var orderedMealPoco in orderPoco.OrderedMeals)
                {
                    orderedMealPoco.OrderId = orderPoco.Id;
                    _db.Insert("OrderedMeals", "Id", orderedMealPoco);
                }
                _db.Update("Orders", "Id", orderPoco);
                _db.CompleteTransaction();
            }
            catch (Exception)
            {
                _db.AbortTransaction();
                throw;
            }
        }

        public void RemoveOrder(int id)
        {

            try
            {
                _db.BeginTransaction();

                _db.Execute("DELETE FROM OrderedMeals WHERE OrderId = @0", id);
                _db.Execute("DELETE FROM Orders WHERE Id = @0", id);

                _db.CompleteTransaction();
            }
            catch (Exception)
            {
                _db.AbortTransaction();
                throw;
            }
        }

        public Order GetOrder(int id)
        {
            var order = _db.Fetch<OrderPoco, OrderedMealPoco, StatusPoco, RestaurantPoco, OrderPoco>(
                new OrderRelator().MapIt,
                "SELECT * FROM Orders"
                + " LEFT JOIN OrderedMeals ON OrderedMeals.OrderId = Orders.Id"
                + " LEFT JOIN Statuses ON Statuses.Id = Orders.StatusId"
                + " LEFT JOIN Restaurants ON Restaurants.Id = Orders.RestaurantId"
                + " WHERE Orders.Id = @0"
                , id
                ).FirstOrDefault();
            if (order == null)
            {
                throw new KeyNotFoundException("Order has not been found.");
            }
            return _mapper.MapToDomain(order);
        }

        public IEnumerable<Order> GetOrders()
        {
            //var order = db.Query<OrderPoco>("SELECT * FROM Orders WHERE Active = 1");
            var orders = _db.Fetch<OrderPoco, OrderedMealPoco, StatusPoco, RestaurantPoco, OrderPoco>(
                new OrderRelator().MapIt,
                "SELECT * FROM Orders"
                + " LEFT JOIN OrderedMeals ON OrderedMeals.OrderId = Orders.Id"
                + " LEFT JOIN Statuses ON Statuses.Id = Orders.StatusId"
                + " LEFT JOIN Restaurants ON Restaurants.Id = Orders.RestaurantId"
                );
            return orders.Select(o => _mapper.MapToDomain(o));
        }
    }
}