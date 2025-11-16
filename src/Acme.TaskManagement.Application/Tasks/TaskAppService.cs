using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.TaskManagement.Domain.Entities;

namespace Acme.TaskManagement.Application.Tasks
{
    public class TaskAppService : CrudAppService<
        Task,
        TaskDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateTaskDto>,
        ITaskAppService
    {
        public TaskAppService(IRepository<Task, Guid> repository)
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
            task.Status = ObjectMapper.Map<TaskStatusDto, TaskStatus>(status);
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