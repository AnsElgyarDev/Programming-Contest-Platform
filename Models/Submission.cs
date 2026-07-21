namespace Programming_Contest_Platform.Entity;

public class Submission
{
    public int SubmissionId { get; set; }
    public string SubmissionCode { get; set; } = string.Empty;
    public DateTime SubmissionTime { get; set; }
    public string SubmissionState { get; set; } = string.Empty;
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;
}