using Blazing.Application.Dto;
using Blazing.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blazing.Test.Data
{
    public class PeopleOfData
    {
        #region Guid id

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


        //User
        public Guid UserId { get; set; } = Guid.NewGuid();
        public Guid UserId1 { get; set; } = Guid.NewGuid();
        public Guid AddressDtoId { get; set; } = Guid.NewGuid();
        public Guid AddressDtoId1 { get; set; } = Guid.NewGuid();
        public Guid AddressDtoId2 { get; set; } = Guid.NewGuid();

        #endregion

        #region Categories

        // Method to add categories.
        public IEnumerable<CategoryDto> AddCategory()
        {
            return
            [
                new CategoryDto
                {
                    Id = CategoryId,
                    Name = "Category"
                },
                new CategoryDto
                {
                    Id = CategoryId1,
                    Name = "Category 1"
                }
            ];
        }

        // Method to get categories.
        public IEnumerable<CategoryDto> GetCategoryItems()
        {
            return
            [
                new CategoryDto
                {
                    Id = CategoryId,
                    Name = "Category"
                },
                new CategoryDto
                {
                    Id = CategoryId1,
                    Name = "Category 1"
                }
            ];
        }


        // Method to update a category.
        public IEnumerable<CategoryDto> UpdateCategory()
        {
            return
            [
                new CategoryDto
                {
                    Id = CategoryId,
                    Name = "Category edited"
                },
                new CategoryDto
                {
                    Id = CategoryId1,
                    Name = "Category edited"
                }
            ];
        }

        public IEnumerable<Guid> GetCategoryIds()
        {
            return
            [
                CategoryId, CategoryId1
            ];
        }

        #endregion

        #region Products

        // Method to initialize the list of products
        public IEnumerable<ProductDto> GetProducts()
        {
            return
            [
                new ProductDto
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
                    Dimensions = new DimensionsDto
                    {
                        Id = DimensionsId,
                        Weight = 1.5,
                        Height = 10.0,
                        Width = 15.0,
                        Depth = 20.0,
                        Unit = "cm"
                    },
                    AssessmentId = AssessmentId,
                    Assessment = new AssessmentDto
                    {
                        Id = AssessmentId,
                        RevisionDetail =
                        [
                            new RevisionDto()
                        ]
                    },
                    AttributesId = AttributesId,
                    Attributes = new AttributeDto
                    {
                        Id = AttributesId,
                        Color = "Blue",
                        Material = "Plastic",
                        Model = "Model A"
                    },
                    AvailabilityId = AvailabilityId,
                    Availability = new AvailabilityDto
                    {
                        Id = AvailabilityId,
                        IsAvailable = true,
                        EstimatedDeliveryDate = DateTime.Now.AddDays(5)
                    },
                    ImageId = ImageId,
                    Image = new ImageDto
                    {
                        Id = ImageId,
                        Url = "https://example.com/image1.jpg",
                        AltText = "Image of Product 1"
                    }
                },
                new ProductDto
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
                    Dimensions = new DimensionsDto
                    {
                        Id = DimensionsId1,
                        Weight = 2.5,
                        Height = 20.0,
                        Width = 25.0,
                        Depth = 30.0,
                        Unit = "cm"
                    },
                    AssessmentId = AssessmentId1,
                    Assessment = new AssessmentDto
                    {
                        Id = AssessmentId1,
                        RevisionDetail =
                        [
                            new RevisionDto()
                        ]
                    },
                    AttributesId = AttributesId1,
                    Attributes = new AttributeDto
                    {
                        Id = AttributesId1,
                        Color = "Red",
                        Material = "Metal",
                        Model = "Model B"
                    },
                    AvailabilityId = AvailabilityId1,
                    Availability = new AvailabilityDto
                    {
                        Id = AvailabilityId1,
                        IsAvailable = false,
                        EstimatedDeliveryDate = DateTime.Now.AddDays(10)
                    },
                    ImageId = ImageId1,
                    Image = new ImageDto
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
                new ProductDto
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
                    Dimensions = new DimensionsDto
                    {
                        Id = DimensionsId,
                        Weight = 3.5,
                        Height = 30.0,
                        Width = 35.0,
                        Depth = 30.0,
                        Unit = "cm Edit"
                    },
                    AssessmentId = AssessmentId,
                    Assessment = new AssessmentDto
                    {
                        Id = AssessmentId,

                        RevisionDetail =
                        [
                            new RevisionDto()
                        ]
                    },
                    AttributesId = AttributesId,
                    Attributes = new AttributeDto
                    {
                        Id = AttributesId,
                        Color = "Blue Edit",
                        Material = "Plastic Edit",
                        Model = "Model A Edit"
                    },
                    AvailabilityId = AvailabilityId,
                    Availability = new AvailabilityDto
                    {
                        Id = AvailabilityId,
                        IsAvailable = true,
                        EstimatedDeliveryDate = DateTime.Now.AddDays(5)
                    },
                    ImageId = ImageId,
                    Image = new ImageDto
                    {
                        Id = ImageId,
                        Url = "https://example.com/image1.jpg",
                        AltText = "Image of Product 1 Edit"
                    }
                },

                new ProductDto
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
                    Dimensions = new DimensionsDto
                    {
                        Id = DimensionsId1,
                        Weight = 3.5,
                        Height = 30.0,
                        Width = 35.0,
                        Depth = 30.0,
                        Unit = "cm Edit"
                    },
                    AssessmentId = AssessmentId1,
                    Assessment = new AssessmentDto
                    {
                        Id = AssessmentId1,
                        RevisionDetail =
                        [
                            new RevisionDto()
                        ]
                    },
                    AttributesId = AttributesId1,
                    Attributes = new AttributeDto
                    {
                        Id = AttributesId1,
                        Color = "Blue Edit",
                        Material = "Plastic Edit",
                        Model = "Model B Edit"
                    },
                    AvailabilityId = AvailabilityId1,
                    Availability = new AvailabilityDto
                    {
                        Id = AvailabilityId1,
                        IsAvailable = true,
                        EstimatedDeliveryDate = DateTime.Now.AddDays(5)
                    },
                    ImageId = ImageId1,
                    Image = new ImageDto
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

        #region Users

        //User
        public IEnumerable<Guid> GetUserId()
        {
            return
            [
                UserId, UserId1
            ];
        }

        public IEnumerable<UserDto> GetAddUsers()
        {
            return
            [
                new UserDto
                {
                    Id = UserId,
                    Status = true,
                    UserName = "john.doe",
                    Email = "john.doe@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = "Matheus@2017",
                    PhoneNumber = "123-456-7890",
                    Addresses =
                    [
                        new()
                        {
                            Id = AddressDtoId,
                            UserId = UserId,
                            Street = "Avenida Paulista",
                            Number = "1000",
                            Complement = "Apto 101",
                            Neighborhood = "Bela Vista",
                            City = "São Paulo",
                            State = "SP",
                            PostalCode = "01310-100"
                        }
                    ]
                },
                new UserDto
                {
                    Id = UserId1,
                    Status = true,
                    UserName = "jane.smith",
                    Email = "jane.smith@example.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PasswordHash = "Matheus@2017",
                    PhoneNumber = "987-654-3210",
                    Addresses =
                    [
                        new()
                        {
                            Id = AddressDtoId1,
                            UserId = UserId1,
                            Street = "Avenida Paulista",
                            Number = "1000",
                            Complement = "Apto 101",
                            Neighborhood = "Bela Vista",
                            City = "São Paulo",
                            State = "SP",
                            PostalCode = "01310-100"
                        },
                        new()
                        {
                            Id = AddressDtoId2,
                            UserId = UserId1,
                            Street = "Rua XV de Novembro",
                            Number = "500",
                            Complement = "Sala 305",
                            Neighborhood = "Centro",
                            City = "Curitiba",
                            State = "PR",
                            PostalCode = "80020-310"
                        }
                    ]
                }
            ];
        }

        public IEnumerable<UserDto> GetUpdateUsers()
        {
            return
            [
                new UserDto
                {
                    Id = UserId,
                    Status = true,
                    UserName = "john.doe edit",
                    Email = "john.doedit@example.com",
                    FirstName = "John edit",
                    LastName = "Doe edit",
                    PhoneNumber = "123-456-7891",
                    Addresses =
                    [
                        //new ()
                        //{
                        //    Id = AddressDtoId,
                        //    UserId = UserId,
                        //    Street = "Avenida Paulista edit",
                        //    Number = "1000",
                        //    Complement = "Apto 101 edit",
                        //    Neighborhood = "Bela Vista edit",
                        //    City = "São Paulo",
                        //    State = "SP",
                        //    PostalCode = "01310-100"
                        //}
                    ]
                },
                new UserDto
                {
                    Id = UserId1,
                    Status = true,
                    UserName = "jane.smith edit",
                    Email = "jane.smithedit@example.com",
                    FirstName = "Janedit",
                    LastName = "SmithEdit",
                    PhoneNumber = "987-654-3210",
                    Addresses =
                    [
                        //new ()
                        //{
                        //    Id = AddressDtoId1,
                        //    UserId = UserId1,
                        //    Street = "Avenida Paulista edit",
                        //    Number = "1000",
                        //    Complement = "Apto 101",
                        //    Neighborhood = "Bela Vista edit",
                        //    City = "São Paulo",
                        //    State = "SP",
                        //    PostalCode = "01310-100"
                        //},
                        //new ()
                        //{
                        //    Id = AddressDtoId2,
                        //    UserId = UserId1,
                        //    Street = "Rua XV de Novembro edit",
                        //    Number = "500",
                        //    Complement = "Sala 305",
                        //    Neighborhood = "Centro edit",
                        //    City = "Curitiba",
                        //    State = "PR",
                        //    PostalCode = "80020-310"
                        //}
                    ]
                }
            ];
        }

        public string GeneratePasswordHash(string password)
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser();

            return passwordHasher.HashPassword(user, password);
        }

        #endregion
    }
}
