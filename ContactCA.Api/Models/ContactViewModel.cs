using System;

namespace ContactCA.Api.Models
{
   public class ContactViewModel : ContactCAViewModelBase, IContactViewModel
   {
      public int ContactID { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string PhoneNumber { get; set; }
      public DateTime BestCallDateTime { get; set; }
      public DateTime BestCallDate { get; set; }
      public DateTime BestCallTime { get; set; }
   }

   public interface IContactViewModel
   {
      int ContactID { get; set; }
      string FirstName { get; set; }
      string LastName { get; set; }
      string Email { get; set; }
      string PhoneNumber { get; set; }
      DateTime BestCallDateTime { get; set; }
      DateTime BestCallDate { get; set; }
      DateTime BestCallTime { get; set; }
   }
}