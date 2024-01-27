using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class AgregueNuevosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "numPages",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "biography",
                table: "Authors",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "birthDate",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Authors",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "numPages",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "biography",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Authors");
        }
    }
}
