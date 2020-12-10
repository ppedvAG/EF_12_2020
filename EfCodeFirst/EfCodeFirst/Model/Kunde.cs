namespace EfCodeFirst.Model
{
    class Kunde : Person
    {
        public string KdNummer { get; set; }
        public virtual Mitarbeiter Mitarbeiter { get; set; }

        public double Schuhgröße { get; set; }
        //public double Kopfgröße { get; set; }


    }
}
