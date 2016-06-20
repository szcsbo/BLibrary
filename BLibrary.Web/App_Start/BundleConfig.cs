using System.Web;
using System.Web.Optimization;
namespace BLibrary.Framework.Portal
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
        }
    }
}