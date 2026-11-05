using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISproject.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLastColumnChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookmarkCartId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookmarkCartId",
                table: "AspNetUsers");
        }
    }
}
