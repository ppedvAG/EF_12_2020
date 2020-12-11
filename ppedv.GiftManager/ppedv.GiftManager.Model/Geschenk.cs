using System;
using System.Collections.Generic;

namespace ppedv.GiftManager.Model
{
    public class Geschenk : Entity
    {
        public DateTime GeschenkDatum { get; set; }
        public virtual Anlass Anlass { get; set; }
        public virtual ICollection<Produkt> Produkte { get; set; } = new HashSet<Produkt>();
        public virtual Person Beschenkter { get; set; }
        public virtual ICollection<Person> Schenker { get; set; } = new HashSet<Person>();
    }
}
