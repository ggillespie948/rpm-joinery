using System.Web;
using System.Web.Optimization;

namespace RPMJoinery
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/vendor/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/imageModal").Include(
            "~/js/cardCarousel.js",
            "~/js/ImageModal.js"));

            bundles.Add(new ScriptBundle("~/bundles/contact").Include(
            "~/js/contact_me.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/vendor/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/ImageModal.css",
                      "~/vendor/bootstrap/css/bootstrap.css",
                      "~/css/Site.css"));


        }
    }
}
