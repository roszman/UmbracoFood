using System;
using System.Collections.Generic;
using Lucene.Net.Index;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UmbracoFood.Core.Models;

namespace UmbracoFood.ViewModels
{
    public class CreateOrderViewModel
    {
        public string Owner { get; set; }

        public int SelectedRestaurantId { get; set; }

        public DateTime Deadline { get; set; }
        
        public string AccountNumber { get; set; }

        public IEnumerable<CreateOrderMeal> Meals { get; set; }
    }
}