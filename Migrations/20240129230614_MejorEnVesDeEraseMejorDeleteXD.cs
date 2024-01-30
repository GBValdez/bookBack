using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class MejorEnVesDeEraseMejorDeleteXD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "eraseAt",
                table: "Language",
                newName: "deleteAt");

            migrationBuilder.RenameColumn(
                name: "eraseAt",
                table: "Country",
                newName: "deleteAt");

            migrationBuilder.RenameColumn(
                name: "eraseAt",
                table: "comments",
                newName: "deleteAt");

            migrationBuilder.RenameColumn(
                name: "eraseAt",
                table: "Category",
                newName: "deleteAt");

            migrationBuilder.RenameColumn(
                name: "eraseAt",
                table: "Books",
                newName: "deleteAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deleteAt",
                table: "Language",
                newName: "eraseAt");

            migrationBuilder.RenameColumn(
                name: "deleteAt",
                table: "Country",
                newName: "eraseAt");

            migrationBuilder.RenameColumn(
                name: "deleteAt",
                table: "comments",
                newName: "eraseAt");

            migrationBuilder.RenameColumn(
                name: "deleteAt",
                table: "Category",
                newName: "eraseAt");

            migrationBuilder.RenameColumn(
                name: "deleteAt",
                table: "Books",
                newName: "eraseAt");
        }
    }
}
