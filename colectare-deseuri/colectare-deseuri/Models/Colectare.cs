using System.ComponentModel.DataAnnotations.Schema;

namespace colectare_deseuri.Models
{
    public class Colectare
    {
        public int Id { get; set; }
        public string IdPubela { get; set; }

        [ForeignKey("IdPubela")]
        public Pubela Pubela { get; set; }

        public DateTime TimpRidicare { get; set; }
        public required string Adresa { get; set; }

        public required float Longitude { get; set; }

        public required float Latitude { get; set; }

    }
}
