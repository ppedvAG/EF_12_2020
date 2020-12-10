using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCodeFirst.Model
{
    class Abteilung
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }

        
        [MaxLength(99)]
        [Column("Desc")]
        [EmailAddress]
        public string Beschreibung { get; set; }

        public virtual ICollection<Mitarbeiter> Mitarbeiter { get; set; } = new HashSet<Mitarbeiter>();

    }
}
