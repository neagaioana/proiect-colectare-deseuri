using System.ComponentModel.DataAnnotations.Schema;

namespace colectare_deseuri.Models
{
    public class PubelaCetatean
    {
        public string IdPubela { get; set; }

        [ForeignKey("IdPubela")]
        public Pubela Pubela { get; set; }
        public int IdCetatean { get; set; }
        public Cetatean Cetatean { get; set; }

        public string Adresa { get; set; }
    }
}
