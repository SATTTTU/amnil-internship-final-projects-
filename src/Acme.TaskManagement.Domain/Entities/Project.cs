using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.TaskManagement.Domain.Entities
{
    public class Project : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected Project()
        {
        }

        public Project(Guid id, string name, string description = null)
            : base(id)
        {
            SetName(name);
            SetDescription(description);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BusinessException("Project name cannot be empty.");
            }

            if (name.Length > 150)
            {
                throw new BusinessException("Project name cannot exceed 150 characters.");
            }

            Name = name.Trim();
        }

        public void SetDescription(string description)
        {
            if (description != null && description.Length > 1000)
            {
                throw new BusinessException("Project description cannot exceed 1000 characters.");
            }

            Description = description?.Trim();
        }
    }
}
