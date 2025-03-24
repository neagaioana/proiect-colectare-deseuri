namespace colectare_deseuri.Models
{
    public class CollectedInfo
    {
        public int Id { get; set; }
        public required string IdPubela { get; set; }
        public DateTime CollectionTime { get; set; }
    }
}
