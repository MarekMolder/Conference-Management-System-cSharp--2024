using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class RegisterFormAttendee : PageModel
{
    private readonly AppDbContext _context;
    
    public RegisterFormAttendee(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Participant Participant { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int SessionId { get; set; }

    public Session Session { get; set; } = default!;
    
    [BindProperty]
    public string? UserName { get; set; }
    
    public async Task<IActionResult> OnGetAsync(int sessionId, string userName)
    {
        UserName = userName;
        ViewData["userName"] = userName; 
        
        Session = await _context.Sessions.Include(s => s.SessionParticipants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (Session == null)
        {
            return NotFound();
        }
        
        int currentAttendeeCount = Session.SessionParticipants.Count(sp => !sp.IsSpeaker);
        
        if (Session.AttendeeCapacity <= currentAttendeeCount)
        {
            TempData["ErrorMessage"] = "Sorry, this session is full for attendees.";
            return RedirectToPage("/AllConferences", new { userName = UserName });
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var existingParticipant = await _context.Participants
            .FirstOrDefaultAsync(p => p.Email == Participant.Email);
        
        if (existingParticipant == null)
        {
            existingParticipant = new Participant
            {
                FirstName = Participant.FirstName,
                LastName = Participant.LastName,
                Email = Participant.Email,
                Phone = Participant.Phone
            };

            _context.Participants.Add(existingParticipant);
            await _context.SaveChangesAsync();
        }
        
        var existingSessionParticipant = await _context.SessionParticipants
            .FirstOrDefaultAsync(sp => sp.ParticipantId == existingParticipant.Id && sp.SessionId == SessionId);

        if (existingSessionParticipant != null)
        {
            TempData["ErrorMessage"] = "You are already registered for this session.";
            return RedirectToPage("/AllConferences", new { userName = UserName });
        }
        
        var sessionParticipant = new SessionParticipant
        {
            ParticipantId = existingParticipant.Id,
            SessionId = SessionId,
            RegistrationTime = DateTime.Now,
            IsSpeaker = false
        };

        _context.SessionParticipants.Add(sessionParticipant);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "You have been successfully registered as an Attendee!";
        return RedirectToPage("/AllConferences", new { userName = UserName });
    }
}