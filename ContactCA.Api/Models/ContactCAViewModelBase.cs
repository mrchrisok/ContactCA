namespace ContactCA.Api.Models
{
   public class ContactCAViewModelBase : IContactCAViewModel
   {
      public bool ShowAll { get; set; }
      public bool ShowWelcome { get; set; }
      public bool ShowServices { get; set; }
      public bool ShowPortfolio { get; set; }
      public bool ShowAbout { get; set; }
      public bool ShowTeam { get; set; }
      public bool ShowClients { get; set; }
      public bool ShowContact { get; set; }
   }

   public interface IContactCAViewModel
   {
      bool ShowAll { get; set; }
      bool ShowWelcome { get; set; }
      bool ShowServices { get; set; }
      bool ShowPortfolio { get; set; }
      bool ShowAbout { get; set; }
      bool ShowTeam { get; set; }
      bool ShowClients { get; set; }
      bool ShowContact { get; set; }
   }
}