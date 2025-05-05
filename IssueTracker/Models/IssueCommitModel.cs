namespace IssueTracker.Models
{
    public class IssueCommitModel
    {
        public List<IssueModel> Issues { get; set; }
        public List<string> CommitData { get; set; }
    }
}
