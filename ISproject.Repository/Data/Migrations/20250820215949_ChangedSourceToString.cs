using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISproject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSourceToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_NewsSources_SourceId",
                table: "NewsArticles");

            migrationBuilder.DropIndex(
                name: "IX_NewsArticles_SourceId",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "NewsArticles");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "NewsArticles");

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "NewsArticles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_SourceId",
                table: "NewsArticles",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_NewsSources_SourceId",
                table: "NewsArticles",
                column: "SourceId",
                principalTable: "NewsSources",
                principalColumn: "Id");
        }
    }
}
