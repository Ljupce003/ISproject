using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISproject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedFolderEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookmarkFolder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkFolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookmarkFolder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArticleInFolder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookmarkFolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleInFolder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleInFolder_BookmarkFolder_BookmarkFolderId",
                        column: x => x.BookmarkFolderId,
                        principalTable: "BookmarkFolder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ArticleInFolder_NewsArticles_NewsArticleId",
                        column: x => x.NewsArticleId,
                        principalTable: "NewsArticles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleInFolder_BookmarkFolderId",
                table: "ArticleInFolder",
                column: "BookmarkFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleInFolder_NewsArticleId",
                table: "ArticleInFolder",
                column: "NewsArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_BookmarkFolder_UserId",
                table: "BookmarkFolder",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleInFolder");

            migrationBuilder.DropTable(
                name: "BookmarkFolder");
        }
    }
}
