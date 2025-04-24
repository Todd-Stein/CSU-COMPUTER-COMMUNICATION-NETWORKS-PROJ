namespace IssueTracker.Models
{
    public enum UserType { Standard, Admin};
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; } 
    }
}
