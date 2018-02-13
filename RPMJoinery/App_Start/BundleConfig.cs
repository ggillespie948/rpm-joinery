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
            "~/vendor/OwlCarousel2-2.2.1/dist/owl.carousel.min.js",
            "~/js/ImageModal.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/vendor/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/vendor/bootstrap/css/bootstrap.css",
                      "~/css/ImageModal.css",
                      "~/vendor/OwlCarousel2-2.2.1/dist/assets/owl.carousel.min.css",
                      "~/vendor/OwlCarousel2-2.2.1/dist/assets/owl.theme.default.min.css",
                      "~/css/Site.css"));


        }
    }
}
