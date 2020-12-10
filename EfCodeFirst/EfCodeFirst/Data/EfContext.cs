using EfCodeFirst.Model;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace EfCodeFirst.Data
{
    class EfContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Abteilung> Abteilungen { get; set; }

        // public EfContext() : base("Server=(localdb)\\mssqllocaldb;Database=EfCodeFirst;Trusted_Connection=true")
        public EfContext() : base("name=myConString")
        {

#if DEBUG
            Database.SetInitializer<EfContext>(null);  //so kommt kein Migration Check
#endif

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfContext, Migrations.Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<EfContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Abteilung>().Property(x => x.Bezeichnung)
                                            .HasMaxLength(29)
                                            .IsRequired()
                                            .HasColumnName("Abtbez");

            //modelBuilder.Entity<Abteilung>().Ignore(x => x.Bezeichnung);

            //TpT bzw TpCp (je nach DbSet Person)
            /*modelBuilder.Entity<Person>().ToTable("Opfer")*/
            ;
            modelBuilder.Entity<Mitarbeiter>().ToTable("Mitarbeiter");
            modelBuilder.Entity<Kunde>().ToTable("Kunden");

            modelBuilder.Configurations.Add(new MyConfig());
        }
    }

    class MyConfig : EntityTypeConfiguration<Person>
    {
        public MyConfig()
        {
            Property(x => x.Name).HasMaxLength(11);
        }
    }

}
