using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
   public enum IssueStatus
    {
        ToBeAssigned,
        CurrentlyWorkedOn,
        Completed
    }
    public class IssueModel
    {
        public (int, string) ProjectId { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = String.Empty;
        [MaxLength(2000)]
        public string Description { get; set; } = String.Empty;
        public int Id { get; set; }
        public IssueStatus Status { get; set; } = IssueStatus.CurrentlyWorkedOn;
        public List<int> UserIds { get; set; } = new List<int>();
        [Required]
        public string AssociatedFiles { get; set; } = String.Empty;
    }
}
