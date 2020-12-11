using System.Collections.Generic;

namespace ppedv.GiftManager.Model
{
    public class Person : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Geschenk> AlsSchenker { get; set; } = new HashSet<Geschenk>();
        public virtual ICollection<Geschenk> GeschenkeErhalten { get; set; } = new HashSet<Geschenk>();
    }
}
