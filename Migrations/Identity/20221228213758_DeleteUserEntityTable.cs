using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations.Identity
{
    public partial class DeleteUserEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c62157f-f1cb-4120-939c-5c3e2198c206");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39e063d7-d46c-4456-8905-3b16df9880ee");

            migrationBuilder.RenameColumn(
                name: "ValidToDate",
                table: "AspNetUserTokens",
                newName: "ValidTo");

            migrationBuilder.RenameColumn(
                name: "ValidFromDate",
                table: "AspNetUserTokens",
                newName: "ValidFrom");

            migrationBuilder.RenameColumn(
                name: "IssuedAtDate",
                table: "AspNetUserTokens",
                newName: "IssuedAt");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "Receipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidTo",
                table: "AspNetUserTokens",
                newName: "ValidToDate");

            migrationBuilder.RenameColumn(
                name: "ValidFrom",
                table: "AspNetUserTokens",
                newName: "ValidFromDate");

            migrationBuilder.RenameColumn(
                name: "IssuedAt",
                table: "AspNetUserTokens",
                newName: "IssuedAtDate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "39e063d7-d46c-4456-8905-3b16df9880ee", "adminxnet!", "b910c6a9-b39d-41fd-944f-7b6d2d9f5666", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0c62157f-f1cb-4120-939c-5c3e2198c206", "customerxnet?", "7542c894-2b29-4aa7-b25a-3bdf6f325eca", "RoleEntity", "Customer", null });
        }
    }
}
