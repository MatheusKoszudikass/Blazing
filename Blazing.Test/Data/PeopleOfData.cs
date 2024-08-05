using Blazing.Application.Dto;
using Blazing.Domain.Entities;

namespace BlazingPizzaTest.Data
{
    public class PeopleOfData
    {
        // Keys generated for the first product.
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public Guid DimensionsId { get; set; } = Guid.NewGuid();
        public Guid AssessmentId { get; set; } = Guid.NewGuid();
        public Guid RevisionId { get; set; } = Guid.NewGuid();
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public Guid AttributesId { get; set; } = Guid.NewGuid();
        public Guid AvailabilityId { get; set; } = Guid.NewGuid();
        public Guid ImageId { get; set; } = Guid.NewGuid();

        // Keys generated for the second product.
        public Guid ProductId1 { get; set; } = Guid.NewGuid();
        public Guid DimensionsId1 { get; set; } = Guid.NewGuid();
        public Guid AssessmentId1 { get; set; } = Guid.NewGuid();
        public Guid RevisionId1 { get; set; } = Guid.NewGuid();
        public Guid CategoryId1 { get; set; } = Guid.NewGuid();
        public Guid AttributesId1 { get; set; } = Guid.NewGuid();
        public Guid AvailabilityId1 { get; set; } = Guid.NewGuid();
        public Guid ImageId1 { get; set; } = Guid.NewGuid();

        #region Categories
        // Method to add categories.
        public IEnumerable<CategoryDto> AddCategory()
        {
            return
            [
                new ()
                {
                     Id = CategoryId,
                     Name = "Category",
                },
                new ()
                {
                     Id = CategoryId1,
                     Name = "Category 1",

                }
            ];
        }

        // Method to get categories.
        public IEnumerable<CategoryDto> GetCategoryItems()
        {
            return
            [
               new ()
               {
                   Id = CategoryId,
                   Name = "Category",
               },
               new ()
               {
                    Id = CategoryId1,
                    Name = "Category 1",
               }

           ];
        }


        // Method to update a category.
        public IEnumerable<CategoryDto> UpdateCategory()
        {
            return
            [
                    new ()
                    {
                        Id = CategoryId,
                        Name = "Category edited",
                    }
            ];
        }

        public IEnumerable<Guid> GetCategoryIds()
        {
            return
            [
                  CategoryId,
            ];
        }
        #endregion

        #region Products
        // Method to initialize the list of products
        public IEnumerable<ProductDto> GetProducts()
        {
            return
        [
        new ()
        {
            Id = ProductId,
            Name = "Product 1",
            Description = "Description of Product 1",
            Price = 100.00M,
            Currency = "BRL",
            CategoryId = CategoryId,
            Brand = "Brand A",
            SKU = "SKU001",
            StockQuantity = 10,
            StockLocation = "A1",
            DimensionsId = DimensionsId,
            Dimensions = new ()
            {
                Id = DimensionsId,
                Weight = 1.5,
                Height = 10.0,
                Width = 15.0,
                Depth = 20.0,
                Unit = "cm"
            },
            AssessmentId = AssessmentId,
            AttributesId = AttributesId,
            Attributes = new ()
            {
                Id = AttributesId,
                Color = "Blue",
                Material = "Plastic",
                Model = "Model A"
            },
            AvailabilityId = AvailabilityId,
            Availability = new ()
            {
                Id = AvailabilityId,
                IsAvailable = true,
                EstimatedDeliveryDate = DateTime.Now.AddDays(5)
            },
            ImageId = ImageId,
            Image = new ()
            {
                Id = ImageId,
                Url = "https://example.com/image1.jpg",
                AltText = "Image of Product 1"
            }
        },
        new ()
        {
            Id = ProductId1,
            Name = "Product 2",
            Description = "Description of Product 2",
            Price = 200.00M,
            Currency = "BRL",
            CategoryId = CategoryId1,
            Brand = "Brand B",
            SKU = "SKU002",
            StockQuantity = 20,
            StockLocation = "B1",
            DimensionsId = DimensionsId1,
            Dimensions = new ()
            {
                Id = DimensionsId1,
                Weight = 2.5,
                Height = 20.0,
                Width = 25.0,
                Depth = 30.0,
                Unit = "cm"
            },
            AssessmentId = AssessmentId,
            AttributesId = AttributesId1,
            Attributes = new ()
            {
                Id = AttributesId1,
                Color = "Red",
                Material = "Metal",
                Model = "Model B"
            },
            AvailabilityId = AvailabilityId1,
            Availability = new ()
            {
                Id = AvailabilityId1,
                IsAvailable = false,
                EstimatedDeliveryDate = DateTime.Now.AddDays(10)
            },
            ImageId = ImageId1,
            Image = new ()
            {
                Id = ImageId1,
                Url = "https://example.com/image1.jpg",
                AltText = "Image of Product 1"
            }
            }
        ];
        }

        // Updates and returns a product with edited details
        public IEnumerable<ProductDto> UpdateProduct()
        {
            return
            [
                 new()
                    {
                        Id = ProductId,
                        Name = "Product 1 edited",
                        Description = "Description of Product 1 Edit",
                        Price = 300.00M,
                        Currency = "BRL Edit",
                        CategoryId = CategoryId,
                        Brand = "Brand A Edit",
                        SKU = "SKU001 Edit",
                        StockQuantity = 30,
                        StockLocation = "A1 Edit",
                        DimensionsId = DimensionsId,
                        Dimensions = new()
                        {
                            Id = DimensionsId,
                            Weight = 3.5,
                            Height = 30.0,
                            Width = 35.0,
                            Depth = 30.0,
                            Unit = "cm Edit"
                        },
                        AssessmentId = AssessmentId,
                        AttributesId = AttributesId,
                        Attributes = new()
                        {
                            Id = AttributesId,
                            Color = "Blue Edit",
                            Material = "Plastic Edit",
                            Model = "Model A Edit"
                        },
                        AvailabilityId = AvailabilityId,
                        Availability = new()
                        {
                            Id = AvailabilityId,
                            IsAvailable = true,
                            EstimatedDeliveryDate = DateTime.Now.AddDays(5)
                        },
                        ImageId = ImageId,
                        Image = new()
                        {
                            Id = ImageId,
                            Url = "https://example.com/image1.jpg",
                            AltText = "Image of Product 1 Edit"
                        }
                    },

                  new()
                    {
                        Id = ProductId1,
                        Name = "Product 1 edited 2",
                        Description = "Description of Product 1 Edit 2",
                        Price = 300.00M,
                        Currency = "BRL Edit 2",
                        CategoryId = CategoryId1,
                        Brand = "Brand B Edit",
                        SKU = "SKU001 Edit",
                        StockQuantity = 30,
                        StockLocation = "A1 Edit",
                        DimensionsId = DimensionsId1,
                        Dimensions = new()
                        {
                            Id = DimensionsId1,
                            Weight = 3.5,
                            Height = 30.0,
                            Width = 35.0,
                            Depth = 30.0,
                            Unit = "cm Edit"
                        },
                        AssessmentId = AssessmentId1,
                        AttributesId = AttributesId1,
                        Attributes = new()
                        {
                            Id = AttributesId1,
                            Color = "Blue Edit",
                            Material = "Plastic Edit",
                            Model = "Model B Edit"
                        },
                        AvailabilityId = AvailabilityId1,
                        Availability = new()
                        {
                            Id = AvailabilityId1,
                            IsAvailable = true,
                            EstimatedDeliveryDate = DateTime.Now.AddDays(5)
                        },
                        ImageId = ImageId1,
                        Image = new()
                        {
                            Id = ImageId1,
                            Url = "https://example.com/image1.jpg",
                            AltText = "Image of Product 1 Edit"
                        }
                    }
            ];

        }
        public IEnumerable<Guid> GetIdsProduct()
        {
            return
            [
                ProductId1, ProductId

            ];
        }
        #endregion
    }
}