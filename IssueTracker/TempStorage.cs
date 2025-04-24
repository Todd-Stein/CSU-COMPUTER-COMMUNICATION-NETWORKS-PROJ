using IssueTracker.Models;

namespace IssueTracker
{
    public static class TempStorage
    {
        public static Dictionary<int, IssueModel> IssueStorage { get; } = new Dictionary<int, IssueModel>();
        public static Dictionary<int, ProjectModel> ProjectStorage { get; } = new Dictionary<int, ProjectModel>();
        public static List<int> tempUserIdStorage { get; } = new List<int>()
        {
            45,
            22, 
            56,
            107
        };
    }
}
