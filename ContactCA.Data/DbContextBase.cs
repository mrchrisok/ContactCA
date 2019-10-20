using ContactCA.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ContactCA.Data
{
   public abstract class DbContextBase : DbContext
   {
      Type _seedEntityTypeConfigurationType;

      public DbContextBase(string nameOrConnectionString, Type seedEntityTypeConfigurationType)
         : base(nameOrConnectionString)
      {
         _seedEntityTypeConfigurationType = seedEntityTypeConfigurationType;
      }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
         modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // singular table names

         // ignore properties associated with these types and interfaces
         //modelBuilder.Ignore<PropertyChangedEventHandler>();
         //modelBuilder.Ignore<ExtensionDataObject>();
         modelBuilder.Ignore<IIdentifiableEntity>();

         // add entity configurations
         var typesToRegister = Assembly.GetAssembly(_seedEntityTypeConfigurationType).GetTypes()
            .Where(type => type.Namespace == _seedEntityTypeConfigurationType.Namespace)
            .Where(type => type.BaseType?.IsGenericType ?? false)
            .Where(type => type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

         foreach (var type in typesToRegister)
         {
            dynamic configurationInstance = Activator.CreateInstance(type);
            modelBuilder.Configurations.Add(configurationInstance);
         }

         // ALWAYS 
         base.OnModelCreating(modelBuilder);
      }

      protected virtual void BeforeSaveChanges()
      {
         _saveChangesTime = DateTime.UtcNow;

         #region set DateCreated and DateModified

         void setCreatedOrModifiedDate(DbEntityEntry entry)
         {
            if (entry.Entity is IDateTracking changedOrAddedItem)
            {
               if (entry.State == EntityState.Added)
                  changedOrAddedItem.DateCreated = _saveChangesTime.Value;
               else
                  entry.Property("DateCreated").IsModified = false;

               changedOrAddedItem.DateModified = _saveChangesTime.Value;
            }
         }

         foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            setCreatedOrModifiedDate(entry);
         foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            setCreatedOrModifiedDate(entry);

         #endregion
      }

      protected DateTime? _saveChangesTime { get; set; }

      public override int SaveChanges()
      {
         int saveResult = default(int);

         try
         {
            BeforeSaveChanges();

            saveResult = base.SaveChanges();

            AfterSaveChanges();
         }
         catch (Exception ex)
         {
            SaveChangesCatch(ex);
         }
         finally
         {
            SaveChangesFinally();
         }

         return saveResult;
      }

      protected virtual void AfterSaveChanges()
      {
      }

      protected virtual void SaveChangesCatch(Exception ex)
      {
         if (ex is DbEntityValidationException validationException)
         {
            var errors = validationException.EntityValidationErrors;
            var result = new StringBuilder();
            var allErrors = new List<ValidationResult>();

            foreach (var error in errors)
               foreach (var validationError in error.ValidationErrors)
               {
                  result.Append($"\r\n  Entity of type {error.Entry.Entity.GetType().ToString()} has validation error \"{validationError.ErrorMessage}\" for property {validationError.PropertyName}.\r\n");
                  if (error.Entry.Entity is IIdentifiableEntity domainEntity)
                  {
                     result.Append((domainEntity.EntityID == 0)
                        ? "  This entity was created in this session.\r\n"
                        : $"  The id of the entity is {domainEntity.EntityID}.\r\n");
                  }
                  allErrors.Add(new ValidationResult(validationError.ErrorMessage, new[] { validationError.PropertyName }));
               }

            //var fault = new EntityValidationFault(result.ToString(), allErrors);
            //throw new FaultException<EntityValidationFault>(fault, fault.Message);

            throw new ApplicationException(result.ToString());
         }
         else if (ex is DbUpdateException dbUpdateException)
         {
            var updateException = (UpdateException)dbUpdateException.InnerException;
            var sqlException = (SqlException)updateException.InnerException;
            var result = new StringBuilder("The following errors during the context.SaveChanges() operation: \r\n");

            foreach (SqlError error in sqlException.Errors)
               result.Append("\r\n" + error.Message);

            //throw new FaultException(result.ToString());

            throw new ApplicationException(result.ToString());
         }
      }

      protected virtual void SaveChangesFinally()
      {
         // done .. clear this
         _saveChangesTime = null;
      }
   }
}
