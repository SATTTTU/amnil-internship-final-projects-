using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.TaskManagement.Tasks
{
    public interface ITaskAppService : ICrudAppService<
        TaskDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateTaskDto>
    {
        Task AssignTaskAsync(Guid id, AssignTaskDto input);
        Task UpdateTaskStatusAsync(Guid id, TaskStatusDto status);
        Task UpdateTaskProgressAsync(Guid id, int progress);
    }
}