using System.ComponentModel.DataAnnotations.Schema;
using Programming_Contest_Platform.Data;

namespace Programming_Contest_Platform.Entity;

public class User
{
    public int UserId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string UserPassword { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public long? UserRating { get; set; }
    public bool IsAdmin { get; set; }

    // Navigation Properties
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    
    public ICollection<Problem>? RecentProblemsSolved { get; set; }
}