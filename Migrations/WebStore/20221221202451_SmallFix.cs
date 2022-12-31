using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations
{
    public partial class SmallFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Currency", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("8e6d20e9-08f7-4bd2-b743-81da4a5f358f"), "Football", 9, "EUR", "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.", "UEFA Champions Leage 22/23 Final Original Edition", 102.98999999999999 },
                    { new Guid("4d4ab102-c4e0-4f47-819b-adb0fd8273aa"), "DrinkingBottle", 8, "EUR", "High quality drinking bottlefrom Puma.", "Puma X2 Bottle", 22.989999999999998 },
                    { new Guid("3f164cfa-ae09-41e1-a07b-281b5f6bfc0b"), "Sweater", 7, "EUR", "A highquality breathable sweater produced by Nike. Works well for most physical activity.", "Nike Sweater XZ21 Breather Edition", 45.990000000000002 },
                    { new Guid("2eae9a89-5a72-4b71-a1e4-f8e2860f7cb7"), "FootballBoots", 7, "EUR", "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.", "Nike Hypervenom Phantom ACC", 249.49000000000001 },
                    { new Guid("6c48f07c-f21d-43ba-a770-1c3919977a52"), "Bag", 7, "EUR", "Completely new and solid bag fromPuma.", "Puma T23 Bag", 75.390000000000001 },
                    { new Guid("11e19e2c-6417-4eab-88e6-226fbc1b22a9"), "MountainBoots", 8, "EUR", "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.", "Goretex Z34 Climber Boots", 167.59 },
                    { new Guid("1fe4ad2b-8f62-4069-968b-7b204b20dd37"), "SportsPants", 11, "EUR", "Nice and comfortable sports pants by Adidas.", "Adidas F99 Pants Long", 34.990000000000002 }
                });
        }
    }
}
