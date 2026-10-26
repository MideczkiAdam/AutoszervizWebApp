using System.ComponentModel.DataAnnotations;

namespace AutoszervizWebApp.DTOs
{
    public class JavitasPostDTO
    {
        public int UgyfelId { get; set; }
        [RegularExpression(@"^[A-Za-z]{3}-[0-9]{3}$", ErrorMessage = "A rendszám formátuma érvénytelen. A helyes formátum: ABC-123.")]
        public string Rendszam { get; set; }
        public string Leiras { get; set; }
        public decimal Koltseg { get; set; }
        public DateTime FelvetelDatuma { get; set; }
    }
}
