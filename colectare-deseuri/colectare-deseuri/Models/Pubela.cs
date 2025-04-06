namespace colectare_deseuri.Models
{
    public class Pubela
    {

        public string Id { get; set; }
        public ICollection<PubelaCetatean> PubeleCetateni { get; set; }
        public ICollection<Colectare> Colectari { get; set; }
    }
}
