using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISproject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookmarkFolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleInFolder_BookmarkFolder_BookmarkFolderId",
                table: "ArticleInFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleInFolder_NewsArticles_NewsArticleId",
                table: "ArticleInFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_BookmarkFolder_AspNetUsers_UserId",
                table: "BookmarkFolder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookmarkFolder",
                table: "BookmarkFolder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleInFolder",
                table: "ArticleInFolder");

            migrationBuilder.RenameTable(
                name: "BookmarkFolder",
                newName: "BookmarkFolders");

            migrationBuilder.RenameTable(
                name: "ArticleInFolder",
                newName: "ArticlesInFolder");

            migrationBuilder.RenameIndex(
                name: "IX_BookmarkFolder_UserId",
                table: "BookmarkFolders",
                newName: "IX_BookmarkFolders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleInFolder_NewsArticleId",
                table: "ArticlesInFolder",
                newName: "IX_ArticlesInFolder_NewsArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleInFolder_BookmarkFolderId",
                table: "ArticlesInFolder",
                newName: "IX_ArticlesInFolder_BookmarkFolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookmarkFolders",
                table: "BookmarkFolders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticlesInFolder",
                table: "ArticlesInFolder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesInFolder_BookmarkFolders_BookmarkFolderId",
                table: "ArticlesInFolder",
                column: "BookmarkFolderId",
                principalTable: "BookmarkFolders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticlesInFolder_NewsArticles_NewsArticleId",
                table: "ArticlesInFolder",
                column: "NewsArticleId",
                principalTable: "NewsArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookmarkFolders_AspNetUsers_UserId",
                table: "BookmarkFolders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesInFolder_BookmarkFolders_BookmarkFolderId",
                table: "ArticlesInFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticlesInFolder_NewsArticles_NewsArticleId",
                table: "ArticlesInFolder");

            migrationBuilder.DropForeignKey(
                name: "FK_BookmarkFolders_AspNetUsers_UserId",
                table: "BookmarkFolders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookmarkFolders",
                table: "BookmarkFolders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticlesInFolder",
                table: "ArticlesInFolder");

            migrationBuilder.RenameTable(
                name: "BookmarkFolders",
                newName: "BookmarkFolder");

            migrationBuilder.RenameTable(
                name: "ArticlesInFolder",
                newName: "ArticleInFolder");

            migrationBuilder.RenameIndex(
                name: "IX_BookmarkFolders_UserId",
                table: "BookmarkFolder",
                newName: "IX_BookmarkFolder_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticlesInFolder_NewsArticleId",
                table: "ArticleInFolder",
                newName: "IX_ArticleInFolder_NewsArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticlesInFolder_BookmarkFolderId",
                table: "ArticleInFolder",
                newName: "IX_ArticleInFolder_BookmarkFolderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookmarkFolder",
                table: "BookmarkFolder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleInFolder",
                table: "ArticleInFolder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleInFolder_BookmarkFolder_BookmarkFolderId",
                table: "ArticleInFolder",
                column: "BookmarkFolderId",
                principalTable: "BookmarkFolder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleInFolder_NewsArticles_NewsArticleId",
                table: "ArticleInFolder",
                column: "NewsArticleId",
                principalTable: "NewsArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookmarkFolder_AspNetUsers_UserId",
                table: "BookmarkFolder",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
