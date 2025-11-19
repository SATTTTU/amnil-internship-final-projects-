using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Acme.TaskManagement.Domain.Shared.Enums;
using Acme.TaskManagement.Domain.Entities;

namespace Acme.TaskManagement.Domain.Entities
{
    public class TaskItem : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProjectId { get; protected set; }
        public Project Project { get; protected set; }   

        public string Title { get; protected set; }
        public string Description { get; protected set; }

        public TaskStatus Status { get; protected set; }
        public Guid? AssignedUserId { get; protected set; }

        public int Progress { get; protected set; }

        // EF Core needs protected parameterless constructor
        protected TaskItem() { }

        public TaskItem(Guid id, Guid projectId, string title, string description = null)
            : base(id)
        {
            SetProject(projectId);
            SetTitle(title);
            SetDescription(description);

            Status = TaskStatus.ToDo;
            Progress = 0;
        }

        public void SetProject(Guid projectId)
        {
            if (projectId == Guid.Empty)
                throw new BusinessException("ProjectId is required.");

            ProjectId = projectId;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessException("Task title cannot be empty.");

            if (title.Length > 200)
                throw new BusinessException("Task title cannot exceed 200 characters.");

            Title = title.Trim();
        }

        public void SetDescription(string description)
        {
            if (description != null && description.Length > 1000)
                throw new BusinessException("Task description cannot exceed 1000 characters.");

            Description = description?.Trim();
        }

        public void SetStatus(TaskStatus status)
        {
            Status = status;
        }

        public void SetAssignedUser(Guid? userId)
        {
            AssignedUserId = userId;
        }

        public void SetProgress(int progress)
        {
            if (progress < 0 || progress > 100)
                throw new BusinessException("Progress must be between 0 and 100.");

            Progress = progress;
        }
    }
}
