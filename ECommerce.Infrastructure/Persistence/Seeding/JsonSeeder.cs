using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ECommerce.Infrastructure.Persistence.Seeding;

public static class JsonSeeder
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public static async Task SeedIfEmpty<TEntitiy, TModel>(
        DbSet<TEntitiy> dbSet,
        string fileName,
        Func<TModel, TEntitiy> map,
        CancellationToken ck = default
        ) where TEntitiy : BaseEntity
    {
        if (await dbSet.AnyAsync(ck))
            return;

        var filePath = Path.Combine(AppContext.BaseDirectory, "Persistence", "Seeding", "Data", fileName);

        if (!File.Exists(filePath))
            return;

        await using var stream = File.OpenRead(filePath);

        var models = await JsonSerializer.DeserializeAsync<List<TModel>>(stream, options, ck);

        if (models is null || models.Count == 0)
            return;

        await dbSet.AddRangeAsync(models.Select(map),ck);
    }
}
