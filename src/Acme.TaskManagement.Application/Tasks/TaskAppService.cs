using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Acme.TaskManagement.Contracts.Tasks;
using Acme.TaskManagement.Domain.Entities;


namespace Acme.TaskManagement.Tasks
{
    public class TaskAppService : TaskManagementAppService, ITaskAppService
    {
        private readonly IRepository<TaskItem, Guid> _taskRepository;

        public TaskAppService(IRepository<TaskItem, Guid> taskRepository)

        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> CreateAsync(CreateUpdateTaskDto input)
        {
            try
            {
                var task = new TaskItem(
                    GuidGenerator.Create(),
                    input.ProjectId,
                    input.Title,
                    input.Description
                );

                await _taskRepository.InsertAsync(task);

                return MapToDto(task);
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_CREATION_FAILED", ex.Message);
            }
        }

        public async Task<TaskDto> UpdateAsync(Guid id, CreateUpdateTaskDto input)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);

                task.SetTitle(input.Title);
                task.SetDescription(input.Description);

                await _taskRepository.UpdateAsync(task);

                return MapToDto(task);
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_UPDATE_FAILED", ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _taskRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_DELETE_FAILED", ex.Message);
            }
        }

        public async Task<TaskDto> AssignUserAsync(Guid id, Guid userId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);
                task.SetAssignedUser(userId);

                await _taskRepository.UpdateAsync(task);

                return MapToDto(task);
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_ASSIGN_FAILED", ex.Message);
            }
        }

        public async Task<TaskDto> UpdateProgressAsync(Guid id, int progress)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);
                task.SetProgress(progress);

                await _taskRepository.UpdateAsync(task);

                return MapToDto(task);
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_PROGRESS_UPDATE_FAILED", ex.Message);
            }
        }

        public async Task<TaskDto> ChangeStatusAsync(Guid id, TaskStatusDto status)
        {
            try
            {
                var task = await _taskRepository.GetAsync(id);
                task.SetStatus((Acme.TaskManagement.Domain.Shared.Enums.TaskStatus)status);


                await _taskRepository.UpdateAsync(task);

                return MapToDto(task);
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_STATUS_CHANGE_FAILED", ex.Message);
            }
        }

        public async Task<TaskDto> GetAsync(Guid id)
        {
            var task = await _taskRepository.GetAsync(id);
            return MapToDto(task);
        }

        public async Task<PagedResultDto<TaskDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            try
            {
                var items = await _taskRepository.GetListAsync();
                var total = await _taskRepository.CountAsync();

                return new PagedResultDto<TaskDto>(
                    total,
                    items.ConvertAll(MapToDto)
                );
            }
            catch (Exception ex)
            {
                throw new BusinessException("TASK_LIST_FETCH_FAILED", ex.Message);
            }
        }

        private TaskDto MapToDto(TaskItem entity)

        {
            return new TaskDto
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                Title = entity.Title,
                Description = entity.Description,
                Status = (TaskStatusDto)entity.Status,
                AssignedUserId = entity.AssignedUserId,
                Progress = entity.Progress,
                CreationTime = entity.CreationTime
            };
        }
    }
}
