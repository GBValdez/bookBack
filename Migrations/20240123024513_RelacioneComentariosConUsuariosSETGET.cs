using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class RelacioneComentariosConUsuariosSETGET : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_comments_userId",
                table: "comments",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_userId",
                table: "comments",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_userId",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_userId",
                table: "comments");
        }
    }
}
