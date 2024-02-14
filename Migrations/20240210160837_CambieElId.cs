using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class CambieElId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Language",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Country",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Category",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Authors",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Language",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Country",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "comments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Authors",
                newName: "id");
        }
    }
}
