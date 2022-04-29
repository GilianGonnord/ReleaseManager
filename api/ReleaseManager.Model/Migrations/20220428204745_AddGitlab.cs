using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReleaseManager.Model.Migrations
{
    public partial class AddGitlab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_apps_github_credentials_git_hub_credential_id",
                table: "apps");

            migrationBuilder.DropPrimaryKey(
                name: "pk_github_credentials",
                table: "github_credentials");

            migrationBuilder.RenameTable(
                name: "github_credentials",
                newName: "github_credential");

            migrationBuilder.AddColumn<int>(
                name: "gitlab_credential_id",
                table: "apps",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_github_credential",
                table: "github_credential",
                column: "id");

            migrationBuilder.CreateTable(
                name: "gitlab_credential",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    owner = table.Column<string>(type: "text", nullable: false),
                    project = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gitlab_credential", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_apps_gitlab_credential_id",
                table: "apps",
                column: "gitlab_credential_id");

            migrationBuilder.AddForeignKey(
                name: "fk_apps_github_credential_git_hub_credential_id",
                table: "apps",
                column: "git_hub_credential_id",
                principalTable: "github_credential",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_apps_gitlab_credential_gitlab_credential_id",
                table: "apps",
                column: "gitlab_credential_id",
                principalTable: "gitlab_credential",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_apps_github_credential_git_hub_credential_id",
                table: "apps");

            migrationBuilder.DropForeignKey(
                name: "fk_apps_gitlab_credential_gitlab_credential_id",
                table: "apps");

            migrationBuilder.DropTable(
                name: "gitlab_credential");

            migrationBuilder.DropIndex(
                name: "ix_apps_gitlab_credential_id",
                table: "apps");

            migrationBuilder.DropPrimaryKey(
                name: "pk_github_credential",
                table: "github_credential");

            migrationBuilder.DropColumn(
                name: "gitlab_credential_id",
                table: "apps");

            migrationBuilder.RenameTable(
                name: "github_credential",
                newName: "github_credentials");

            migrationBuilder.AddPrimaryKey(
                name: "pk_github_credentials",
                table: "github_credentials",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_apps_github_credentials_git_hub_credential_id",
                table: "apps",
                column: "git_hub_credential_id",
                principalTable: "github_credentials",
                principalColumn: "id");
        }
    }
}
