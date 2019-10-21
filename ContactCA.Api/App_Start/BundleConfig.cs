using System.Web.Optimization;

namespace ContactCA.Api
{
   public class BundleConfig
   {
      // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
      public static void RegisterBundles(BundleCollection bundles)
      {
         // layout
         // --------------------------
         bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
            "~/Content/bootstrap.min.css"
            , "~/Content/bootstrap-datetimepicker.min.css"
            ));

         bundles.Add(new ScriptBundle("~/bundles/moment").Include(
            "~/Scripts/moment.min.js"
            ));

         bundles.Add(new StyleBundle("~/Content/fonts").Include(
            "~/Content/vendor/fontawesome-free/css/all.min.css"));

         bundles.Add(new StyleBundle("~/Content/site").Include(
            "~/Content/css/agency.min.css",
            "~/Content/Site.css"
            ));

         //
         bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-3.4.1.min.js"
            //, "~/Scripts/jquery-ui-1.12.1.min.js"
            ));

         bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            "~/Scripts/bootstrap.bundle.min.js"));

         bundles.Add(new ScriptBundle("~/bundles/misc").Include(
            "~/Scripts/jquery.easing.1.3.js"
            ));

         bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
             "~/Scripts/knockout-3.5.0.js",
             "~/Scripts/knockout.mapping-latest.js"));


         // contact
         // --------------------------------

         bundles.Add(new ScriptBundle("~/bundles/contact").Include(
               "~/Content/js/jqBootstrapValidation.js"
               , "~/Content/js/contact_me.js"
               , "~/Scripts/bootstrap-datetimepicker.min.js"
               , "~/Models/Client/ContactViewModel.js"
               ));

         bundles.Add(new ScriptBundle("~/bundles/agency").Include(
            "~/Content/js/agency.min.js"));

         //// Use the development version of Modernizr to develop with and learn from. Then, when you're
         //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
         //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
         //            "~/Scripts/modernizr-*"));

      }
   }
}
