using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class agreguelosvaloralautor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleteAt",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "Authors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId",
                table: "Authors",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_userUpdateId",
                table: "Authors",
                column: "userUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_userUpdateId",
                table: "Authors",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_userUpdateId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_userUpdateId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "deleteAt",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "userUpdateId",
                table: "Authors");
        }
    }
}
