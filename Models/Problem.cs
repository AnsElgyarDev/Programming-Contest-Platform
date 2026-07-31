using Programming_Contest_Platform.Data;

namespace Programming_Contest_Platform.Entity;

public class Problem
{
    public int ProblemId { get; set; }
    public string ProblemName { get; set; } = string.Empty;
    public int ProblemLevel { get; set; }
    public string ProblemDescription { get; set; } = string.Empty;
    public double TimeLimitInSeconds { get; set; }
    public int MemoryLimitInMB { get; set; }
    public int ContestId { get; set; }
    public Contest Contest { get; set; } = null!;
    public ICollection<TestCase> TestCases { get; set; } = new List<TestCase>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}