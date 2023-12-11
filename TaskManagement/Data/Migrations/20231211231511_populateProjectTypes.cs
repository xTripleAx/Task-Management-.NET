using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class populateProjectTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.ProjectTypes(ProjecTypetName) VALUES ('Kanban')");
            migrationBuilder.Sql("INSERT INTO dbo.ProjectTypes(ProjecTypetName) VALUES ('Scrum')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.ProjectTypes WHERE ProjecTypetName = 'Kanban'");
            migrationBuilder.Sql("DELETE FROM dbo.ProjectTypes WHERE ProjecTypetName = 'Scrum'");
        }
    }
}