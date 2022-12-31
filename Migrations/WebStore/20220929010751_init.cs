using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatePlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFulfilled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoggedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CancelOrderConfirmations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCancelled = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelOrderConfirmations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelOrderConfirmations_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderRelation",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderRelation", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ProductOrderRelation_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrderRelation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Currency", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3471cbfc-7956-4c2c-829b-ca9908a31eb3"), "Football", 9, "EUR", "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", "UEFA Champions Leage 22/23 Final Original Edition", 102.98999999999999 },
                    { new Guid("de5f9448-4b8c-499c-83fc-84f2fa64ee45"), "DrinkingBottle", 8, "EUR", "High quality drinking bottlefrom Puma.", "Puma X2 Bottle", 22.989999999999998 },
                    { new Guid("16ef8e29-71a1-46fa-901a-bfe12d0ad789"), "Sweater", 7, "EUR", "A highquality breathable sweater produced by Nike. Works well for most physical activity.", "Nike Sweater XZ21 Breather Edition", 45.990000000000002 },
                    { new Guid("792eb163-ef07-4019-82e1-65c4e431f24c"), "FootballBoots", 7, "EUR", "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.", "Nike Hypervenom Phantom ACC", 249.49000000000001 },
                    { new Guid("287f9ac8-c918-43f2-9b37-6f84027381bf"), "Bag", 7, "EUR", "Completely new and solid bag fromPuma.", "Puma T23 Bag", 75.390000000000001 },
                    { new Guid("28b6eb5e-9197-4d09-a63a-a11b757a0628"), "MountainBoots", 8, "EUR", "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", "Goretex Z34 Climber Boots", 167.59 },
                    { new Guid("5ba308de-6ec3-433b-9539-97cceade2138"), "SportsPants", 11, "EUR", "Nice and comfortable sports pants by Adidas.", "Adidas F99 Pants Long", 34.990000000000002 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelOrderConfirmations_OrderId",
                table: "CancelOrderConfirmations",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderRelation_OrderId",
                table: "ProductOrderRelation",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_OrderId",
                table: "Receipts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CustomerId",
                table: "Sessions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelOrderConfirmations");

            migrationBuilder.DropTable(
                name: "ProductOrderRelation");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
