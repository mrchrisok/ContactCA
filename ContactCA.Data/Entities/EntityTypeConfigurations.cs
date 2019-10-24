using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ContactCA.Data.Entities
{
   public class ContactConfiguration : EntityTypeConfiguration<Contact>
   {
      public class MetaSettingConfiguration : EntityTypeConfiguration<MetaSetting>
      {
         public MetaSettingConfiguration()
         {
            int i = -1;
            Property(x => x.MetaSettingID).HasColumnOrder(++i);

            string ixnuEnvironmentTypeCode = "IX_NU_EnvironmentTypeCode";
            Property(x => x.Environment).IsRequired().HasMaxLength(50).HasColumnOrder(++i)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName
                  , new IndexAnnotation(new IndexAttribute(ixnuEnvironmentTypeCode, 1) { IsUnique = true }));

            Property(x => x.Type).IsRequired().HasMaxLength(50).HasColumnOrder(++i)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName
                  , new IndexAnnotation(new IndexAttribute(ixnuEnvironmentTypeCode, 2) { IsUnique = true }));

            Property(x => x.Code).IsRequired().HasMaxLength(50).HasColumnOrder(++i)
               .HasColumnAnnotation(IndexAnnotation.AnnotationName
                  , new IndexAnnotation(new IndexAttribute(ixnuEnvironmentTypeCode, 3) { IsUnique = true }));

            Property(x => x.Value).HasColumnOrder(++i);
            Property(x => x.SortOrder).HasColumnOrder(++i);
            Property(x => x.Enabled).HasColumnOrder(++i);
         }
      }

      public ContactConfiguration()
      {
         int i = -1;
         Property(x => x.ContactID).HasColumnOrder(++i).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         Property(x => x.FirstName).HasColumnOrder(++i).IsRequired().HasMaxLength(40);
         Property(x => x.LastName).HasColumnOrder(++i).IsRequired().HasMaxLength(40);
         Property(x => x.EmailAddress).HasColumnOrder(++i).IsRequired().HasMaxLength(80);
         Property(x => x.Telephone).HasColumnOrder(++i).IsRequired().HasMaxLength(16);
         Property(x => x.Message).HasColumnOrder(++i).HasMaxLength(500);
         Property(x => x.BestTimeToCall).HasColumnOrder(++i);
      }
   }
}
