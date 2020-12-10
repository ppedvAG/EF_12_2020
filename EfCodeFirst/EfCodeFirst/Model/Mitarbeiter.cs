using System.Collections.Generic;

namespace EfCodeFirst.Model
{
    partial class Mitarbeiter : Person
    {
        public string Beruf { get; set; }
        
        public virtual ICollection<Kunde> Kunden { get; set; } = new HashSet<Kunde>();
        public virtual ICollection<Abteilung> Abteilungen { get; set; } = new HashSet<Abteilung>();

        public int Ps { get; set; }
        public int AnzahlFinger { get; set; }
        //public int TolleFeld { get; set; }


    }
}
