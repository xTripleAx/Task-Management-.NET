using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifiedIssueModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssueStatus",
                table: "Issues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_ListId",
                table: "Issues",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Lists_ListId",
                table: "Issues",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "ListId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Lists_ListId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_ListId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "IssueStatus",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Issues");
        }
    }
}
