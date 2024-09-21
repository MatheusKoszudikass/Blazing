using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace Blazing.Application.Mappings
{
    public sealed class ProductMapping
    {
        public IEnumerable<Product?> ReturnProduct(IEnumerable<ProductDto>? productsDto)
        {
            var productResult = new List<Product>();
            foreach (var productDto in productsDto)
            {
                var products = new Product
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Currency = productDto.Currency,
                    CategoryId = productDto.CategoryId,
                    Brand = productDto.Brand,
                    Sku = productDto.Sku,
                    StockQuantity = productDto.StockQuantity,
                    StockLocation = productDto.StockLocation,
                    DimensionsId = productDto.DimensionsId,
                    AssessmentId = productDto.AssessmentId,
                    AttributesId = productDto.AttributesId,
                    AvailabilityId = productDto.AvailabilityId,
                    ImageId = productDto.ImageId,

                    Dimensions = ConvertToDimensions(productDto.Dimensions),
                    Assessment = ConvertToAssessment(productDto.Assessment),
                    Attributes = ConvertToAttributes(productDto.Attributes),
                    Availability = ConvertToAvailability(productDto.Availability),
                    Image = ConvertToImage(productDto.Image)
                };

                if (productDto.Assessment?.RevisionDetail != null)
                {
                    foreach (var revision in productDto.Assessment.RevisionDetail)
                    {
                        var revisionDto = ConvertToRevision(revision);
                        if (revisionDto != null) products.Assessment.RevisionDetail.ToList().Add(revisionDto);
                    }
                }

                productResult.Add(products);
            }
            return productResult;
        }

        private Dimensions? ConvertToDimensions(DimensionsDto? dimensionsDto)
        {
            if (dimensionsDto == null) return null;

            return new Dimensions
            {
                Id = dimensionsDto.Id,
                Width = dimensionsDto.Width,
                Height = dimensionsDto.Height,
                Depth = dimensionsDto.Depth
            };
        }

        private static Assessment? ConvertToAssessment(AssessmentDto? assessmentDto)
        {
            if (assessmentDto == null) return null;

            return new Assessment
            {
                Id = assessmentDto.Id,
                NumberOfReviews = assessmentDto.NumberOfReviews,
                RevisionId = assessmentDto.RevisionId
            };
        }

        private static Revision? ConvertToRevision(RevisionDto? revisionDto)
        {
            if (revisionDto == null) return null;

            return new Revision
            {
                Id = revisionDto.Id,
                Date = revisionDto.Date,
                User = revisionDto.User != null ? new User
                {
                    Id = revisionDto.User.Id,
                    UserName = revisionDto.User.UserName,
                    Email = revisionDto.User.Email
                } : null
            };
        }

        private static Attributes? ConvertToAttributes(AttributeDto? attributeDto)
        {
            if (attributeDto == null) return null;
            return new Attributes
            {
                Id = attributeDto.Id,
                Color = attributeDto.Color,
                Material = attributeDto.Material,
                Model = attributeDto.Model
            };
        }

        private static Availability? ConvertToAvailability(AvailabilityDto? availabilityDto)
        {
            if (availabilityDto == null) return null;

            return new Availability
            {
                Id = availabilityDto.Id,
                IsAvailable = availabilityDto.IsAvailable,
                EstimatedDeliveryDate = availabilityDto.EstimatedDeliveryDate
            };
        }

        private static Image? ConvertToImage(ImageDto? imageDto)
        {
            if (imageDto == null) return null;

            return new Image
            {
                Id = imageDto.Id,
                Url = imageDto.Url,
                AltText = imageDto.AltText
            };
        }
    }
}
