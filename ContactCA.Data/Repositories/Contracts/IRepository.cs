using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ContactCA.Data.Repositories.Contracts
{
   public interface IDataRepository
   {
   }

   public interface IDataRepository<T> : IDataRepository
      where T : class, IIdentifiableEntity, new()
   {
      T Add(T entity);
      IEnumerable<T> Get();
      T Get(int id);
      void Remove(T entity);
      void Remove(int id);
      T Update(T entity);
   }
   public interface IDbContextRepository<T> : IDataRepository<T>, IDisposable
      where T : class, IIdentifiableEntity, new()
   {
      DbContext Context { get; }
      DbSet<T> EntitySet { get; }

      T Entity(T proxy);
   }
}
