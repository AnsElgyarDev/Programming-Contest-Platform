using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Programming_Contest_Platform.Data;

namespace Programming_Contest_Platform.Entity;

public class User: IdentityUser<int>
{
public string? FullName { get; set; } = string.Empty;
    public string? Country { get; set; }              
    public string? Organization { get; set; }            
    public string? ProfilePictureUrl { get; set; }         
    public long UserRating { get; set; } = 1500;  
    public long MaxRating { get; set; } = 1500;   
    public int SolvedProblemsCount { get; set; } = 0;

    // Navigation Properties
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();   
}