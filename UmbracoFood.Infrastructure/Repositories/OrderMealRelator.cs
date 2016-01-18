using System.Collections.Generic;
using UmbracoFood.Core.Models;

internal class OrderMealRelator
{
    public Order currentOrder;
    public Order MapIt(Order order, OrderedMeal orderedMeal)
    {
        // Terminating call.  Since we can return null from this function
        // we need to be ready for PetaPoco to callback later with null
        // parameters
        if (order == null)
            return currentOrder;

        // Is this the same author as the current one we're processing
        if (currentOrder != null && currentOrder.Id == order.Id)
        {
            // Yes, just add this post to the current author's collection of posts
            currentOrder.OrderedMeals.Add(orderedMeal);

            // Return null to indicate we're not done with this author yet
            return null;
        }

        // This is a different author to the current one, or this is the 
        // first time through and we don't have an author yet

        // Save the current author
        var previousOrder = currentOrder;

        // Setup the new current author
        currentOrder = order;
        currentOrder.OrderedMeals = new List<OrderedMeal>();
        currentOrder.OrderedMeals.Add(orderedMeal);

        // Return the now populated previous author (or null if first time through)
        return previousOrder;
    }
}