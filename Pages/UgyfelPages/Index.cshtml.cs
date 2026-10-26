using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoszervizWebApp.Models;
using AutoszervizWebApp.Data;

namespace AutoszervizWebApp.Pages.UgyfelPages;

public class IndexModel : PageModel
{
    private readonly SzervizDbContext _context;

    public IndexModel(SzervizDbContext context)
    {
        _context = context;
    }

    public IList<Ugyfel> Ugyfel { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Ugyfel = await _context.Ugyfelek.ToListAsync();
    }
}
