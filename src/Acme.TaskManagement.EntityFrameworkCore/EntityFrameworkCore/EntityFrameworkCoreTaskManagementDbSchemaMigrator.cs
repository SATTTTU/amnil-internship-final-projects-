using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.TaskManagement.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.TaskManagement.EntityFrameworkCore;

public class EntityFrameworkCoreTaskManagementDbSchemaMigrator
    : ITaskManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTaskManagementDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the TaskManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TaskManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
