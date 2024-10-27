using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor:SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntites(eventData.Context);
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntites(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntites(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State==EntityState.Added)
                {
                    entry.Entity.CreatedBy = "Mostafa";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = "Mostafa";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }
}
