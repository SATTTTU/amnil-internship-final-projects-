using System;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.TaskManagement.Domain.Shared.Enums;
namespace Acme.TaskManagement.Domain.Entities
{
    public class Task : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public Guid? AssignedUserId { get; set; }
        public int Progress { get; set; }

        protected Task()
        {
        }

        public Task(Guid id, Guid projectId, string title, string description = null) : base(id)
        {
            ProjectId = projectId;
            Title = title;
            Description = description;
            Status = TaskStatus.ToDo;
            Progress = 0;
        }
    }
}