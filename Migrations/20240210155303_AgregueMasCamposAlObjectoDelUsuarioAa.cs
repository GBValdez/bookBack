using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class AgregueMasCamposAlObjectoDelUsuarioAa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleteAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_userUpdateId",
                table: "AspNetUsers",
                column: "userUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_userUpdateId",
                table: "AspNetUsers",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_userUpdateId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_userUpdateId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "deleteAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "userUpdateId",
                table: "AspNetUsers");
        }
    }
}
