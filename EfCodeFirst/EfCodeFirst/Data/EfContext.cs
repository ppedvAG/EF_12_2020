using EfCodeFirst.Model;
using System.Data.Entity;

namespace EfCodeFirst.Data
{
    class EfContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Abteilung> Abteilungen { get; set; }
    }
}
