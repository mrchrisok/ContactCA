namespace ContactCA.Api.Core
{
   public interface ILocalStrings
   {
      string SiteTitle { get; }
      string PageTitle { get; }

   }

   public class LocalStrings : ILocalStrings
   {
      // demo/reminder that views can be injected into to enhance testability

      public string SiteTitle
      {
         get { return "Contact.CA"; }
      }
      public string PageTitle
      {
         get { return "Contact.CA"; }
      }
   }
}