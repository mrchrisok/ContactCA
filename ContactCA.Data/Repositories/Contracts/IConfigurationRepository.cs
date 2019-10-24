using ContactCA.Data.Entities;
using System.Collections.Generic;

namespace ContactCA.Data.Repositories.Contracts
{
   public interface IConfigurationRepository
   {
      // todo: set this up as a front repo for all the config repos

      IMetaSettingRepository MetaSettingRepository { get; set; }
   }

   public interface IMetaSettingRepository : IDataRepository<MetaSetting>
   {
      IEnumerable<MetaSetting> GetMetaSettings();
      MetaSetting GetMetaSetting(string type, string code, bool enabled);
      MetaSetting GetMetaSetting(string environment, string type, string code, bool enabled);
   }
}
