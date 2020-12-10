using EfCodeFirst.Data;
using EfCodeFirst.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EfCodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        EfContext context = new EfContext();

        private void DemoDatenButton_click(object sender, EventArgs e)
        {
            var abt1 = new Abteilung() { Bezeichnung = "Holz" };
            var abt2 = new Abteilung() { Bezeichnung = "Steine" };

            Mitarbeiter mm;
            for (int i = 0; i < 100; i++)
            {
                var m = new Mitarbeiter()
                {
                    Name = $"Fred #{i:000}",
                    Beruf = "Macht dinge",
                    GebDatum = DateTime.Now.AddYears(-50).AddDays(i * 133)
                };

                if (i % 2 == 0)
                    m.Abteilungen.Add(abt1);

                if (i % 3 == 0)
                    m.Abteilungen.Add(abt2);

                context.Mitarbeiter.Add(m);

                mm = m;
            }
            context.SaveChanges();
        }

        private void LadenButton_click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Mitarbeiter.ToList();
        }
    }
}
