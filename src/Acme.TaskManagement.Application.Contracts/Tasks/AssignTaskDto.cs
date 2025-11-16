using System;

namespace Acme.TaskManagement. Contracts.Tasks
{
    public class AssignTaskDto
    {
        public Guid? AssignedUserId { get; set; }
    }
}