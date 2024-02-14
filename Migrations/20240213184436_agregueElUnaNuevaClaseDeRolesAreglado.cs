using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba.Migrations
{
    /// <inheritdoc />
    public partial class agregueElUnaNuevaClaseDeRolesAreglado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deleteAt",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateAt",
                table: "AspNetRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "userUpdateId",
                table: "AspNetRoles",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_userUpdateId",
                table: "AspNetRoles",
                column: "userUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_userUpdateId",
                table: "AspNetRoles",
                column: "userUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_userUpdateId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_userUpdateId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "deleteAt",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "updateAt",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "userUpdateId",
                table: "AspNetRoles");
        }
    }
}
