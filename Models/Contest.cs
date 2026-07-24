namespace Programming_Contest_Platform.Entity;

public class Contest
{
    public int ContestId { get; set; }
    public string ContestName { get; set; } = string.Empty;
    public int ContestLevel { get; set; }
    public DateTime ContestStartTime { get; set; }
    public DateTime ContestEndTime { get; set; }
    public ICollection<Problem> Problems { get; set; } = new List<Problem>();
}