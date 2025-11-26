using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unolink.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VoteComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vote",
                table: "PostComment");

            migrationBuilder.CreateTable(
                name: "CommentVotes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentVotes", x => new { x.UserId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_CommentVotes_PostComment_CommentId",
                        column: x => x.CommentId,
                        principalTable: "PostComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentVotes_PostComment_PostCommentId",
                        column: x => x.PostCommentId,
                        principalTable: "PostComment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommentVotes_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_CommentId",
                table: "CommentVotes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentVotes_PostCommentId",
                table: "CommentVotes",
                column: "PostCommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentVotes");

            migrationBuilder.AddColumn<int>(
                name: "Vote",
                table: "PostComment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
