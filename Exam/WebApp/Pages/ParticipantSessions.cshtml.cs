using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class ParticipantSessions : PageModel
{
    private readonly AppDbContext _context;
    
    public ParticipantSessions(AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public string? UserName { get; set; }

    public IList<SessionParticipant> SessionParticipants { get; set; } = new List<SessionParticipant>();

    public async Task OnGetAsync(string userName)
    {
        UserName = userName;
        ViewData["userName"] = userName; 
        
        var participant = await _context.Participants
            .Where(p => p.Email == userName)
            .FirstOrDefaultAsync();

        if (participant != null)
        {
            SessionParticipants = await _context.SessionParticipants
                .Where(sp => sp.ParticipantId == participant.Id)
                .Include(sp => sp.Session)
                .ToListAsync();
        }
    }

    public async Task<IActionResult> OnPost(int sessionParticipantId)
    {
        var sessionParticipant = await _context.SessionParticipants
            .Where(sp => sp.Id == sessionParticipantId)
            .FirstOrDefaultAsync();

        if (sessionParticipant != null)
        {
            _context.SessionParticipants.Remove(sessionParticipant);
            _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Successfully removed from the session.";
        }
        else
        {
            TempData["ErrorMessage"] = "Session participant not found.";
        }

        return RedirectToPage("/AllConferences", new { userName = UserName });
    }
}