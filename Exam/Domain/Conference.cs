namespace Domain;

public class Conference
{
    //Primary key
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Location { get; set; } = default!;
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public string Topic { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    //Foreign key
    public ICollection<Session>? Sessions {get; set;}
}