using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.TaskManagement.Domain.Entities;
using Acme.TaskManagement.Application.Contracts.Projects;
using Acme.TaskManagement.Contracts.Projects;
namespace Acme.TaskManagement.Application.Projects
{
    public class ProjectAppService : CrudAppService<
        Project,
        ProjectDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateProjectDto>,
        IProjectAppService
    {
        public ProjectAppService(IRepository<Project, Guid> repository)
            : base(repository)
        {
        }
    }
}