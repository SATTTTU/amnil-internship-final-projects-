using System;
using Volo.Abp.Domain.Entities.Auditing;
namespace Acme.TaskManagement.Domain.Entities
{
	public class Project : FullAuditedAggregateRoot<Guid>
	{
		public string Name { get; set; }
		public string Description { get; set; }

		protected Project()
		{
		}

		public Project(Guid id, string name, string description = null) : base(id)
		{
			Name = name;
			Description = description;
		}
	}
}