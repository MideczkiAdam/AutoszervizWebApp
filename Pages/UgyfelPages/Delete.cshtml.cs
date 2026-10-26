using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;

namespace AutoszervizWebApp.Pages.UgyfelPages;

public class DeleteModel : PageModel
{
    private readonly SzervizDbContext _context;

    public DeleteModel(SzervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Ugyfel Ugyfel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.FirstOrDefaultAsync(m => m.Id == id);
        if (ugyfel is null)
        {
            return NotFound();
        }
        else
        {
            Ugyfel = ugyfel;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.FindAsync(id);
        bool VanJavitas = await _context.Javitasok.AnyAsync(j => j.UgyfelId == id);
        if (ugyfel != null && !VanJavitas)
        {
            Ugyfel = ugyfel;
            _context.Ugyfelek.Remove(Ugyfel);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
