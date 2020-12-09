using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using System.Data.Entity;

namespace EfModelFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;

            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Mitarbeiter m)
            {
                var abts = context.AbteilungSet.Where(x => x.Mitarbeiter.Any(y => y.Id == m.Id)).ToList();

                //MessageBox.Show(string.Join(", ", abts.Select(x => x.Bezeichnung)));
                context.ChangeTracker.Entries().FirstOrDefault(x => x.Entity == m).State = EntityState.Modified;

                var neu = new Mitarbeiter() { Id = 777, Name = "FFRRREEDDD",Beruf="killer" };
                context.PersonSet.Attach(neu);
                context.ChangeTracker.Entries().FirstOrDefault(x => x.Entity == neu).State = EntityState.Modified;

                MessageBox.Show(context.ChangeTracker.Entries().FirstOrDefault(x => x.Entity == m).State.ToString());

            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is IEnumerable<Abteilung> abts)
            {
                e.Value = string.Join(", ", abts.Select(x => x.Bezeichnung));
            }
        }

        Model1Container context = new Model1Container();

        private void button1_Click(object sender, EventArgs e)
        {
            var abt1 = new Abteilung() { Bezeichnung = "Holz" };
            var abt2 = new Abteilung() { Bezeichnung = "Steine" };

            for (int i = 0; i < 100; i++)
            {
                var m = new Mitarbeiter()
                {
                    Name = $"Fred #{i:000}",
                    Beruf = "Macht dinge",
                    GebDatum = DateTime.Now.AddYears(-50).AddDays(i * 133)
                };

                if (i % 2 == 0)
                    m.Abteilung.Add(abt1);

                if (i % 3 == 0)
                    m.Abteilung.Add(abt2);

                context.PersonSet.Add(m);
            }
            context.SaveChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var query = context.PersonSet.OfType<Mitarbeiter>()
                               .Include(x => x.Abteilung) //eager Loader
                               .Include(x => x.Kunden) //eager Loader
                               .Where(x => x.Name.StartsWith("F") &&
                                           x.GebDatum.Month > 2);
            //.OrderBy(x => x.Abteilung.Count());


            Trace.WriteLine(query.ToString());

            dataGridView1.DataSource = query.ToList();
        }

        private void SpeichernButton_Click(object sender, EventArgs e)
        {
            context.SaveChanges();
        }

        private void LöschenButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is Mitarbeiter m)
            {
                if (MessageBox.Show(
                    $"Soll der Mitarbeiter {m.Name} wirklich gelöscht werden?",
                    "löschen",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    context.PersonSet.Remove(m);
                }
            }
        }

        private void NeuButton_Click(object sender, EventArgs e)
        {
            var m = new Mitarbeiter()
            {
                Name = $"Fred NEU",
                Beruf = "Macht dinge",
                GebDatum = DateTime.Now.AddYears(-50)
            };

            context.PersonSet.Add(m);

        }
    }
}
