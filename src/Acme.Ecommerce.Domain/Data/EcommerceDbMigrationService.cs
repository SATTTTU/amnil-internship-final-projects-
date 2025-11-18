using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.Ecommerce.Data;

public class EcommerceDbMigrationService : ITransientDependency
{
    public ILogger<EcommerceDbMigrationService> Logger { get; set; }

    private readonly IDataSeeder _dataSeeder;
    private readonly IEnumerable<IEcommerceDbSchemaMigrator> _dbSchemaMigrators;

    public EcommerceDbMigrationService(
        IDataSeeder dataSeeder,
        IEnumerable<IEcommerceDbSchemaMigrator> dbSchemaMigrators)
    {
        _dataSeeder = dataSeeder;
        _dbSchemaMigrators = dbSchemaMigrators;

        Logger = NullLogger<EcommerceDbMigrationService>.Instance;
    }

    public async Task MigrateAsync()
    {
        var initialMigrationAdded = AddInitialMigrationIfNotExist();

        if (initialMigrationAdded)
        {
            return;
        }

        Logger.LogInformation("Started database migrations...");

        await MigrateDatabaseSchemaAsync();
        await SeedDataAsync();

        Logger.LogInformation($"Successfully completed host database migrations.");
        Logger.LogInformation("Successfully completed all database migrations.");
        Logger.LogInformation("You can safely end this process...");
    }

    public async Task DropAndRecreateDatabase()
    {
        try
        {
            Logger.LogInformation("Dropping existing database if it exists...");
            
            var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=postgres;";
            var dbFactory = DbProviderFactories.GetFactory("Npgsql");
            
            using (var connection = dbFactory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                await connection.OpenAsync();
                
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DROP DATABASE IF EXISTS \"Ecom\";";
                    await cmd.ExecuteNonQueryAsync();
                }
                
                Logger.LogInformation("Database dropped successfully");
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning($"Could not drop database: {ex.Message}");
        }
    }

    private async Task MigrateDatabaseSchemaAsync()
    {
        Logger.LogInformation("Migrating schema for host database...");

        foreach (var migrator in _dbSchemaMigrators)
        {
            await migrator.MigrateAsync();
        }
    }

    private async Task SeedDataAsync()
    {
        Logger.LogInformation("Executing host database seed...");

        try
        {
            await _dataSeeder.SeedAsync(new DataSeedContext());
        }
        catch (Exception ex)
        {
            // Log full exception details including inner exceptions for debugging
            Logger.LogError(ex, "Error during database seeding: {Message}", ex.Message);
            if (ex.InnerException != null)
            {
                Logger.LogError(ex.InnerException, "Inner exception: {Message}", ex.InnerException.Message);
            }
            // If some module's seeders (e.g. Identity) cannot be resolved because
            // their EF Core stores are not registered in this project, we should
            // log a warning and continue. This allows migrations to complete.
            Logger.LogWarning($"Skipping host database seeding due to an error: {ex.Message}");
        }
    }

    private bool AddInitialMigrationIfNotExist()
    {
        try
        {
            if (!DbMigrationsProjectExists())
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }

        try
        {
            if (!MigrationsFolderExists())
            {
                AddInitialMigration();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Logger.LogWarning("Couldn't determinate if any migrations exist : " + e.Message);
            return false;
        }
    }

    private bool DbMigrationsProjectExists()
    {
        var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();

        return dbMigrationsProjectFolder != null;
    }

    private bool MigrationsFolderExists()
    {
        var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();
        return dbMigrationsProjectFolder != null && Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
    }

    private void AddInitialMigration()
    {
        Logger.LogInformation("Creating initial migration...");

        string argumentPrefix;
        string fileName;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            argumentPrefix = "-c";
            fileName = "/bin/bash";
        }
        else
        {
            argumentPrefix = "/C";
            fileName = "cmd.exe";
        }

        var procStartInfo = new ProcessStartInfo(fileName,
            $"{argumentPrefix} \"abp create-migration-and-run-migrator \"{GetEntityFrameworkCoreProjectFolderPath()}\"\""
        );

        try
        {
            Process.Start(procStartInfo);
        }
        catch (Exception)
        {
            throw new Exception("Couldn't run ABP CLI...");
        }
    }

    private string? GetEntityFrameworkCoreProjectFolderPath()
    {
        var slnDirectoryPath = GetSolutionDirectoryPath();

        if (slnDirectoryPath == null)
        {
            throw new Exception("Solution folder not found!");
        }

        var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

        return Directory.GetDirectories(srcDirectoryPath)
            .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
    }

    private string? GetSolutionDirectoryPath()
    {
        var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (currentDirectory != null && Directory.GetParent(currentDirectory.FullName) != null)
        {
            currentDirectory = Directory.GetParent(currentDirectory.FullName);

            if (currentDirectory != null && Directory.GetFiles(currentDirectory.FullName).FirstOrDefault(f => f.EndsWith(".sln")) != null)
            {
                return currentDirectory.FullName;
            }
        }

        return null;
    }
}
