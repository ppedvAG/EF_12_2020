using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.GiftManager.Model;
using System;
using System.Data.SqlClient;
using System.Threading;

namespace ppedv.GiftManager.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void EfContext_Can_Create_New_DB()
        {
            using (var con = new EfContext())
            {
                if (con.Database.Exists())
                    con.Database.Delete();

                con.Database.Create();

                Assert.IsTrue(con.Database.Exists());
            }
        }

        [TestMethod]
        public void EfContext_Can_CRUD_Produkt()
        {
            var prod = new Produkt()
            {
                Bezeichnung = $"test produkt{Guid.NewGuid()}",
                Preis = 12.68m,
                Quelle = "Quelle",
                Status = Produkt.GeschenkProduktStatus.Geplant
            };

            var newName = $"neues produkt{Guid.NewGuid()}";

            //INSERT
            using (var con = new EfContext())
            {
                con.Produkte.Add(prod);
                Assert.AreEqual(1, con.SaveChanges());
            }

            //READ && UPDATE
            using (var con = new EfContext())
            {
                var loaded = con.Produkte.Find(prod.Id);
                Assert.IsNotNull(loaded);
                Assert.AreEqual(prod.Bezeichnung, loaded.Bezeichnung);

                loaded.Bezeichnung = newName;
                con.SaveChanges();
            }


            //check UPDATE && DELETE
            using (var con = new EfContext())
            {
                var loaded = con.Produkte.Find(prod.Id);
                Assert.AreEqual(newName, loaded.Bezeichnung);

                con.Produkte.Remove(loaded);
                con.SaveChanges();
            }

            //check DELETE
            using (var con = new EfContext())
            {
                var loaded = con.Produkte.Find(prod.Id);
                Assert.IsNull(loaded);
            }
        }

        [TestMethod]
        public void EfContext_Can_CRUD_Produkt_AutoFix()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            var prod = fix.Create<Produkt>();

            using (var con = new EfContext())
            {
                con.Produkte.Add(prod);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Produkte.Find(prod.Id);
                //Assert.IsNotNull(loaded);
                loaded.Should().NotBeNull();

                //Assert.AreEqual(prod.Bezeichnung, loaded.Bezeichnung);
                loaded.Bezeichnung.Should().Be(prod.Bezeichnung);
                loaded.Id.Should().BeInRange(0, 1000);

                loaded.Should().BeEquivalentTo(prod, x => x.IgnoringCyclicReferences());
            }
        }

        [TestMethod]
        public void EfContext_Created_and_Modiefied_set_by_EF()
        {
            var prod = new Produkt();
            var nowCreated = DateTime.Now;

            using (var con = new EfContext())
            {
                con.Produkte.Add(prod);
                con.SaveChanges();
            }

            var nowMod = DateTime.Now;

            //check add
            using (var con = new EfContext())
            {
                var loaded = con.Produkte.Find(prod.Id);
                loaded.Created.Should().BeCloseTo(nowCreated, 5000);
                loaded.Modified.Should().BeCloseTo(nowCreated, 5000);
                Thread.Sleep(5000);
                loaded.Bezeichnung = "lala";

                nowMod = DateTime.Now;
                con.SaveChanges();
            }

            //check mod
            using (var con = new EfContext())
            {
                var loaded = con.Produkte.Find(prod.Id);
                loaded.Created.Should().BeCloseTo(nowCreated, 5000);
                loaded.Modified.Should().BeCloseTo(nowMod, 5000);

                loaded.Modified.Should().NotBe(loaded.Created);
            }
        }

        [TestMethod]
        public void EfContext_Trans()
        {
            using (var sqlCon = new SqlConnection(("Server=(localdb)\\mssqllocaldb;Database=GiftManager_dev;Trusted_Connection=true")))
            {
                sqlCon.Open();
                using (var trans = sqlCon.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {

                    using (var con = new EfContext(sqlCon))
                    {
                        con.Database.UseTransaction(trans);

                        var prod = con.Produkte.Find(1);
                        prod.Preis += 1;
                        con.SaveChanges();

                        trans.Commit();
                    }
                }
            }
        }
    }
}
