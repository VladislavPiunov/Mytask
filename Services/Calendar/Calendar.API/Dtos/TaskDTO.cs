namespace Calendar.API.Dtos;

public class TaskDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string BoardId { get; set; }
    public string StageId { get; set; } 
    public string Description { get; set; } 
    public DateTime? Deadline { get; set; } 
    public string Executor { get; set; }
}