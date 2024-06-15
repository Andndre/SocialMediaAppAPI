using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserLikedPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPost_Posts_PostId",
                table: "UserLikedPost");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPost_Users_UserId",
                table: "UserLikedPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedPost",
                table: "UserLikedPost");

            migrationBuilder.RenameTable(
                name: "UserLikedPost",
                newName: "UserLikedPosts");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikedPost_UserId",
                table: "UserLikedPosts",
                newName: "IX_UserLikedPosts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedPosts",
                table: "UserLikedPosts",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPosts_Posts_PostId",
                table: "UserLikedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPosts_Users_UserId",
                table: "UserLikedPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPosts_Posts_PostId",
                table: "UserLikedPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPosts_Users_UserId",
                table: "UserLikedPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedPosts",
                table: "UserLikedPosts");

            migrationBuilder.RenameTable(
                name: "UserLikedPosts",
                newName: "UserLikedPost");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikedPosts_UserId",
                table: "UserLikedPost",
                newName: "IX_UserLikedPost_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedPost",
                table: "UserLikedPost",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPost_Posts_PostId",
                table: "UserLikedPost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPost_Users_UserId",
                table: "UserLikedPost",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
