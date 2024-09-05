using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazing.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Average = table.Column<double>(type: "float", nullable: false),
                    NumberOfReviews = table.Column<int>(type: "int", nullable: false),
                    RevisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Material = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Model = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Depth = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    AltText = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    LastName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    UserName = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Currency = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    SKU = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    StockLocation = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    DimensionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailabilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Availabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Dimensions_DimensionsId",
                        column: x => x.DimensionsId,
                        principalTable: "Dimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Number = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Complement = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    City = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    State = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Revisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Revisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Revisions_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Revisions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddCartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCart_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddCartItems_ProductId",
                table: "AddCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AssessmentId",
                table: "Products",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AttributesId",
                table: "Products",
                column: "AttributesId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_AvailabilityId",
                table: "Products",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DimensionsId",
                table: "Products",
                column: "DimensionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ImageId",
                table: "Products",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_AssessmentId",
                table: "Revisions",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Revisions_UserId",
                table: "Revisions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_UserId",
                table: "ShoppingCart",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddCartItems");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Revisions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
