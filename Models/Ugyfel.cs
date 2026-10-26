using System.ComponentModel.DataAnnotations;

namespace AutoszervizWebApp.Models
{
    public class Ugyfel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A név megadása kötelező.")]
        [MaxLength(100, ErrorMessage = "A név nem lehet hosszabb 100 karakternél.")]
        public string Nev { get; set; }
        [MaxLength(20, ErrorMessage = "A telefonszám nem lehet hosszabb 20 karakternél.")]
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage = "A megadott email cím nem érvényes.")]
        public string Email { get; set; }
        public IList<Javitas> Javitasok { get; set; }
    }
}
