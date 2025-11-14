using System.ComponentModel.DataAnnotations;

namespace Acme.Ecommerce.Products.Dtos
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}