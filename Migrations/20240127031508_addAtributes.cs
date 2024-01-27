using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class addAtributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "country",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "languageId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "countryId",
                table: "Authors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_languageId",
                table: "Books",
                column: "languageId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_countryId",
                table: "Authors",
                column: "countryId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Country_countryId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Language_languageId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Books_languageId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_countryId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "languageId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "countryId",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Authors",
                type: "text",
                nullable: true);
        }
    }
}
