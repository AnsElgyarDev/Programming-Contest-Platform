namespace Programming_Contest_Platform.Entity;
public class ContestStandings 
{
    public int ContestId { get; set; }
    public Guid UserId {get; set; }
    public int Rank {get; set; }
    public int TotalScore {get; set; }
    public int TotalPenalty {get; set; }
}