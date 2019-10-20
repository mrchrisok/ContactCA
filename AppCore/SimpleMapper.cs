using System;
using System.Linq;
using System.Reflection;

namespace AppCore
{
   public static class SimpleMapper
   {
      public static void MapProperties<T, U>(T source, U destination)
         where T : class, new()
         where U : class, new()
      {
         var sourceProperties = source.GetType().GetProperties().ToList();
         var destinationProperties = destination.GetType().GetProperties().ToList();

         foreach (PropertyInfo sourceProperty in sourceProperties)
         {
            PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

            if (destinationProperty != null)
            {
               try
               {
                  destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
               }
               catch (ArgumentException)
               {
               }
            }
         }
      }
   }
}

