using System.Data.Entity.ModelConfiguration;

namespace ContactCA.Data.Entities
{
   public class ContactConfiguration : EntityTypeConfiguration<Contact>
   {
      public ContactConfiguration()
      {
         int i = 0;
         Property(x => x.ContactID).HasColumnOrder(i);
         Property(x => x.FirstName).HasColumnOrder(++i).IsRequired();
         Property(x => x.LastName).HasColumnOrder(++i).IsRequired();
         Property(x => x.Email).HasColumnOrder(++i).IsRequired();
         Property(x => x.Telephone).HasColumnOrder(++i).IsRequired();
         Property(x => x.BestCallTime).HasColumnOrder(++i).IsRequired();
      }
   }
}
