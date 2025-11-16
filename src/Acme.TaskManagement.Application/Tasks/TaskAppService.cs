using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

// Alias to avoid conflict with System.Threading.Tasks.Task
using TaskEntity = Acme.TaskManagement.Domain.Entities.Task;

using Acme.TaskManagement.Contracts.Tasks;
using Acme.TaskManagement.Application.Contracts.Tasks;

namespace Acme.TaskManagement.Application.Tasks
{
    public class TaskAppService : CrudAppService<
        TaskEntity,
        TaskDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateTaskDto>,
        ITaskAppService
    {
        public TaskAppService(IRepository<TaskEntity, Guid> repository)
            : base(repository)
        {
        }

        public async Task AssignTaskAsync(Guid id, AssignTaskDto input)
        {
            var task = await GetEntityByIdAsync(id);
            task.AssignedUserId = input.AssignedUserId;
            await Repository.UpdateAsync(task);
        }

        public async Task UpdateTaskStatusAsync(Guid id, TaskStatusDto status)
        {
            var task = await GetEntityByIdAsync(id);

            task.Status = (Acme.TaskManagement.Domain.Shared.Enums.TaskStatus)status;


            await Repository.UpdateAsync(task);
        }


        public async Task UpdateTaskProgressAsync(Guid id, int progress)
        {
            var task = await GetEntityByIdAsync(id);

            if (progress >= 0 && progress <= 100)
            {
                task.Progress = progress;
                await Repository.UpdateAsync(task);
            }
        }
    }
}
