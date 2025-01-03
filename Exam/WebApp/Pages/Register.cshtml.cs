using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class Register : PageModel
{
    private readonly AppDbContext _context;
    
    public Register(AppDbContext context)
    {
        _context = context;
    }
    
    [BindProperty(SupportsGet = true)]
    public int ConferenceId { get; set; }

    [BindProperty]
    public string? Search { get; set; }
    
    [BindProperty]
    public string? UserName { get; set; }

    public IList<Session> Sessions { get; set; } = new List<Session>();
    
    public async Task<IActionResult> OnGetAsync(int conferenceId, string userName)
    {
        UserName = userName;
        ViewData["userName"] = userName; 
        
        Sessions = await _context.Sessions
            .Include(s => s.SessionParticipants)
            .Where(s => s.ConferenceId == conferenceId)
            .ToListAsync();

        return Page();
    }

    public async Task OnPostAsync()
    {
        var query = _context.Sessions.Where(s => s.ConferenceId == ConferenceId);

        if (!string.IsNullOrEmpty(Search))
        {
            query = query.Where(s => s.Name.Trim().ToLower().Contains(Search.Trim().ToLower()) ||
                                     s.Location.Trim().ToLower().Contains(Search.Trim().ToLower()) ||
                                     s.Topic.Trim().ToLower().Contains(Search.Trim().ToLower()) ||
                                     s.Description.Trim().ToLower().Contains(Search.Trim().ToLower()));
        }

        Sessions = await query.Include(s => s.SessionParticipants).ToListAsync();
        ViewData["userName"] = UserName;
    }
}