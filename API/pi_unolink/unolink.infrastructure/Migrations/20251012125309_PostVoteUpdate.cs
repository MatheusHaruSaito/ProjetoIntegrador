using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unolink.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PostVoteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "UserPost");

            migrationBuilder.CreateTable(
                name: "PostVotes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVotes", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostVotes_UserPost_PostId",
                        column: x => x.PostId,
                        principalTable: "UserPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostVotes_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVotes",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostVotes");

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "UserPost",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
