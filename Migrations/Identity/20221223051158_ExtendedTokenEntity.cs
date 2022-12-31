using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nettbutikk.Migrations.Identity
{
    public partial class ExtendedTokenEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d4f4941-d704-4426-9b2a-3e4dabf45ee9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55cd0b2c-5456-4465-8ad2-3fd954afc4ff");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserTokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedAtDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LogoutTerminated",
                table: "AspNetUserTokens",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LogoutTerminationDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignatureAlgorithm",
                table: "AspNetUserTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFromDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidToDate",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "39e063d7-d46c-4456-8905-3b16df9880ee", "adminxnet!", "b910c6a9-b39d-41fd-944f-7b6d2d9f5666", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0c62157f-f1cb-4120-939c-5c3e2198c206", "customerxnet?", "7542c894-2b29-4aa7-b25a-3bdf6f325eca", "RoleEntity", "Customer", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c62157f-f1cb-4120-939c-5c3e2198c206");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39e063d7-d46c-4456-8905-3b16df9880ee");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "IssuedAtDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "LogoutTerminated",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "LogoutTerminationDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "SignatureAlgorithm",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ValidFromDate",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ValidToDate",
                table: "AspNetUserTokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0d4f4941-d704-4426-9b2a-3e4dabf45ee9", "adminxnet!", "2b33b6cf-7903-42c3-8a40-a00578d81926", "RoleEntity", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Code", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "55cd0b2c-5456-4465-8ad2-3fd954afc4ff", "customerxnet?", "a1607c0a-9de7-4914-8d03-7f74396d1077", "RoleEntity", "Customer", null });
        }
    }
}
