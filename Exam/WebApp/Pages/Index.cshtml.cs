using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class Index : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Error { get; set; }
    
    [BindProperty]
    public string? UserNamer { get; set; }

    public void OnGet()
    {
    }
    
    public IActionResult OnPost()
    {
        UserNamer = UserNamer?.Trim();

        if (!string.IsNullOrWhiteSpace(UserNamer))
        {
            return RedirectToPage("/AllConferences", new { userName = UserNamer });
        }

        Error = "Please enter a Gmail";

        return Page();
    }
}