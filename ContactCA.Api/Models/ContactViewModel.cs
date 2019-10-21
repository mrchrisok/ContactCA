using System;
using System.ComponentModel.DataAnnotations;

namespace ContactCA.Api.Models
{
   public class ContactViewModel : ContactCAViewModelBase, IContactViewModel
   {
      public string ContactID { get; set; }
      [StringLength(40)]
      public string FirstName { get; set; }
      [StringLength(40)]
      public string LastName { get; set; }
      [StringLength(80)]
      public string EmailAdress { get; set; }
      [StringLength(16)]
      public string Telephone { get; set; }
      [StringLength(400)]
      public string Message { get; set; }
      public DateTime BestTimeToCall { get; set; }
   }

   public interface IContactViewModel
   {
      string ContactID { get; set; }
      string FirstName { get; set; }
      string LastName { get; set; }
      string EmailAdress { get; set; }
      string Telephone { get; set; }
      DateTime BestTimeToCall { get; set; }
   }
}