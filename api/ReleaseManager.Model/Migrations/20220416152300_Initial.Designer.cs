﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReleaseManager.Model;

#nullable disable

namespace ReleaseManager.Model.Migrations
{
    [DbContext(typeof(ReleaseManagerContext))]
    [Migration("20220416152300_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ReleaseManager.Model.Models.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GitHubCredentialId")
                        .HasColumnType("integer")
                        .HasColumnName("git_hub_credential_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("RepoUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("repo_url");

                    b.HasKey("Id")
                        .HasName("pk_apps");

                    b.HasIndex("GitHubCredentialId")
                        .HasDatabaseName("ix_apps_git_hub_credential_id");

                    b.ToTable("apps", (string)null);
                });

            modelBuilder.Entity("ReleaseManager.Model.Models.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GitExePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("git_exe_path");

                    b.Property<string>("RootProjectRepository")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("root_project_repository");

                    b.HasKey("Id")
                        .HasName("pk_configs");

                    b.ToTable("configs", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GitExePath = "C:\\Program Files\\Git\\bin\\git.exe",
                            RootProjectRepository = "C:\\ReleaseManager"
                        });
                });

            modelBuilder.Entity("ReleaseManager.Model.Models.GithubCredential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("access_token");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_github_credentials");

                    b.ToTable("github_credentials", (string)null);
                });

            modelBuilder.Entity("ReleaseManager.Model.Models.Release", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_created")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("VersionNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("version_number");

                    b.HasKey("Id")
                        .HasName("pk_releases");

                    b.HasIndex("VersionNumber")
                        .IsUnique()
                        .HasDatabaseName("ix_releases_version_number");

                    b.ToTable("releases", (string)null);
                });

            modelBuilder.Entity("ReleaseManager.Model.Models.User", b =>
                {
                    b.Property<string>("Uid")
                        .HasColumnType("text")
                        .HasColumnName("uid");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("Uid")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("ReleaseManager.Model.Models.App", b =>
                {
                    b.HasOne("ReleaseManager.Model.Models.GithubCredential", "GithubCredential")
                        .WithMany()
                        .HasForeignKey("GitHubCredentialId")
                        .HasConstraintName("fk_apps_github_credentials_git_hub_credential_id");

                    b.Navigation("GithubCredential");
                });
#pragma warning restore 612, 618
        }
    }
}
