using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;

namespace AutoszervizWebApp.Pages.JavitasPages;

public class IndexModel : PageModel
{
    private readonly SzervizDbContext _context;

    public IndexModel(SzervizDbContext context)
    {
        _context = context;
    }
    [BindProperty(SupportsGet = true)]
    public EAllapot? KivalasztottAllapot { get; set; }

    public IList<Javitas> Javitas { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var query = _context.Javitasok.Include(j => j.Ugyfel).AsQueryable();

        if(KivalasztottAllapot.HasValue)
        {
            query = query.Where(j => j.Allapot == KivalasztottAllapot.Value);
        }
        Javitas = await query.ToListAsync();
    }

    public async Task<IActionResult> OnPostAllapotAsync(int id)
    {
        var javitas = await _context.Javitasok.FindAsync(id);

        if (javitas is not null)
        {
            switch(javitas.Allapot)
            {
                case EAllapot.Felveve:
                    javitas.Allapot = EAllapot.Folyamatban;
                    break;
                case EAllapot.Folyamatban:
                    if(javitas.Koltseg > 0)
                    {
                        javitas.Allapot = EAllapot.Kesz;
                        break;
                    }
                    break;
                case EAllapot.Kesz:
                    javitas.Allapot = EAllapot.Felveve;
                    break;
            }

            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}
