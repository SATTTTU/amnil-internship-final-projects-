using System.Threading.Tasks;

namespace Acme.TaskManagement.Data;

public interface ITaskManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
