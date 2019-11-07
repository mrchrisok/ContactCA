using System.Data.Entity;
using ContactCA.Data.Entities;

namespace ContactCA.Data
{
   public class ContactCADbContext : DbContextBase
   {
      #region Constructors
      public ContactCADbContext()
         : base("name=DefaultConnection", typeof(ContactConfiguration))
      {
         Configuration.LazyLoadingEnabled = true;
         Configuration.ProxyCreationEnabled = true;
      }
      #endregion

      #region Sets

      public DbSet<Contact> ContactSet { get; set; }
      public DbSet<MetaSetting> MetaSettingSet { get; set; }

      #endregion

      #region Methods.Override
      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         // all decimals are *10^6
         modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(22, 6));

         // ALWAYS call base
         base.OnModelCreating(modelBuilder);
      }

      #endregion
   }
}
