using System.Collections.Generic;

namespace ppedv.GiftManager.Model
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Geschenk> Schenker { get; set; } = new HashSet<Geschenk>();

    }
}
