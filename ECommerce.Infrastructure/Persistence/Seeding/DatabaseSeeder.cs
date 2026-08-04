using ECommerce.Infrastructure.Persistence.Data.DbContexts;

namespace ECommerce.Infrastructure.Persistence.Seeding;

public sealed class DatabaseSeeder(StoreDbContext dbContext, IEnumerable<IDataSeeder> seeders)
{
    public async Task SeedAll(CancellationToken ck = default)
    {
        foreach (var seeder in seeders.OrderBy(s => s.Order)) 
        {
            await seeder.SeedAsync(ck);
            await dbContext.SaveChangesAsync(ck);
        }
    }
}
