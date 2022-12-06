using System.Web;
using System.Web.Optimization;

namespace Easy.Hosts.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-3.2.1.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/custom.js",
                        "~/Scripts/stellar.js",
                        "~/Scripts/popper.js",
                        "~/Content/vendors/nice-select/jquery.nice-select.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/_elements.css",
                      "~/Content/css/_footer.css",
                      "~/Content/css/_testmonial.css",
                      "~/Content/css/_variables.css",
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/responsive.css",
                      "~/Content/css/style-royal-template.map",
                      "~/Content/css/style-royal.css",
                      "~/Content/css/style.css",
                      "~/Content/vendors/linericon/linericon_style.css",
                      "~/Content/vendors/linericon/Linearicons-Free.woff",
                      "~/Content/vendors/linericon/Linearicons-Free.woff2",
                      "~/Content/vendors/nice-select/css/nice-select.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-icons").Include(
                   "~/Content/bootstrap-icons/bootstrap-icons.css",
                   "~/Content/bootstrap-icons/bootstrap-icons.json",
                   "~/Content/bootstrap-icons/bootstrap-icons.scss",
                   "~/Content/bootstrap-icons/fonts/bootstrap-icons.woff",
                   "~/Content/bootstrap-icons/fonts/bootstrap-icons.woff2"));



        }
    }
}
