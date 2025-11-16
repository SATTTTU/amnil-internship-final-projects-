using Acme.TaskManagement.Contracts.Projects;

using System.ComponentModel.DataAnnotations;

namespace Acme.TaskManagement.Contracts.Projects
{
    public class CreateUpdateProjectDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}