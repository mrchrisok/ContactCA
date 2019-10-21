using ContactCA.Data.Entities;
using System;
using System.Collections.Generic;

namespace ContactCA.Data.Repositories.Contracts
{
   public interface IContactRepository : IDataRepository<Contact>
   {
      Contact GetContact(string email);
      Contact GetContactById(Guid id);
      IEnumerable<Contact> GetContacts();
      IEnumerable<Contact> GetContactsByFirstName(string firstName);
      IEnumerable<Contact> GetContactsByLastName(string lastName);
      IEnumerable<Contact> GetContactsByDateCreated(DateTime dateCreated);
      IEnumerable<Contact> GetContactsByCallDateRange(DateTime dateBottom, DateTime dateTop);
      IEnumerable<Contact> GetContactByCallTimeRange(DateTime timeBottom, DateTime timeTop);
   }
}
