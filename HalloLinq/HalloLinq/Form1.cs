using Bogus;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HalloLinq
{
    public partial class Form1 : Form
    {
        List<Person> personen = new List<Person>();

        public Form1()
        {
            InitializeComponent();

            var faker = new Faker<Person>("de")
                        .UseSeed(1234)
                        .RuleFor(x => x.Vorname, r => r.Name.FirstName())
                        .RuleFor(x => x.Nachname, r => r.Name.LastName())
                        .RuleFor(x => x.City, r => r.Address.City())
                        .RuleFor(x => x.GebDatum, r => r.Date.Past(50))
                        .RuleFor(x => x.Gehalt, r => r.Random.Decimal(0, 10000));

            for (int i = 0; i < 1000; i++)
            {
                var p = faker.Generate();
                p.Id = i;
                personen.Add(p);
            }

        }

        private void AlleDemoDatenButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = personen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var query = from p in personen
                        where p.Gehalt > 8000 && p.GebDatum.Year < 2000
                        orderby p.GebDatum.Year descending, p.Nachname
                        select p;

            dataGridView1.DataSource = query.ToList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = personen.Where(p => p.Gehalt > 8000 && p.GebDatum.Year < 2000)
                                               .OrderByDescending(x => x.GebDatum.Year)
                                               .ThenBy(x => x.Nachname)
                                               .ToList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //anonymer datentyp
            var dings = new { Text = "Hallo", Zahl = 5 };

            IEnumerable<string> nurNachNamen = personen.Select(x => x.Nachname);

            var nurNamen = personen.Select(x => new { VN = x.Vorname, NN = x.Nachname });
            dataGridView1.DataSource = nurNamen.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(personen.Count(x => x.GebDatum.Year < 2000).ToString());
            //MessageBox.Show(personen.Average(x => x.Gehalt).ToString());
            MessageBox.Show(personen.Where(x => x.GebDatum.Year < 2000).Sum(x => x.Gehalt).ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var p = personen.FirstOrDefault(x => x.GebDatum.Year < 2000);
            if (p != null)
                MessageBox.Show(personen.FirstOrDefault(x => x.GebDatum.Year < 1000).Nachname);
            else
                MessageBox.Show("Nix gefunden");


            //MessageBox.Show(personen.FirstOrDefault(x => x.GebDatum.Year < 1000).Nachname);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var sl = new SLDocument();
            for (int i = 0; i < personen.Count(); i++)
            {
                sl.SetCellValue(i + 1, 1, personen[i].Nachname);
                sl.SetCellValue(i + 1, 2, personen[i].Vorname);
                sl.SetCellValue(i + 1, 3, personen[i].GebDatum);
                sl.SetCellValue(i + 1, 4, personen[i].Gehalt);
            }

            sl.SaveAs("MeinZeug.xlsx");
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var sl = new SLDocument("MeinZeug.xlsx");

            //todo
            sl.GetCells().SelectMany(x => x.Value).Where(x => x.Value is decimal).Sum(x => x.Value. as decimal);
            

        }
    }
}
