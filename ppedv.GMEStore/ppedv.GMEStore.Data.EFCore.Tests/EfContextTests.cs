using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.GMEStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace ppedv.GMEStore.Data.EFCore.Tests
{
    public class EfContextTests
    {
        [Fact]
        public void Can_create_new_db()
        {
            var con = new EfContext();

            con.Database.EnsureDeleted();
            Assert.False(con.Database.CanConnect());

            con.Database.EnsureCreated();
            Assert.True(con.Database.CanConnect());
        }

        [Fact]
        public void Can_CRUD_Game()
        {

            var game = new Game() { Name = $"New World_{Guid.NewGuid()}", Published = new DateTime(2021, 9, 28), Description = "AAAA" };
            var newName = $"WoW 2{Guid.NewGuid()}";

            //CREATE
            using (var con = new EfContext())
            {
                con.Add(game);
                con.SaveChanges();
            }

            //READ (ADD)
            using (var con = new EfContext())
            {
                //var loaded = con.Find<Game>(game.Id);
                var loaded = con.Games.FirstOrDefault(x => x.Id == game.Id);
                Assert.Equal(game.Name, loaded.Name);

                //UPDATE
                loaded.Name = newName;
                con.SaveChanges();
            }

            //READ (UPDATE)
            using (var con = new EfContext())
            {
                var loaded = con.Find<Game>(game.Id);
                Assert.Equal(newName, loaded.Name);

                //DELETE
                con.Games.Remove(loaded);
                con.SaveChanges();
            }

            //READ (DELETE)
            using (var con = new EfContext())
            {
                var loaded = con.Find<Game>(game.Id);
                Assert.Null(loaded);
            }
        }


        [Fact]
        public void Can_insert_game_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id), "Created"));

            var game = fix.Build<Game>().Without(x => x.Id).Create();
            using (var con = new EfContext())
            {
                con.Games.Add(game);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Games.Include(x => x.Genres)
                                      .Include(x => x.Publisher)
                                      .Include(x => x.Developer)
                                      .FirstOrDefault(x => x.Id == game.Id);

                loaded.Name.Should().Be(game.Name);

                loaded.Should().BeEquivalentTo(game, x => x.IgnoringCyclicReferences());
            }
        }


        [Fact]
        public void Delete_publisher_should_be_set_NULL_on_games()
        {
            var game = new Game() { Name = "G1" };
            var pub = new Company() { Name = "C1" };
            game.Publisher = pub;

            //INSERT
            using (var con = new EfContext())
            {
                con.Add(game);
                con.SaveChanges();
            }

            //KILL COMPANY
            using (var con = new EfContext())
            {
                var loadedPub = con.Find<Company>(pub.Id);
                con.Companies.Remove(loadedPub);
                con.SaveChanges();
            }

            //CHECK
            using (var con = new EfContext())
            {
                con.Find<Company>(pub.Id).Should().BeNull();
                con.Find<Game>(game.Id).Should().NotBeNull();
            }

        }


        [Fact]
        public void Delete_game_should_not_delete_publisher()
        {
            var game = new Game() { Name = "G1" };
            var pub = new Company() { Name = "C1" };
            game.Publisher = pub;

            using (var con = new EfContext())
            {
                con.Add(game);
                con.SaveChanges();
            }

            //KILL GAME
            using (var con = new EfContext())
            {
                var loaded = con.Find<Game>(game.Id);
                con.Remove(loaded);
                con.SaveChanges();
            }

            //CHECK
            using (var con = new EfContext())
            {
                con.Find<Game>(game.Id).Should().BeNull();
                con.Find<Company>(pub.Id).Should().NotBeNull();
            }
        }

        [Fact]
        public void Delete_game_should_not_delete_developer()
        {
            var game = new Game() { Name = "G1" };
            var dev = new Company() { Name = "D1" };
            game.Developer = dev;

            using (var con = new EfContext())
            {
                con.Add(game);
                con.SaveChanges();
            }

            //KILL GAME
            using (var con = new EfContext())
            {
                var loaded = con.Find<Game>(game.Id);
                con.Remove(loaded);
                con.SaveChanges();
            }

            //CHECK
            using (var con = new EfContext())
            {
                con.Find<Game>(game.Id).Should().BeNull();
                con.Find<Company>(dev.Id).Should().NotBeNull();
            }
        }

        [Fact]
        public void Delete_dev_should_delete_all_games()
        {
            var game1 = new Game() { Name = "G1" };
            var game2 = new Game() { Name = "G2" };
            var dev = new Company() { Name = "D1" };
            dev.Developed.Add(game1);
            dev.Developed.Add(game2);

            using (var con = new EfContext())
            {
                con.Add(dev);
                con.SaveChanges();
            }

            //KILL DEV
            using (var con = new EfContext())
            {
                con.Find<Game>(game1.Id).Should().NotBeNull();
                con.Find<Game>(game2.Id).Should().NotBeNull();

                var loaded = con.Find<Company>(dev.Id);
                con.Remove(loaded);
                con.SaveChanges();
            }

            //CHECK
            using (var con = new EfContext())
            {
                con.Find<Game>(game1.Id).Should().BeNull();
                con.Find<Game>(game2.Id).Should().BeNull();
                con.Find<Company>(dev.Id).Should().BeNull();
            }
        }


        internal class PropertyNameOmitter : ISpecimenBuilder
        {
            private readonly IEnumerable<string> names;

            internal PropertyNameOmitter(params string[] names)
            {
                this.names = names;
            }

            public object Create(object request, ISpecimenContext context)
            {
                var propInfo = request as PropertyInfo;
                if (propInfo != null && names.Contains(propInfo.Name))
                    return new OmitSpecimen();

                return new NoSpecimen();
            }
        }

    }
}
