using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Acme.TaskManagement.Contracts.Tasks;

namespace Acme.TaskManagement.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<TaskDto> CreateAsync(CreateUpdateTaskDto input);
        Task<TaskDto> UpdateAsync(Guid id, CreateUpdateTaskDto input);
        Task<TaskDto> AssignUserAsync(Guid id, Guid userId);
        Task<TaskDto> UpdateProgressAsync(Guid id, int progress);
        Task<TaskDto> ChangeStatusAsync(Guid id, TaskStatusDto status);
        Task<TaskDto> GetAsync(Guid id);
        Task<PagedResultDto<TaskDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task DeleteAsync(Guid id);
    }
}
