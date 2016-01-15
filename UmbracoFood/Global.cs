using System;
<<<<<<< HEAD
using System.Web.Http;
=======
using System.Web.Optimization;
>>>>>>> 2a43cd0... Added bootstrap
using Umbraco.Web;
using UmbracoFood;

namespace UmbracoFood
{
    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarting(object sender, EventArgs e)
        {
            base.OnApplicationStarting(sender, e);
        }

        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);

            AutofacConfig.Configure();
            AutomapperConfig.Configure();
<<<<<<< HEAD
            // to return json instead of xml in RestaurantsApiController
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
=======
            BundleConfig.RegisterBundles(BundleTable.Bundles);
>>>>>>> 2a43cd0... Added bootstrap
        }
    }
}