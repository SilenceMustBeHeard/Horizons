using Horizons.Data.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Data.Seeding;

public static class DbSeeder
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public static async Task SeedTerrainsAsync(AppDbContext context)
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

    public static async Task SeedDestinationsAsync(AppDbContext context)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (await context.Destinations.AnyAsync())
            {
                Console.WriteLine("Destinations already exist. Skipping.");
                return;
            }

            var jsonPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "data",
                "destinations.json"
            );

            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"destinations.json NOT FOUND at: {jsonPath}");
            }

            var json = await File.ReadAllTextAsync(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var destinations = JsonSerializer.Deserialize<List<Destination>>(json, options)
                ?? throw new Exception("destinations.json is empty or invalid");

            foreach (var destination in destinations)
            {
                if (string.IsNullOrWhiteSpace(destination.Name))
                    throw new Exception("Destination Name is NULL or EMPTY");

                if (destination.Id == Guid.Empty)
                    destination.Id = Guid.NewGuid();
                var terrainExists = await context.Terrains.AnyAsync(t => t.Id == destination.TerrainId);
                if (!terrainExists)
                {
                    throw new Exception($"TerrainId {destination.TerrainId} does not exist for destination: {destination.Name}");
                }

                destination.IsDeleted = false;
            }

            await context.Destinations.AddRangeAsync(destinations);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Seeded {destinations.Count} destinations from file.");
        }
        finally
        {
            _semaphore.Release();
        }
    }

   

    public static async Task SeedAllAsync(AppDbContext context)
    {
        Console.WriteLine("🌱 Starting database seeding from files...");

        await SeedTerrainsAsync(context); 
        await SeedDestinationsAsync(context); 
       
        Console.WriteLine("✅ Database seeding completed!");
    }
}