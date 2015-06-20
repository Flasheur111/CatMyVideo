using System.Web.Optimization;

namespace CatMyVideo.Bundles
{
    public static class Coffescript
    {
        public static Bundle Scripts()
        {
            return new ScriptBundle("~/bundles/coffee").Include(
                      "~/Scripts/coffee/Site.js");
        }
    }
}