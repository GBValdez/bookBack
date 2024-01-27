using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class addAtributesXD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Country_countryId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Language_languageId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "language");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_language",
                table: "language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_country",
                table: "country",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_country_countryId",
                table: "Authors",
                column: "countryId",
                principalTable: "country",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_country_countryId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_language_languageId",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_language",
                table: "language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_country",
                table: "country");

            migrationBuilder.RenameTable(
                name: "language",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "country",
                newName: "Country");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Country_countryId",
                table: "Authors",
                column: "countryId",
                principalTable: "Country",
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
    }
}
