namespace Domain;

public class Session
{
    //Primary key
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Location { get; set; } = default!;
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public string Topic { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public int AttendeeCapacity { get; set; }
    
    public int SpeakerCapacity { get; set; }
    
    //Foreign key
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
    
    public ICollection<SessionParticipant>? SessionParticipants {get; set;}
}