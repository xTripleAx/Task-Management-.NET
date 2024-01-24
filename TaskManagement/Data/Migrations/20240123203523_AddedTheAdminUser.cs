using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTheAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.AspNetRoles(Id,Name,NormalizedName) VALUES('7c7a2175-c1f1-4485-9d73-912f77a4acdd', 'Admin', 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO dbo.AspNetUsers(Id, UserName, NormalizedUserName,  Email, NormalizedEmail, EmailConfirmed,PhoneNumberConfirmed, LockoutEnabled,TwoFactorEnabled, AccessFailedCount, PasswordHash,SecurityStamp) VALUES('055c0289-8f4f-4345-9642-0ddc4529a325','Admin','ADMIN','Admin@admin.com','ADMIN@ADMIN.COM',1,0,0,0,0,'AQAAAAIAAYagAAAAECg1Nw7NBQrqP01I4cbdXfKCZdcw5k7UtvHW52OdldUiuzrKPU+D0uXaAbrRAGZGHg==','697337aa-e488-4dab-b478-a9751cf59042')");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles]([UserId],[RoleId])VALUES('055c0289-8f4f-4345-9642-0ddc4529a325','7c7a2175-c1f1-4485-9d73-912f77a4acdd')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetRoles] WHERE Id = '7c7a2175-c1f1-4485-9d73-912f77a4acdd'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = '055c0289-8f4f-4345-9642-0ddc4529a325'");
        }
    }
}