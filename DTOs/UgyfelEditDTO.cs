using System.ComponentModel.DataAnnotations;

namespace AutoszervizWebApp.DTOs
{
    public class UgyfelEditDTO
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage = "A megadott email cím nem érvényes.")]
        public string Email { get; set; }
    }
}
