using EfCoreCodeFirst.Data;
using EfCoreCodeFirst.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EfCoreCodeFirst
{
    public partial class Form1 : Form
    {
        EfContext con = new EfContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void LadeAlleMitarbeiterButtonClick(object sender, EventArgs e)
        {
            dataGridView1.DataSource = con.Mitarbeiter.ToList();
        }

        private void CreateDemoDatenButtonClick(object sender, EventArgs e)
        {
            var abt1 = new Abteilung() { Bezeichnung = "Holz" };
            var abt2 = new Abteilung() { Bezeichnung = "Steine" };

            for (int i = 0; i < 100; i++)
            {
                var m = new Mitarbeiter()
                {
                    Name = $"Fred #{i:000}",
                    Beruf = "Macht dinge",
                    GebDatum = DateTime.Now.AddYears(-50).AddDays(i * 17)
                };

                if (i % 2 == 0)
                    m.Abteilungen.Add(abt1);

                if (i % 3 == 0)
                    m.Abteilungen.Add(abt2);


                con.Mitarbeiter.Add(m);
            }
            con.SaveChanges();
        }

        private void SpeichernButtonClick(object sender, EventArgs e)
        {
            var aff = con.SaveChanges();
            MessageBox.Show($"{aff} Rows affected");
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is Mitarbeiter selectedMitarbeiter)
            {
                var msgText = $"Soll der Mitarbeiter {selectedMitarbeiter.Name} wirklich gelöscht werden?";

                if (MessageBox.Show(msgText, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.Mitarbeiter.Remove(selectedMitarbeiter);
                    con.SaveChanges();
                    LadeAlleMitarbeiterButtonClick(null, null);
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is Mitarbeiter selectedMitarbeiter)
            {
                var entry = con.ChangeTracker.Entries().FirstOrDefault(x => x.Entity == selectedMitarbeiter);
                if (entry != null)
                {
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    MessageBox.Show(entry.State.ToString());
                }
            }
        }

        private void LadeMitarbeiterMitLinqExpression(object sender, EventArgs e)
        {
            using var killCon = new EfContext();

            //expression
            var query = from m in killCon.Mitarbeiter.AsNoTracking()
                        where m.GebDatum.Year > 1975 && m.Name.StartsWith("F")
                        orderby m.GebDatum.Month, m.GebDatum.Day descending
                        select m;

            dataGridView1.DataSource = query.ToList();

            //kill em all
            killCon.Mitarbeiter.RemoveRange(query.ToList());
            killCon.SaveChanges();
        }

        private void LadeMitarbeiterMitLinqLambda(object sender, EventArgs e)
        {
            //lambda
            dataGridView1.DataSource = con.Mitarbeiter.AsNoTracking()
                                          .Where(m => m.GebDatum.Year > 1975 && m.Name.StartsWith("F"))
                                          .OrderBy(x => x.GebDatum.Month)
                                          .ThenByDescending(x => x.GebDatum.Day)
                                          .ToList();


            //Attach
            con.Attach(con.Mitarbeiter.AsNoTracking().FirstOrDefault());

            //Detach
            con.Entry(con.Mitarbeiter.FirstOrDefault()).State = EntityState.Detached;

        }
    }
}
