using System.Collections.Generic;
using UmbracoFood.Core.Models;
using UmbracoFood.Infrastructure.Models.POCO;

namespace UmbracoFood.Infrastructure.Repositories
{
    public class OrderRelator
    {
        private OrderPoco _currentOrder;
        public OrderPoco MapIt(OrderPoco order, OrderedMealPoco orderedMeal, StatusPoco status, RestaurantPoco restaurant)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (order == null)
                return _currentOrder;

            // Is this the same author as the current one we're processing
            if (_currentOrder != null && _currentOrder.Id == order.Id)
            {
                // Yes, just add this post to the current author's collection of posts
                _currentOrder.OrderedMeals.Add(orderedMeal);

                // Return null to indicate we're not done with this author yet
                return null;
            }
            

            // This is a different author to the current one, or this is the
            // first time through and we don't have an author yet

            // Save the current author
            var previousOrder = _currentOrder;

            // Setup the new current author
            _currentOrder = order;
            _currentOrder.OrderedMeals = new List<OrderedMealPoco>();
            if (orderedMeal.OrderId == order.Id)
                _currentOrder.OrderedMeals.Add(orderedMeal);
            if (status.Id == order.StatusId)
                _currentOrder.Status = status;
            if (restaurant.ID == order.RestaurantId)
                _currentOrder.Restaurant = restaurant;
            // Return the now populated previous author (or null if first time through)
            return previousOrder;
        }
    }
}