namespace ContactCA.Data.Migrations
{
   using System.Data.Entity.Migrations;

   internal sealed class Configuration : DbMigrationsConfiguration<ContactCA.Data.ContactCADbContext>
   {
      public Configuration()
      {
         // can use a gated build pattern with DEV and RELEASE repos to address this
         //    after good gated build, the RELEASE build should update this to true.

         // for single repo shops (who does that?) this can be updated by a .ps1 script
         //    added to the pipeline as a finishing task
         AutomaticMigrationsEnabled = false;
      }

      protected override void Seed(ContactCA.Data.ContactCADbContext context)
      {
         //  This method will be called after migrating to the latest version.

         //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
         //  to avoid creating duplicate seed data.
      }
   }
}
