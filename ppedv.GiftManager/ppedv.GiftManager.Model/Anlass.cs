using System.Collections.Generic;

namespace ppedv.GiftManager.Model
{
    public class Anlass : Entity
    {
        public string Bezeichnung { get; set; }
        public virtual ICollection<Geschenk> Geschenke { get; set; } = new HashSet<Geschenk>();
    }
}
