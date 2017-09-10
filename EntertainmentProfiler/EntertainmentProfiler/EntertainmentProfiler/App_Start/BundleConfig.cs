using System.Web;
using System.Web.Optimization;

namespace EntertainmentProfiler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.2.1.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // JS
            bundles.Add(new ScriptBundle("~/bundles/tools").Include(
                        "~/Content/FullMotionSources/js/jquery.poptrox.min.js",
                        "~/Content/FullMotionSources/js/jquery.scrolly.min.js",
                        "~/Content/FullMotionSources/js/skel.min.js",
                        "~/Content/FullMotionSources/js/util.js",
                        "~/Content/FullMotionSources/js/main.js",
                        "~/Scripts/EPScripts/EPScripts.js"                        
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/FullMotionSources/css/font-awesome.min.css",
                      "~/Content/FullMotionSources/css/main.css"
                      ));
        }
    }
}
