﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Data;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231231195439_FixedUserProject")]
    partial class FixedUserProject
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Models.Backlog", b =>
                {
                    b.Property<int>("BacklogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BacklogId"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("BacklogId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Backlogs");
                });

            modelBuilder.Entity("TaskManagement.Models.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BoardId"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("BoardId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TaskManagement.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PostedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CommentId");

                    b.HasIndex("CommentorId");

                    b.HasIndex("IssueId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TaskManagement.Models.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"));

                    b.Property<string>("AssigneeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("IssueDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssueName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IssueStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListId")
                        .HasColumnType("int");

                    b.Property<string>("ReporterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("blockedBy")
                        .HasColumnType("bit");

                    b.Property<bool?>("blocks")
                        .HasColumnType("bit");

                    b.HasKey("IssueId");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("ListId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("TaskManagement.Models.List", b =>
                {
                    b.Property<int>("ListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ListId"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<int>("ColumnLimit")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isListForFinish")
                        .HasColumnType("bit");

                    b.HasKey("ListId");

                    b.HasIndex("BoardId");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("TaskManagement.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProjectTypeId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ProjectTypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TaskManagement.Models.ProjectTypes", b =>
                {
                    b.Property<int>("ProjectTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectTypeId"));

                    b.Property<string>("ProjecTypetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectTypeId");

                    b.ToTable("ProjectTypes");
                });

            modelBuilder.Entity("TaskManagement.Models.Sprint", b =>
                {
                    b.Property<int>("SprintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SprintId"));

                    b.Property<int>("BacklogId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SprintEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("SprintName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SprintStart")
                        .HasColumnType("datetime2");

                    b.HasKey("SprintId");

                    b.HasIndex("BacklogId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("TaskManagement.Models.UserProject", b =>
                {
                    b.Property<string>("MemberId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("UserProjects");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskManagement.Models.Backlog", b =>
                {
                    b.HasOne("TaskManagement.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskManagement.Models.Board", b =>
                {
                    b.HasOne("TaskManagement.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskManagement.Models.Comment", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Commentor")
                        .WithMany()
                        .HasForeignKey("CommentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagement.Models.Issue", "Issue")
                        .WithMany("Comments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commentor");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("TaskManagement.Models.Issue", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("TaskManagement.Models.List", "List")
                        .WithMany("Issues")
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");

                    b.Navigation("List");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("TaskManagement.Models.List", b =>
                {
                    b.HasOne("TaskManagement.Models.Board", "Board")
                        .WithMany("Lists")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("TaskManagement.Models.Project", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagement.Models.ProjectTypes", "ProjectType")
                        .WithMany("projects")
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("ProjectType");
                });

            modelBuilder.Entity("TaskManagement.Models.Sprint", b =>
                {
                    b.HasOne("TaskManagement.Models.Backlog", "Backlog")
                        .WithMany("Sprints")
                        .HasForeignKey("BacklogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Backlog");
                });

            modelBuilder.Entity("TaskManagement.Models.UserProject", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagement.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskManagement.Models.Backlog", b =>
                {
                    b.Navigation("Sprints");
                });

            modelBuilder.Entity("TaskManagement.Models.Board", b =>
                {
                    b.Navigation("Lists");
                });

            modelBuilder.Entity("TaskManagement.Models.Issue", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TaskManagement.Models.List", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("TaskManagement.Models.ProjectTypes", b =>
                {
                    b.Navigation("projects");
                });
#pragma warning restore 612, 618
        }
    }
}
