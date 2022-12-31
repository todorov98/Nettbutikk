using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations.Identity
{
    public partial class AddRolesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "2273bb03-a465-4e09-ad5f-8693a60e3cea", "8c7cf254-1324-4566-a042-304a1ec7cefa", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8188653d-8e62-44fa-a8cb-c0ec8dd73e6d", "07170c73-bc92-4715-9db5-75694a2be682", "RoleEntity", "Customer", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2273bb03-a465-4e09-ad5f-8693a60e3cea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8188653d-8e62-44fa-a8cb-c0ec8dd73e6d");
        }
    }
}
