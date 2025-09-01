using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISproject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedMoreEntitiess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleInBookMarkCart_BookmarkCart_BookmarkCartId",
                table: "BookmarkedArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleInBookMarkCart_NewsArticle_ArticleId",
                table: "BookmarkedArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_BookmarkCart_AspNetUsers_UserId",
                table: "BookmarkCart");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_Category_CategoryId",
                table: "NewsArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_Country_CountryId",
                table: "NewsArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_Language_LanguageId",
                table: "NewsArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_NewsSource_SourceId",
                table: "NewsArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsSource_Category_CategoryId",
                table: "NewsSource");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsSource_Country_CountryId",
                table: "NewsSource");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsSource_Language_LanguageId",
                table: "NewsSource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsSource",
                table: "NewsSource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsArticle",
                table: "NewsArticle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookmarkCart",
                table: "BookmarkCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleInBookMarkCart",
                table: "BookmarkedArticle");

            migrationBuilder.RenameTable(
                name: "NewsSource",
                newName: "NewsSources");

            migrationBuilder.RenameTable(
                name: "NewsArticle",
                newName: "NewsArticles");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "BookmarkCart",
                newName: "BookmarkCarts");

            migrationBuilder.RenameTable(
                name: "BookmarkedArticle",
                newName: "ArticleInBookMarkCarts");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSource_LanguageId",
                table: "NewsSources",
                newName: "IX_NewsSources_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSource_CountryId",
                table: "NewsSources",
                newName: "IX_NewsSources_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSource_Code",
                table: "NewsSources",
                newName: "IX_NewsSources_Code");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSource_CategoryId",
                table: "NewsSources",
                newName: "IX_NewsSources_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticle_SourceId",
                table: "NewsArticles",
                newName: "IX_NewsArticles_SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticle_LanguageId",
                table: "NewsArticles",
                newName: "IX_NewsArticles_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticle_CountryId",
                table: "NewsArticles",
                newName: "IX_NewsArticles_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticle_CategoryId",
                table: "NewsArticles",
                newName: "IX_NewsArticles_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Language_Code",
                table: "Languages",
                newName: "IX_Languages_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Country_Code",
                table: "Countries",
                newName: "IX_Countries_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Code",
                table: "Categories",
                newName: "IX_Categories_Code");

            migrationBuilder.RenameIndex(
                name: "IX_BookmarkCart_UserId",
                table: "BookmarkCarts",
                newName: "IX_BookmarkCarts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleInBookMarkCart_BookmarkCartId",
                table: "ArticleInBookMarkCarts",
                newName: "IX_ArticleInBookMarkCarts_BookmarkCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleInBookMarkCart_ArticleId",
                table: "ArticleInBookMarkCarts",
                newName: "IX_ArticleInBookMarkCarts_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsSources",
                table: "NewsSources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsArticles",
                table: "NewsArticles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookmarkCarts",
                table: "BookmarkCarts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleInBookMarkCarts",
                table: "ArticleInBookMarkCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleInBookMarkCarts_BookmarkCarts_BookmarkCartId",
                table: "ArticleInBookMarkCarts",
                column: "BookmarkCartId",
                principalTable: "BookmarkCarts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleInBookMarkCarts_NewsArticles_ArticleId",
                table: "ArticleInBookMarkCarts",
                column: "ArticleId",
                principalTable: "NewsArticles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookmarkCarts_AspNetUsers_UserId",
                table: "BookmarkCarts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Categories_CategoryId",
                table: "NewsArticles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Countries_CountryId",
                table: "NewsArticles",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Languages_LanguageId",
                table: "NewsArticles",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_NewsSources_SourceId",
                table: "NewsArticles",
                column: "SourceId",
                principalTable: "NewsSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsSources_Categories_CategoryId",
                table: "NewsSources",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsSources_Countries_CountryId",
                table: "NewsSources",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsSources_Languages_LanguageId",
                table: "NewsSources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleInBookMarkCarts_BookmarkCarts_BookmarkCartId",
                table: "ArticleInBookMarkCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleInBookMarkCarts_NewsArticles_ArticleId",
                table: "ArticleInBookMarkCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_BookmarkCarts_AspNetUsers_UserId",
                table: "BookmarkCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Categories_CategoryId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Countries_CountryId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Languages_LanguageId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_NewsSources_SourceId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsSources_Categories_CategoryId",
                table: "NewsSources");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsSources_Countries_CountryId",
                table: "NewsSources");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsSources_Languages_LanguageId",
                table: "NewsSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsSources",
                table: "NewsSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsArticles",
                table: "NewsArticles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookmarkCarts",
                table: "BookmarkCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleInBookMarkCarts",
                table: "ArticleInBookMarkCarts");

            migrationBuilder.RenameTable(
                name: "NewsSources",
                newName: "NewsSource");

            migrationBuilder.RenameTable(
                name: "NewsArticles",
                newName: "NewsArticle");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "BookmarkCarts",
                newName: "BookmarkCart");

            migrationBuilder.RenameTable(
                name: "ArticleInBookMarkCarts",
                newName: "BookmarkedArticle");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSources_LanguageId",
                table: "NewsSource",
                newName: "IX_NewsSource_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSources_CountryId",
                table: "NewsSource",
                newName: "IX_NewsSource_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSources_Code",
                table: "NewsSource",
                newName: "IX_NewsSource_Code");

            migrationBuilder.RenameIndex(
                name: "IX_NewsSources_CategoryId",
                table: "NewsSource",
                newName: "IX_NewsSource_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticles_SourceId",
                table: "NewsArticle",
                newName: "IX_NewsArticle_SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticles_LanguageId",
                table: "NewsArticle",
                newName: "IX_NewsArticle_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticles_CountryId",
                table: "NewsArticle",
                newName: "IX_NewsArticle_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsArticles_CategoryId",
                table: "NewsArticle",
                newName: "IX_NewsArticle_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_Code",
                table: "Language",
                newName: "IX_Language_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Countries_Code",
                table: "Country",
                newName: "IX_Country_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Code",
                table: "Category",
                newName: "IX_Category_Code");

            migrationBuilder.RenameIndex(
                name: "IX_BookmarkCarts_UserId",
                table: "BookmarkCart",
                newName: "IX_BookmarkCart_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleInBookMarkCarts_BookmarkCartId",
                table: "BookmarkedArticle",
                newName: "IX_ArticleInBookMarkCart_BookmarkCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleInBookMarkCarts_ArticleId",
                table: "BookmarkedArticle",
                newName: "IX_ArticleInBookMarkCart_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsSource",
                table: "NewsSource",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsArticle",
                table: "NewsArticle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookmarkCart",
                table: "BookmarkCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleInBookMarkCart",
                table: "BookmarkedArticle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleInBookMarkCart_BookmarkCart_BookmarkCartId",
                table: "BookmarkedArticle",
                column: "BookmarkCartId",
                principalTable: "BookmarkCart",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleInBookMarkCart_NewsArticle_ArticleId",
                table: "BookmarkedArticle",
                column: "ArticleId",
                principalTable: "NewsArticle",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookmarkCart_AspNetUsers_UserId",
                table: "BookmarkCart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_Category_CategoryId",
                table: "NewsArticle",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_Country_CountryId",
                table: "NewsArticle",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_Language_LanguageId",
                table: "NewsArticle",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_NewsSource_SourceId",
                table: "NewsArticle",
                column: "SourceId",
                principalTable: "NewsSource",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsSource_Category_CategoryId",
                table: "NewsSource",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsSource_Country_CountryId",
                table: "NewsSource",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsSource_Language_LanguageId",
                table: "NewsSource",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");
        }
    }
}
