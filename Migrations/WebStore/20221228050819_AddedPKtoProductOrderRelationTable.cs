using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations
{
    public partial class AddedPKtoProductOrderRelationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrderRelations",
                table: "ProductOrderRelations");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("27a7308e-135d-4c3e-af2c-340e2dbfca67"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("314fcd12-630f-49de-afd8-a175c1c5f2d7"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("33351f26-ebe0-477d-8aa7-47ea341caa14"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4791f5a2-0855-48ef-9bde-f96bd30e8ea6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50e0b109-043b-45b8-a308-9c646a83eb4d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a1f4c5f1-a375-4020-8a68-59a1adc9418c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f3666474-40b4-4f46-b27a-103a36d19772"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductOrderRelations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrderRelations",
                table: "ProductOrderRelations",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Currency", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("2fc06450-adde-43f6-b833-4f832fd437ce"), "Football", 19, "EUR", "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", "UEFA Champions Leage 22/23 Final Original Edition", 102.98999999999999 },
                    { new Guid("57c81098-e6a7-49da-b791-c89fe1730575"), "DrinkingBottle", 18, "EUR", "High quality drinking bottlefrom Puma.", "Puma X2 Bottle", 22.989999999999998 },
                    { new Guid("59f2a137-7c0f-4dbf-9508-eb3b5f0290c6"), "Sweater", 17, "EUR", "A highquality breathable sweater produced by Nike. Works well for most physical activity.", "Nike Sweater XZ21 Breather Edition", 45.990000000000002 },
                    { new Guid("add7e84e-0c9e-44f9-8e5e-95060c278196"), "FootballBoots", 17, "EUR", "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.", "Nike Hypervenom Phantom ACC", 249.49000000000001 },
                    { new Guid("f9f4beb3-2526-4a22-94dd-e5dd425d4a80"), "Bag", 17, "EUR", "Completely new and solid bag fromPuma.", "Puma T23 Bag", 75.390000000000001 },
                    { new Guid("00c8f864-5479-4e88-85d1-9f2dd29f1b4e"), "MountainBoots", 18, "EUR", "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", "Goretex Z34 Climber Boots", 167.59 },
                    { new Guid("e3744a41-c6e0-4f90-abe7-3877d0329a5b"), "SportsPants", 21, "EUR", "Nice and comfortable sports pants by Adidas.", "Adidas F99 Pants Long", 34.990000000000002 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrderRelations_ProductId",
                table: "ProductOrderRelations",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrderRelations",
                table: "ProductOrderRelations");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrderRelations_ProductId",
                table: "ProductOrderRelations");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00c8f864-5479-4e88-85d1-9f2dd29f1b4e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2fc06450-adde-43f6-b833-4f832fd437ce"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("57c81098-e6a7-49da-b791-c89fe1730575"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("59f2a137-7c0f-4dbf-9508-eb3b5f0290c6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("add7e84e-0c9e-44f9-8e5e-95060c278196"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e3744a41-c6e0-4f90-abe7-3877d0329a5b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f9f4beb3-2526-4a22-94dd-e5dd425d4a80"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductOrderRelations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrderRelations",
                table: "ProductOrderRelations",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Currency", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("50e0b109-043b-45b8-a308-9c646a83eb4d"), "Football", 9, "EUR", "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", "UEFA Champions Leage 22/23 Final Original Edition", 102.98999999999999 },
                    { new Guid("4791f5a2-0855-48ef-9bde-f96bd30e8ea6"), "DrinkingBottle", 8, "EUR", "High quality drinking bottlefrom Puma.", "Puma X2 Bottle", 22.989999999999998 },
                    { new Guid("33351f26-ebe0-477d-8aa7-47ea341caa14"), "Sweater", 7, "EUR", "A highquality breathable sweater produced by Nike. Works well for most physical activity.", "Nike Sweater XZ21 Breather Edition", 45.990000000000002 },
                    { new Guid("f3666474-40b4-4f46-b27a-103a36d19772"), "FootballBoots", 7, "EUR", "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.", "Nike Hypervenom Phantom ACC", 249.49000000000001 },
                    { new Guid("27a7308e-135d-4c3e-af2c-340e2dbfca67"), "Bag", 7, "EUR", "Completely new and solid bag fromPuma.", "Puma T23 Bag", 75.390000000000001 },
                    { new Guid("a1f4c5f1-a375-4020-8a68-59a1adc9418c"), "MountainBoots", 8, "EUR", "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", "Goretex Z34 Climber Boots", 167.59 },
                    { new Guid("314fcd12-630f-49de-afd8-a175c1c5f2d7"), "SportsPants", 11, "EUR", "Nice and comfortable sports pants by Adidas.", "Adidas F99 Pants Long", 34.990000000000002 }
                });
        }
    }
}
