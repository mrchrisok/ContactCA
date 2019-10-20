using ContactCA.Data.Entities;
using ContactCA.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactCA.Data.Repositories
{
   public class ContactRepository : ContactCARepositoryBase<Contact>, IContactRepository
   {
      public ContactRepository()
      {
      }

      #region Members.Override

      protected override Contact AddEntity(ContactCADbContext entityContext, Contact entity)
      {
         return entityContext.ContactSet.Add(entity);
      }

      protected override Contact UpdateEntity(ContactCADbContext entityContext, Contact entity)
      {
         return (from e in entityContext.ContactSet
                 where e.ContactID == entity.ContactID
                 select e).FirstOrDefault();
      }

      protected override IEnumerable<Contact> GetEntities(ContactCADbContext entityContext)
      {
         return from e in entityContext.ContactSet
                select e;
      }

      protected override Contact GetEntity(ContactCADbContext entityContext, int id)
      {
         var query = (from e in entityContext.ContactSet
                      where e.ContactID == id
                      select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion

      #region Members.IContactRepository

      public Contact GetContact(string email)
      {
         return GetContacts().SingleOrDefault(x => x.Email == email);
      }

      public Contact GetContactById(int id)
      {
         return GetContacts().SingleOrDefault(x => x.ContactID == id);
      }

      public IEnumerable<Contact> GetContacts()
      {
         using (ContactCADbContext entityContext = new ContactCADbContext())
            return GetEntities(entityContext).ToArray().ToList();
      }

      public IEnumerable<Contact> GetContactsByFirstName(string firstName)
      {
         return GetContacts().Where(x => x.FirstName == firstName);
      }

      public IEnumerable<Contact> GetContactsByLastName(string lastName)
      {
         return GetContacts().Where(x => x.LastName == lastName);
      }

      public IEnumerable<Contact> GetContactsByDateCreated(DateTime dateCreated)
      {
         return GetContacts().Where(x => x.DateCreated.Value.Date == dateCreated.Date);
      }

      public IEnumerable<Contact> GetContactsByCallDateRange(DateTime dateBottom, DateTime dateTop)
      {
         return GetContacts().Where(x => x.BestCallDateTime.Date >= dateBottom.Date && x.BestCallDateTime.Date <= dateTop.Date);
      }

      public IEnumerable<Contact> GetContactByCallTimeRange(DateTime timeBottom, DateTime timeTop)
      {
         return GetContacts().Where(x => x.BestCallDateTime.TimeOfDay >= timeBottom.TimeOfDay && x.BestCallDateTime.TimeOfDay <= timeTop.TimeOfDay);
      }
      #endregion
   }
}
