using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IssueTracker.Models
{
   public enum IssueStatus
    {
        ToBeAssigned,
        CurrentlyWorkedOn,
        Completed
    }
    [Table("Issue")]
    public class IssueModel
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = String.Empty;
        [MaxLength(2000)]
        public string Description { get; set; } = String.Empty;
        public int Id { get; set; }
        public IssueStatus Status { get; set; } = IssueStatus.CurrentlyWorkedOn;
        public List<string> UserIds { get; set; } = new List<string>();
       // [Required]
        public string CommitInfo { get; set; } = String.Empty;
    }
}
