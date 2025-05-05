using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Models
{
    [Table("Project")]
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<string> ProjectLeadIDs { get; set; } = new List<string>();
        [Required]
        public List<string> ProjectMemberIDs { get; set; } = new List<string>();
        public List<int> ProjectIssues { get; set; } = new List<int>();

        public string GithubAccountOwner { get; set; } = string.Empty;
        public string GithubRepoName {  get; set; } = string.Empty;
    }
}
