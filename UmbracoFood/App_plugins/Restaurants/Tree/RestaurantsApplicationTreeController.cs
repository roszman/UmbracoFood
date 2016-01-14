using System.Collections.Generic;
using System.Net.Http.Formatting;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using UmbracoFood.Core.Interfaces;
using UmbracoFood.Core.Models;

namespace UmbracoFood.App_plugins.Restaurants.Tree
{
    [PluginController("Restaurants")]
    [Umbraco.Web.Trees.Tree("Restaurants", "RestaurantsTree", "Restauracje")]
    public class RestaurantsApplicationTreeController : TreeController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsApplicationTreeController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        private const string InactiveRestaurants = "inactiveRestaurants";
        private const string ActiveRestaurants = "activeRestaurants";
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            if (id.Equals("-1"))
            {
                TreeNodeCollection restaurants = new TreeNodeCollection();
                TreeNode activeRestaurantsNode = this.CreateTreeNode(ActiveRestaurants, id, queryStrings, "Active Restaurants", "icon-food", true, "/Restaurants/RestaurantsTree/activeRestaurants/1");
                restaurants.Add(activeRestaurantsNode);

                TreeNode inactiveRestaurantsNode = this.CreateTreeNode(InactiveRestaurants, id, queryStrings, "Inactive Restaurants", "icon-food", true, "/Restaurants/RestaurantsTree/inactiveRestaurants/1");
                restaurants.Add(inactiveRestaurantsNode);
                return restaurants;
            }
            if (id.Equals(ActiveRestaurants))
            {
                TreeNodeCollection restaurants = new TreeNodeCollection();
                IEnumerable<Restaurant> activeRestaurants = _restaurantService.GetActiveRestaurants();
                foreach (var activeRestaurant in activeRestaurants)
                {
                    TreeNode activeRestaurantsNode = this.CreateTreeNode(activeRestaurant.ID.ToString(), id, queryStrings, activeRestaurant.Name, "icon-wine-glass", false);
                    restaurants.Add(activeRestaurantsNode);
                }
                return restaurants;

            }
            if (id.Equals(InactiveRestaurants))
            {
                TreeNodeCollection restaurants = new TreeNodeCollection();
                IEnumerable<Restaurant> inactiveRestaurants = _restaurantService.GetInactiveRestaurants();
                foreach (var inactiveRestaurant in inactiveRestaurants)
                {
                    TreeNode activeRestaurantsNode = this.CreateTreeNode(inactiveRestaurant.ID.ToString(), id, queryStrings, inactiveRestaurant.Name, "icon-wine-glass", false);
                    restaurants.Add(activeRestaurantsNode);
                }
                return restaurants;
            }

            return null;

        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == ActiveRestaurants || id == InactiveRestaurants)
            {
                // root actions
                //menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));

                menu.Items.Add<ActionNew>("Create");
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
                return menu;
            }
            else if(id != "-1")
            {
                //menu.DefaultMenuAlias = ActionDelete.Instance.Alias;
                menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias));

            }
            return menu;
        }
    }
}
