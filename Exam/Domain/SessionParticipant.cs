namespace Domain;

public class SessionParticipant
{
    //Primary key
    public int Id { get; set; }
    
    public DateTime RegistrationTime { get; set; }
    
    public DateTime? UnRegistrationTime { get; set; }
    
    public bool IsSpeaker { get; set; }
    
    //Foreign key
    public int SessionId { get; set; }
    public Session? Session { get; set; }
    
    //Foreign key
    public int ParticipantId { get; set; }
    public Participant? Participant { get; set; }
}