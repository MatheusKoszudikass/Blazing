using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Application.Mappings
{
    public class ProductDtoMapping
    {
        public virtual IEnumerable<ProductDto?> ReturnProductDto(IEnumerable<Product>? products, CancellationToken cancellationToken)
        {
            var productResultDto = new List<ProductDto?>();
            foreach (var product in products)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Currency = product.Currency,
                    CategoryId = product.CategoryId,
                    Brand = product.Brand,
                    Sku = product.Sku,
                    StockQuantity = product.StockQuantity,
                    StockLocation = product.StockLocation,
                    DimensionsId = product.DimensionsId,
                    AssessmentId = product.AssessmentId,
                    AttributesId = product.AttributesId,
                    AvailabilityId = product.AvailabilityId,
                    ImageId = product.ImageId,

                    Dimensions = ConvertToDimensionsDto(product.Dimensions),
                    Assessment = ConvertToAssessmentDto(product.Assessment),
                    Attributes = ConvertToAttributeDto(product.Attributes),
                    Availability = ConvertToAvailabilityDto(product.Availability),
                    Image = ConvertToImageDto(product.Image)
                };

                if (product.Assessment?.RevisionDetail != null)
                {
                    foreach (var revision in product.Assessment.RevisionDetail)
                    {
                        var revisionDto = ConvertToRevisionDto(revision);
                        productDto.Assessment.RevisionDetail.ToList().Add(revisionDto);
                    }
                }

                productResultDto.Add(productDto);
            }
            return productResultDto;
        }

        public virtual DimensionsDto? ConvertToDimensionsDto(Dimensions? dimensions)
        {
            if (dimensions == null) return null;

            return new DimensionsDto
            {
                Id = dimensions.Id,
                Width = dimensions.Width,
                Height = dimensions.Height,
                Depth = dimensions.Depth
            };
        }

        public virtual AssessmentDto? ConvertToAssessmentDto(Assessment? assessment)
        {
            if (assessment == null) return null;

            return new AssessmentDto
            {
                Id = assessment.Id,
                NumberOfReviews = assessment.NumberOfReviews,
                RevisionId = assessment.RevisionId,
            };
        }

        public virtual AttributeDto? ConvertToAttributeDto(Attributes? attribute)
        {
            if (attribute == null) return null;
            return new AttributeDto
            {
                Id = attribute.Id,
                Color = attribute.Color,
                Material = attribute.Material,
                Model = attribute.Model
            };
        }

        public virtual AvailabilityDto? ConvertToAvailabilityDto(Availability? availability)
        {
            if (availability == null) return null;

            return new AvailabilityDto
            {
                Id = availability.Id,
                IsAvailable = availability.IsAvailable,
                EstimatedDeliveryDate = availability.EstimatedDeliveryDate
            };
        }

        public virtual ImageDto? ConvertToImageDto(Image? image)
        {
            if (image == null) return null;

            return new ImageDto
            {
                Id = image.Id,
                Url = image.Url,
                AltText = image.AltText
            };
        }

        protected virtual RevisionDto ConvertToRevisionDto(Revision revision)
        {
            return new RevisionDto
            {
                Id = revision.Id,
                Date = revision.Date,
                User = revision.User != null ? ConvertToUserDto(revision.User) : null
            };
        }

        protected virtual UserDto ConvertToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Status = Convert.ToBoolean(user.Status),
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                Addresses = null,
                ShoppingCarts = null,
                DateCreate = user.DataCreated,
                DateDelete = user.DataDeleted,
                DateUpdate = user.DataUpdated
            };
        }
    }
}
