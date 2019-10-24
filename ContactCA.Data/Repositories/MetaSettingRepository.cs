using ContactCA.Data.Entities;
using ContactCA.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactCA.Data.Repositories
{
   public class MetaSettingRepository : ContactCARepositoryBase<MetaSetting>, IMetaSettingRepository
   {
      public MetaSettingRepository()
      {
         // need to figure out why migrations doesn't work in EF6+
         // looked all over .. stackO has no answers .. sooo frustrating
         // also no time to hook in db caching with EFCache pkg .. but never liked it
         //       heavy data caching sb via other services to take to load of the db server

         // until the migrations fix, dirty scaffold in a setting here
         // would otherwise do this in the db Seed method

         //using (ContactCADbContext entityContext = new ContactCADbContext())
         //{
         //   // check for an entity: environment/type/code/value="ShowClients"/enabled
         //   // if not exists, add these: 

         //   // environment: "DEVELOPMENT"
         //   //        type: "HOME_VIEWMODEL_PROPERTY"
         //   //        code: "ShowClients"
         //   //       value: "False"
         //   //     enabled: true

         //   // environment: "PRODUCTION"
         //   //        type: "HOME_VIEWMODEL_PROPERTY"
         //   //        code: "ShowClients"
         //   //       value: "False"
         //   //     enabled: false
         //}
      }

      #region DataRepositoryBase abstract implementations

      protected override MetaSetting AddEntity(ContactCADbContext entityContext, MetaSetting entity)
      {
         return entityContext.MetaSettingSet.Add(entity);
      }

      protected override MetaSetting UpdateEntity(ContactCADbContext entityContext, MetaSetting entity)
      {
         return (from e in entityContext.MetaSettingSet
                 where e.MetaSettingID == entity.MetaSettingID
                 select e).FirstOrDefault();
      }

      protected override IEnumerable<MetaSetting> GetEntities(ContactCADbContext entityContext)
      {
         return from e in entityContext.MetaSettingSet
                select e;
      }

      protected override MetaSetting GetEntity(ContactCADbContext entityContext, Guid id)
      {
         var query = (from e in entityContext.MetaSettingSet
                      where e.MetaSettingID == id
                      select e);

         var results = query.FirstOrDefault();

         return results;
      }
      #endregion

      #region Members.IContactRepository

      public IEnumerable<MetaSetting> GetMetaSettings()
      {
         using (ContactCADbContext entityContext = new ContactCADbContext())
            return GetEntities(entityContext).ToArray().ToList();
      }

      public MetaSetting GetMetaSetting(string type, string code, bool enabled)
      {
         return GetMetaSettings()
            .Where(x => x.Environment == _environment && x.Type == type && x.Code == code && x.Enabled == enabled)
            .FirstOrDefault();
      }

      public MetaSetting GetMetaSetting(string environment, string type, string code, bool enabled)
      {
         return GetMetaSettings()
            .Where(x => x.Environment == environment && x.Type == type && x.Code == code && x.Enabled == enabled)
            .FirstOrDefault();
      }

      #endregion
   }
}
