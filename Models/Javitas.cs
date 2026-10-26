using System.ComponentModel.DataAnnotations;

namespace AutoszervizWebApp.Models
{
    public enum EAllapot
    {
        [Display(Name = "Felvéve")]
        Felveve,

        [Display(Name = "Folyamatban")]
        Folyamatban,

        [Display(Name = "Kész")]
        Kesz
    }
    public class Javitas
    {
        public int Id { get; set; }
        public int UgyfelId { get; set; }
        public Ugyfel Ugyfel { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "A rendszám nem lehet hosszabb 10 karakternél.")]
        [RegularExpression(@"^[A-Za-z]{3}-[0-9]{3}$", ErrorMessage = "A rendszám formátuma érvénytelen. A helyes formátum: ABC-123.")]
        public string Rendszam { get; set; }
        [MaxLength(300, ErrorMessage = "A leírás nem lehet hosszabb 300 karakternél.")]
        public string Leiras { get; set; }
        public decimal Koltseg { get; set; }
        public EAllapot Allapot { get; set; }
        public DateTime FelvetelDatuma { get; set; }
    }
}
