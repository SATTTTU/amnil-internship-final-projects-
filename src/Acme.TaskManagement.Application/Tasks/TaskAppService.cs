using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
            if (input == null || input.AssignedUserId == Guid.Empty)
            {
                throw new BusinessException("InvalidAssignedUser")
                    .WithData("Message", "Assigned user ID cannot be empty.");
            }

            var task = await GetEntityByIdAsync(id);

            task.AssignedUserId = input.AssignedUserId;

            await Repository.UpdateAsync(task, autoSave: true);
        }

       

        public async Task UpdateTaskStatusAsync(Guid id, TaskStatusDto status)
        {
            var task = await GetEntityByIdAsync(id);

            if (!Enum.IsDefined(typeof(TaskStatusDto), status))
            {
                throw new BusinessException("InvalidTaskStatus")
                    .WithData("Status", status);
            }

            task.Status = (Acme.TaskManagement.Domain.Shared.Enums.TaskStatus)status;

            await Repository.UpdateAsync(task, autoSave: true);
        }


        public async Task UpdateTaskProgressAsync(Guid id, int progress)
        {
            if (progress < 0 || progress > 100)
            {
                throw new BusinessException("InvalidProgress")
                    .WithData("Message", "Progress must be between 0 and 100.")
                    .WithData("Progress", progress);
            }

            var task = await GetEntityByIdAsync(id);

            if (task.Status == Acme.TaskManagement.Domain.Shared.Enums.TaskStatus.Completed)
            {
                throw new BusinessException("TaskAlreadyCompleted")
                    .WithData("Message", "Cannot modify progress of a completed task.");
            }

            task.Progress = progress;

            await Repository.UpdateAsync(task, autoSave: true);
        }
    }
}
