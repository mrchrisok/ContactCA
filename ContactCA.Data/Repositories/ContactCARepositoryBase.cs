using ContactCA.Data.Repositories.Contracts;
using System.Configuration;

namespace ContactCA.Data.Repositories
{
   public abstract class ContactCARepositoryBase<T> : DataRepositoryBase<T, ContactCADbContext>
      where T : class, IIdentifiableEntity, new()
   {
      protected string _environment = ConfigurationManager.AppSettings["Environment"];
   }
}
