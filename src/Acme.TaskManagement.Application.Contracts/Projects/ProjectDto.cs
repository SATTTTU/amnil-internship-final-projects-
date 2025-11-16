using System;
using Volo.Abp.Application.Dtos;
namespace Acme.TaskManagement.Contracts.Projects
{
    public class ProjectDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}