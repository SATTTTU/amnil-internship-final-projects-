using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Acme.TaskManagement.Contracts.Projects;


namespace Acme.TaskManagement.Application.Contracts.Projects

{
    public interface IProjectAppService : ICrudAppService<
        ProjectDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProjectDto>
    {
    }
}