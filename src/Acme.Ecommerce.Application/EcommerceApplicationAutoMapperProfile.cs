using AutoMapper;
using Volo.Abp.AutoMapper;
using Acme.Ecommerce.Domain.Entities;
using Acme.Ecommerce.Products.Dtos;
using Acme.Ecommerce.Inventory.Dtos;
using Acme.Ecommerce.Orders.Dtos;

namespace Acme.Ecommerce;

public class EcommerceApplicationAutoMapperProfile : Profile
{
    public EcommerceApplicationAutoMapperProfile()
    {
        // ============================================================
        // CATEGORY
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
        // PRODUCT
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

        // This mapping assumes you load the Inventory entity before updating.
        CreateMap<UpdateInventoryDto, Domain.Entities.Inventory>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Quantity));


        // ============================================================
        // ORDER
        // ============================================================
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();

        CreateMap<CreateOrderDto, Order>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.OrderDate, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)) // Correctly map the collection
            .ForMember(x => x.TotalAmount, opt => opt.Ignore())
            .ForMember(x => x.Status, opt => opt.Ignore()) // Explicitly ignore server-side set properties
            .IgnoreAuditedObjectProperties()
            .IgnoreFullAuditedObjectProperties()
            .ForMember(x => x.ExtraProperties, opt => opt.Ignore())
            .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore());

        CreateMap<CreateOrderItemDto, OrderItem>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.OrderId, opt => opt.Ignore())
            .ForMember(x => x.UnitPrice, opt => opt.Ignore()); // This should be set in the application service
    }
}