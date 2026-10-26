using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;
using AutoszervizWebApp.DTOs;

namespace AutoszervizWebApp.Pages.UgyfelPages;

public class EditModel : PageModel
{
    private readonly SzervizDbContext _context;

    public EditModel(SzervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public UgyfelEditDTO Ugyfel { get; set; } = default!;

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
        Ugyfel = new UgyfelEditDTO
        {
            Id = ugyfel.Id,
            Nev = ugyfel.Nev,
            Telefon = ugyfel.Telefon,
            Email = ugyfel.Email
        };
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var modositandoUgyfel = await _context.Ugyfelek.FindAsync(Ugyfel.Id);

        modositandoUgyfel.Nev = Ugyfel.Nev;
        modositandoUgyfel.Telefon = Ugyfel.Telefon;
        modositandoUgyfel.Email = Ugyfel.Email;

        _context.Attach(modositandoUgyfel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UgyfelExists(Ugyfel.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool UgyfelExists(int id)
    {
        return _context.Ugyfelek.Any(e => e.Id == id);
    }
}
