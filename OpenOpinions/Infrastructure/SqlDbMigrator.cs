using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace OpenOpinions.Infrastructure
{
    public class SqlDbMigrator
    {
        public static async Task Migrate(DbContext dbContext)
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            var migrations = pendingMigrations.ToList();

            if (migrations.Any())
            {
                Console.WriteLine($"You have {migrations.Count} pending migrations to apply.");
                Console.WriteLine("Applying pending migrations now");
                await dbContext.Database.MigrateAsync();
            }
        }
    }
}
