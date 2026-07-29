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
    public string ContestName { get; set; } = string.Empty;   
}
