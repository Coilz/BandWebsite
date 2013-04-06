using System.Web.Optimization;

namespace Ewk.BandWebsite.Web.UI.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/libs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.easing.{version}.js",
                "~/Scripts/jquery.mousewheel-{version}.pack.js",
                "~/Scripts/jquery.fancybox.pack.js",
                "~/Scripts/jquery.fancybox-*",
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/koExternalTemplateEngine_all.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/libs/css").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.resizable.css",
                "~/Content/themes/base/jquery.ui.selectable.css",
                "~/Content/themes/base/jquery.ui.accordion.css",
                "~/Content/themes/base/jquery.ui.autocomplete.css",
                "~/Content/themes/base/jquery.ui.button.css",
                "~/Content/themes/base/jquery.ui.dialog.css",
                "~/Content/themes/base/jquery.ui.slider.css",
                "~/Content/themes/base/jquery.ui.tabs.css",
                "~/Content/themes/base/jquery.ui.datepicker.css",
                "~/Content/themes/base/jquery.ui.progressbar.css",
                "~/Content/themes/base/jquery.ui.theme.css",
                "~/Content/jquery.fancybox*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bandwebsite/site.css",
                "~/Content/bandwebsite/home.css",
                "~/Content/bandwebsite/entityview.css",
                "~/Content/bandwebsite/blog.css",
                "~/Content/bandwebsite/music.css",
                "~/Content/bandwebsite/performance.css"));

            bundles.Add(new ScriptBundle("~/bundles/bandwebsite").Include(
                "~/Scripts/bandwebsite/bandwebsite.js",
                "~/Scripts/bandwebsite/header/header.js",
                "~/Scripts/bandwebsite/header/picturebar.js",
                "~/Scripts/bandwebsite/home.js",
                "~/Scripts/bandwebsite/blog.js"));
        }
    }
}