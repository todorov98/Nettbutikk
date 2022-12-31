﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations
{
    public partial class ExtendedProductOrderRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00d856ce-cb1b-4811-87e5-c3403ee98b85"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("108d33ae-64d1-4895-8e13-b370b399dbe4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22bd0a0d-e704-42a8-865e-d58b58f118d2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3ad3e476-0d70-4c5a-9ddd-ff68ebbb9d8e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a21a9091-a965-439d-9ae9-39f1dfbd575e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a35dfefc-e86f-49cc-a30e-fe3abf2d7a02"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f76aeef4-f729-4dc5-9ca7-73b29f5e0725"));

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "ProductOrderRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "ProductOrderRelations");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Currency", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("3ad3e476-0d70-4c5a-9ddd-ff68ebbb9d8e"), "Football", 9, "EUR", "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", "UEFA Champions Leage 22/23 Final Original Edition", 102.98999999999999 },
                    { new Guid("22bd0a0d-e704-42a8-865e-d58b58f118d2"), "DrinkingBottle", 8, "EUR", "High quality drinking bottlefrom Puma.", "Puma X2 Bottle", 22.989999999999998 },
                    { new Guid("00d856ce-cb1b-4811-87e5-c3403ee98b85"), "Sweater", 7, "EUR", "A highquality breathable sweater produced by Nike. Works well for most physical activity.", "Nike Sweater XZ21 Breather Edition", 45.990000000000002 },
                    { new Guid("a21a9091-a965-439d-9ae9-39f1dfbd575e"), "FootballBoots", 7, "EUR", "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.", "Nike Hypervenom Phantom ACC", 249.49000000000001 },
                    { new Guid("a35dfefc-e86f-49cc-a30e-fe3abf2d7a02"), "Bag", 7, "EUR", "Completely new and solid bag fromPuma.", "Puma T23 Bag", 75.390000000000001 },
                    { new Guid("f76aeef4-f729-4dc5-9ca7-73b29f5e0725"), "MountainBoots", 8, "EUR", "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", "Goretex Z34 Climber Boots", 167.59 },
                    { new Guid("108d33ae-64d1-4895-8e13-b370b399dbe4"), "SportsPants", 11, "EUR", "Nice and comfortable sports pants by Adidas.", "Adidas F99 Pants Long", 34.990000000000002 }
                });
        }
    }
}
