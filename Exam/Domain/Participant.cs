namespace Domain;

public class Participant
{
    //Primary key
    public int Id { get; set; }
    
    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public string Phone { get; set; } = default!;
    
    public ICollection<SessionParticipant>? SessionParticipants {get; set;}
}