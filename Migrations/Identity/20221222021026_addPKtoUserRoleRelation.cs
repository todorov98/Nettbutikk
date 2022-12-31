using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations.Identity
{
    public partial class addPKtoUserRoleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94f64653-8780-4c9a-b6ab-6de740808f0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4159411-2176-4145-ad05-ea18728de63a");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0d4f4941-d704-4426-9b2a-3e4dabf45ee9", "adminxnet!", "2b33b6cf-7903-42c3-8a40-a00578d81926", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "55cd0b2c-5456-4465-8ad2-3fd954afc4ff", "customerxnet?", "a1607c0a-9de7-4914-8d03-7f74396d1077", "RoleEntity", "Customer", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d4f4941-d704-4426-9b2a-3e4dabf45ee9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55cd0b2c-5456-4465-8ad2-3fd954afc4ff");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "e4159411-2176-4145-ad05-ea18728de63a", "adminxnet!", "5f46b88c-d9d0-4b40-bcdb-d965bd2541e5", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "94f64653-8780-4c9a-b6ab-6de740808f0e", "customerxnet?", "009b888b-f157-44d8-9bcd-e988449737b1", "RoleEntity", "Customer", null });
        }
    }
}
