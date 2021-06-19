using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Data.EntityConfiguratons;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AcademicCredential> AcademicCredentials  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AcademicCredentialConfiguration());
        }


        public async Task<int> SaveChangesAsync(string IP)
        {

            // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
            var changedEntity = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added ||
                                                                        p.State == EntityState.Deleted ||
                                                                        p.State == EntityState.Modified).ToList();

            foreach (var ent in changedEntity)
            {
                // For each changed record, get the audit record entries and add them
                var auditRecordsForChange = GetAuditRecordsForChange(ent,  IP);

                foreach (var x in auditRecordsForChange)
                {
                  await  this.AuditLogs.AddAsync(x);
                }
            }

            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return await base.SaveChangesAsync();

        }

        private static IEnumerable<AuditLog> GetAuditRecordsForChange(EntityEntry dbEntry, string Ip)
        {
            List<AuditLog> result = new List<AuditLog>();

            DateTimeOffset changeTime = DateTimeOffset.UtcNow;

            // Get the Table() attribute, if one exists
            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            // Get primary key value (If you have more than one key column, this will need to be adjusted)
            if (dbEntry != null)
            {
                var keyName = dbEntry.Entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;
                //string keyName = dbEntry.Entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), true).Count() < 0).Name;
                if (dbEntry.State == EntityState.Added)
                {
                    // For Inserts, just add the whole record
                    // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
                    var a = new AuditLog
                    {
                        AuditLogId = Guid.NewGuid(),
                        EventDateUtc = changeTime,
                        EventType = "A",
                        TableName = tableName,
                        Ip = Ip,
                        RecordId = dbEntry.Property(keyName).CurrentValue.ToString(),
                        ColumnName = "*ALL",
                        NewValue = JsonSerializer.Serialize(dbEntry.CurrentValues.ToObject())
                    };
                    // Added

                    // r.ToString();// dbEntry.CurrentValues.GetValue<string>(keyName);  // Again, adjust this if you have a multi-column key
                    //"Couldnt get this, will comeback to this",
                    // Or make it nullable, whatever you want
                    //NewValue = (dbEntry.CurrentValues.ToObject() is IDescribableEntity) ? (dbEntry.CurrentValues.ToObject() as IDescribableEntity).Describe() : dbEntry.CurrentValues.ToObject().ToString()
                    // "NewEntityEntry";
                    result.Add(a);

                }
                else if (dbEntry.State == EntityState.Deleted)
                {
                    // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                    result.Add(new AuditLog
                    {
                        AuditLogId = Guid.NewGuid(),
                        EventDateUtc = changeTime,
                        EventType = "D", // Deleted
                        TableName = tableName,
                        RecordId = dbEntry.OriginalValues[keyName].ToString(),// dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                        ColumnName = "*ALL",
                        Ip = Ip,
                        NewValue = JsonSerializer.Serialize(dbEntry.OriginalValues.ToObject())// (dbEntry.OriginalValues.ToObject() is IDescribableEntity) ? (dbEntry.OriginalValues.ToObject() as IDescribableEntity).Describe() : dbEntry.OriginalValues.ToObject().ToString()
                    }
                    );
                }
                else if (dbEntry.State == EntityState.Modified)
                {
                    foreach (var propertyName in dbEntry.OriginalValues.Properties)
                    {
                        if (!object.Equals(dbEntry.OriginalValues[propertyName], dbEntry.CurrentValues[propertyName]))
                        {
                            result.Add(new AuditLog
                            {
                                AuditLogId = Guid.NewGuid(),
                                EventDateUtc = changeTime,
                                EventType = "M",    // Modified
                                TableName = tableName,
                                RecordId = dbEntry.OriginalValues[keyName].ToString(),// dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
                                ColumnName = propertyName.Name,
                                Ip = Ip,
                                OriginalValue = dbEntry.OriginalValues[propertyName] == null ? null : dbEntry.OriginalValues[propertyName].ToString(),
                                NewValue = dbEntry.CurrentValues[propertyName] == null ? null : dbEntry.CurrentValues[propertyName].ToString()

                            }
                                );
                        }
                    }
                }
            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities

            return result;
        }

    }
}
