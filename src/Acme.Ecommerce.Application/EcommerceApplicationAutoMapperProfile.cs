using AutoMapper;
using Volo.Abp.AutoMapper;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Products.Dtos;
using Acme.Ecommerce.Inventory.Dtos;
using Acme.Ecommerce.Orders.Dtos;
using Volo.Abp.Identity;

namespace Acme.Ecommerce;

public class EcommerceApplicationAutoMapperProfile : Profile
{
    public EcommerceApplicationAutoMapperProfile()
    {
        // ============================================================
        // IDENTITY (Correct - No changes needed)
        // ============================================================
        CreateMap<IdentityUser, IdentityUserDto>();
        CreateMap<IdentityRole, IdentityRoleDto>();

        // ============================================================
        // CATEGORY (Correct - No changes needed)
        // ============================================================
        CreateMap<Category, CategoryDto>();

        CreateMap<CreateUpdateCategoryDto, Category>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Products, opt => opt.Ignore())
            .IgnoreAuditedObjectProperties()
            .IgnoreFullAuditedObjectProperties()
            .ForMember(x => x.ExtraProperties, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore());


        // ============================================================
        // PRODUCT (Correct - No changes needed)
        // ============================================================
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<CreateUpdateProductDto, Product>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Category, opt => opt.Ignore())
            .IgnoreAuditedObjectProperties()
            .IgnoreFullAuditedObjectProperties()
            .ForMember(x => x.ExtraProperties, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore());


        // ============================================================
        // INVENTORY
        // ============================================================
        CreateMap<Domain.Entities.Inventory, InventoryDto>();

        // FIXED MAPPING: Added ignores for all unmapped base entity and navigation properties.
        CreateMap<UpdateInventoryDto, Domain.Entities.Inventory>()
            .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.ProductId, opt => opt.Ignore())
            .ForMember(x => x.Product, opt => opt.Ignore()) // Must ignore navigation properties
            .IgnoreAuditedObjectProperties() // Ignores CreationTime, CreatorId, etc.
            .IgnoreFullAuditedObjectProperties() // Ignores DeletionTime, IsDeleted, etc.
            .ForMember(x => x.ExtraProperties, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore());


        // ============================================================
        // ORDER
        // ============================================================
        // These mappings from Entity to DTO are correct and should remain.
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();

        // DELETED MAPPING: The following two `CreateMap` calls have been removed
        // because you should not use AutoMapper to create complex entities
        // like Orders and OrderItems. This must be done manually in your AppService.
    }
}