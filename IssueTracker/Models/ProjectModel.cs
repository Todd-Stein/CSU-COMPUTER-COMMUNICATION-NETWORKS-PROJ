using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public List<int> ProjectLeadIDs { get; set; } = new List<int>();
        [Required]
        public List<int> ProjectMemberIDs { get; set; } = new List<int>();
        public List<IssueModel> ProjectIssues { get; set; } = new List<IssueModel>();

        public string GithubProjectLink { get; set; } = string.Empty;
    }
}
