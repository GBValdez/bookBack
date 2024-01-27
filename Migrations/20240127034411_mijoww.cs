using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class mijoww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "countryId",
                table: "Authors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Country_countryId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_countryId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "countryId",
                table: "Authors");
        }
    }
}
