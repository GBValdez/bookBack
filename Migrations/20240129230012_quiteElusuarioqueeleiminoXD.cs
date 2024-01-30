using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class quiteElusuarioqueeleiminoXD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_userDeleteId1",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_userUpdateId1",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_userDeleteId1",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_userUpdateId1",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_userDeleteId1",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_userUpdateId1",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_AspNetUsers_userDeleteId1",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_AspNetUsers_userUpdateId1",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_AspNetUsers_userDeleteId1",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_AspNetUsers_userUpdateId1",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Language_userDeleteId1",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Country_userDeleteId1",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_comments_userDeleteId1",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_Category_userDeleteId1",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Books_userDeleteId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "userUpdateId1",
                table: "Language",
                newName: "userUpdateId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_userUpdateId1",
                table: "Language",
                newName: "IX_Language_userUpdateId");

            migrationBuilder.RenameColumn(
                name: "userUpdateId1",
                table: "Country",
                newName: "userUpdateId");

            migrationBuilder.RenameIndex(
                name: "IX_Country_userUpdateId1",
                table: "Country",
                newName: "IX_Country_userUpdateId");

            migrationBuilder.RenameColumn(
                name: "userUpdateId1",
                table: "comments",
                newName: "userUpdateId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_userUpdateId1",
                table: "comments",
                newName: "IX_comments_userUpdateId");

            migrationBuilder.RenameColumn(
                name: "userUpdateId1",
                table: "Category",
                newName: "userUpdateId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_userUpdateId1",
                table: "Category",
                newName: "IX_Category_userUpdateId");

            migrationBuilder.RenameColumn(
                name: "userUpdateId1",
                table: "Books",
                newName: "userUpdateId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_userUpdateId1",
                table: "Books",
                newName: "IX_Books_userUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_userUpdateId",
                table: "Books",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_userUpdateId",
                table: "Category",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_userUpdateId",
                table: "comments",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_AspNetUsers_userUpdateId",
                table: "Country",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_AspNetUsers_userUpdateId",
                table: "Language",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_userUpdateId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_userUpdateId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_AspNetUsers_userUpdateId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Country_AspNetUsers_userUpdateId",
                table: "Country");

            migrationBuilder.DropForeignKey(
                name: "FK_Language_AspNetUsers_userUpdateId",
                table: "Language");

            migrationBuilder.RenameColumn(
                name: "userUpdateId",
                table: "Language",
                newName: "userUpdateId1");

            migrationBuilder.RenameIndex(
                name: "IX_Language_userUpdateId",
                table: "Language",
                newName: "IX_Language_userUpdateId1");

            migrationBuilder.RenameColumn(
                name: "userUpdateId",
                table: "Country",
                newName: "userUpdateId1");

            migrationBuilder.RenameIndex(
                name: "IX_Country_userUpdateId",
                table: "Country",
                newName: "IX_Country_userUpdateId1");

            migrationBuilder.RenameColumn(
                name: "userUpdateId",
                table: "comments",
                newName: "userUpdateId1");

            migrationBuilder.RenameIndex(
                name: "IX_comments_userUpdateId",
                table: "comments",
                newName: "IX_comments_userUpdateId1");

            migrationBuilder.RenameColumn(
                name: "userUpdateId",
                table: "Category",
                newName: "userUpdateId1");

            migrationBuilder.RenameIndex(
                name: "IX_Category_userUpdateId",
                table: "Category",
                newName: "IX_Category_userUpdateId1");

            migrationBuilder.RenameColumn(
                name: "userUpdateId",
                table: "Books",
                newName: "userUpdateId1");

            migrationBuilder.RenameIndex(
                name: "IX_Books_userUpdateId",
                table: "Books",
                newName: "IX_Books_userUpdateId1");

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Language",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_userDeleteId1",
                table: "Language",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Country_userDeleteId1",
                table: "Country",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userDeleteId1",
                table: "comments",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_userDeleteId1",
                table: "Category",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Books_userDeleteId1",
                table: "Books",
                column: "userDeleteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_userDeleteId1",
                table: "Books",
                column: "userDeleteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_userUpdateId1",
                table: "Books",
                column: "userUpdateId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_userDeleteId1",
                table: "Category",
                column: "userDeleteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_userUpdateId1",
                table: "Category",
                column: "userUpdateId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_userDeleteId1",
                table: "comments",
                column: "userDeleteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_AspNetUsers_userUpdateId1",
                table: "comments",
                column: "userUpdateId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_AspNetUsers_userDeleteId1",
                table: "Country",
                column: "userDeleteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_AspNetUsers_userUpdateId1",
                table: "Country",
                column: "userUpdateId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_AspNetUsers_userDeleteId1",
                table: "Language",
                column: "userDeleteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_AspNetUsers_userUpdateId1",
                table: "Language",
                column: "userUpdateId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
