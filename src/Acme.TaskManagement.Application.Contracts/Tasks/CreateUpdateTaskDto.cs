using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.TaskManagement.Tasks
{
    public class CreateUpdateTaskDto
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}