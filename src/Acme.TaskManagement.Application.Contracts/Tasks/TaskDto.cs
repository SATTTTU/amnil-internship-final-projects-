using System;
using Volo.Abp.Application.Dtos;

namespace Acme.TaskManagement.Contracts.Tasks
{
    public enum TaskStatusDto
    {
        ToDo,
        InProgress,
        Done
    }

    public class TaskDto : AuditedEntityDto<Guid>
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusDto Status { get; set; }
        public Guid? AssignedUserId { get; set; }
        public int Progress { get; set; }
    }
}