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
            
            bundles.Add(new ScriptBundle("~/Scripts/angular/app").Include(
                    "~/Scripts/angular/angular-growl.min.js",
                    "~/Scripts/angular/umbracoFoodApp.js",
                    "~/Scripts/angular/utilService.js"
                ));


            bundles.Add(new StyleBundle("~/CSS/angular-growl").Include(
                    "~/Content/angular-growl.min.css"
                ));


            bundles.Add(new ScriptBundle("~/Scripts/AddRestaurant").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/AddRestaurantController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/AddRestaurantService.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Restaurants").Include(
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantsController.js",
                    "~/App_plugins/UmbracoFoodPlugins/Scripts/RestaurantSurface/RestaurantsService.js"
                ));




            //BundleTable.EnableOptimizations = true;
        }
    }
}