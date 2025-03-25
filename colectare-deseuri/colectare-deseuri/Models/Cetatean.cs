using System.ComponentModel.DataAnnotations;

namespace colectare_deseuri.Models
{
    public class Cetatean
    {
        public int Id { get; set; }

        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }

        public ICollection<PubelaCetatean> PubeleCetateni { get; set; } = new List<PubelaCetatean>();
    }
}
