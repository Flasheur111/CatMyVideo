using System.Web;
using System.Web.Optimization;

namespace CatMyVideo
{
  public class BundleConfig
  {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/videojs").Include(
              "~/Scripts/video.js",
              "~/Scripts/video-quality-selector.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/momentjs").Include(
              "~/Scripts/moment.min.js"
            ));

            #region Foundation Bundles
            bundles.Add(Bundles.Foundation.Scripts());
            bundles.Add(Bundles.Coffeescript.Scripts());
            #endregion

            bundles.Add(new StyleBundle("~/Content/css").Include(
              "~/Content/site.css",
              "~/Content/foundation-icons.css",
              "~/Content/fonts.css",
              "~/Content/video-js.css",
              "~/Content/video-quality-selector.css"
            ));
        }
  }
}
