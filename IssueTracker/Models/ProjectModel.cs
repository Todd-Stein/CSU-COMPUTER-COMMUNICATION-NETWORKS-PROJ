using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IssueTracker.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<int> ProjectLeadIDs { get; set; }
        public List<int> ProjectMemberIDs { get; set; }
        public List<IssueModel> ProjectIssues { get; set; }

    }
}
