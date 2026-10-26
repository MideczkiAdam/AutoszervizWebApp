using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoszervizWebApp.DTOs;

namespace AutoszervizWebApp.Pages.JavitasPages;

public class CreateModel : PageModel
{
    private readonly SzervizDbContext _context;

    public CreateModel(SzervizDbContext context)
    {
        _context = context;
    }

    public List<Ugyfel> Ugyfelek { get; set; }
    public IEnumerable<SelectListItem> UgyfelSelectList { get; set; }

    public IActionResult OnGet()
    {
        Ugyfelek = _context.Ugyfelek.ToList();
        UgyfelSelectList = Ugyfelek.Select(u => new SelectListItem
        {
            Value = u.Id.ToString(),
            Text = u.Nev
        });

        return Page();
    }

    [BindProperty]
    public JavitasPostDTO Javitas { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        var ujJavitas = new Javitas
        {
            UgyfelId = Javitas.UgyfelId,
            Rendszam = Javitas.Rendszam,
            Leiras = Javitas.Leiras,
            Koltseg = Javitas.Koltseg,
            FelvetelDatuma = DateTime.Now,
        };

        _context.Javitasok.Add(ujJavitas);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
