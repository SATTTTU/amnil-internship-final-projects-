using System;
using System.ComponentModel.DataAnnotations;

namespace MyECommerce.Products.Dtos
{
    public class CreateUpdateProductDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}