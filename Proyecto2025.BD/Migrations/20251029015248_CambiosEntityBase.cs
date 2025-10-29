using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class CambiosEntityBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "ChatMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Roles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Notifications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Chats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoRegistro",
                table: "ChatMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
