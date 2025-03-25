namespace colectare_deseuri.Models
{
    public class PubelaCetatean
    {
        public int IdPubela { get; set; }
        public Pubela Pubela { get; set; }

        public int IdCetatean { get; set; }
        public Cetatean Cetatean { get; set; }

        public string Adresa { get; set; }
    }
}
