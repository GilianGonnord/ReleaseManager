using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReleaseManager.Model.Migrations
{
    public partial class AddGithubProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "project",
                table: "github_credentials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "repo_url",
                table: "apps",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "git_provider",
                table: "apps",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "project",
                table: "github_credentials");

            migrationBuilder.DropColumn(
                name: "git_provider",
                table: "apps");

            migrationBuilder.AlterColumn<string>(
                name: "repo_url",
                table: "apps",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
