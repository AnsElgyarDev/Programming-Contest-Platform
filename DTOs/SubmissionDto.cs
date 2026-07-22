using Microsoft.Identity.Client;

namespace Programming_Contest_Platform.DTO;

public class SubmissionSummaryDto
{
    public int SubmissionId;
    public int ProblemId;
    public string ProblemName { get; set; } = string.Empty;
    public string ProblemStatus { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
}

public class ProblemSubmissionsDto
{
    public string userName { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public string SubmissionStatus { get; set; } = string.Empty;
}