using System.Net.Http.Formatting;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace UmbracoFood.Custom.Trees
{
    [PluginController("Restaurants")]
    [Umbraco.Web.Trees.Tree("Restaurants", "RestaurantsTree", "Restauracje")]
    public class RestaurantsApplicationTreeController : TreeController
    {
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var item = this.CreateTreeNode("dashboard", id, queryStrings, "My item", "icon-truck", false);
            nodes.Add(item);
            return nodes;
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