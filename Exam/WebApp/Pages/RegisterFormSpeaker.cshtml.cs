using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class RegisterFormSpeaker : PageModel
{
    private readonly AppDbContext _context;
    
    public RegisterFormSpeaker(AppDbContext context)
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
        
        int currentSpeakerCount = Session.SessionParticipants.Count(sp => sp.IsSpeaker);
        
        if (Session.SpeakerCapacity <= currentSpeakerCount)
        {
            TempData["ErrorMessage"] = "Sorry, this session is full for speakers.";
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
        
        var sessionParticipant = new SessionParticipant
        {
            ParticipantId = existingParticipant.Id,
            SessionId = SessionId,
            RegistrationTime = DateTime.Now,
            IsSpeaker = true
        };

        _context.SessionParticipants.Add(sessionParticipant);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "You have been successfully registered as a Speaker!";
        return RedirectToPage("/AllConferences", new { userName = UserName });
    }
}