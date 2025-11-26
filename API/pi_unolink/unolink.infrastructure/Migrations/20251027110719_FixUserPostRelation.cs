using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unolink.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPostRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVotes_User_UserId",
                table: "PostVotes");

            migrationBuilder.CreateIndex(
                name: "IX_UserPost_UserId",
                table: "UserPost",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostVotes_User_UserId",
                table: "PostVotes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPost_User_UserId",
                table: "UserPost",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostVotes_User_UserId",
                table: "PostVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPost_User_UserId",
                table: "UserPost");

            migrationBuilder.DropIndex(
                name: "IX_UserPost_UserId",
                table: "UserPost");

            migrationBuilder.AddForeignKey(
                name: "FK_PostVotes_User_UserId",
                table: "PostVotes",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
