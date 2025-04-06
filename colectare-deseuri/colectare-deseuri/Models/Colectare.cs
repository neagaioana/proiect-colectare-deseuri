namespace colectare_deseuri.Models
{
    public class Colectare
    {
        public int Id { get; set; }
        public int IdPubela { get; set; }
        public Pubela Pubela { get; set; }
        public DateTime TimpRidicare { get; set; }
    }
}
