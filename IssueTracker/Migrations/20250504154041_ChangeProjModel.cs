using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProjModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GithubProjectLink",
                table: "Project",
                newName: "GithubRepoName");

            migrationBuilder.AddColumn<string>(
                name: "GithubAccountOwner",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GithubAccountOwner",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "GithubRepoName",
                table: "Project",
                newName: "GithubProjectLink");
        }
    }
}
