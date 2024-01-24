using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetNullListInIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Lists_ListId",
                table: "Issues");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Lists_ListId",
                table: "Issues",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "ListId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Lists_ListId",
                table: "Issues");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Lists_ListId",
                table: "Issues",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "ListId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
