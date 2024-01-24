using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedIssueModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BacklogId",
                table: "Issues",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_BacklogId",
                table: "Issues",
                column: "BacklogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Backlogs_BacklogId",
                table: "Issues",
                column: "BacklogId",
                principalTable: "Backlogs",
                principalColumn: "BacklogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Backlogs_BacklogId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_BacklogId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "BacklogId",
                table: "Issues");
        }
    }
}
