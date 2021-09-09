using Bogus;
using EfMigrations.Data;
using EfMigrations.Model;
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

        private void DemoDatenButtonClick(object sender, EventArgs e)
        {
            var comps = new Faker<Company>("de")
                            .UseSeed(12)
                            .RuleFor(x => x.Name, x => x.Company.CompanyName())
                            .Generate(100);

            var faker = new Faker<Game>("de")
                            .UseSeed(12)
                            .RuleFor(x => x.Name, x => $"{x.Commerce.Color()} {x.Name.LastName()}")
                            .RuleFor(x => x.PublishedDate, x => x.Date.Past(4))
                            .RuleFor(x => x.Description, x => x.Lorem.Sentences(2))
                            .RuleFor(x => x.Genre, x => x.Internet.UserName())
                            .RuleFor(x => x.Developer, x => x.Random.ListItem(comps));

            for (int i = 0; i < 100; i++)
            {
                var game = faker.Generate();
                context.Games.Add(game);
                context.Entry<Game>(game).Property("LastEdit").CurrentValue = DateTime.Now;

            }
            context.SaveChanges();

        }
    }
}
