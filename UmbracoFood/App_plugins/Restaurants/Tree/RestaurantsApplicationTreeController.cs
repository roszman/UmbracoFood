using System.Net.Http.Formatting;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace UmbracoFood.App_plugins.Restaurants.Tree
{
    [PluginController("Restaurants")]
    [Umbraco.Web.Trees.Tree("Restaurants", "RestaurantsTree", "Restauracje")]
    public class RestaurantsApplicationTreeController : TreeController
    {
        private const string InactiveRestaurants = "inactiverRestaurants";
        private const string ActiveRestaurants = "nactiverRestaurants";
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            if (id.Equals("-1"))
            {
                TreeNodeCollection restaurants = new TreeNodeCollection();
                TreeNode activeRestaurantsNode = this.CreateTreeNode(ActiveRestaurants, id, queryStrings, "Active Restaurants", "icon-food", true);
                restaurants.Add(activeRestaurantsNode);

                TreeNode inactiveRestaurantsNode = this.CreateTreeNode(InactiveRestaurants, id, queryStrings, "Inactive Restaurants", "icon-food", true);
                restaurants.Add(inactiveRestaurantsNode);
                return restaurants;
            }
            if (id.Equals(ActiveRestaurants))
            {
                TreeNodeCollection restaurants = new TreeNodeCollection();
                TreeNode activeRestaurantsNode = this.CreateTreeNode(ActiveRestaurants, id, queryStrings, "My item", "icon-wine-glass", false);
                restaurants.Add(activeRestaurantsNode);
                return restaurants;

            }
            if (id.Equals(InactiveRestaurants))
            {
                TreeNodeCollection restaurants = new TreeNodeCollection();
                TreeNode activeRestaurantsNode = this.CreateTreeNode(ActiveRestaurants, id, queryStrings, "My item", "icon-wine-glass", false);
                restaurants.Add(activeRestaurantsNode);
                return restaurants;
            }

            return null;

        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            menu.DefaultMenuAlias = ActionNew.Instance.Alias;
            menu.Items.Add<ActionNew>("Create");
            return menu;
        }
    }
}