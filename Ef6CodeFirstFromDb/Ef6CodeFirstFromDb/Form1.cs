using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ef6CodeFirstFromDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Model1 context = new Model1();

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Employees.ToList();
        }
    }
}
