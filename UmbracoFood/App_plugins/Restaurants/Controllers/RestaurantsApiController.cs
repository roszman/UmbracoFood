using System.Collections.Generic;
using System.Web.Http;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using UmbracoFood.Core.Models;

namespace UmbracoFood.App_plugins.Restaurants.Controllers
{
    [PluginController("Restaurants")]
    public class RestaurantsApiController : UmbracoAuthorizedApiController
    {
        public IEnumerable<Restaurant> GetAll()
        {
            return new List<Restaurant>
            {
                new Restaurant
                {
                    ID = 1,
                    MenuUrl = "menu url",
                    Name = "nameJ",
                    WebsiteUrl = "website url",
                    Phone = "123455667"
                },
                new Restaurant
                {
                    ID = 2,
                    MenuUrl = "menu url",
                    Name = "nameJ",
                    WebsiteUrl = "website url",
                    Phone = "123455667"
                },
                new Restaurant
                {
                    ID = 3,
                    MenuUrl = "menu url",
                    Name = "nameJ",
                    WebsiteUrl = "website url",
                    Phone = "123455667"
                },
            };
        }

        public Restaurant GetById(int id)
        {
            return new Restaurant
            {
                ID = 1,
                MenuUrl = "menu url",
                Name = "nameJ",
                WebsiteUrl = "website url",
                Phone = "123455667"
            };
        }

        public Restaurant PostSave([FromBody]Restaurant restaurant)
        {
            return new Restaurant
            {
                ID = 1,
                MenuUrl = "menu url",
                Name = "nameJ",
                WebsiteUrl = "website url",
                Phone = "123455667"
            };
        }

        public void Put(int id, [FromBody]string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
