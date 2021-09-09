using EfMigrations.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EfMigrations
{
    public partial class Form1 : Form
    {
        EfContext context = new EfContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void GamesLadenButtonClick(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = context.Games.ToList();
        }
    }
}
