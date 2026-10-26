using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;
using AutoszervizWebApp.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoszervizWebApp.Pages.JavitasPages;

public class EditModel : PageModel
{
    private readonly SzervizDbContext _context;

    public EditModel(SzervizDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public JavitasEditDTO Javitas { get; set; } = default!;
    public IEnumerable<SelectListItem> UgyfelSelectList { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var ugyfel = await _context.Ugyfelek.ToListAsync();
        UgyfelSelectList = ugyfel.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.Nev
        });

        var javitas = await _context.Javitasok.FirstOrDefaultAsync(m => m.Id == id);
        if (javitas is null)
        {
            return NotFound();
        }
        Javitas = new JavitasEditDTO
        {
            Id = javitas.Id,
            UgyfelId = javitas.UgyfelId,
            Rendszam = javitas.Rendszam,
            Leiras = javitas.Leiras,
            Koltseg = javitas.Koltseg
        };

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadUgyfelSelectListAsync();
            return Page();
        }

        var modositandoJavitas = await _context.Javitasok
            .FirstOrDefaultAsync(j => j.Id == Javitas.Id);

        if (modositandoJavitas is null)
        {
            return NotFound();
        }

        modositandoJavitas.UgyfelId = Javitas.UgyfelId;
        modositandoJavitas.Rendszam = Javitas.Rendszam;
        modositandoJavitas.Leiras = Javitas.Leiras;
        modositandoJavitas.Koltseg = Javitas.Koltseg;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!JavitasExists(Javitas.Id))
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

    private async Task LoadUgyfelSelectListAsync()
    {
        var ugyfelek = await _context.Ugyfelek.ToListAsync();

        UgyfelSelectList = ugyfelek.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.Nev
        }).ToList();
    }

    private bool JavitasExists(int id)
    {
        return _context.Javitasok.Any(e => e.Id == id);
    }
}
