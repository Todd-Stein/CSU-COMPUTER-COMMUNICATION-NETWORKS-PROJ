namespace IssueTracker.Models
{
   public  enum IssueStatus
    {
        ToBeAssigned,
        CurrentlyWorkedOn,
        Completed
    }
    public class IssueModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public IssueStatus Status { get; set; } = IssueStatus.CurrentlyWorkedOn;
        //public List<int> UserIds { get; set; } = new List<int>();
        //public string AssociateFiles { get; set; } = String.Empty;
    }
}
