using System.ComponentModel.DataAnnotations;

namespace colectare_deseuri.Models
{
    public class Cetatean
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu")]
        [RegularExpression(@"^[A-Za-zĂÂÎȘȚăâîșț\s\-]+$", ErrorMessage = "Numele trebuie să conțină doar litere")]
        public string Nume { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        [RegularExpression(@"^[A-Za-zĂÂÎȘȚăâîșț\s\-]+$", ErrorMessage = "Prenumele trebuie să conțină doar litere")]
        public string Prenume { get; set; }

        [Required(ErrorMessage = "CNP-ul este obligatoriu")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP-ul trebuie să conțină exact 13 cifre")]
        public string CNP { get; set; }

        [Required(ErrorMessage = "Emailul este obligatoriu")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|ro|net|org)$", ErrorMessage = "Emailul trebuie să fie valid și să conțină domeniu (ex: .com, .ro)")]

        public string Email { get; set; }

        public ICollection<PubelaCetatean> PubeleCetateni { get; set; } = new List<PubelaCetatean>();
    }
}
