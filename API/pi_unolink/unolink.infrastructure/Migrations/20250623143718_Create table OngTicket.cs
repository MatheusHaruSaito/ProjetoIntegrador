using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unolink.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatetableOngTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "User",
                type: "nvarchar(266)",
                maxLength: 266,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(266)",
                oldMaxLength: 266);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "User",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "OngTicket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reviwed = table.Column<bool>(type: "bit", nullable: false),
                    Accepeted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OngTicket", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OngTicket");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "User",
                type: "nvarchar(266)",
                maxLength: 266,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(266)",
                oldMaxLength: 266,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "User",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
