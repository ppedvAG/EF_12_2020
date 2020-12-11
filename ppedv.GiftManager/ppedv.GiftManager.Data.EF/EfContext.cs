using ppedv.GiftManager.Model;
using ppedv.GiftManager.Model.Fault;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ppedv.GiftManager.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Person> Personen { get; set; }
        public DbSet<Geschenk> Geschenke { get; set; }
        public DbSet<Anlass> Anlasse { get; set; }
        public DbSet<Produkt> Produkte { get; set; }

        public EfContext(string conString) : base(conString)
        { }
        public EfContext(DbConnection con) : base(con, false)
        {

        }
        public EfContext() : this("Server=(localdb)\\mssqllocaldb;Database=GiftManager_dev;Trusted_Connection=true")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Produkt>().Property(x => x.Modified).IsConcurrencyToken();

            modelBuilder.Entity<Geschenk>().HasRequired(geschenk => geschenk.BeschenktePerson)
                                           .WithMany(person => person.GeschenkeErhalten)
                                           .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Produkt>().Property(x => x.Modified).HasColumnType("datetime2");
            //modelBuilder.Entity<Produkt>().Property(x => x.Created).HasColumnType("datetime3");

            //für ALLE Datetime soll Datetime2 in Server verwendet werden

            modelBuilder.Properties<DateTime>().Configure(x => { x.HasColumnType(nameof(SqlDbType.DateTime2)); });

            modelBuilder.Entity<Geschenk>()
                .HasMany(geschenk => geschenk.SchenkendePersonen)
                .WithMany(person => person.AlsSchenker)
                .Map(m =>
                {
                    m.ToTable("GeschenkSchenker");
                    m.MapLeftKey("GeschenkId");
                    m.MapRightKey("SchenkerId");
                });
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;
            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                ((Entity)item.Entity).Created = now;
                ((Entity)item.Entity).Modified = now;
                ((Entity)item.Entity).ModifiedBy = Environment.UserName;
            }

            foreach (var item in ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
            {
                ((Entity)item.Entity).Modified = now;
                ((Entity)item.Entity).ModifiedBy = Environment.UserName;
            }


            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException ex)
            {
                var myConEx = new ConcurrencyException("my Concurrency Exception Text..");
                myConEx.DbWins = () =>
                {
                    foreach (var item in ex.Entries)
                    {
                        item.CurrentValues.SetValues(item.GetDatabaseValues());
                    }
                };

                myConEx.UserWins = () =>
                {
                    foreach (var item in ex.Entries)
                    {
                        item.OriginalValues.SetValues(item.GetDatabaseValues());
                        item.State = EntityState.Modified;
                    }
                    SaveChanges();
                };

                throw myConEx;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
