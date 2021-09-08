using EfCoreCodeFirst.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EfCoreCodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LadeAlleMitarbeiterButtonClick(object sender, EventArgs e)
        {
            var con = new EfContext();

            dataGridView1.DataSource = con.Mitarbeiter.ToList();
        }
    }
}
