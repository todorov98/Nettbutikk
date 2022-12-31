using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations
{
    public partial class RemoveUnessecaryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrderRelation_Orders_OrderId",
                table: "ProductOrderRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrderRelation_Products_ProductId",
                table: "ProductOrderRelation");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrderRelation",
                table: "ProductOrderRelation");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("16ef8e29-71a1-46fa-901a-bfe12d0ad789"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("287f9ac8-c918-43f2-9b37-6f84027381bf"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("28b6eb5e-9197-4d09-a63a-a11b757a0628"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3471cbfc-7956-4c2c-829b-ca9908a31eb3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5ba308de-6ec3-433b-9539-97cceade2138"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("792eb163-ef07-4019-82e1-65c4e431f24c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("de5f9448-4b8c-499c-83fc-84f2fa64ee45"));

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "ProductOrderRelation",
                newName: "ProductOrderRelations");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrderRelation_OrderId",
                table: "ProductOrderRelations",
                newName: "IX_ProductOrderRelations_OrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderReceiptId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrderRelations",
                table: "ProductOrderRelations",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Currency", "Description", "Name", "OrderReceiptId", "Price" },
                values: new object[,]
                {
                    { new Guid("8e6d20e9-08f7-4bd2-b743-81da4a5f358f"), "Football", 9, "EUR", "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", "UEFA Champions Leage 22/23 Final Original Edition", null, 102.98999999999999 },
                    { new Guid("4d4ab102-c4e0-4f47-819b-adb0fd8273aa"), "DrinkingBottle", 8, "EUR", "High quality drinking bottlefrom Puma.", "Puma X2 Bottle", null, 22.989999999999998 },
                    { new Guid("3f164cfa-ae09-41e1-a07b-281b5f6bfc0b"), "Sweater", 7, "EUR", "A highquality breathable sweater produced by Nike. Works well for most physical activity.", "Nike Sweater XZ21 Breather Edition", null, 45.990000000000002 },
                    { new Guid("2eae9a89-5a72-4b71-a1e4-f8e2860f7cb7"), "FootballBoots", 7, "EUR", "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.", "Nike Hypervenom Phantom ACC", null, 249.49000000000001 },
                    { new Guid("6c48f07c-f21d-43ba-a770-1c3919977a52"), "Bag", 7, "EUR", "Completely new and solid bag fromPuma.", "Puma T23 Bag", null, 75.390000000000001 },
                    { new Guid("11e19e2c-6417-4eab-88e6-226fbc1b22a9"), "MountainBoots", 8, "EUR", "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", "Goretex Z34 Climber Boots", null, 167.59 },
                    { new Guid("1fe4ad2b-8f62-4069-968b-7b204b20dd37"), "SportsPants", 11, "EUR", "Nice and comfortable sports pants by Adidas.", "Adidas F99 Pants Long", null, 34.990000000000002 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderReceiptId",
                table: "Products",
                column: "OrderReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserEntity_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "UserEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderRelations_Orders_OrderId",
                table: "ProductOrderRelations",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderRelations_Products_ProductId",
                table: "ProductOrderRelations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Receipts_OrderReceiptId",
                table: "Products",
                column: "OrderReceiptId",
                principalTable: "Receipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserEntity_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrderRelations_Orders_OrderId",
                table: "ProductOrderRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrderRelations_Products_ProductId",
                table: "ProductOrderRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Receipts_OrderReceiptId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderReceiptId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrderRelations",
                table: "ProductOrderRelations");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11e19e2c-6417-4eab-88e6-226fbc1b22a9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1fe4ad2b-8f62-4069-968b-7b204b20dd37"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2eae9a89-5a72-4b71-a1e4-f8e2860f7cb7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3f164cfa-ae09-41e1-a07b-281b5f6bfc0b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4d4ab102-c4e0-4f47-819b-adb0fd8273aa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6c48f07c-f21d-43ba-a770-1c3919977a52"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8e6d20e9-08f7-4bd2-b743-81da4a5f358f"));

            migrationBuilder.DropColumn(
                name: "OrderReceiptId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "ProductOrderRelations",
                newName: "ProductOrderRelation");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrderRelations_OrderId",
                table: "ProductOrderRelation",
                newName: "IX_ProductOrderRelation_OrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrderRelation",
                table: "ProductOrderRelation",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    LoggedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoggedOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CustomerId",
                table: "Sessions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderRelation_Orders_OrderId",
                table: "ProductOrderRelation",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrderRelation_Products_ProductId",
                table: "ProductOrderRelation",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
