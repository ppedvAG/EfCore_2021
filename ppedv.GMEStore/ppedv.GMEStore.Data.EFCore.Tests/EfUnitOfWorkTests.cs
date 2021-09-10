using ppedv.GMEStore.Model;
using ppedv.GMEStore.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ppedv.GMEStore.Data.EFCore.Tests
{
    public class EfUnitOfWorkTests
    {
        [Fact]
        public void EfRepository_GMEDBConcurrencyException()
        {
            var g = new Game() { Name = "Best Game ever" };

            using (var uow = new EfUnitOfWork())
            {
                uow.GameRepository.Add(g);
                uow.SaveAll();
            }

            using (var uow1 = new EfUnitOfWork())
            {
                var loaded = uow1.GameRepository.GetById(g.Id);
                loaded.Name = "So lalal";

                using (var uow2 = new EfUnitOfWork())
                {
                    var loaded2 = uow2.GameRepository.GetById(g.Id);
                    loaded2.Name = "Over Mega Game";
                    uow2.SaveAll();
                }

                Assert.Throws<GMEDbConcurrencyException>(() => uow1.SaveAll());
            }
        }

        [Fact]
        public void EfRepository_GMEDBConcurrencyException_UserWins()
        {
            var g = new Game() { Name = "AAA" };

            //ADD
            using (var uow = new EfUnitOfWork())
            {
                uow.GameRepository.Add(g);
                uow.SaveAll();
            }


            //CONFLICT 
            using (var uow1 = new EfUnitOfWork())
            {
                var loaded = uow1.GameRepository.GetById(g.Id);
                loaded.Name = "BBB";

                using (var uow2 = new EfUnitOfWork())
                {
                    var loaded2 = uow2.GameRepository.GetById(g.Id);
                    loaded2.Name = "CCC";
                    uow2.SaveAll();
                }

                try
                {
                    uow1.SaveAll();
                }
                catch (GMEDbConcurrencyException ex)
                {
                    //Handling User Wins
                    uow1.ConcurrencyUserWins(ex.MyVersion);
                    uow1.SaveAll(); //goto SaveAll :-D
                    
                } 
                catch(Exception ex)
                {
                    //Assert.Fail();
                    Assert.Equal(1,2);
                }
            }

            //Check
            using (var uow = new EfUnitOfWork())
            {
                var loaded = uow.GameRepository.GetById(g.Id);
                Assert.Equal("BBB", loaded.Name);
            }
        }

        [Fact]
        public void EfRepository_GMEDBConcurrencyException_DBWins()
        {
            var g = new Game() { Name = "AAA" };

            //ADD
            using (var uow = new EfUnitOfWork())
            {
                uow.GameRepository.Add(g);
                uow.SaveAll();
            }


            //CONFLICT 
            using (var uow1 = new EfUnitOfWork())
            {
                var loaded = uow1.GameRepository.GetById(g.Id);
                loaded.Name = "BBB";

                using (var uow2 = new EfUnitOfWork())
                {
                    var loaded2 = uow2.GameRepository.GetById(g.Id);
                    loaded2.Name = "CCC";
                    uow2.SaveAll();
                }

                try
                {
                    uow1.SaveAll();
                }
                catch (GMEDbConcurrencyException ex)
                {
                    //Handling User Wins
                    uow1.ConcurrencyDBWins(ex.MyVersion);

                    Assert.Equal("CCC", loaded.Name);
                }
                catch (Exception)
                {
                    //Assert.Fail();
                    Assert.Equal(1, 2);
                }
            }

            //check DB
            using (var uow = new EfUnitOfWork())
            {
                var loaded = uow.GameRepository.GetById(g.Id);
                Assert.Equal("CCC", loaded.Name);
            }
        }

    }
}
