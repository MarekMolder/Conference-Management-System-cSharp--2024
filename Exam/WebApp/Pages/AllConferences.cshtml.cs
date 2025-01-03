using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;

public class AllConferencesModel : PageModel
{
    private readonly AppDbContext _context;
    
    [BindProperty] public string Search { get; set; } = default!;
    
    [BindProperty]
    public string? UserName { get; set; }

    public IList<Conference> Conferences { get; set; } = new List<Conference>();

    public AllConferencesModel(AppDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync(string userName)
    {
        UserName = userName;
        ViewData["userName"] = userName; 
        
        Conferences = await _context.Conferences.ToListAsync();
    }
    
    public async Task OnPostAsync()
    {
        var query = _context.Conferences.AsQueryable();

        if (!string.IsNullOrEmpty(Search))
        {
            query = query.Where(c => c.Name.Trim().ToLower().Contains(Search.Trim().ToLower()) ||
                                     c.Location.Trim().ToLower().Contains(Search.Trim().ToLower()) ||
                                     c.Description.Trim().ToLower().Contains(Search.Trim().ToLower()) ||
                                     c.Topic.Trim().ToLower().Contains(Search.Trim().ToLower()));
        }

        Conferences = await query.ToListAsync();
        ViewData["userName"] = UserName;
    }
}