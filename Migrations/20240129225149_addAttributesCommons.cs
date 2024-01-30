using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class addAttributesCommons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "eraseAt",
                table: "Language",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Language",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Language",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId1",
                table: "Language",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "eraseAt",
                table: "Country",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Country",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId1",
                table: "Country",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "eraseAt",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId1",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "eraseAt",
                table: "Category",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Category",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId1",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "eraseAt",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Books",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userDeleteId1",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId1",
                table: "Books",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_userDeleteId1",
                table: "Language",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Language_userUpdateId1",
                table: "Language",
                column: "userUpdateId1");

            migrationBuilder.CreateIndex(
                name: "IX_Country_userDeleteId1",
                table: "Country",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Country_userUpdateId1",
                table: "Country",
                column: "userUpdateId1");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userDeleteId1",
                table: "comments",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_comments_userUpdateId1",
                table: "comments",
                column: "userUpdateId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_userDeleteId1",
                table: "Category",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Category_userUpdateId1",
                table: "Category",
                column: "userUpdateId1");

            migrationBuilder.CreateIndex(
                name: "IX_Books_userDeleteId1",
                table: "Books",
                column: "userDeleteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Books_userUpdateId1",
                table: "Books",
                column: "userUpdateId1");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Language_userUpdateId1",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Country_userDeleteId1",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Country_userUpdateId1",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_comments_userDeleteId1",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_userUpdateId1",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_Category_userDeleteId1",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_userUpdateId1",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Books_userDeleteId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_userUpdateId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "eraseAt",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "userUpdateId1",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "eraseAt",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "userUpdateId1",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "eraseAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "userUpdateId1",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "eraseAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "userUpdateId1",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "eraseAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "userDeleteId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "userUpdateId1",
                table: "Books");
        }
    }
}
