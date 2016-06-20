using System.Web;
using System.Web.Optimization;
namespace BLibrary.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.unobtrusive-ajax*"));

            bundles.Add(new ScriptBundle("~/bundles/foolproof").Include(
              "~/Scripts/MicrosoftAjax.js",
              "~/Scripts/MicrosoftMvcAjax.js",
              "~/Scripts/MicrosoftMvcValidation.js",
              "~/Scripts/MvcFoolproofValidation.min.js",
              "~/Scripts/mvcfoolproof.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));


            bundles.Add(new ScriptBundle("~/Content/Scripts").Include("~/Scripts/bootstrap.min.js",
              "~/Scripts/DataTables/jquery.dataTables.min.js",
              "~/Scripts/DataTables/dataTables.responsive.min.js",
              "~/Scripts/DataTables/dataTables.bootstrap4.min.js",
              "~/Scripts/DataTables/responsive.bootstrap4.min.js"
             ));

            bundles.Add(new ScriptBundle("~/Content/custom").Include(
             "~/Scripts/custom/blib/blib.datatable.js"
             ));

            //css
            bundles.Add(new StyleBundle("~/Content/themes/font-awesome").Include("~/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
               "~/Content/bootstrap.css",
               "~/Content/DataTables/css/responsive.bootstrap4.css",
               "~/Content/DataTables/css/dataTables.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                      "~/Content/themes/base/core.css",
                      "~/Content/themes/base/resizable.css",
                      "~/Content/themes/base/selectable.css",
                      "~/Content/themes/base/accordion.css",
                      "~/Content/themes/base/autocomplete.css",
                      "~/Content/themes/base/button.css",
                      "~/Content/themes/base/dialog.css",
                      "~/Content/themes/base/slider.css",
                      "~/Content/themes/base/tabs.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/progressbar.css",
                      "~/Content/themes/base/theme.css"
              ));
        }
    }
}