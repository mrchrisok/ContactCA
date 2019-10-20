namespace ContactCA.Api.Core
{
   public interface ILocalStrings
   {
      string SiteTitle { get; }
      string GetPageTitle(string pageId);

   }

   public class LocalStrings : ILocalStrings
   {
      // demo/reminder that views can be injected into to enhance testability

      public string SiteTitle
      {
         get { return "ContactCA"; }
      }
      public virtual string GetPageTitle(string pageId)
      {
         return pageId;
      }
   }
}