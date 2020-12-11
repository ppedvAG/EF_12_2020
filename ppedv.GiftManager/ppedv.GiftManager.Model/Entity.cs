using System;
using System.Collections.Generic;

namespace ppedv.GiftManager.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Produkt : Entity
    {
        public string Bezeichnung { get; set; }
        public GeschenkProduktStatus Status { get; set; }
        public decimal Preis { get; set; }
        public string Quelle { get; set; }
        public virtual Geschenk Geschenk { get; set; }
    }

    public class Geschenk : Entity
    {
        public DateTime GeschenkDatum { get; set; }
        public virtual Anlass Anlass { get; set; }
        public virtual ICollection<Produkt> Produkte { get; set; } = new HashSet<Produkt>();
        public virtual Person Beschenkter { get; set; }
        public virtual ICollection<Person> Personen { get; set; } = new HashSet<Person>();
    }

    public class Person : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Geschenk> Schenker { get; set; } = new HashSet<Geschenk>();
        public virtual ICollection<Geschenk> Beschenkter { get; set; } = new HashSet<Geschenk>();
    }

    public class Anlass : Entity
    {
        public string Bezeichnung { get; set; }
        public virtual ICollection<Geschenk> Geschenke { get; set; } = new HashSet<Geschenk>();
    }



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
