using AutoMapper;
using Blazing.Domain.Entities;
using Blazing.Application.Dto;


namespace Blazing.Application.Mappings
{
    #region Data Transfer Object.
    /// <summary>
    /// AutoMapper profile configuration for the Blazing application.
    /// This profile defines mappings between entity classes and their corresponding DTO (Data Transfer Object) classes.
    /// The ReverseMap() method is used to allow bidirectional mapping between the source and destination types.
    /// </summary>
    public class BlazingProfile : Profile
    {
        public BlazingProfile() 
        {
            CreateMap<AddCartItem, AddCartItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Assessment, AssessmentDto>().ReverseMap();
            CreateMap<Attributes, AttributeDto>().ReverseMap();
            CreateMap<Availability, AvailabilityDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Dimensions, DimensionsDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Revision, RevisionDto>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
    #endregion
}
