using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class mijo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_category_categoryId",
                table: "Book_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_language_languageId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_language",
                table: "language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_country",
                table: "country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

            migrationBuilder.RenameTable(
                name: "language",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "country",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_Category_categoryId",
                table: "Book_Category",
                column: "categoryId",
                principalTable: "Category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Language_languageId",
                table: "Books",
                column: "languageId",
                principalTable: "Language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category_Category_categoryId",
                table: "Book_Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Language_languageId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "language");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "country");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_language",
                table: "language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_country",
                table: "country",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category_category_categoryId",
                table: "Book_Category",
                column: "categoryId",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_language_languageId",
                table: "Books",
                column: "languageId",
                principalTable: "language",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
