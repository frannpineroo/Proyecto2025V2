using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto2025.BD.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "IsModerated",
                table: "Chats");

            migrationBuilder.AddColumn<int>(
                name: "ChatType",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatType",
                table: "Chats");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsModerated",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
