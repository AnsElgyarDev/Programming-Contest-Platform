using System.Net.Cache;
using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.DTO;

public class ProblemSummaryDto
{
    public string ProblemName { get; set; } = string.Empty;
    public int ProblemLevel { get; set; }   
}

public class ProblemDetailsDto
{    
    public string ProblemName { get; set; } = string.Empty;
    public string ProblemDescription { get; set; } = string.Empty;   
    public int ProblemLevel { get; set; }   
    public int ContestId { get; set; }
    public string ContestName { get; set; } = string.Empty;   
}

public class ProblemSubmissionDto
{
    public Guid userId { get; set; }
    public int problemId { get; set; }
    public string Language { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}