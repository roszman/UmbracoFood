using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Umbraco.Web;
using System.Web.Optimization;

namespace UmbracoFood
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/CSS/Common").Include(
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/jQuery").Include(
                    "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Bootstrap").Include(
                    "~/Scripts/bootstrap.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/angular").Include(
                    "~/Scripts/angular/angular.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/angular-ui").Include(
                    "~/Scripts/angular-ui/ui-bootstrap.js",
                    "~/Scripts/angular-ui/ui-bootstrap-tpls.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/angular/app").Include(
                    "~/Scripts/angular/angular-growl.min.js",
                    "~/Scripts/angular/umbracoFoodApp.js",
                    "~/Scripts/angular/utilService.js",
                    "~/Scripts/angular/angular-directives.js"
                ));

            bundles.Add(new StyleBundle("~/CSS/angular-growl").Include(
                    "~/Content/angular-growl.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/AddRestaurant").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/AddRestaurantController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/EditRestaurant").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/EditRestaurantController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Restaurants").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantsController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Orders").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/OrderSurface/OrdersController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/OrderSurface/OrderService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/CreateOrder").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/OrderSurface/CreateOrderController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/OrderSurface/OrderService.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/EditOrder").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/OrderSurface/EditOrderController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/OrderSurface/OrderService.js"
                ));
        }
    }
}