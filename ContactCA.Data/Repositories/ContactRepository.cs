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

      #region DataRepositoryBase overrides

      public override Contact Add(Contact entity)
      {
         // need to build some rules around when/if to save a contact
         // uniqueness by Email is not reasonable
         // how about save if the person sends a new Date, ie. Email and B

         if (entity.BestCallDateTime <= DateTime.UtcNow)
            throw new ApplicationException("Contact date is invalid.");

         var sameEmailContacts = GetContacts().Where(x => x.Email.Equals(entity.Email));

         if (sameEmailContacts.Count(x => x.BestCallDateTime >= DateTime.UtcNow) >= 5)
         {
            // already 5 in the db .. don't save this one
            return entity;
         }

         //if (sameEmailContacts.Any(x =>
         //   x.BestCallDateTime >= DateTime.UtcNow
         //   && x.BestCallDateTime.Year == entity.BestCallDateTime.Year
         //   && x.BestCallDateTime.Month == entity.BestCallDateTime.Month
         //   && (x.BestCallDateTime.Day == entity.BestCallDateTime.Day || x.BestCallDateTime.Day == entity.BestCallDateTime.Day + 1)))
         //{
         //   // 
         //   var random = new Random();
         //   entity.BestCallDateTime = entity.BestCallDateTime.AddDays(random.Next(2, 10));
         //}

         return base.Add(entity);
      }

      #endregion


      #region DataRepositoryBase abstract implementations

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
