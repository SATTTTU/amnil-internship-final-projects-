using AutoMapper;
using Volo.Abp.AutoMapper;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Products.Dtos;

namespace Acme.Ecommerce;

public class EcommerceApplicationAutoMapperProfile : Profile
{
    public EcommerceApplicationAutoMapperProfile()
    {
        // Entity → DTO
        CreateMap<Category, CategoryDto>();

        // Create / Update DTO → Entity
        CreateMap<CreateUpdateCategoryDto, Category>()
    .Ignore(x => x.Id)
    .Ignore(x => x.Products) // Assuming you have a 'Products' collection on your Category entity
    .IgnoreAuditedObjectProperties()
    .IgnoreFullAuditedObjectProperties()
    .Ignore(x => x.ExtraProperties)
    .Ignore(x => x.ConcurrencyStamp);
    }
}
