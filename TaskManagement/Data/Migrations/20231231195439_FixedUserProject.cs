using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedUserProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects");

            migrationBuilder.DropIndex(
                name: "IX_UserProjects_MemberId",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "CompositeKey",
                table: "UserProjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects",
                columns: new[] { "MemberId", "ProjectId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects");

            migrationBuilder.AddColumn<string>(
                name: "CompositeKey",
                table: "UserProjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProjects",
                table: "UserProjects",
                column: "CompositeKey");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_MemberId",
                table: "UserProjects",
                column: "MemberId");
        }
    }
}
