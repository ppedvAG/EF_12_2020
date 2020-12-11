namespace ppedv.GiftManager.Model
{
    public class Produkt : Entity
    {
        public string Bezeichnung { get; set; }
        public GeschenkProduktStatus Status { get; set; }
        public decimal Preis { get; set; }
        public string Quelle { get; set; }
        public virtual Geschenk Geschenk { get; set; }

        public enum GeschenkProduktStatus
        {
            Idee,
            Geplant,
            Verworfen,
            Bestellt,
            Gekauft,
            Geschenkt
        }
    }
}
