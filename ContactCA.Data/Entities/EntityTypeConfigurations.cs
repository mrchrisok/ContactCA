using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContactCA.Data.Entities
{
   public class ContactConfiguration : EntityTypeConfiguration<Contact>
   {
      public ContactConfiguration()
      {
         int i = 0;
         Property(x => x.ContactID).HasColumnOrder(i).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         Property(x => x.FirstName).HasColumnOrder(++i).IsRequired().HasMaxLength(40);
         Property(x => x.LastName).HasColumnOrder(++i).IsRequired().HasMaxLength(40);
         Property(x => x.EmailAddress).HasColumnOrder(++i).IsRequired().HasMaxLength(80);
         Property(x => x.Telephone).HasColumnOrder(++i).IsRequired().HasMaxLength(16);
         Property(x => x.Message).HasColumnOrder(++i).HasMaxLength(500);
         Property(x => x.BestTimeToCall).HasColumnOrder(++i);
      }
   }
}
