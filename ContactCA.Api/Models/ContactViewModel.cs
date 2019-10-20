using ContactCA.Data.Entities;
using System;

namespace ContactCA.Api.Models
{
   public class ContactViewModel : Contact
   {
      public DateTime BestCallDate { get; set; }
      public DateTime BestCallTime { get; set; }
   }
}