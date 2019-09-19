using System.Web;
using System.Web.Optimization;

namespace ERP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                          "~/Scripts/jquery.validate.min.js",
                          "~/Scripts/jquery.unobtrusive-ajax.js",
                          "~/Scripts/jquery.validate.unobtrusive.min.js"
                         
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",


                       "~/Content/other/themes/base/jquery-ui.min.css",
                      // "~/Content/icomoon/styles.css",
                      "~/Content/other/bootstrap.min.css",
                      "~/Content/other/bootstrap-datepicker.css",
                      "~/Content/core.css",
                      "~/Content/other/components.min.css",
                      //"~/Content/other/bootstrap-editable.css",
                      "~/Content/other/colors.min.css"

                      ));
        }
    }
}
