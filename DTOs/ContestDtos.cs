using Programming_Contest_Platform.Entity;

namespace Programming_Contest_Platform.DTO;

public class ShowContestDto
{
    public  string ContestName { get; set; } = string.Empty;
    public DateTime contestStartTime;
    public int DurationMinutes;
    public DateTime contestEndTime;
}

public class ContestDetailsDto
{
    public  string ContestName { get; set; } = string.Empty;
    public int ContestLevel { get; set; }
    public ICollection<ProblemSummaryDto> Problems { get; set; } = new List<ProblemSummaryDto>();
}


