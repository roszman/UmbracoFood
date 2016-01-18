using System;
using System.Web.Http;
using System.Web.Optimization;
using Umbraco.Web;
using UmbracoFood;

namespace UmbracoFood
{
    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);

            AutofacConfig.Configure();
            AutomapperConfig.Configure();
            // to return json instead of xml in RestaurantsApiController
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}