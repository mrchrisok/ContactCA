using ContactCA.Data.Repositories.Contracts;
using System;

namespace ContactCA.Data.Entities
{
   public class MetaSetting : EntityBase, IIdentifiableEntity
   {
      public MetaSetting()
      {

      }

      public Guid MetaSettingID { get; set; }
      public string Environment { get; set; }
      public string @Type { get; set; }
      public string Code { get; set; }
      public string Value { get; set; }
      public int SortOrder { get; set; }
      public bool Enabled { get; set; }

      public Guid EntityID { get => MetaSettingID; set => MetaSettingID = value; }
   }
}
