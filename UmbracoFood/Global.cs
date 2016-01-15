using System;
using System.Web.Optimization;
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
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}