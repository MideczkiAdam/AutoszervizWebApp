using System.ComponentModel.DataAnnotations;

namespace AutoszervizWebApp.DTOs
{
    public class UgyfelPostDTO
    {
        public string Nev { get; set; }
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage = "A megadott email cím nem érvényes.")]
        public string Email { get; set; }
    }
}
