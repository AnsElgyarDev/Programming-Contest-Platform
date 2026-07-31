using Programming_Contest_Platform.Entity;

public class TestCase
{
    public int Id { get; set; }
    public string Input { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    public bool IsSample { get; set; } 
    public int ProblemId { get; set; }
    public Problem Problem { get; set; } = null!;
}