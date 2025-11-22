using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unolink.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOngTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "User",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "OngTicket",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "OngTicket",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "User",
                type: "nvarchar(10)", 
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8,
                oldNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "Cep",
                table: "OngTicket",
                type: "nvarchar(11)", 
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);
            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "OngTicket",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);
        }
    }
}
