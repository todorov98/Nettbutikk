using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations.Identity
{
    public partial class AddedRoleCodesForRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2273bb03-a465-4e09-ad5f-8693a60e3cea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8188653d-8e62-44fa-a8cb-c0ec8dd73e6d");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "e4159411-2176-4145-ad05-ea18728de63a", "adminxnet!", "5f46b88c-d9d0-4b40-bcdb-d965bd2541e5", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "94f64653-8780-4c9a-b6ab-6de740808f0e", "customerxnet?", "009b888b-f157-44d8-9bcd-e988449737b1", "RoleEntity", "Customer", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94f64653-8780-4c9a-b6ab-6de740808f0e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4159411-2176-4145-ad05-ea18728de63a");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2273bb03-a465-4e09-ad5f-8693a60e3cea", "8c7cf254-1324-4566-a042-304a1ec7cefa", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8188653d-8e62-44fa-a8cb-c0ec8dd73e6d", "07170c73-bc92-4715-9db5-75694a2be682", "RoleEntity", "Customer", null });
        }
    }
}
