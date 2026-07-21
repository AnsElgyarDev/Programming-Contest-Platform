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