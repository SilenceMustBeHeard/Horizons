using Horizons.Data.Models;
using System.Text.Json;

namespace Horizons.Data.Seeding;

public class DbSeeder
{

    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public static async Task SeedTerrainAsync(AppDbContext context)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (await context.Terrains.AnyAsync())
            {
                Console.WriteLine("Terrains already exist. Skipping.");
                return;
            }

            var jsonPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "data",
                "terrains.json"
            );

            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"terrains.json NOT FOUND at: {jsonPath}");
            }

            var json = await File.ReadAllTextAsync(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var terrains = JsonSerializer.Deserialize<List<Terrain>>(json, options)
                ?? throw new Exception("terrains.json is empty or invalid");

            foreach (var terrain in terrains)
            {
                if (string.IsNullOrWhiteSpace(terrain.Name))
                    throw new Exception("Terrain Name is NULL or EMPTY");

                if (terrain.Id == Guid.Empty)
                    terrain.Id = Guid.NewGuid();
                terrain.IsDeleted = false;

            }

            await context.Terrains.AddRangeAsync(terrains);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Seeded {terrains.Count} terrains from file.");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
