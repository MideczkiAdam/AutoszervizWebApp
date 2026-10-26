using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;
using AutoszervizWebApp.DTOs;

namespace AutoszervizWebApp.Pages.UgyfelPages;

public class CreateModel : PageModel
{
    private readonly SzervizDbContext _context;

    public CreateModel(SzervizDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public UgyfelPostDTO Ugyfel { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var ujUgyfel = new Ugyfel
        {
            Nev = Ugyfel.Nev,
            Telefon = Ugyfel.Telefon,
            Email = Ugyfel.Email
        };

        _context.Ugyfelek.Add(ujUgyfel);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
